import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import * as AuthSelectors from '../root-store/auth-store/selectors';
import * as AuthState from '../root-store/auth-store/state';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(
    private store: Store<AuthState.State>,
    private router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.store.select(AuthSelectors.selectAuthenticated)
      .pipe(
        tap(authenticated => {
          if (!authenticated) {
            this.router.navigate(['login'], { queryParams: { return_url: state.url }});
          }
        })
      );
  }
}
