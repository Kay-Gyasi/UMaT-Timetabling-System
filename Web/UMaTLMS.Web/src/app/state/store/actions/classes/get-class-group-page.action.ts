import { PaginatedQuery } from "src/app/models/paginated-query";
import { PaginatedList } from "src/app/models/paginated-list";
import { createAction, props } from "@ngrx/store";
import {ClassGroupActionType} from "../../enums/class-group-action-type.enum";
import {ClassResponse} from "../../../../models/responses/class-response";

export const GetClassGroupPage = createAction(
  ClassGroupActionType.GET_PAGE,
  props<{ query:PaginatedQuery }>()
)

export const GetClassGroupPageSuccess = createAction(
  ClassGroupActionType.GET_PAGE_SUCCESS,
  props<{ payload:PaginatedList<ClassResponse > | undefined, query:PaginatedQuery }>()
)

export const GetClassGroupPageFailure = createAction(
  ClassGroupActionType.GET_PAGE_FAILURE,
  props<{ error:Error }>()
)
