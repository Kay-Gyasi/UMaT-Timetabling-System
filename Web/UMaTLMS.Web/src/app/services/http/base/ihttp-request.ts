import {Observable} from "rxjs";
import {ApiResponse} from "../../../models/api-response";
import {PaginatedList} from "../../../models/paginated-list";
import {PaginatedQuery} from "../../../models/paginated-query";

export abstract class IHttpRequest{
  public abstract getRequestAsync<TResponse>(path:string) : Observable<ApiResponse<TResponse>>;
  public abstract postRequestAsync<T>(path:string, command:T): Observable<ApiResponse<any>>;
  public abstract putRequestAsync<T>(path:string, command:T): Observable<ApiResponse<any>>;
  public abstract deleteRequestAsync(path:string): Observable<ApiResponse<any>>;
  public abstract getPageRequestAsync<T>(path:string, payload:PaginatedQuery): Observable<ApiResponse<PaginatedList<T>>>
}
