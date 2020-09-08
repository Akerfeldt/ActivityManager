import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { ActivityEffects } from './effects';
import { reducer } from './reducer';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forFeature('activities', reducer),
    EffectsModule.forFeature([ActivityEffects])
  ]
})
export class ActivityStoreModule { }
