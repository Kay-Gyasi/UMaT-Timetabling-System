import {Component, OnInit} from '@angular/core';
import {Navigations} from "../../../helpers/navigations";
import {PaginatedList} from "../../../models/paginated-list";
import {LecturePageResponse} from "../../../models/responses/lecture-response";
import {LectureService} from "../../../services/http/lecture.service";
import {NotificationService} from "../../../services/notification.service";
import {PaginatedQuery} from "../../../models/paginated-query";
import {FormBuilder, FormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {Store} from "@ngrx/store";
import {AppState} from "../../../state/store/reducers";
import {GetLecturesPage} from "../../../state/store/actions/lectures/get-lecture-page.action";

declare var KTMenu:any;

@Component({
  selector: 'app-view-lectures',
  templateUrl: './view-lectures.component.html',
  styleUrls: ['./view-lectures.component.css']
})
export class ViewLecturesComponent implements OnInit{

  navigator = new Navigations();
  lectures = new PaginatedList<LecturePageResponse>();
  pages:number[] = [];
  searchForm:UntypedFormGroup;
  isLoading:boolean = false;
  query:PaginatedQuery = PaginatedQuery.Build(0, 1, 20);

  constructor(private lectureService:LectureService, private toast:NotificationService,
              private fb:FormBuilder, private store: Store<AppState>) {
    this.searchForm = this.fb.group({
      "term": ["", [Validators.maxLength(30)]]
    });
  }

  ngOnInit() {
    this.initialize();
  }

  getLectures(pageNumber:number = 1){
    this.isLoading = true;
    this.buildQuery(pageNumber);
    this.pages = [];
    const newQuery = Object.assign({}, this.query);
    this.store.dispatch(GetLecturesPage({ query: newQuery }));
  }

  get term(){
    return this.searchForm.get('term') as FormControl;
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

    this.store.select(store => store.lectures_page.query).subscribe({
      next: query => {
        this.query.PageNumber = query.PageNumber;
        this.query.thenSearch(query.Search);
        this.searchForm.setValue({
          term: query.Search ?? ''
        })
      },
      error: _ => console.log('Error while retrieving query state')
    });

    this.store.select(store => store.lectures_page.data).subscribe({
      next: data => {
        if (data == undefined){
          this.toast.showError("Unable to load lectures", "Failed");
          return;
        }
        this.lectures = data;
        for (let i = 1; i <= data.totalPages; i++){
          this.pages.push(i);
        }
        this.isLoading = false;
      },
      error: err => {
        this.toast.showError("Unable to load lectures", "Failed");
      },
      complete: () => {
        this.isLoading = false;
      }
    });

    this.isLoading = true;
    const newQuery = Object.assign({}, this.query);
    this.store.dispatch(GetLecturesPage({ query: newQuery }));
  }
}
