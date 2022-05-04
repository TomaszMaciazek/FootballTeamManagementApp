import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { CardColor } from 'src/app/enums/card-color';
import { MatchPointType } from 'src/app/enums/match-point-type';
import { MatchType } from 'src/app/enums/match-type';
import { PlayerPosition } from 'src/app/enums/player-position';
import { PlayersGender } from 'src/app/enums/players-gender';
import { CoachCard } from 'src/app/models/cards/coach-card';
import { PlayerCard } from 'src/app/models/cards/player-card';
import { MatchScore } from 'src/app/models/match-score.model';
import { Match } from 'src/app/models/match/match';
import { MatchPoint } from 'src/app/models/match/match-point';
import { MatchPlayer } from 'src/app/models/player/match-player';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { CoachCardService } from 'src/app/services/coach-card.service';
import { MatchPlayerPerformanceService } from 'src/app/services/match-player-performance.service';
import { MatchPointService } from 'src/app/services/match-point.service';
import { MatchScoreService } from 'src/app/services/match-score.service';
import { MatchService } from 'src/app/services/match.service';
import { PlayerCardService } from 'src/app/services/player-card.service';
import { EditMatchScoreComponent } from '../match-scores/edit-match-score/edit-match-score.component';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss']
})
export class MatchComponent implements OnInit {

  id: string;
  match: Match = null;
  
  players: Array<MatchPlayer> = null;
  matchPoints: Array<MatchPoint> = null;
  coachesCards: Array<CoachCard> = null;
  playersCards : Array<PlayerCard> = null;
  matchScores: Array<MatchScore> = null;
  displayCreateScoreDialog: boolean = false;
  displayEditScoreDialog: boolean = false;

  cardColor = CardColor;
  matchPointType = MatchPointType;

  comparePoints = (a: MatchPoint, b: MatchPoint) => {
    if (a.minuteOfMatch < b.minuteOfMatch)
      return -1;
    if (a.minuteOfMatch > b.minuteOfMatch)
      return 1;
    return 0;
  };

  playerPositionsLabel = new Map<number, string>([
    [PlayerPosition.AttackingMidfielder, 'position_attackingMidfielder'],
    [PlayerPosition.CenterBack, 'position_centerBack'],
    [PlayerPosition.DefensiveMiedfielder, 'position_defensiveMidfielder'],
    [PlayerPosition.Goalkeeper, 'position_goalkeeper'],
    [PlayerPosition.LeftBack, 'position_leftBack'],
    [PlayerPosition.RightBack, 'position_rightBack'],
    [PlayerPosition.Striker, 'position_striker'],
    [PlayerPosition.Reserve, 'position_reserve']
  ]);

  playersGenderLabel = new Map<number, string>([
    [PlayersGender.Males, 'male'],
    [PlayersGender.Females, 'female'],
    [PlayersGender.Both, 'players_gender_both']
  ]);

  matchTypeOptionsLabel = new Map<number, string>([
    [MatchType.Cup, 'cup_match'],
    [MatchType.League, 'league_match'],
    [MatchType.Friendly, 'friendly_match']
  ]);

  pointTypesLabels = new Map<number, string>([
    [MatchPointType.CornerKick, 'corner_kick_goal'],
    [MatchPointType.FreeKick, 'free_kick_goal'],
    [MatchPointType.InGame, 'in_game_goal'],
    [MatchPointType.Own, 'own_goal'],
    [MatchPointType.Penalty, 'penalty_goal']
  ]);

  @ViewChild(EditMatchScoreComponent) editScoreValue: EditMatchScoreComponent;

  constructor(
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private matchService: MatchService,
    private matchScoreService: MatchScoreService,
    private playerCardService: PlayerCardService,
    private matchPlayerPerformanceService : MatchPlayerPerformanceService,
    private matchPointService: MatchPointService,
    private coachCardService: CoachCardService,
    private toastr : ToastrService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private translationProvider: TranslationProvider,
  ) { }


  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      forkJoin([
        this.matchService.getById(this.id).then(res => {
          this.match = res;
          this.spinner.hide();
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        }),
        this.playerCardService.getAllFromMatch(this.id).then(res => this.playersCards = res)
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        }),
        this.coachCardService.getAllFromMatch(this.id).then(res => this.coachesCards = res)
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        }),
        this.matchPointService.getAllFromMatch(this.id).then(res => this.matchPoints = res.sort(this.comparePoints))
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        }),
        this.matchScoreService.getAllFromMatch(this.id).then(res => this.matchScores = res)
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        }),
        this.matchPlayerPerformanceService.getAllFromMatch(this.id).then(res => this.players = res)
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        })
      ])
      .subscribe(res => this.spinner.hide());
    });
  }

  reload(event: any){
    this.router.navigate(['/refresh']);
  }

  deleteCoachCard(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_card'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.coachCardService.delete(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deactivateCoachCard(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_deactivate_card'),
      header: this.translationProvider.getTranslation('deactivate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.coachCardService.deactivate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  activateCoachCard(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_activate_card'),
      header: this.translationProvider.getTranslation('activate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.coachCardService.activate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deletePlayerCard(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_card'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.playerCardService.delete(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deactivatePlayerCard(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_deactivate_card'),
      header: this.translationProvider.getTranslation('deactivate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.playerCardService.deactivate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  activatePlayerCard(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_activate_card'),
      header: this.translationProvider.getTranslation('activate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.playerCardService.activate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deletePlayerPerformance(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_player_performance'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchPlayerPerformanceService.delete(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deactivatePlayerPerformance(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_deactivate_player_performance'),
      header: this.translationProvider.getTranslation('deactivate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchPlayerPerformanceService.deactivate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  activatePlayerPerformance(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_activate_player_performance'),
      header: this.translationProvider.getTranslation('activate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchPlayerPerformanceService.activate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deleteMatchPoint(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_match_point'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchPointService.delete(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deactivateMatchPoint(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_deactivate_match_point'),
      header: this.translationProvider.getTranslation('deactivate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchPointService.deactivate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  activateMatchPoint(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_activate_match_point'),
      header: this.translationProvider.getTranslation('activate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchPointService.activate(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deleteScore(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_match_score'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchScoreService.deleteScore(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  deactivateScore(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_deactivate_match_score'),
      header: this.translationProvider.getTranslation('deactivate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchScoreService.deactivateScore(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  activateScore(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_activate_match_score'),
      header: this.translationProvider.getTranslation('activate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.matchScoreService.activateScore(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }

  updateScore(id: string){
    var score = this.matchScores.find(x => x.id === id);

    this.editScoreValue.setValues(id, score.value);

    this.displayCreateScoreDialog = false;
    this.displayEditScoreDialog = true;
  }

  getPlayerName(id : string){
    if(this.players && this.players.length > 0){
      let player = this.players.find(x => x.playerId == id);
      if(player){
        return player.name + ' ' + player.middleName + ' ' + player.surname;
      }
    }
    return '';
  }

}
