import { PaginatedQuery } from "src/app/models/paginated-query";
import { PaginatedList } from "src/app/models/paginated-list";
import { createAction, props } from "@ngrx/store";
import {RoomActionType} from "../../enums/room-action-type.enum";
import {RoomResponse} from "../../../../models/responses/room-response";

export const GetRoomsPage = createAction(
  RoomActionType.GET_PAGE,
  props<{ query:PaginatedQuery }>()
)

export const GetRoomPageSuccess = createAction(
  RoomActionType.GET_PAGE_SUCCESS,
  props<{ payload:PaginatedList<RoomResponse > | undefined, query:PaginatedQuery }>()
)

export const GetRoomPageFailure = createAction(
  RoomActionType.GET_PAGE_FAILURE,
  props<{ error:Error }>()
)
