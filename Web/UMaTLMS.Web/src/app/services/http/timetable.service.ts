import { Injectable } from '@angular/core';
import {IHttpRequest} from "./base/ihttp-request";
import {map, Observable, throwError} from "rxjs";
import {NotificationService} from "../notification.service";
import {environment} from "../../../environments/environment";
import { ExamTimetableRequest } from 'src/app/models/requests/exam-timetable-request';

@Injectable({
  providedIn: 'root'
})
export class TimetableService {

  constructor(private http:IHttpRequest, private toast: NotificationService) {
  }

  generate(): Observable<any>{
    return this.http.getRequestAsync<any>(`timetable/generate`).pipe(
      map(data => {
        if(data === undefined){
          this.toast.showError("Failed to generate timetable", "Failed");
          return undefined;
        }

        if (data.statusCode == 206){
          this.toast.showError(data.message, "Failed");
          return undefined;
        }

        if (data.statusCode == 200 || data.statusCode == 204 || data.statusCode == 201){
          this.download();
          this.toast.showSuccess("Timetable has been generated", "Succeeded");
          return data.data;
        }
      })
    )
  }

  generateExam(command:ExamTimetableRequest): Observable<any>{
    return this.http.postRequestAsync<any>(`timetable/generate`, command).pipe(
      map(data => {
        if(data === undefined){
          this.toast.showError("Failed to generate exams timetable", "Failed");
          return undefined;
        }

        if (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201){
          this.toast.showError(data.message, "Failed");
          return undefined;
        }

        this.downloadExam();
        this.toast.showSuccess("Exams timetable has been generated", "Succeeded");
        return data.data;
      })
    )
  }

  download(){
    const downloadLink = document.createElement('a');
    downloadLink.href = environment.timetableUrl;
    downloadLink.download = 'timetable.xlsx';
    downloadLink.click();
    URL.revokeObjectURL(downloadLink.href);
  }

  downloadExam(){
    const downloadLink = document.createElement('a');
    downloadLink.href = environment.examTimetableUrl;
    downloadLink.download = 'exam-timetable.xlsx';
    downloadLink.click();
    URL.revokeObjectURL(downloadLink.href);
  }

  generateLectures(): Observable<any>{
    return this.http.getRequestAsync<any>(`timetable/generateLectures`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }

  syncData(): Observable<any>{
    return this.http.getRequestAsync<any>(`timetable/getData`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }

  reset(): Observable<any>{
    return this.http.getRequestAsync<any>('initialization/reset').pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }
}
