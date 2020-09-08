import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';

import * as AuthSelectors from '../../root-store/auth-store/selectors';
import * as AuthState from '../../root-store/auth-store/state';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit {
  loggedIn$: Observable<boolean>;
  name$: Observable<string>;
  
  constructor(
    private oauthService: OAuthService,
    private store: Store<AuthState.State>)
  {
  }

  ngOnInit() {
    this.loggedIn$ = this.store.select(AuthSelectors.selectAuthenticated);
    this.name$ = this.store.select(AuthSelectors.selectGivenName);
  }

  public logOut() {
    this.oauthService.logOut();
  }

}
