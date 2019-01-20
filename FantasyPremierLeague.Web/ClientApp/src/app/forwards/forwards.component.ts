import { Component, Inject, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { HttpClient } from '@angular/common/http';

export interface Forward {
  id: number;
  name: string;
  team: string;
  nowCost: number;
  minutesPlayed: number;
  points: number;
  pointsPerMinutePlayed: number;
  pointsPerMinutePlayedPerNowCost: number;
  ictIndex: number;
  ictIndexPerMinutePlayed: number;
  ictIndexPerMinutePlayedPerNowCost: number;
}

@Component({
  selector: 'app-forwards',
  templateUrl: './forwards.component.html',
  styleUrls: ['./forwards.component.css']
})
export class ForwardsComponent {
  public forwards: Forward[];
  dataSource: MatTableDataSource<Forward>;
  displayedColumns: string[] = [
    'name',
    'team',
    'nowCost',
    'minutesPlayed',
    'points',
    'pointsPerMinutePlayed',
    'pointsPerMinutePlayedPerNowCost',
    'ictIndex',
    'ictIndexPerMinutePlayed',
    'ictIndexPerMinutePlayedPerNowCost',
  ];
  @ViewChild(MatSort) sort: MatSort;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Forward[]>(baseUrl + 'api/forwards').subscribe(result => {
      this.forwards = result;
      this.dataSource = new MatTableDataSource(this.forwards);
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
}
