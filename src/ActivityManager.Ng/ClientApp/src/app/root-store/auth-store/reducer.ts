import { createReducer, on, Action } from '@ngrx/store';

import * as AuthActions from './actions';
import { initialState, State } from './state';

const AuthReducer = createReducer(
  initialState,
  on(AuthActions.authenticated, (state, { claims }) => {
    return { ...state, authenticating: false, authenticated: true, claims: claims, isLoading: false, error: null }
  }),
  on(AuthActions.authenticating, state => {
    return { ...state, authenticating: true, authenticated: false, claims: null, isLoading: true, error: null }
  }),
  on(AuthActions.authenticationFailed, (state, { error }) => {
    return { ...state, authenticating: false, authenticated: false, claims: null, isLoading: false, error: error }
  }),
);

export function reducer(state: State | undefined, action: Action) {
  return AuthReducer(state, action);
}
