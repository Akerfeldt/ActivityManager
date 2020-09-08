import { createAction, props } from '@ngrx/store';

import { Activity } from '../../models';
import { Update, EntityMap, Predicate } from '@ngrx/entity';

export const loadActivitiesFromApi = createAction('[Activity/API] Load Activities From API');
export const loadActivitiesFromApiSuccess = createAction('[Activity/API] Load Activities From API Success', props<{ activities: Activity[] }>());
export const loadActivitiesFromApiFailure = createAction('[Activity/API] Load Activities From API Failure', props<{ error: any }>());

export const postActivityToApi = createAction('[Activity/API] Post Activity To API', props<{ activity: Activity }>());
export const postActivityToApiSuccess = createAction('[Activity/API] Post Activity To API Success', props<{ activity: Activity }>());
export const postActivityToApiFailure = createAction('[Activity/API] Post Activity To API Failure', props<{ error: any }>());

export const putActivityToApi = createAction('[Activity/API] Put Activity To API', props<{ activity: Activity }>());
export const putActivityToApiSuccess = createAction('[Activity/API] Put Activity To API Success', props<{ activity: Activity }>());
export const putActivityToApiFailure = createAction('[Activity/API] Put Activity To API Failure', props<{ error: any }>());

export const deleteActivityToApi = createAction('[Activity/API] Delete Activity To API', props<{ id: number }>());
export const deleteActivityToApiSuccess = createAction('[Activity/API] Delete Activity To API Success', props<{ id: number }>());
export const deleteActivityToApiFailure = createAction('[Activity/API] Delete Activity To API Failure', props<{ error: any }>());

export const loadActivities = createAction('[Activity/API] Load Activities', props<{ Activities: Activity[] }>());
export const addActivity = createAction('[Activity/API] Add Activity', props<{ Activity: Activity }>());
export const upsertActivity = createAction('[Activity/API] Upsert Activity', props<{ Activity: Activity }>());
export const addActivities = createAction('[Activity/API] Add Activities', props<{ Activities: Activity[] }>());
export const upsertActivities = createAction('[Activity/API] Upsert Activities', props<{ Activities: Activity[] }>());
export const updateActivity = createAction('[Activity/API] Update Activity', props<{ Activity: Update<Activity> }>());
export const updateActivities = createAction('[Activity/API] Update Activities', props<{ Activities: Update<Activity>[] }>());
export const mapActivities = createAction('[Activity/API] Map Activities', props<{ entityMap: EntityMap<Activity> }>());
export const deleteActivity = createAction('[Activity/API] Delete Activity', props<{ id: string }>());
export const deleteActivities = createAction('[Activity/API] Delete Activities', props<{ ids: string[] }>());
export const deleteActivitiesByPredicate = createAction('[Activity/API] Delete Activities By Predicate', props<{ predicate: Predicate<Activity> }>());
export const clearActivities = createAction('[Activity/API] Clear Activities');
