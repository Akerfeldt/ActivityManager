import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { OAuthResourceServerErrorHandler } from 'angular-oauth2-oidc';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnauthorizedErrorHandlerService
  implements OAuthResourceServerErrorHandler {

  constructor(private route: ActivatedRoute, private router: Router) { }

  handleError(err: HttpResponse<any>): Observable<any> {

    // token expired, or logged out
    // so begin sign-in flow
    if (err.status === 401) {
      const returnUrl = this.route.snapshot.url;
      this.router.navigate(['login'], { queryParams: { return_url: returnUrl } });
    }

    return throwError(err);
  }
}
