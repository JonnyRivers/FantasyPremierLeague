import { Component, Inject, OnInit } from '@angular/core';
import { Sort } from '@angular/material';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {
  public players: Player[];
  sortedPlayers: Player[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Player[]>(baseUrl + 'api/players').subscribe(result => {
      this.players = result;
      this.sortedPlayers = this.players.slice();
    }, error => console.error(error));
  }

  ngOnInit(): void {
    
  }

  sortData(sort: Sort) {
    const data = this.players.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedPlayers = data;
      return;
    }

    this.sortedPlayers = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'points': return compare(a.points, b.points, isAsc);
        case 'nowCost': return compare(a.nowCost, b.nowCost, isAsc);
        case 'minutesPlayed': return compare(a.minutesPlayed, b.minutesPlayed, isAsc);
        case 'goals': return compare(a.goals, b.goals, isAsc);
        case 'assists': return compare(a.assists, b.assists, isAsc);
        case 'conceded': return compare(a.conceded, b.conceded, isAsc);
        case 'cleanSheets': return compare(a.cleanSheets, b.cleanSheets, isAsc);
        case 'ictIndex': return compare(a.ictIndex, b.ictIndex, isAsc);
        default: return 0;
      }
    });
  }
}

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}

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
