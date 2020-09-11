import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatToolbarModule } from '@angular/material/toolbar';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GoalkeepersComponent } from './goalkeepers/goalkeepers.component';
import { DefendersComponent } from './defenders/defenders.component';
import { MidfieldersComponent } from './midfielders/midfielders.component';
import { ForwardsComponent } from './forwards/forwards.component';
import { MyTeamComponent } from './my-team/my-team.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GoalkeepersComponent,
    DefendersComponent,
    MidfieldersComponent,
    ForwardsComponent,
    MyTeamComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'myteam', component: MyTeamComponent },
      { path: 'goalkeepers', component: GoalkeepersComponent },
      { path: 'defenders', component: DefendersComponent },
      { path: 'midfielders', component: MidfieldersComponent },
      { path: 'forwards', component: ForwardsComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
