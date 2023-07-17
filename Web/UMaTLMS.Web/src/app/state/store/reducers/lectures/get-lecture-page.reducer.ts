import { PaginatedList } from "src/app/models/paginated-list";
import { createReducer, on } from "@ngrx/store";
import {PaginatedQuery} from "../../../../models/paginated-query";
import {LecturePageResponse} from "../../../../models/responses/lecture-response";
import {
  GetLecturePageFailure,
  GetLecturePageSuccess,
  GetLecturesPage
} from "../../actions/lectures/get-lecture-page.action";

export interface LecturePageState {
  data: PaginatedList<LecturePageResponse> | undefined,
  query:PaginatedQuery,
  loading: boolean,
  error: Error
}

export const initialState : LecturePageState = {
  data: new PaginatedList<LecturePageResponse>(),
  query: new PaginatedQuery(0, 1, 15),
  loading: false,
  error: Error()
}

export const getLecturePageReducer = createReducer(initialState,
  on(GetLecturesPage, (state, { query }) => ({
    ...state,
    loading: true
  })),
  on(GetLecturePageSuccess, (state, { payload, query }) => ({
    ...state,
    data: payload,
    query: query,
    loading: false
  })),
  on(GetLecturePageFailure, (state, { error }) => ({
    ...state,
    error: error,
    loading: false
  }))
)
