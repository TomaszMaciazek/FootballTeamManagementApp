import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { PlayerMatchScoreStatistics } from 'src/app/models/report/player-match-score-statistics.model';
import { ClubCardsComponent } from './components/club-cards/club-cards.component';
import { ClubMatchScoresComponent } from './components/club-match-scores/club-match-scores.component';
import { ClubPointsComponent } from './components/club-points/club-points.component';
import { ClubTrainingScoresComponent } from './components/club-training-scores/club-training-scores.component';
import { CoachesCardsComponent } from './components/coaches-cards/coaches-cards.component';
import { CoachesMonthCountComponent } from './components/coaches-month-count/coaches-month-count.component';
import { MatchScoresRankingComponent } from './components/match-scores-ranking/match-scores-ranking.component';
import { PlayerMatchScoresComponent } from './components/player-match-scores/player-match-scores.component';
import { PlayerTrainingScoresComponent } from './components/player-training-scores/player-training-scores.component';
import { PlayersCardsComponent } from './components/players-cards/players-cards.component';
import { PlayersMonthCountComponent } from './components/players-month-count/players-month-count.component';
import { PlayersPointsComponent } from './components/players-points/players-points.component';
import { TeamsCardsComponent } from './components/teams-cards/teams-cards.component';
import { TeamsPointsComponent } from './components/teams-points/teams-points.component';
import { TrainingScoresRankingComponent } from './components/training-scores-ranking/training-scores-ranking.component';

const routes: Routes = [
  { path: 'players/count', component: PlayersMonthCountComponent, canActivate: [AuthGuard]},
  { path: 'coaches/count', component: CoachesMonthCountComponent, canActivate: [AuthGuard]},
  { path: 'cards/all', component: ClubCardsComponent, canActivate: [AuthGuard]},
  { path: 'cards/players', component: PlayersCardsComponent, canActivate: [AuthGuard]},
  { path: 'cards/coaches', component: CoachesCardsComponent, canActivate: [AuthGuard]},
  { path: 'cards/teams', component: TeamsCardsComponent, canActivate: [AuthGuard]},
  { path: 'points/all', component: ClubPointsComponent, canActivate: [AuthGuard]},
  { path: 'points/players', component: PlayersPointsComponent, canActivate: [AuthGuard]},
  { path: 'points/teams', component: TeamsPointsComponent, canActivate: [AuthGuard]},
  { path: 'trainingScores/all', component: ClubTrainingScoresComponent, canActivate: [AuthGuard]},
  { path: 'trainingScores/players', component: PlayerTrainingScoresComponent, canActivate: [AuthGuard]},
  { path: 'trainingScores/ranking', component: TrainingScoresRankingComponent, canActivate: [AuthGuard]},
  { path: 'matchScores/all', component: ClubMatchScoresComponent, canActivate: [AuthGuard]},
  { path: 'matchScores/players', component: PlayerMatchScoresComponent, canActivate: [AuthGuard]},
  { path: 'matchScores/ranking', component: MatchScoresRankingComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
