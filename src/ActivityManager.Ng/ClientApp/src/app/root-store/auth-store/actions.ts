import { createAction, props } from '@ngrx/store';

export const authenticated = createAction('[Auth/API] Authenticated', props<{ claims: object }>());
export const authenticating = createAction('[Auth/API] Authenticating');
export const authenticationFailed = createAction('[Auth/API] Authentication Failed', props<{ error: string }>());
