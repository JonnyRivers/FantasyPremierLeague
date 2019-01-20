export interface Player {
  id: number;
  name: string;
  team: string;
  position: string;
  nowCost: number;
  minutesPlayed: number;
  goals: number;
  assists: number;
  saves: number;
  savesPerNinety: number;
  savesPerNinetyPerNowCost: number;
  conceded: number;
  concededPerNinety: number;
  concededPerNinetyPerNowCost: number;
  cleanSheets: number;
  cleanSheetsPerNinety: number;
  cleanSheetsPerNinetyPerNowCost: number;
  points: number;
  pointsPerNinety: number;
  pointsPerNinetyPerNowCost: number;
  ictIndex: number;
  ictIndexPerNinety: number;
  ictIndexPerNinetyPerNowCost: number;
}
