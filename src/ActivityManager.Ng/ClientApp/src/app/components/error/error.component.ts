import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as RootSelectors from '../../root-store/selectors';
import * as RootState from '../../root-store/root-state';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html'
})
export class ErrorComponent implements OnInit {
  error$: Observable<string>;

  constructor(
    private store: Store<RootState.State>
  )
  {
  }

  ngOnInit(): void {
    this.error$ = this.store.select(RootSelectors.selectError);
  }

}
