export interface State {
  authenticated?: boolean;
  authenticating?: boolean;
  claims: object;
  error?: string;
  isLoading?: boolean;
}

export const initialState: State = {
  authenticated: false,
  authenticating: false,
  claims: null,
  error: null,
  isLoading: false
};
