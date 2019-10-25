import { Component, Inject, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { HttpClient } from '@angular/common/http';

import { Player } from '../player';

@Component({
  selector: 'app-midfielders',
  templateUrl: './midfielders.component.html',
  styleUrls: ['./midfielders.component.css']
})
export class MidfieldersComponent {
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
    'cleanSheets',
    'cleanSheetsPerNinety',
    'cleanSheetsPerNinetyPerNowCost',
  ];
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Player[]>(baseUrl + 'api/midfielders').subscribe(result => {
      this.players = result;
      this.dataSource = new MatTableDataSource(this.players);
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
}
