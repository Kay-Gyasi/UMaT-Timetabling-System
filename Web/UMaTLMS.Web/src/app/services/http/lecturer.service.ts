import {IHttpRequest} from "./base/ihttp-request";
import {PaginatedQuery} from "../../models/paginated-query";
import {map, Observable} from "rxjs";
import {PaginatedList} from "../../models/paginated-list";
import {LecturerResponse} from "../../models/responses/lecturer-response";
import {Injectable} from "@angular/core";
import {PreferenceResponse} from "../../models/responses/preference-response";
import {PreferenceLookups} from "../../models/responses/preference-lookups";
import {Lookup} from "../../models/lookup";
import {ApiResponse} from "../../models/api-response";

@Injectable({
  providedIn: 'root'
})
export class LecturerService{
  constructor(private http:IHttpRequest) {
  }

  getPage(payload:PaginatedQuery): Observable<PaginatedList<LecturerResponse> | undefined> {
    return this.http.getPageRequestAsync<LecturerResponse>("lecturers/getpage", payload).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    );
  }

  getPreferences(query:PaginatedQuery): Observable<PaginatedList<PreferenceResponse> | undefined>{
    return this.http.getPageRequestAsync<PreferenceResponse>(`lecturers/getPreferences`, query)
      .pipe(
        map(data => {
          if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
            return undefined;
          }
          return data.data;
        })
      )
  }

  delete(id:number):Observable<any>{
    return this.http.deleteRequestAsync(`lecturers/delete/${id}`).pipe(
      map(data => {
        if (data.statusCode != 204){
          return undefined;
        }
        return true;
      })
    )
  }
}
