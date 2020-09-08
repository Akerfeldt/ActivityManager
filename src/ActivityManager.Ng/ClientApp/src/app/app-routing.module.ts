import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthorizedComponent, DashboardComponent, ErrorComponent, LoginComponent } from './components';
import { AuthGuard } from './guards';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/dashboard'
  },
  {
    path: 'authorized',
    component: AuthorizedComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'error',
    component: ErrorComponent
  },
  {
    path: 'activities',
    canActivate: [AuthGuard],
    loadChildren: () => import('./modules/activities/activities.module').then(m => m.ActivitiesModule)
  },
  {
    path: 'login',
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
