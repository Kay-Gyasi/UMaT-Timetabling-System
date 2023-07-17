import { PaginatedList } from "src/app/models/paginated-list";
import { CourseResponse } from "src/app/models/responses/course-response";
import { GetCoursesPage, GetCoursePageFailure, GetCoursePageSuccess } from "../../actions/courses/get-course-page.action";
import { CourseActionType } from "../../enums/course-action-type.enum";
import { createReducer, on } from "@ngrx/store";
import {PaginatedQuery} from "../../../../models/paginated-query";

export interface CoursePageState {
  data: PaginatedList<CourseResponse> | undefined,
  query:PaginatedQuery,
  loading: boolean,
  error: Error
}

export const initialState : CoursePageState = {
  data: new PaginatedList<CourseResponse>(),
  query: new PaginatedQuery(0, 1, 15),
  loading: false,
  error: Error()
}

export const getCoursePageReducer = createReducer(initialState,
  on(GetCoursesPage, (state, { query }) => ({
    ...state,
    loading: true
  })),
  on(GetCoursePageSuccess, (state, { payload, query }) => ({
    ...state,
    data: payload,
    query: query,
    loading: false
  })),
  on(GetCoursePageFailure, (state, { error }) => ({
    ...state,
    error: error,
    loading: false
  }))
)
