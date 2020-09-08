import { Component, ElementRef, Inject, OnDestroy, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store, ActionsSubject } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { Activity } from '../../../../models';
import * as ActivityActions from '../../../../root-store/activity-store/actions';
import * as ActivitySelectors from '../../../../root-store/activity-store/selectors';
import { ofType } from '@ngrx/effects';

@Component({
  templateUrl: 'delete-activity.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DeleteActivityComponent implements OnDestroy, OnInit {
  private closing = false;
  private readonly onDestroy = new Subject<void>();
  isReady$: Observable<boolean>;

  constructor(
    public dialogRef: MatDialogRef<DeleteActivityComponent>,
    private store: Store<{ activity: { activities: Activity[] } }>,
    @Inject(MAT_DIALOG_DATA) public activity: Activity,
    private actionsSubject$: ActionsSubject)
  {
  }

  ngOnDestroy() {
    this.onDestroy.next();
  }

  ngOnInit() {
    this.isReady$ = this.store.select(ActivitySelectors.selectActivityIsLoading).pipe(map(isLoading => !isLoading));

    this.actionsSubject$.pipe(
      takeUntil(this.onDestroy),
      ofType(ActivityActions.deleteActivityToApiSuccess)
    ).subscribe(_ => {
      if (this.closing) {
        this.dialogRef.close();
        this.closing = false;
      }
    });
  }

  onSubmit() {
    this.closing = true;
    this.store.dispatch(ActivityActions.deleteActivityToApi({ id: this.activity.id }));
  }
}
