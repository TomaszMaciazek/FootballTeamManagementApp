import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportsRoutingModule } from './reports-routing.module';
import { PlayersMonthCountComponent } from './components/players-month-count/players-month-count.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoachesMonthCountComponent } from './components/coaches-month-count/coaches-month-count.component';
import { ClubCardsComponent } from './components/club-cards/club-cards.component';
import { PlayersCardsComponent } from './components/players-cards/players-cards.component';
import { CoachesCardsComponent } from './components/coaches-cards/coaches-cards.component';
import { ClubPointsComponent } from './components/club-points/club-points.component';
import { PlayersPointsComponent } from './components/players-points/players-points.component';
import { TeamsCardsComponent } from './components/teams-cards/teams-cards.component';
import { TeamsPointsComponent } from './components/teams-points/teams-points.component';
import { ClubTrainingScoresComponent } from './components/club-training-scores/club-training-scores.component';
import { PlayerTrainingScoresComponent } from './components/player-training-scores/player-training-scores.component';
import { PlayerMatchScoresComponent } from './components/player-match-scores/player-match-scores.component';
import { ClubMatchScoresComponent } from './components/club-match-scores/club-match-scores.component';
import { TrainingScoresRankingComponent } from './components/training-scores-ranking/training-scores-ranking.component';
import { MatchScoresRankingComponent } from './components/match-scores-ranking/match-scores-ranking.component';


@NgModule({
  declarations: [
    PlayersMonthCountComponent,
    CoachesMonthCountComponent,
    ClubCardsComponent,
    PlayersCardsComponent,
    CoachesCardsComponent,
    ClubPointsComponent,
    PlayersPointsComponent,
    TeamsCardsComponent,
    TeamsPointsComponent,
    ClubTrainingScoresComponent,
    PlayerTrainingScoresComponent,
    PlayerMatchScoresComponent,
    ClubMatchScoresComponent,
    TrainingScoresRankingComponent,
    MatchScoresRankingComponent
  ],
  imports: [
    CommonModule,
    ReportsRoutingModule,
    SharedModule
  ]
})
export class ReportsModule { }
