import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CourseRequest } from 'src/app/models/requests/course-request';
import { CourseService } from 'src/app/services/http/course.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrls: ['./edit-course.component.css']
})
export class EditCourseComponent implements OnInit {
  courseForm:UntypedFormGroup;
  courseId:number;
  course:string;
  isLoading:boolean = false;
  constructor(private fb:FormBuilder, private route:ActivatedRoute,
              private courseService:CourseService, private toast:NotificationService) {
  }

  ngOnInit() {
    this.initialize()
  }

  editCourse(){
    this.isLoading = true;
    let request = new CourseRequest();
    request.isExaminable = this.courseForm.get('isExaminable')?.value;
    request.isToHaveWeeklyLectureSchedule = this.courseForm.get('isToHaveWeeklyLectureSchedule')?.value;
    request.hasPracticalExams = this.courseForm.get('hasPracticalExams')?.value;
    return this.courseService.edit(request).subscribe({
      error: err => {
        this.toast.showError("Unable to edit course", "Failed");
      },
      complete: () => {
        this.isLoading = false;
      }
    })
  }

  getControl(name: string){
    return this.courseForm.get(name) as FormControl;
  }

  private initialize(){
    this.courseForm = this.fb.group({
      isExaminable: [true, [Validators.required]],
      isToHaveWeeklyLectureSchedule: [true, [Validators.required]],
      hasPracticalExams: [false, [Validators.required]]
    })

    this.courseId = this.route.snapshot.params["id"];
    this.courseService.get(this.courseId).subscribe({
      next: response => {
        this.course = response?.name ?? '';
        this.courseForm.setValue({
          isExaminable: response?.isExaminable,
          isToHaveWeeklyLectureSchedule: response?.isToHaveWeeklyLectureSchedule,
          hasPracticalExams: response?.hasPracticalExams
        })
      },
      error: err => {
        this.toast.showError("Unable to fetch course details", "Failed");
      }
    })
  }

}
