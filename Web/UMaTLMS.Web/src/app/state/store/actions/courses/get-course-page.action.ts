import { PaginatedQuery } from "src/app/models/paginated-query";
import { CourseActionType } from "../../enums/course-action-type.enum";
import { CourseResponse } from "src/app/models/responses/course-response";
import { PaginatedList } from "src/app/models/paginated-list";
import { createAction, props } from "@ngrx/store";

export const GetCoursesPage = createAction(
  CourseActionType.GET_PAGE,
  props<{ query:PaginatedQuery }>()
)

export const GetCoursePageSuccess = createAction(
  CourseActionType.GET_PAGE_SUCCESS,
  props<{ payload:PaginatedList<CourseResponse> | undefined }>()
)

export const GetCoursePageFailure = createAction(
  CourseActionType.GET_PAGE_FAILURE,
  props<{ error:Error }>()
)
