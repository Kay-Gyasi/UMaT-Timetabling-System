import { Component, OnInit } from '@angular/core';
import {Navigations} from "../../helpers/navigations";
import {TimetableService} from "../../services/http/timetable.service";
import {NotificationService} from "../../services/notification.service";
import { ExamTimetableRequest } from 'src/app/models/requests/exam-timetable-request';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  navigator = new Navigations();
  isLoading:boolean = false;
  examRequest:ExamTimetableRequest;

  constructor(private timetableService:TimetableService, private toast:NotificationService) {
  }

  ngOnInit(): void {
    this.examRequest = new ExamTimetableRequest();
  }

  syncData(){
    this.isLoading = true;
    this.timetableService.syncData().subscribe({
      next: data => {
        this.isLoading = false;
        this.toast.showSuccess(data.message, "Success");
      },
      error: _ => {
        this.isLoading = false;
        this.toast.showError("Failed to sync data", "Failed");
      }
    })
  }

  generateLectures(){
    this.isLoading = true;
    this.timetableService.generateLectures().subscribe({
      next: data => {
        this.isLoading = false;
        this.toast.showSuccess(data.message, "Success");
      },
      error: _ => {
        this.isLoading = false;
        this.toast.showError("Failed to generate lectures", "Failed");
      }
    })
  }

  download(){
    this.isLoading = true;
    this.timetableService.download();
    this.isLoading = false;
  }

  downloadExams(){
    this.isLoading = true;
    this.timetableService.downloadExam();
    this.isLoading = false;
  }

  generate(){
    this.isLoading = true;
    return this.timetableService.generate().subscribe({
      next: _ => {
        this.isLoading = false;
      },
      error: _ => {
        this.isLoading = false;
        this.toast.showError("Failed to generate timetable", "Failed");
      }
    });
  }

  generateExams(){
    this.isLoading = true;
    return this.timetableService.generateExam(this.examRequest).subscribe({
      next: _ => {
        this.isLoading = false;
      },
      error: _ => {
        this.isLoading = false;
        this.toast.showError("Failed to generate timetable", "Failed");
      }
    });
  }

  reset(){
    this.isLoading = true;
    return this.timetableService.reset().subscribe({
      next: data => {
        this.isLoading = false;
        this.toast.showSuccess(data.message, "Success");
      },
      error: _ => {
        this.isLoading = false;
        this.toast.showError("Failed to reset system", "Failed");
      }
    })
  }
}
