import { PaginatedList } from "src/app/models/paginated-list";
import { createReducer, on } from "@ngrx/store";
import {PaginatedQuery} from "../../../../models/paginated-query";
import {RoomResponse} from "../../../../models/responses/room-response";
import {GetRoomPageFailure, GetRoomPageSuccess, GetRoomsPage} from "../../actions/rooms/get-room-page.action";

export interface RoomPageState {
  data: PaginatedList<RoomResponse> | undefined,
  query:PaginatedQuery,
  loading: boolean,
  error: Error
}

export const initialState : RoomPageState = {
  data: new PaginatedList<RoomResponse>(),
  query: new PaginatedQuery(0, 1, 15),
  loading: false,
  error: Error()
}

export const getRoomPageReducer = createReducer(initialState,
  on(GetRoomsPage, (state, { query }) => ({
    ...state,
    loading: true
  })),
  on(GetRoomPageSuccess, (state, { payload, query }) => ({
    ...state,
    data: payload,
    query: query,
    loading: false
  })),
  on(GetRoomPageFailure, (state, { error }) => ({
    ...state,
    error: error,
    loading: false
  }))
)
