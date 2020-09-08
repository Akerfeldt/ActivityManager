import { createSelector, MemoizedSelector } from '@ngrx/store';
import { selectActivity, selectActivityError, selectActivityIsLoading } from './activity-store/selectors';
import { selectAuthenticationError, selectAuthenticationIsLoading } from './auth-store/selectors';

export const selectError: MemoizedSelector<object, string> = createSelector(
  selectActivityError,
  selectAuthenticationError,
  (
    activityError: string,
    authenticationError: string
  ) => {
    return activityError || authenticationError;
  }
);

export const selectIsLoading: MemoizedSelector<
  object,
  boolean
> = createSelector(
  selectActivityIsLoading,
  selectAuthenticationIsLoading,
  (
    activityLoading: boolean,
    authenticationIsLoading: boolean
  ) => {
    return activityLoading || authenticationIsLoading;
  }
);
