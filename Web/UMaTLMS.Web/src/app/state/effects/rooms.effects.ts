import { Actions, createEffect, ofType } from "@ngrx/effects";
import {catchError, map, of, switchMap} from "rxjs";
import {Injectable} from "@angular/core";
import {RoomService} from "../../services/http/room.service";
import {GetRoomPageFailure, GetRoomPageSuccess, GetRoomsPage} from "../store/actions/rooms/get-room-page.action";

@Injectable()
export class RoomEffects {

  getPage$ = createEffect(() =>
    this.action$.pipe(
      ofType(GetRoomsPage),
      switchMap((action) => this.roomService.getPage(action.query).pipe(
          map(data => GetRoomPageSuccess({ payload: data, query: action.query })),
          catchError(error => of(GetRoomPageFailure({ error })))
        )
      )
    )
  );

  constructor(private action$: Actions, private roomService: RoomService) {
  }
}
