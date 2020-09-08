import { NgModule } from '@angular/core';

import { FirstErrorPipe } from './pipes/first-error.pipe';

@NgModule({
  declarations: [
    FirstErrorPipe
  ],
  exports: [
    FirstErrorPipe
  ]
})
export class CoreModule { }
