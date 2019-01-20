export interface Player {
  id: number;
  name: string;
  team: string;
  position: string;
  nowCost: number;
  minutesPlayed: number;
  goals: number;
  assists: number;
  conceded: number;
  cleanSheets: number;
  points: number;
  pointsPerMinutePlayed: number;
  pointsPerMinutePlayedPerNowCost: number;
  ictIndex: number;
  ictIndexPerMinutePlayed: number;
  ictIndexPerMinutePlayedPerNowCost: number;
}
