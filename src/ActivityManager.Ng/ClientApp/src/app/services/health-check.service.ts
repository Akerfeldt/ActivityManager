import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { authConfig } from '../auth.config';

@Injectable({
  providedIn: 'root'
})
export class HealthCheckService {
  constructor(private http: HttpClient) { }

  public authCheck(): Promise<any> {
    return this.http.get(`${authConfig.issuer}/.well-known/openid-configuration`).toPromise();
  }
}
