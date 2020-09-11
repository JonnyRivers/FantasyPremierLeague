import { Component, Inject, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { HttpClient } from '@angular/common/http';

import { Player } from '../player';

@Component({
  selector: 'app-my-team',
  templateUrl: './my-team.component.html',
  styleUrls: ['./my-team.component.css']
})
export class MyTeamComponent {
  public players: Player[];
  dataSource: MatTableDataSource<Player>;
  displayedColumns: string[] = [
    'name',
    'team',
    'nowCost',
    'minutesPlayed',
    'points',
    'pointsPerNinety',
    'pointsPerNinetyPerNowCost',
    'ictIndex',
    'ictIndexPerNinety',
    'ictIndexPerNinetyPerNowCost',
  ];
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Player[]>(baseUrl + 'api/myteam').subscribe(result => {
      this.players = result;
      this.dataSource = new MatTableDataSource(this.players);
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
}
