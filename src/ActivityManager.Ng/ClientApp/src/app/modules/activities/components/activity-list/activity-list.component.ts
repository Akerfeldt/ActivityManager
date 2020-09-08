import { AfterViewInit, Component, OnInit, ViewChild, Input, ChangeDetectionStrategy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Observable } from 'rxjs';

import { ActivityListDataSource } from './activity-list-datasource';
import { Activity } from '../../../../models';
import { CreateActivityComponent } from '../create-activity/create-activity.component';
import { DeleteActivityComponent } from '../delete-activity/delete-activity.component';
import { EditActivityComponent } from '../edit-activity/edit-activity.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  templateUrl: './activity-list.component.html',
  selector: 'activity-list',
  styleUrls: ['./activity-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ActivityListComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatTable, { static: true }) table: MatTable<Activity>;
  @Input() activities$: Observable<Activity[]>;
  @Input() isReady: boolean;
  dataSource: ActivityListDataSource;
  displayedColumns = ['id', 'description', 'actions'];

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
    this.dataSource = new ActivityListDataSource(this.activities$);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  openCreateDialog(): void {
    this.dialog.open(CreateActivityComponent, {
      height: '210px',
      width: '600px',
      disableClose: true
    });
  }

  openDeleteDialog(activity: Activity): void {
    this.dialog.open(DeleteActivityComponent, {
      height: '250px',
      width: '400px',
      data: { ...activity },
      disableClose: true
    });
  }

  openEditDialog(activity: Activity): void {
    this.dialog.open(EditActivityComponent, {
      height: '250px',
      width: '400px',
      data: { ...activity },
      disableClose: true
    });
  }
}
