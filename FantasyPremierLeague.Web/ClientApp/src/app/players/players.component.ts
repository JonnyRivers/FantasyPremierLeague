import { Component, Inject, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { HttpClient } from '@angular/common/http';

export interface Player {
  id: number;
  name: string;
  points: number;
  nowCost: number;
  minutesPlayed: number;
  goals: number;
  assists: number;
  conceded: number;
  cleanSheets: number;
  ictIndex: number;
}

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent {
  public players: Player[];
  dataSource: MatTableDataSource<Player>;
  displayedColumns: string[] = [
    'name',
    'position',
    'team',
    'points',
    'nowCost',
    'minutesPlayed',
    'goals',
    'assists',
    'conceded',
    'cleanSheets',
    'ictIndex'
  ];
  @ViewChild(MatSort) sort: MatSort;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Player[]>(baseUrl + 'api/players').subscribe(result => {
      this.players = result;
      this.dataSource = new MatTableDataSource(this.players);
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
}
