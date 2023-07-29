import { Actions, createEffect, ofType } from "@ngrx/effects";
import {catchError, map, of, switchMap} from "rxjs";
import {Injectable} from "@angular/core";
import {GetLecturerPageSuccess, GetLecturersPage} from "../store/actions/lecturers/get-lecturer-page.action";
import {LecturerService} from "../../services/http/lecturer.service";
import {GetLecturePageFailure} from "../store/actions/lectures/get-lecture-page.action";

@Injectable()
export class LecturerEffects {

  getPage$ = createEffect(() =>
    this.action$.pipe(
      ofType(GetLecturersPage),
      switchMap((action) => this.lecturerService.getPage(action.query).pipe(
          map(data => GetLecturerPageSuccess({ payload: data, query: action.query })),
          catchError(error => of(GetLecturePageFailure({ error })))
        )
      )
    )
  );

  constructor(private action$: Actions, private lecturerService: LecturerService) {
  }
}
