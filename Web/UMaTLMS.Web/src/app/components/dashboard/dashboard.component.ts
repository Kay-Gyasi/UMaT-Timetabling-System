import { Component } from '@angular/core';
import {Navigations} from "../../helpers/navigations";
import {TimetableService} from "../../services/http/timetable.service";
import {NotificationService} from "../../services/notification.service";
import {ApiResponse} from "../../models/api-response";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  navigator = new Navigations();
  isLoading:boolean = false;

  constructor(private timetableService:TimetableService, private toast:NotificationService) {
  }

  syncData(){
    this.isLoading = true;
    this.timetableService.syncData().subscribe({
      next: _ => {
        this.isLoading = false;
        this.toast.showSuccess("System data synced with that of UMaT", "Success");
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
      next: _ => {
        this.isLoading = false;
        this.toast.showSuccess("Lectures have been generated", "Success");
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

  reset(){
    this.isLoading = true;
    return this.timetableService.reset().subscribe({
      next: _ => {
        this.isLoading = false;
        this.toast.showSuccess("System has been successfully reset", "Success");
      },
      error: _ => {
        this.isLoading = false;
        this.toast.showError("Failed to reset system", "Failed");
      }
    })
  }
}
