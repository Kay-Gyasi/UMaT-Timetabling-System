import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Navigations } from 'src/app/helpers/navigations';
import { PaginatedList } from 'src/app/models/paginated-list';
import { PaginatedQuery } from 'src/app/models/paginated-query';
import { CourseResponse } from 'src/app/models/responses/course-response';
import { CourseService } from 'src/app/services/http/course.service';
import { NotificationService } from 'src/app/services/notification.service';

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
  constructor(private courseService:CourseService, private fb:FormBuilder,
              private toast: NotificationService) {
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
    this.courseService.getPage(this.query).subscribe({
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
    })
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
    this.getCourses();
  }
}
