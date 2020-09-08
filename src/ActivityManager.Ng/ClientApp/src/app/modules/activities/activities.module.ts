import { LayoutModule } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ActivitiesRoutingModule } from './activities-routing.module';
import { ActivityListComponent, ActivityListContainerComponent, CreateActivityComponent, DeleteActivityComponent, EditActivityComponent } from './components';
import { AppMaterialModule } from '../../app-material.module';
import { CoreModule } from '../core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ActivityListComponent,
    ActivityListContainerComponent,
    CreateActivityComponent,
    DeleteActivityComponent,
    EditActivityComponent
  ],
  entryComponents: [
    CreateActivityComponent,
    DeleteActivityComponent,
    EditActivityComponent
  ],
  imports: [
    ActivitiesRoutingModule,
    AppMaterialModule,
    CommonModule,
    CoreModule,
    FormsModule,
    LayoutModule,
    ReactiveFormsModule
  ]
})
export class ActivitiesModule { }
