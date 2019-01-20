import { Component, Inject, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { HttpClient } from '@angular/common/http';

import { Player } from '../player';

@Component({
  selector: 'app-goalkeepers',
  templateUrl: './goalkeepers.component.html',
  styleUrls: ['./goalkeepers.component.css']
})
export class GoalkeepersComponent {
  public players: Player[];
  dataSource: MatTableDataSource<Player>;
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
    http.get<Player[]>(baseUrl + 'api/goalkeepers').subscribe(result => {
      this.players = result;
      this.dataSource = new MatTableDataSource(this.players);
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
}
