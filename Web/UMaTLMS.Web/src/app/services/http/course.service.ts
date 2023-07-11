import {map, Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {IHttpRequest} from "./base/ihttp-request";
import {PaginatedQuery} from "../../models/paginated-query";
import {PaginatedList} from "../../models/paginated-list";
import {NotificationService} from "../notification.service";
import { CourseRequest } from "src/app/models/requests/course-request";
import { CourseResponse } from "src/app/models/responses/course-response";

@Injectable({
  providedIn: 'root'
})
export class CourseService{
    constructor(private http:IHttpRequest, private toast:NotificationService) {
  }

  get(id:number): Observable<CourseResponse | undefined>{
    return this.http.getRequestAsync<CourseResponse>(`courses/get/${id}`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }

  getPage(payload:PaginatedQuery): Observable<PaginatedList<CourseResponse> | undefined> {
    return this.http.getPageRequestAsync<CourseResponse>("courses/getpage", payload).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    );
  }

  edit(payload:CourseRequest): Observable<any>{
    return this.http.postRequestAsync("courses/save", payload).pipe(
      map(data => {
          if (data.statusCode != 201 && data.statusCode != 200 && data.statusCode != 204){
            this.toast.showError("Unable to edit course", "Failed");
            return;
          }
          this.toast.showSuccess("Course updated", "Succeeded");
        }
      )
    )
  }
}
