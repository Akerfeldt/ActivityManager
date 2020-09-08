import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { ActivityStoreModule } from './activity-store/activity-store.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ActivityStoreModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([])
  ]
})
export class RootStoreModule { }
