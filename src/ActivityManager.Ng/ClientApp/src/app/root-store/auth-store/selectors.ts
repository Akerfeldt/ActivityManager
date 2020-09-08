import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as AuthState from './state';

export const selectAuth = createFeatureSelector<AuthState.State>('auth');

export const selectAuthenticated = createSelector(
  selectAuth,
  (state: AuthState.State) => state.authenticated
);

export const selectAuthenticating = createSelector(
  selectAuth,
  (state: AuthState.State) => state.authenticating
);

export const selectAuthenticationError = createSelector(
  selectAuth,
  (state: AuthState.State) => state.error
);

export const selectAuthenticationIsLoading = createSelector(
  selectAuth,
  (state: AuthState.State) => state.isLoading
);

export const selectClaims = createSelector(
  selectAuth,
  (state: AuthState.State) => state.claims
);

export const selectGivenName = createSelector(
  selectClaims,
  (claims: any) => claims && claims.given_name
);
