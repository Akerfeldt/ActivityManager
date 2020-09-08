import * as ActivityState from './activity-store/state';
import * as AuthState from './auth-store/state';

export interface State {
  activity: ActivityState.State;
  auth: AuthState.State;
}
