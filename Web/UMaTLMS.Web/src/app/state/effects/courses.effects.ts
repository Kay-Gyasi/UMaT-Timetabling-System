import { Actions, createEffect, ofType } from "@ngrx/effects";
import { CourseService } from "src/app/services/http/course.service";
import { GetCoursePageFailure, GetCoursePageSuccess, GetCoursesPage } from "../store/actions/courses/get-course-page.action";
import {catchError, map, of, switchMap} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable()
export class CourseEffects {

  getPage$ = createEffect(() =>
    this.action$.pipe(
      ofType(GetCoursesPage),
      switchMap((action) => this.courseService.getPage(action.query).pipe(
          map(data => GetCoursePageSuccess({ payload: data, query: action.query })),
          catchError(error => of(GetCoursePageFailure({ error })))
        )
      )
    )
  );

  constructor(private action$: Actions, private courseService: CourseService) {
  }
}
