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

export interface Position {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent {
  positions: Position[] = [
    { value: 'all-0', viewValue: '(All Positions)' },
    { value: 'goalkeepers-1', viewValue: 'Goalkeepers' },
    { value: 'defenders-2', viewValue: 'Defenders' },
    { value: 'midfielders-3', viewValue: 'Midfielders' },
    { value: 'forwards-4', viewValue: 'Forwards' }
  ];
  selectedPosition: Position = this.positions[0].value;

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
