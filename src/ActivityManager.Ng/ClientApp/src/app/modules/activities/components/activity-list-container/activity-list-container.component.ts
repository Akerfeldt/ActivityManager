import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Activity } from '../../../../models';
import * as ActivitySelectors from '../../../../root-store/activity-store/selectors';

@Component({
  template: `<activity-list [activities$]="activities$" [isReady]="isReady$ | async"></activity-list>`,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ActivityListContainerComponent implements OnInit {
  activities$: Observable<Activity[]>;
  isReady$: Observable<boolean>;

  constructor(private store: Store<{ activity: { activities: Activity[] } }>) { }

  ngOnInit() {
    this.activities$ = this.store.select(ActivitySelectors.selectAllActivities);
    this.isReady$ = this.store.select(ActivitySelectors.selectActivityIsLoading).pipe(map(isLoading => !isLoading));
  }
}
