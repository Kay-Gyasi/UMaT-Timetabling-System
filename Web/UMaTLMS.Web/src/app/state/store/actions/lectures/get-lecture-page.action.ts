import { PaginatedQuery } from "src/app/models/paginated-query";
import { CourseResponse } from "src/app/models/responses/course-response";
import { PaginatedList } from "src/app/models/paginated-list";
import { createAction, props } from "@ngrx/store";
import {LectureActionType} from "../../enums/lecture-action-type.enum";
import {LecturePageResponse} from "../../../../models/responses/lecture-response";

export const GetLecturesPage = createAction(
  LectureActionType.GET_PAGE,
  props<{ query:PaginatedQuery }>()
)

export const GetLecturePageSuccess = createAction(
  LectureActionType.GET_PAGE_SUCCESS,
  props<{ payload:PaginatedList<LecturePageResponse > | undefined, query:PaginatedQuery }>()
)

export const GetLecturePageFailure = createAction(
  LectureActionType.GET_PAGE_FAILURE,
  props<{ error:Error }>()
)
