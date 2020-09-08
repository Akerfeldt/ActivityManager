import { Component, ElementRef, OnDestroy, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Store, ActionsSubject } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { Activity } from '../../../../models';
import * as ActivityActions from '../../../../root-store/activity-store/actions';
import * as ActivitySelectors from '../../../../root-store/activity-store/selectors';
import { ofType } from '@ngrx/effects';

@Component({
  templateUrl: 'create-activity.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateActivityComponent implements OnDestroy, OnInit {
  private closing = false;
  private readonly onDestroy = new Subject<void>();
  form: FormGroup;
  isReady$: Observable<boolean>;

  get description() {
    return this.form.controls['description'];
  }

  constructor(
    public dialogRef: MatDialogRef<CreateActivityComponent>,
    private elementRef: ElementRef,
    private formBuilder: FormBuilder,
    private store: Store<{ activity: { activities: Activity[] } }>,
    private actionsSubject$: ActionsSubject) {

    this.form = this.formBuilder.group({
      description: ['', [Validators.maxLength(50), Validators.required]]
    });
  }

  ngOnDestroy() {
    this.onDestroy.next();
  }

  ngOnInit() {
    this.isReady$ = this.store.select(ActivitySelectors.selectActivityIsLoading).pipe(map(isLoading => !isLoading));

    this.actionsSubject$.pipe(
      takeUntil(this.onDestroy),
      ofType(ActivityActions.postActivityToApiSuccess)
    ).subscribe(_ => {
      if (this.closing) {
        this.dialogRef.close();
        this.closing = false;
      }
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      const errors = this.elementRef.nativeElement.querySelectorAll('mat-form-field.ng-invalid');

      if (errors && errors.length) {
        errors[0].scrollIntoView({ behavior: 'smooth' });
      }

      return;
    }

    const activity: Activity = this.form.value;
    this.closing = true;
    this.store.dispatch(ActivityActions.postActivityToApi({ activity: { ...activity } }));
  }
}
