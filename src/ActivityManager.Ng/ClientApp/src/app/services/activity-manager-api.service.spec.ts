import { TestBed } from '@angular/core/testing';

import { ActivityManagerApiService } from './activity-manager-api.service';

describe('ActivityManagerApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ActivityManagerApiService = TestBed.get(ActivityManagerApiService);
    expect(service).toBeTruthy();
  });
});
