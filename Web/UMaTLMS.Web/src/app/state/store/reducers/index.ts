import { Action, ActionReducer, ActionReducerMap, MetaReducer } from "@ngrx/store";
import { CoursePageState, getCoursePageReducer } from "./courses/get-course-page.reducer";

export const rootReducer = {};

export interface AppState {
  courses_page: CoursePageState;
}

export const reducers: ActionReducerMap<any, any> = {
  courses_page: getCoursePageReducer
}

export function clearState(reducer: ActionReducer<any>) : ActionReducer<any> {
  return function(state: AppState, action: Action) : AppState {
    return reducer(state, action);
  }
}

export const metaReducers : MetaReducer<any>[] = [clearState];
