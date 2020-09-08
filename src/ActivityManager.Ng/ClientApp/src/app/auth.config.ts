import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  clientId: 'amgr.app',
  issuer: 'https://localhost:44390',
  redirectUri: window.location.origin + '/authorized',
  responseType: 'code',
  scope: 'openid profile email offline_access amgr_api'
}
