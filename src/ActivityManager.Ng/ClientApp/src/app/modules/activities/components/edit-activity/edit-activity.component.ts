import { Component, ElementRef, Inject, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store, ActionsSubject } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { Activity } from '../../../../models';
import * as ActivityActions from '../../../../root-store/activity-store/actions';
import * as ActivitySelectors from '../../../../root-store/activity-store/selectors';
import { ofType } from '@ngrx/effects';

@Component({
  templateUrl: 'edit-activity.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditActivityComponent implements OnDestroy, OnInit {
  private closing = false;
  private readonly onDestroy = new Subject<void>();
  form: FormGroup;
  isReady$: Observable<boolean>;

  get description() {
    return this.form.controls['description'];
  }

  constructor(
    public dialogRef: MatDialogRef<EditActivityComponent>,
    private elementRef: ElementRef,
    private formBuilder: FormBuilder,
    private store: Store<{ activity: { activities: Activity[] } }>,
    @Inject(MAT_DIALOG_DATA) public activity: Activity,
    private actionsSubject$: ActionsSubject) {

    this.form = this.formBuilder.group({
      id: [activity.id, [Validators.required]],
      description: [activity.description, [Validators.maxLength(50), Validators.required]]
    });
  }

  ngOnDestroy() {
    this.onDestroy.next();
  }

  ngOnInit() {
    this.isReady$ = this.store.select(ActivitySelectors.selectActivityIsLoading).pipe(map(isLoading => !isLoading));

    this.actionsSubject$.pipe(
      takeUntil(this.onDestroy),
      ofType(ActivityActions.putActivityToApiSuccess)
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
    this.store.dispatch(ActivityActions.putActivityToApi({ activity: activity }));
  }
}
