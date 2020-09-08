import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { filter, tap } from 'rxjs/operators';

import * as AuthActions from '../../root-store/auth-store/actions';
import * as AuthSelectors from '../../root-store/auth-store/selectors';
import * as AuthState from '../../root-store/auth-store/state';

@Component({
  template: `
<ng-container *ngIf="loggedIn$ | async; else loggedout">
  <p>Logged In<p>

  <button mat-raised-button (click)="logOut()">
    Logout
  </button>
</ng-container>

<ng-template #loggedout>
  <button mat-raised-button (click)="logIn()">
    Login
  </button>
</ng-template>
`,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AuthorizedComponent implements OnInit {
  loggedIn$: Observable<boolean>;

  constructor(
    private oauthService: OAuthService,
    private route: ActivatedRoute,
    private router: Router,
    private store: Store<AuthState.State>)
  {
    this.configure();
  }

  ngOnInit() {
    this.loggedIn$ = this.store.select(AuthSelectors.selectAuthenticated);

    const error = this.route.snapshot.queryParamMap.get('error');

    if (error) {
      this.store.dispatch(AuthActions.authenticationFailed({ error }));
      this.router.navigate(['/error']);
    }
  }

  public logIn() {
    this.oauthService.initLoginFlow();
  }

  public logOut() {
    this.oauthService.logOut();
  }

  private configure() {
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(() => {
      // might be logged in already
      const claims: any = this.oauthService.getIdentityClaims();

      if (claims) {
        this.loadProfileAndLogIn();
      }
    });
    this.oauthService.setupAutomaticSilentRefresh();

    // Automatically load user profile
    this.oauthService.events
      .pipe(
        filter(e => e.type === 'token_received'),
        tap(_ => {
          this.loadProfileAndLogIn();
        })
      );
  }

  private loadProfileAndLogIn() {
    this.oauthService.loadUserProfile().then((claims) => {
      this.store.dispatch(AuthActions.authenticated({ claims }));

      let returnUrl = this.oauthService.state;
      if (returnUrl) {
        returnUrl = decodeURIComponent(returnUrl);
        this.router.navigateByUrl(returnUrl);
      } else {
        this.router.navigate(['/dashboard']);
      }
    });
  }

}
