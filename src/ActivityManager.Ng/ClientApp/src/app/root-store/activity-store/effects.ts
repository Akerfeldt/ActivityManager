import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY, of } from 'rxjs';
import { map, mergeMap, catchError, exhaustMap } from 'rxjs/operators';

import * as ActivityActions from './actions';
import { ActivityManagerApiService } from '../../services';

@Injectable()
export class ActivityEffects {

  constructor(
    private actions$: Actions,
    private activityManagerApiService: ActivityManagerApiService
  ) { }

  loadActivities$ = createEffect(() => this.actions$.pipe(
    ofType(ActivityActions.loadActivitiesFromApi),
    mergeMap(() => this.activityManagerApiService.getActivities()
      .pipe(
        map(activities => ActivityActions.loadActivitiesFromApiSuccess({ activities: activities })),
        catchError(error => of(ActivityActions.loadActivitiesFromApiFailure({ error })))
    ))));

  postActivity$ = createEffect(() => this.actions$.pipe(
    ofType(ActivityActions.postActivityToApi),
    exhaustMap((action) => this.activityManagerApiService.postActivity(action.activity)
      .pipe(
        map(activity => ActivityActions.postActivityToApiSuccess({ activity: activity })),
        catchError(error => of(ActivityActions.postActivityToApiFailure({ error })))
    ))));

  putActivity$ = createEffect(() => this.actions$.pipe(
    ofType(ActivityActions.putActivityToApi),
    exhaustMap((action) => this.activityManagerApiService.putActivity(action.activity)
      .pipe(
        map(activity => ActivityActions.putActivityToApiSuccess({ activity: activity })),
        catchError(error => of(ActivityActions.putActivityToApiFailure({ error })))
    ))));

  deleteActivity$ = createEffect(() => this.actions$.pipe(
    ofType(ActivityActions.deleteActivityToApi),
    exhaustMap((action) => this.activityManagerApiService.deleteActivity(action.id)
      .pipe(
        map(_ => ActivityActions.deleteActivityToApiSuccess({ id: action.id })),
        catchError(error => of(ActivityActions.deleteActivityToApiFailure({ error })))
      ))));
}
