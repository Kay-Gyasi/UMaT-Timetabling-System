import {Component, OnInit} from '@angular/core';
import {Navigations} from "../../../helpers/navigations";
import {PaginatedList} from "../../../models/paginated-list";
import {ClassResponse} from "../../../models/responses/class-response";
import {ClassService} from "../../../services/http/class.service";
import {NotificationService} from "../../../services/notification.service";
import {PaginatedQuery} from "../../../models/paginated-query";
import {FormBuilder, FormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {Store} from "@ngrx/store";
import {AppState} from "../../../state/store/reducers";
import {GetCoursesPage} from "../../../state/store/actions/courses/get-course-page.action";
import {GetClassGroupPage} from "../../../state/store/actions/classes/get-class-group-page.action";

declare var KTMenu:any;
@Component({
  selector: 'app-view-classes',
  templateUrl: './view-classes.component.html',
  styleUrls: ['./view-classes.component.css']
})
export class ViewClassesComponent implements OnInit{
  navigator = new Navigations();
  query = PaginatedQuery.Build(0, 1, 20);
  pages:number[] = [];
  isLoading = false;
  classLimit:number = 0;
  classes:PaginatedList<ClassResponse> = new PaginatedList<ClassResponse>();
  searchForm:UntypedFormGroup;
  subClassForm:UntypedFormGroup;
  selectedClass:ClassResponse = new ClassResponse();
  constructor(private classService:ClassService, private toast:NotificationService,
              private fb:FormBuilder, private router:Router, private store: Store<AppState>) {
    this.searchForm = this.fb.group({
      "term": ["", [Validators.maxLength(30)]]
    });
    this.subClassForm = this.fb.group({
      "numOfSubClasses": [1, [Validators.required, Validators.max(10)]]
    });
  }

  ngOnInit() {
    this.initialize();
  }

  getClasses(pageNumber:number = 1){
    this.isLoading = true;
    this.buildQuery(pageNumber);
    this.pages = [];
    const newQuery = Object.assign({}, this.query);
    this.store.dispatch(GetClassGroupPage({ query: newQuery }));
  }

  setSubClass(){
    this.isLoading = true;
    return this.classService.setNumOfSubClasses(this.subClassForm.get('numOfSubClasses')?.value,
      this.selectedClass.id).subscribe({
      next: async response => {
        if (response == undefined) {
          this.toast.showError("Unable to complete operation", "Failed");
          return;
        }

        this.selectedClass.numOfSubClasses = this.subClassForm.get('numOfSubClasses')?.value;
        this.toast.showSuccess("", "Succeeded");
        await this.router.navigateByUrl(this.navigator.classes);
      },
      error: err => {
        this.toast.showError("Unable to complete operation", "Failed");
      },
      complete: () => {
        this.isLoading = false;
      }
    })
  }

  setSelectedClass(selected:ClassResponse){
    this.selectedClass = selected;
    this.subClassForm.setValue({
      "numOfSubClasses": selected.numOfSubClasses ?? 1
    })
  }

  setClassLimit(){
    this.isLoading = true;
    this.classService.setLimit(this.classLimit).subscribe({
      next: res => {
        if (res == undefined){
          this.isLoading = false;
          this.toast.showError("Failed to adjust class sizes to limit", "Failed");
          return;
        }

        this.getClasses();
        this.isLoading = false;
        this.toast.showSuccess("Sizes have been adjusted to limit", "Succeeded");
      },
      error : _ => {
        this.isLoading = false;
        this.toast.showError("Failed to adjust class sizes to limit", "Failed");
      }
    })
  }

  get term(){
    return this.searchForm.get('term') as FormControl;
  }

  get numOfSubClasses(){
    return this.subClassForm.get('numOfSubClasses') as FormControl;
  }

  private buildQuery(pageNumber:number){
    if (pageNumber == -1) this.query.PageNumber -= 1;
    else if (pageNumber == -2) this.query.PageNumber += 1;
    else this.query.PageNumber = pageNumber;
    this.query.thenSearch(this.searchForm.get('term')?.value);
  }

  private initialize(){
    KTMenu.init();
    KTMenu.init();

    this.store.select(store => store.class_groups_page.query).subscribe({
      next: query => {
        this.query.PageNumber = query.PageNumber;
        this.searchForm.setValue({
          term: query.Search ?? ''
        })
      },
      error: _ => console.log('Error while retrieving query state')
    });

    this.store.select(store => store.class_groups_page.data).subscribe({
      next: data => {
        if (data == undefined){
          this.toast.showError("Unable to load classes", "Failed")
          return;
        }
        this.classes = data;
        for (let i = 1; i <= data.totalPages; i++){
          this.pages.push(i);
        }
        this.isLoading = false;
      },
      error: err => {
        this.toast.showError("Unable to load classes", "Failed")
      },
      complete: () => {
        this.isLoading = false;
      }
    })

    if (this.classes.data.length == 0){
      this.isLoading = true;
      const newQuery = Object.assign({}, this.query);
      this.store.dispatch(GetClassGroupPage({ query: newQuery }));
    }
  }
}
