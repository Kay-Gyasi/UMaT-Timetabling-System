import {Injectable} from "@angular/core";
import {IHttpRequest} from "./base/ihttp-request";
import {map, Observable} from "rxjs";
import { PreferenceLookups } from "src/app/models/responses/preference-lookups";
import {Lookup} from "../../models/lookup";
import {PreferenceRequest} from "../../models/requests/preference-request";

@Injectable({
  providedIn: 'root'
})
export class PreferenceService {
  constructor(private http:IHttpRequest) {
  }

  set(command:PreferenceRequest): Observable<any | undefined>{
    return this.http.postRequestAsync('preferences/set', command).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }

  getLookups(): Observable<PreferenceLookups | undefined>{
    return this.http.getRequestAsync<PreferenceLookups>(`preferences/getLookups`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }

  getTypeValues(type:number): Observable<Lookup[] | undefined>{
    return this.http.getRequestAsync<Lookup[]>(`preferences/getTypeValues/${type}`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }

  delete(id:number):Observable<any>{
    return this.http.deleteRequestAsync(`preferences/delete/${id}`).pipe(
      map(data => {
        if (data.statusCode != 204){
          return undefined;
        }
        return true;
      })
    )
  }
}
