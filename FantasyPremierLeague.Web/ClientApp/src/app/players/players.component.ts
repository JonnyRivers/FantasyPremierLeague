import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {
  public players: Player[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Player[]>(baseUrl + 'api/players').subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
    
  }
}

interface Player {
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
