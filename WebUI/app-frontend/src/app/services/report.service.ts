import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { ClubCards } from '../models/report/club-cards.model';
import { ClubPoints } from '../models/report/club-points.model';
import { CoachCards } from '../models/report/coach-cards.model';
import { MonthDataCountStatistics } from '../models/report/month-data-count-statistics.model';
import { MonthlyClubCards } from '../models/report/monthly-club-cards.model';
import { MonthlyClubPoints } from '../models/report/monthly-club-points.model';
import { MonthlyCoachCards } from '../models/report/monthly-coach-cards.model';
import { MonthlyPlayerCards } from '../models/report/monthly-player-cards.model';
import { MonthlyPlayerPoints } from '../models/report/monthly-player-points.model';
import { PlayerCards } from '../models/report/player-cards.model';
import { PlayerPoints } from '../models/report/player-points.model';

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
}
