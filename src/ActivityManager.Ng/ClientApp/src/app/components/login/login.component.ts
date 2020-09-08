import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';

import * as AuthActions from '../../root-store/auth-store/actions';
import * as AuthSelectors from '../../root-store/auth-store/selectors';
import * as AuthState from '../../root-store/auth-store/state';


@Component({
  template: `<p>Logging in...</p>`,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit {
  loggingIn$: Observable<boolean>;

  constructor(
    private oauthService: OAuthService,
    private route: ActivatedRoute,
    private router: Router,
    private store: Store<AuthState.State>) {
    this.loggingIn$ = this.store.select(AuthSelectors.selectAuthenticating);
  }

  async ngOnInit() {
    this.store.dispatch(AuthActions.authenticating());

    try {
      await this.oauthService.loadDiscoveryDocument();
    }
    catch (err) {
      this.store.dispatch(AuthActions.authenticationFailed({ error: 'Authentication server offline' }));
      this.router.navigate(['/error']);
      return;
    }

    const returnUrl = this.route.snapshot.queryParamMap.get('return_url');
    if (returnUrl) {
      this.oauthService.initLoginFlow(returnUrl);
    } else {
      this.oauthService.initLoginFlow();
    }
  }

}
