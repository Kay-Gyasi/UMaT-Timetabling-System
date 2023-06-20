import { Injectable } from '@angular/core';
import {IHttpRequest} from "./base/ihttp-request";
import {map, Observable} from "rxjs";
import {Lookup, LookupType} from "../../models/lookup";

@Injectable({
  providedIn: 'root'
})
export class LookupService {

  constructor(private http:IHttpRequest) { }

  get(type: LookupType): Observable<Lookup[] | undefined>{
    return this.http.getRequestAsync<Lookup[]>(`lookup/get/${type}`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
  }
}
