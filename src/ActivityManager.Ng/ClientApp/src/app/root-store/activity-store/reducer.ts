import { createEntityAdapter, EntityAdapter } from '@ngrx/entity';
import { createReducer, on, Action } from '@ngrx/store';

import { Activity } from '../../models';
import * as ActivityActions from './actions';
import { initialState, State } from './state';


export function selectActivityId(a: Activity): number {
  //In this case this would be optional since primary key is id
  return a.id;
}

export function sortByName(a: Activity, b: Activity): number {
  return a.description.localeCompare(b.description);
}

export const adapter: EntityAdapter<Activity> = createEntityAdapter<Activity>({
  selectId: selectActivityId,
  sortComparer: sortByName,
});

const ActivityReducer = createReducer(
  initialState,
  on(ActivityActions.addActivity, (state, { Activity }) => {
    return adapter.addOne(Activity, state)
  }),
  on(ActivityActions.upsertActivity, (state, { Activity }) => {
    return adapter.upsertOne(Activity, state);
  }),
  on(ActivityActions.addActivities, (state, { Activities }) => {
    return adapter.addMany(Activities, state);
  }),
  on(ActivityActions.upsertActivities, (state, { Activities }) => {
    return adapter.upsertMany(Activities, state);
  }),
  on(ActivityActions.updateActivity, (state, { Activity }) => {
    return adapter.updateOne(Activity, state);
  }),
  on(ActivityActions.updateActivities, (state, { Activities }) => {
    return adapter.updateMany(Activities, state);
  }),
  on(ActivityActions.mapActivities, (state, { entityMap }) => {
    return adapter.map(entityMap, state);
  }),
  on(ActivityActions.deleteActivity, (state, { id }) => {
    return adapter.removeOne(id, state);
  }),
  on(ActivityActions.deleteActivities, (state, { ids }) => {
    return adapter.removeMany(ids, state);
  }),
  on(ActivityActions.deleteActivitiesByPredicate, (state, { predicate }) => {
    return adapter.removeMany(predicate, state);
  }),
  on(ActivityActions.loadActivities, (state, { Activities }) => {
    return { ...adapter.addAll(Activities, state), isLoading: false };
  }),
  on(ActivityActions.clearActivities, state => {
    return adapter.removeAll({ ...state, selectedActivityId: null });
  }),
  on(ActivityActions.loadActivitiesFromApi, (state) => ({ ...state, error: null, isLoading: true })),
  on(ActivityActions.loadActivitiesFromApiFailure, (state, { error }) => ({ ...state, error: error, isLoading: false })),
  on(ActivityActions.loadActivitiesFromApiSuccess, (state, { activities }) => {
    return ({ ...adapter.upsertMany(activities, state), error: null, isLoading: false });
  }),
  on(ActivityActions.postActivityToApi, (state) => ({ ...state, error: null, isLoading: true })),
  on(ActivityActions.postActivityToApiFailure, (state, { error }) => ({ ...state, error: error, isLoading: false })),
  on(ActivityActions.postActivityToApiSuccess, (state, { activity }) => {
    return ({ ...adapter.upsertOne(activity, state), error: null, isLoading: false });
  }),
  on(ActivityActions.putActivityToApi, (state) => ({ ...state, error: null, isLoading: true })),
  on(ActivityActions.putActivityToApiFailure, (state, { error }) => ({ ...state, error: error, isLoading: false })),
  on(ActivityActions.putActivityToApiSuccess, (state, { activity }) => {
    return ({ ...adapter.upsertOne(activity, state), error: null, isLoading: false });
  }),
  on(ActivityActions.deleteActivityToApi, (state) => ({ ...state, error: null, isLoading: true })),
  on(ActivityActions.deleteActivityToApiFailure, (state, { error }) => ({ ...state, error: error, isLoading: false })),
  on(ActivityActions.deleteActivityToApiSuccess, (state, { id }) => {
    return ({ ...adapter.removeOne(id, state), error: null, isLoading: false });
  }),
);

export function reducer(state: State | undefined, action: Action) {
  return ActivityReducer(state, action);
}

export const getSelectedActivityId = (state: State) => state.selectedActivityId;

// get the selectors
const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

// select the array of Activity ids
export const selectActivityIds = selectIds;

// select the dictionary of Activity entities
export const selectActivityEntities = selectEntities;

// select the array of Activities
export const selectAllActivities = selectAll;

// select the total Activity count
export const selectActivityTotal = selectTotal;
