import { Component } from '@angular/core';
import {PaginatedList} from "../../../models/paginated-list";
import {Navigations} from "../../../helpers/navigations";
import {FormBuilder, FormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {PaginatedQuery} from "../../../models/paginated-query";
import {NotificationService} from "../../../services/notification.service";
import {Store} from "@ngrx/store";
import {AppState} from "../../../state/store/reducers";
import {LecturerService} from "../../../services/http/lecturer.service";
import {GetLecturersPage} from "../../../state/store/actions/lecturers/get-lecturer-page.action";
import {LecturerResponse} from "../../../models/responses/lecturer-response";

declare var KTMenu:any;
@Component({
  selector: 'app-view-lecturers',
  templateUrl: './view-lecturers.component.html',
  styleUrls: ['./view-lecturers.component.css']
})
export class ViewLecturersComponent {

  lecturers:PaginatedList<LecturerResponse> = new PaginatedList<LecturerResponse>();
  pages:Array<number> = [];
  navigator = new Navigations();
  isLoading:boolean = false;
  searchForm:UntypedFormGroup;
  query:PaginatedQuery = PaginatedQuery.Build(0, 1, 20);
  constructor(private lecturerService:LecturerService, private fb:FormBuilder,
              private toast: NotificationService, private store: Store<AppState>) {
    this.searchForm = this.fb.group({
      "term": ["", [Validators.maxLength(30)]]
    });
  }

  ngOnInit() {
    this.initialize();
  }

  public getLecturers(pageNumber:number = 1){
    this.isLoading = true;
    this.buildQuery(pageNumber);
    this.pages = [];
    const newQuery = Object.assign({}, this.query);
    this.store.dispatch(GetLecturersPage({ query: newQuery }));
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

    this.store.select(store => store.lecturers_page.query).subscribe({
      next: query => {
        this.query.PageNumber = query.PageNumber;
        this.query.thenSearch(query.Search);
        this.searchForm.setValue({
          term: query.Search ?? ''
        })
      },
      error: _ => console.log('Error while retrieving query state')
    });

    this.store.select(store => store.lecturers_page.data).subscribe({
      next: data => {
        if (data == undefined){
          this.toast.showError("Unable to load lecturers", "Failed")
          return;
        }
        this.lecturers = data;
        for (let i = 1; i <= data.totalPages; i++){
          this.pages.push(i);
        }
        this.isLoading = false;
      },
      error: err => {
        this.toast.showError("Unable to load lecturers", "Failed");
        this.isLoading = false;
      }
    });

    this.isLoading = true;
    const newQuery = Object.assign({}, this.query);
    this.store.dispatch(GetLecturersPage({ query: newQuery }));
  }
}
