import {map, Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {IHttpRequest} from "./base/ihttp-request";
import {PaginatedQuery} from "../../models/paginated-query";
import {PaginatedList} from "../../models/paginated-list";
import {RoomResponse} from "../../models/responses/room-response";

@Injectable({
  providedIn: 'root'
})
export class RoomService{
  constructor(private http:IHttpRequest) {
  }

  getPage(payload:PaginatedQuery): Observable<PaginatedList<RoomResponse> | undefined> {
    return this.http.getPageRequestAsync<RoomResponse>("rooms/getpage", payload).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    );
  }
}
