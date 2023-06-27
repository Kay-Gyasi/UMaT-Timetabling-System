import {IHttpRequest} from "./ihttp-request";
import {catchError, map, Observable, throwError} from "rxjs";
import {ApiResponse} from "../../../models/api-response";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {PaginatedQuery} from "../../../models/paginated-query";
import {PaginatedList} from "../../../models/paginated-list";
import {environment} from "../../../../environments/environment";

@Injectable()
export class HttpRequest implements IHttpRequest{
  constructor(private http: HttpClient) {
  }
  deleteRequestAsync(path: string): Observable<ApiResponse<any>> {
    return this.http.delete(`${environment.apiBaseUrl}/${path}`).pipe(
      map(data => {
        return data as ApiResponse<any>;
      }));
  }

  getRequestAsync<TResponse>(path: string): Observable<ApiResponse<TResponse>> {
    return this.http.get(`${environment.apiBaseUrl}/${path}`).pipe(
      map(data => {
        return data as ApiResponse<TResponse>;
      })
    );
  }

  postRequestAsync<T>(path: string, command: T): Observable<ApiResponse<any>> {
    return this.http.post(`${environment.apiBaseUrl}/${path}`, command).pipe(
      map(data => {
        return data as ApiResponse<any>;
      })
    );
  }

  putRequestAsync<T>(path: string, command: T): Observable<ApiResponse<any>> {
    return this.http.put(`${environment.apiBaseUrl}/${path}`, command).pipe(
      map(data => {
        return data as ApiResponse<any>;
      })
    );
  }

  getPageRequestAsync<T>(path: string, payload: PaginatedQuery): Observable<ApiResponse<PaginatedList<T>>> {
    return this.http.post(`${environment.apiBaseUrl}/${path}`, payload).pipe(
      map(data => {
        return data as ApiResponse<PaginatedList<T>>;
      })
    );
  }
}
