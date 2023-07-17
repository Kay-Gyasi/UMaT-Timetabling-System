import { PaginatedList } from "src/app/models/paginated-list";
import { createReducer, on } from "@ngrx/store";
import {PaginatedQuery} from "../../../../models/paginated-query";
import {ClassResponse} from "../../../../models/responses/class-response";
import {
  GetClassGroupPage,
  GetClassGroupPageFailure,
  GetClassGroupPageSuccess
} from "../../actions/classes/get-class-group-page.action";

export interface ClassGroupPageState {
  data: PaginatedList<ClassResponse> | undefined,
  query:PaginatedQuery,
  loading: boolean,
  error: Error
}

export const initialState : ClassGroupPageState = {
  data: new PaginatedList<ClassResponse>(),
  query: new PaginatedQuery(0, 1, 15),
  loading: false,
  error: Error()
}

export const getClassGroupPageReducer = createReducer(initialState,
  on(GetClassGroupPage, (state, { query }) => ({
    ...state,
    loading: true
  })),
  on(GetClassGroupPageSuccess, (state, { payload, query }) => ({
    ...state,
    data: payload,
    query: query,
    loading: false
  })),
  on(GetClassGroupPageFailure, (state, { error }) => ({
    ...state,
    error: error,
    loading: false
  }))
)
