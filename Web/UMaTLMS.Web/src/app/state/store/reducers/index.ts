import { Action, ActionReducer, ActionReducerMap, MetaReducer } from "@ngrx/store";
import { CoursePageState, getCoursePageReducer } from "./courses/get-course-page.reducer";
import {ClassGroupPageState, getClassGroupPageReducer} from "./classes/get-class-group-page.reducer";
import {getLecturePageReducer, LecturePageState} from "./lectures/get-lecture-page.reducer";
import {getRoomPageReducer, RoomPageState} from "./rooms/get-room-page.reducer";
import {getLecturerPageReducer, LecturerPageState} from "./lecturers/get-lecturer-page.reducer";

export const rootReducer = {};

export interface AppState {
  courses_page: CoursePageState;
  class_groups_page: ClassGroupPageState;
  lectures_page: LecturePageState;
  rooms_page: RoomPageState;
  lecturers_page: LecturerPageState;
}

export const reducers: ActionReducerMap<any, any> = {
  courses_page: getCoursePageReducer,
  class_groups_page: getClassGroupPageReducer,
  lectures_page: getLecturePageReducer,
  rooms_page: getRoomPageReducer,
  lecturers_page: getLecturerPageReducer,
}

export function clearState(reducer: ActionReducer<any>) : ActionReducer<any> {
  return function(state: AppState, action: Action) : AppState {
    return reducer(state, action);
  }
}

export const metaReducers : MetaReducer<any>[] = [clearState];
