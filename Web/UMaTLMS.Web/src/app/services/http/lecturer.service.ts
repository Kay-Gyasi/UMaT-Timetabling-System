import {IHttpRequest} from "./base/ihttp-request";
import {PaginatedQuery} from "../../models/paginated-query";
import {map, Observable} from "rxjs";
import {PaginatedList} from "../../models/paginated-list";
import {LecturerResponse} from "../../models/responses/lecturer-response";
import {Injectable} from "@angular/core";
import {PreferenceResponse} from "../../models/responses/preference-response";
import {PreferenceLookups} from "../../models/responses/preference-lookups";
import {Lookup} from "../../models/lookup";

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

  getPreferences(): Observable<PreferenceResponse[] | undefined>{
    return this.http.getRequestAsync<PreferenceResponse[]>(`lecturers/getPreferences`)
      .pipe(
        map(data => {
          if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
            return undefined;
          }
          return data.data;
        })
      )
  }
}
