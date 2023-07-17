import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Navigations } from 'src/app/helpers/navigations';
import { PaginatedList } from 'src/app/models/paginated-list';
import { PaginatedQuery } from 'src/app/models/paginated-query';
import { CourseResponse } from 'src/app/models/responses/course-response';
import { CourseService } from 'src/app/services/http/course.service';
import { NotificationService } from 'src/app/services/notification.service';
import {Store} from "@ngrx/store";
import {AppState} from "../../../state/store/reducers";
import {GetCoursesPage} from "../../../state/store/actions/courses/get-course-page.action";

declare var KTMenu: any;

@Component({
  selector: 'app-view-courses',
  templateUrl: './view-courses.component.html',
  styleUrls: ['./view-courses.component.css']
})
export class ViewCoursesComponent implements OnInit {

  courses:PaginatedList<CourseResponse> = new PaginatedList<CourseResponse>();
  pages:Array<number> = [];
  navigator = new Navigations();
  isLoading:boolean = false;
  searchForm:UntypedFormGroup;
  query:PaginatedQuery = PaginatedQuery.Build(0, 1, 20);
  constructor(private fb:FormBuilder, private toast: NotificationService, private store:Store<AppState>) {
      this.searchForm = this.fb.group({
        "term": ["", [Validators.maxLength(30)]]
      });
  }

  ngOnInit() {
    this.initialize();
  }

  public getCourses(pageNumber:number = 1){
    this.isLoading = true;
    this.buildQuery(pageNumber);
    this.pages = [];
    const newQuery = Object.assign({}, this.query);
    this.store.dispatch(GetCoursesPage({ query: newQuery }));
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

    this.store.select(store => store.courses_page.query).subscribe({
      next: query => {
        this.query.PageNumber = query.PageNumber;
        this.searchForm.setValue({
          term: query.Search ?? ''
        })
      },
      error: _ => console.log('Error while retrieving query state')
    });

    this.store.select((store) => store.courses_page.data).subscribe({
      next: data => {
        if (data == undefined){
          this.toast.showError("Unable to load courses", "Failed")
          return;
        }
        this.courses = data;
        for (let i = 1; i <= data.totalPages; i++){
          this.pages.push(i);
        }
        this.isLoading = false;
      },
      error: err => {
        this.toast.showError("Unable to load courses", "Failed");
        this.isLoading = false;
      }
    });

    if (this.courses.data.length == 0){
      this.isLoading = true;
      const newQuery = Object.assign({}, this.query);
      this.store.dispatch(GetCoursesPage({ query: newQuery }));
    }
  }
}
