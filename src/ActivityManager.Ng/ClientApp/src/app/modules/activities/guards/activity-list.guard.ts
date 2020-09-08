import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CanActivate } from '@angular/router';
import { ScannedActionsSubject, Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { catchError, filter, map } from 'rxjs/operators';

import { Activity } from '../../../models';
import * as ActivityActions from '../../../root-store/activity-store/actions';

@Injectable({
  providedIn: 'root',
})
export class ActivitiesGuard implements CanActivate {

  constructor(
    private snackBar: MatSnackBar,
    private store: Store<{ activity: { activities: Activity[] } }>,
    private scannedActionsSubject$: ScannedActionsSubject) { }

  getFromStoreOrAPI(): Observable<boolean> {
    this.store.dispatch(ActivityActions.loadActivitiesFromApi());

    return this.scannedActionsSubject$
      .pipe(
        filter(action => {
          return action.type === ActivityActions.loadActivitiesFromApiSuccess.type
            || action.type === ActivityActions.loadActivitiesFromApiFailure.type;
        }),
        map(action => {
          return action.type === ActivityActions.loadActivitiesFromApiSuccess.type;
        }));
  }

  canActivate(): Observable<boolean> {
    return this.getFromStoreOrAPI()
      .pipe(
        catchError(() => {
          this.snackBar.open("An Error Happened");

          return of(false);
        })
    )
  }
}
