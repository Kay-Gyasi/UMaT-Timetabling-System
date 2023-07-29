import { PaginatedList } from "src/app/models/paginated-list";
import { createReducer, on } from "@ngrx/store";
import {PaginatedQuery} from "../../../../models/paginated-query";
import {LecturerResponse} from "../../../../models/responses/lecturer-response";
import {
  GetLecturerPageFailure,
  GetLecturerPageSuccess,
  GetLecturersPage
} from "../../actions/lecturers/get-lecturer-page.action";

export interface LecturerPageState {
  data: PaginatedList<LecturerResponse> | undefined,
  query:PaginatedQuery,
  loading: boolean,
  error: Error
}

export const initialState : LecturerPageState = {
  data: new PaginatedList<LecturerResponse>(),
  query: new PaginatedQuery(0, 1, 15),
  loading: false,
  error: Error()
}

export const getLecturerPageReducer = createReducer(initialState,
  on(GetLecturersPage, (state, { query }) => ({
    ...state,
    loading: true
  })),
  on(GetLecturerPageSuccess, (state, { payload, query }) => ({
    ...state,
    data: payload,
    query: query,
    loading: false
  })),
  on(GetLecturerPageFailure, (state, { error }) => ({
    ...state,
    error: error,
    loading: false
  }))
)
