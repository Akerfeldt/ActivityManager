import { ChangeDetectionStrategy, Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { authConfig } from './auth.config';
import * as AuthState from './root-store/auth-store/state';
import * as AuthSelectors from './root-store/auth-store/selectors';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  authenticating$: Observable<boolean>;
  title = 'activity-manager';

  constructor(private oauthService: OAuthService, private store: Store<{ auth: AuthState.State }>) {
    this.oauthService.configure(authConfig);

    this.authenticating$ = this.store.select(AuthSelectors.selectAuthenticating);
  }
}
