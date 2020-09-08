import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ActivityListContainerComponent } from './components';
import { ActivitiesGuard } from './guards';

const routes: Routes = [
  {
    path: '',
    canActivate: [ActivitiesGuard],
    component: ActivityListContainerComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ActivitiesRoutingModule { }
