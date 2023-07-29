import {createAction, props} from "@ngrx/store";
import {PaginatedQuery} from "../../../../models/paginated-query";
import {PaginatedList} from "../../../../models/paginated-list";
import {LecturerActionType} from "../../enums/lecturer-action-type";
import {LecturerResponse} from "../../../../models/responses/lecturer-response";

export const GetLecturersPage = createAction(
  LecturerActionType.GET_PAGE,
  props<{ query:PaginatedQuery }>()
)

export const GetLecturerPageSuccess = createAction(
  LecturerActionType.GET_PAGE_SUCCESS,
  props<{ payload:PaginatedList<LecturerResponse > | undefined, query:PaginatedQuery }>()
)

export const GetLecturerPageFailure = createAction(
  LecturerActionType.GET_PAGE_FAILURE,
  props<{ error:Error }>()
)
