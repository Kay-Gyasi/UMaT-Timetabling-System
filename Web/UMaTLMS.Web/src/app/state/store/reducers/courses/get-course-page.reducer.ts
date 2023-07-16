import { PaginatedList } from "src/app/models/paginated-list";
import { CourseResponse } from "src/app/models/responses/course-response";
import { GetCoursesPage, GetCoursePageFailure, GetCoursePageSuccess } from "../../actions/courses/get-course-page.action";
import { CourseActionType } from "../../enums/course-action-type.enum";
import { createReducer, on } from "@ngrx/store";

export interface CoursePageState {
  data: PaginatedList<CourseResponse> | undefined,
  loading: boolean,
  error: Error
}

export const initialState : CoursePageState = {
  data: new PaginatedList<CourseResponse>,
  loading: false,
  error: Error()
}

export const getCoursePageReducer = createReducer(initialState,
  on(GetCoursesPage, (state, { query }) => ({
    ...state,
    loading: true
  })),
  on(GetCoursePageSuccess, (state, { payload }) => ({
    ...state,
    data: payload,
    loading: false
  })),
  on(GetCoursePageFailure, (state, { error }) => ({
    ...state,
    error: error,
    loading: false
  }))
)
