import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { Activity } from '../../models';

export const featureAdapter: EntityAdapter<Activity> = createEntityAdapter<
  Activity
>({
  selectId: model => model.id,
  sortComparer: (a: Activity, b: Activity): number =>
    b.description.toString().localeCompare(a.description.toString())
});

export interface State extends EntityState<Activity> {
  error?: any;
  isLoading?: boolean;
  selectedActivityId?: number;
}

export const initialState: State = featureAdapter.getInitialState({
  error: null,
  isLoading: false
});
