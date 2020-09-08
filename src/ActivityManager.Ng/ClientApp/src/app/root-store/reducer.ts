import { ActionReducerMap, MetaReducer } from '@ngrx/store';

import { environment } from '../../environments/environment';
import * as ActivityReducer from './activity-store/reducer';
import * as AuthReducer from './auth-store/reducer';
import { State } from './root-state';


export const reducers: ActionReducerMap<State> = {
  activity: ActivityReducer.reducer,
  auth: AuthReducer.reducer
};

export const metaReducers: MetaReducer<State>[] = !environment.production ? [] : [];
