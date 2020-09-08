import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { Activity } from '../models';
import { environment } from '../../environments/environment';

const API_URL = environment.activityManagerApiUrl;

@Injectable({
  providedIn: 'root'
})
export class ActivityManagerApiService {

  constructor(
    private http: HttpClient
  ) { }

  deleteActivity(id: number): Observable<any> {
    return this.http.delete(`${API_URL}/api/activities/${id}`);
  }

  getActivities(): Observable<Activity[]> {
    return this.http.get<Activity[]>(`${API_URL}/api/activities`);
  }

  postActivity(activity: Activity): Observable<Activity> {
    return this.http.post<Activity>(`${API_URL}/api/activities`, activity);
  }

  putActivity(activity: Activity): Observable<Activity> {
    return this.http.put<Activity>(`${API_URL}/api/activities/${activity.id}`, activity);
  }

}
