import { Actions, createEffect, ofType } from "@ngrx/effects";
import {catchError, map, of, switchMap} from "rxjs";
import {Injectable} from "@angular/core";
import {
  GetLecturePageFailure,
  GetLecturePageSuccess,
  GetLecturesPage
} from "../store/actions/lectures/get-lecture-page.action";
import {LectureService} from "../../services/http/lecture.service";

@Injectable()
export class LectureEffects {

  getPage$ = createEffect(() =>
    this.action$.pipe(
      ofType(GetLecturesPage),
      switchMap((action) => this.lectureService.getPage(action.query).pipe(
          map(data => GetLecturePageSuccess({ payload: data, query: action.query })),
          catchError(error => of(GetLecturePageFailure({ error })))
        )
      )
    )
  );

  constructor(private action$: Actions, private lectureService: LectureService) {
  }
}
