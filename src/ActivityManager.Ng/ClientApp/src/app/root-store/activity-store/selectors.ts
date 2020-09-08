import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as ActivityState from './state';
import * as fromActivity from './reducer';

export const selectActivity = createFeatureSelector<ActivityState.State>('activities');

export const selectActivityIds = createSelector(
  selectActivity,
  fromActivity.selectActivityIds // shorthand for ActivitiesState => fromActivity.selectActivityIds(ActivitiesState)
);
export const selectActivityEntities = createSelector(
  selectActivity,
  fromActivity.selectActivityEntities
);
export const selectAllActivities = createSelector(
  selectActivity,
  fromActivity.selectAllActivities
);
export const selectActivityTotal = createSelector(
  selectActivity,
  fromActivity.selectActivityTotal
);
export const selectCurrentActivityId = createSelector(
  selectActivity,
  fromActivity.getSelectedActivityId
);

export const selectCurrentActivity = createSelector(
  selectActivityEntities,
  selectCurrentActivityId,
  (ActivityEntities, ActivityId) => ActivityEntities[ActivityId]
);

export const selectActivityList = createSelector(
  selectActivity,
  (state: ActivityState.State) => state.entities
);

export const selectActivityError = createSelector(
  selectActivity,
  (state: ActivityState.State) => state.error
);

export const selectActivityIsLoading = createSelector(
  selectActivity,
  (state: ActivityState.State) => state.isLoading
);

export const selectLoadingError = createSelector(
  selectActivity,
  (state: ActivityState.State) => state
);
