import {map, Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {IHttpRequest} from "./base/ihttp-request";
import {PaginatedQuery} from "../../models/paginated-query";
import {PaginatedList} from "../../models/paginated-list";
import {RoomResponse} from "../../models/responses/room-response";
import {RoomRequest} from "../../models/requests/room-request";
import {NotificationService} from "../notification.service";

@Injectable({
  providedIn: 'root'
})
export class RoomService{
    constructor(private http:IHttpRequest, private toast:NotificationService) {
  }

  get(id:number): Observable<RoomResponse | undefined>{
    return this.http.getRequestAsync<RoomResponse>(`rooms/get/${id}`).pipe(
      map(data => {
        if(data === undefined || (data.statusCode != 200 && data.statusCode != 204 && data.statusCode != 201)){
          return undefined;
        }
        return data.data;
      })
    )
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

  save(payload:RoomRequest): Observable<any>{
    return this.http.postRequestAsync("rooms/save", payload).pipe(
      map(data => {
          if (data.statusCode != 201){
            this.toast.showError("Unable to complete operation", "Failed");
            return;
          }
          this.toast.showSuccess("Operation completed", "Succeeded");
        }
      )
    )
  }

  delete(id:number):Observable<any>{
    return this.http.deleteRequestAsync(`rooms/delete/${id}`).pipe(
      map(data => {
        if (data.statusCode != 204){
          return undefined;
        }
        return true;
      })
    )
  }
}
