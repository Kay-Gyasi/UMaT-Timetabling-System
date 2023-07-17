import { Actions, createEffect, ofType } from "@ngrx/effects";
import {catchError, map, of, switchMap} from "rxjs";
import {Injectable} from "@angular/core";
import {ClassService} from "../../services/http/class.service";
import {
  GetClassGroupPageFailure,
  GetClassGroupPageSuccess,
  GetClassGroupPage
} from "../store/actions/classes/get-class-group-page.action";

@Injectable()
export class ClassGroupEffects {

  getPage$ = createEffect(() =>
    this.action$.pipe(
      ofType(GetClassGroupPage),
      switchMap((action) => this.classService.getPage(action.query).pipe(
          map(data => GetClassGroupPageSuccess({ payload: data, query: action.query })),
          catchError(error => of(GetClassGroupPageFailure({ error })))
        )
      )
    )
  );

  constructor(private action$: Actions, private classService: ClassService) {
  }
}
