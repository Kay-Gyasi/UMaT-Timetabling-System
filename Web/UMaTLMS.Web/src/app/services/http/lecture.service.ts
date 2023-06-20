import { Injectable } from '@angular/core';
import {IHttpRequest} from "./base/ihttp-request";
import {PaginatedQuery} from "../../models/paginated-query";
import {map, Observable} from "rxjs";
import {PaginatedList} from "../../models/paginated-list";
import {LecturePageResponse, LectureResponse} from "../../models/responses/lecture-response";
import {RoomResponse} from "../../models/responses/room-response";
import {LectureRequest} from "../../models/requests/lecture-request";

@Injectable({
  providedIn: 'root'
})
export class LectureService {

  constructor(private http:IHttpRequest) { }

  getPage(payload:PaginatedQuery): Observable<PaginatedList<LecturePageResponse> | undefined> {
    return this.http.getPageRequestAsync<LecturePageResponse>("lectures/getpage", payload).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    );
  }

  createCombined(payload:LectureRequest[]): Observable<any | undefined> {
    return this.http.postRequestAsync<any>("lectures/createCombined", payload).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    );
  }

  get(id:number): Observable<LectureResponse | undefined>{
    return this.http.getRequestAsync<LectureResponse>(`lectures/get/${id}`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }
}
