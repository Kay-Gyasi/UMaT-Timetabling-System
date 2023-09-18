import {Injectable} from "@angular/core";
import {IHttpRequest} from "./base/ihttp-request";
import {PaginatedQuery} from "../../models/paginated-query";
import {map, Observable} from "rxjs";
import {PaginatedList} from "../../models/paginated-list";
import {RoomResponse} from "../../models/responses/room-response";
import {ClassResponse} from "../../models/responses/class-response";

@Injectable({
  providedIn: 'root'
})
export class ClassService {
 constructor(private http:IHttpRequest) {
 }

 setNumOfSubClasses(numOfSubClasses:number, classId:number): Observable<boolean | undefined>{
   return this.http.putRequestAsync(`classes/setNumberOfSubClasses/${classId}/${numOfSubClasses}`, null)
     .pipe(map(data => {
       if (data.statusCode != 204){
         return undefined;
       }
       return true;
     }))
 }

 setSize(size:number, classId:number): Observable<boolean | undefined>{
   return this.http.putRequestAsync(`classes/setClassSize/${classId}/${size}`, null)
     .pipe(map(data => {
       if (data.statusCode != 204){
         return undefined;
       }
       return true;
     }))
 }

  getPage(payload:PaginatedQuery): Observable<PaginatedList<ClassResponse> | undefined> {
    return this.http.getPageRequestAsync<ClassResponse>("classes/getpage", payload).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    );
  }

  setLimit(limit:number) : Observable<boolean | undefined>{
    return this.http.putRequestAsync(`classes/setLimit/${limit}`, null).pipe(
      map(data => {
        if (data == undefined || data.statusCode != 204 && data.statusCode != 200){
          return undefined;
        }
        return true;
      })
    )
  }
}
