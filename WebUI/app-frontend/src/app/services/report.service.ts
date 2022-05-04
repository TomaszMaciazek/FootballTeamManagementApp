import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { MatchScoreRankingQuery } from '../models/queries/match-score-ranking-query.model';
import { TrainingScoreRankingQuery } from '../models/queries/training-score-ranking-query.model';
import { ClubCards } from '../models/report/club-cards.model';
import { ClubMonthlyMatchScoreStatistics } from '../models/report/club-monthly-match-score-statistics.model';
import { ClubMonthlyTrainingScoreStatistics } from '../models/report/club-monthly-training-score-statistics.model';
import { ClubPoints } from '../models/report/club-points.model';
import { CoachCards } from '../models/report/coach-cards.model';
import { MatchScoreStatistics } from '../models/report/match-score-statistics.model';
import { MonthDataCountStatistics } from '../models/report/month-data-count-statistics.model';
import { MonthlyClubCards } from '../models/report/monthly-club-cards.model';
import { MonthlyClubPoints } from '../models/report/monthly-club-points.model';
import { MonthlyCoachCards } from '../models/report/monthly-coach-cards.model';
import { MonthlyPlayerCards } from '../models/report/monthly-player-cards.model';
import { MonthlyPlayerPoints } from '../models/report/monthly-player-points.model';
import { MonthlyTeamCards } from '../models/report/monthly-team-cards.model';
import { MonthlyTeamPoints } from '../models/report/monthly-team-points.model';
import { PlayerCards } from '../models/report/player-cards.model';
import { PlayerMatchScoreStatistics } from '../models/report/player-match-score-statistics.model';
import { PlayerMonthlyMatchScoreStatistics } from '../models/report/player-monthly-match-score-statistics.model';
import { PlayerMonthlyTrainingScoreStatistics } from '../models/report/player-monthly-training-score-statistics.model';
import { PlayerPoints } from '../models/report/player-points.model';
import { PlayerTrainingScoreStatistics } from '../models/report/player-training-score-statistics.model';
import { PlayersMatchScoresRanking } from '../models/report/players-match-scores-ranking.model';
import { PlayersTrainingScoresRanking } from '../models/report/players-training-scores-ranking.model';
import { TeamCards } from '../models/report/team-cards.model';
import { TeamPoints } from '../models/report/team-points.model';
import { TrainingScoreStatistics } from '../models/report/training-score-statistics.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private url = `${environment.apiUrl}/report`

  constructor(
    private http: HttpClient
  ) { }

  getNewPlayersCount(startDate: string, endDate: string) : Promise<Array<MonthDataCountStatistics>>{
    return this.http.get<Array<MonthDataCountStatistics>>(`${this.url}/playersCount`, {params: toParams({startDate: startDate, endDate: endDate})}).toPromise();
  }

  getNewCoachesCount(startDate: string, endDate: string) : Promise<Array<MonthDataCountStatistics>>{
    return this.http.get<Array<MonthDataCountStatistics>>(`${this.url}/coachesCount`, {params: toParams({startDate: startDate, endDate: endDate})}).toPromise();
  }

  getClubCards(): Promise<ClubCards>{
    return this.http.get<ClubCards>(`${this.url}/cards/club/all`).toPromise();
  }

  getClubCardsMonthly(startDate: string, endDate: string): Promise<MonthlyClubCards>{
    return this.http.get<MonthlyClubCards>(`${this.url}/cards/club/monthly`, {params: toParams({startDate: startDate, endDate: endDate})}).toPromise();
  }

  getPlayerCards(id: string) : Promise<PlayerCards>{
    return this.http.get<PlayerCards>(`${this.url}/cards/player/all`, {params: toParams({playerId: id})}).toPromise();
  }

  getPlayerCardsMonthly(id: string, startDate: string, endDate: string): Promise<MonthlyPlayerCards>{
    return this.http.get<MonthlyPlayerCards>(`${this.url}/cards/player/monthly`, {params: toParams({playerId: id, startDate: startDate, endDate: endDate})}).toPromise();
  }

  getCoachCards(id: string) : Promise<CoachCards>{
    return this.http.get<CoachCards>(`${this.url}/cards/coach/all`, {params: toParams({coachId: id})}).toPromise();
  }

  getCoachCardsMonthly(id: string, startDate: string, endDate: string): Promise<MonthlyCoachCards>{
    return this.http.get<MonthlyCoachCards>(`${this.url}/cards/coach/monthly`, {params: toParams({coachId: id, startDate: startDate, endDate: endDate})}).toPromise();
  }

  getTeamCards(id: string) : Promise<TeamCards>{
    return this.http.get<TeamCards>(`${this.url}/cards/team/all`, {params: toParams({teamId: id})}).toPromise();
  }

  getTeamCardsMonthly(id: string, startDate: string, endDate: string): Promise<MonthlyTeamCards>{
    return this.http.get<MonthlyTeamCards>(`${this.url}/cards/team/monthly`, {params: toParams({teamId: id, startDate: startDate, endDate: endDate})}).toPromise();
  }

  getClubPoints(): Promise<ClubPoints>{
    return this.http.get<ClubPoints>(`${this.url}/points/club/all`).toPromise();
  }

  getClubPointsMonthly(startDate: string, endDate: string): Promise<MonthlyClubPoints>{
    return this.http.get<MonthlyClubPoints>(`${this.url}/points/club/monthly`, {params: toParams({startDate: startDate, endDate: endDate})}).toPromise();
  }

  getPlayerPoints(id: string): Promise<PlayerPoints>{
    return this.http.get<PlayerPoints>(`${this.url}/points/player/all`, {params: toParams({playerId: id})}).toPromise();
  }

  getPlayerPointsMonthly(id: string, startDate: string, endDate: string): Promise<MonthlyPlayerPoints>{
    return this.http.get<MonthlyPlayerPoints>(`${this.url}/points/player/monthly`, {params: toParams({playerId: id, startDate: startDate, endDate: endDate})}).toPromise();
  }

  getTeamPoints(id: string): Promise<TeamPoints>{
    return this.http.get<TeamPoints>(`${this.url}/points/team/all`, {params: toParams({teamId: id})}).toPromise();
  }

  getTeamPointsMonthly(id: string, startDate: string, endDate: string): Promise<MonthlyTeamPoints>{
    return this.http.get<MonthlyTeamPoints>(`${this.url}/points/team/monthly`, {params: toParams({teamId: id, startDate: startDate, endDate: endDate})}).toPromise();
  }

  getClubTrainingScores(): Promise<Array<TrainingScoreStatistics>>{
    return this.http.get<Array<TrainingScoreStatistics>>(`${this.url}/trainingScores/club/all`).toPromise();
  }

  getClubTrainingScoresMonthly(startDate: string, endDate: string): Promise<ClubMonthlyTrainingScoreStatistics>{
    return this.http.get<ClubMonthlyTrainingScoreStatistics>(`${this.url}/trainingScores/club/monthly`, {params: toParams({startDate: startDate, endDate: endDate})}).toPromise();
  }

  getPlayerTrainingScores(id: string): Promise<PlayerTrainingScoreStatistics>{
    return this.http.get<PlayerTrainingScoreStatistics>(`${this.url}/trainingScores/player/all`, {params: toParams({playerId: id})}).toPromise();
  }

  getPlayerTrainingScoresMonthly(id: string, startDate: string, endDate: string): Promise<PlayerMonthlyTrainingScoreStatistics>{
    return this.http.get<PlayerMonthlyTrainingScoreStatistics>(`${this.url}/trainingScores/player/monthly`, {params: toParams({playerId: id, startDate: startDate, endDate: endDate})}).toPromise();
  }

  getClubMatchScores(): Promise<Array<MatchScoreStatistics>>{
    return this.http.get<Array<MatchScoreStatistics>>(`${this.url}/matchScores/club/all`).toPromise();
  }

  getClubMatchScoresMonthly(startDate: string, endDate: string): Promise<ClubMonthlyMatchScoreStatistics>{
    return this.http.get<ClubMonthlyMatchScoreStatistics>(`${this.url}/matchScores/club/monthly`, {params: toParams({startDate: startDate, endDate: endDate})}).toPromise();
  }

  getPlayerMatchScores(id: string): Promise<PlayerMatchScoreStatistics>{
    return this.http.get<PlayerMatchScoreStatistics>(`${this.url}/matchScores/player/all`, {params: toParams({playerId: id})}).toPromise();
  }

  getPlayerMatchScoresMonthly(id: string, startDate: string, endDate: string): Promise<PlayerMonthlyMatchScoreStatistics>{
    return this.http.get<PlayerMonthlyMatchScoreStatistics>(`${this.url}/matchScores/player/monthly`, {params: toParams({playerId: id, startDate: startDate, endDate: endDate})}).toPromise();
  }

  getPlayersTrainingScoreRanking(query: TrainingScoreRankingQuery) : Promise<PlayersTrainingScoresRanking>{
    return this.http.get<PlayersTrainingScoresRanking>(`${this.url}/rankings/trainingScores`, {params: toParams(query)}).toPromise();
  }

  getPlayersMatchScoreRanking(query: MatchScoreRankingQuery) : Promise<PlayersMatchScoresRanking>{
    return this.http.get<PlayersMatchScoresRanking>(`${this.url}/rankings/matchScores`, {params: toParams(query)}).toPromise();
  }
}
