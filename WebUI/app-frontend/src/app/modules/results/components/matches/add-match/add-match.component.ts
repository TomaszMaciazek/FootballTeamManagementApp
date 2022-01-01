import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { AddCoachCardToNewMatch } from 'src/app/models/commands/add-coach-card-to-new-match.model';
import { AddMatchPointToNewMatch } from 'src/app/models/commands/add-match-point-to-new-match.model';
import { AddMatchCommand } from 'src/app/models/commands/add-match.model';
import { AddPlayerCardToNewMatch } from 'src/app/models/commands/add-player-card-to-new-match.model';
import { AddPlayerPerformanceToNewMatch } from 'src/app/models/commands/add-player-performance-to-new-match.model';
import { MatchSteps } from 'src/app/models/steps/match-steps';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { MatchService } from 'src/app/services/match.service';

@Component({
  selector: 'app-add-match',
  templateUrl: './add-match.component.html',
  styleUrls: ['./add-match.component.scss']
})
export class AddMatchComponent implements OnInit {

  currentStep = 0;
  model: MatchSteps;

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private matchService: MatchService,
    private toastr : ToastrService,
    private translationProvider: TranslationProvider,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.model = new MatchSteps();
  }

  nextStep() {
    if(this.currentStep == 0){
      this.validateFirstStep();
    }
    else if(this.currentStep == 2){
      this.validateThirdStep();
    }
    else{
      window.scroll(0,0);
      this.currentStep++;
    }
  }

  previousStep() {
    window.scroll(0,0);
    this.currentStep--;
  }

  confirmSubmit(){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_create_match'),
      header: this.translationProvider.getTranslation('match_new'),
      icon: 'pi pi-info-circle',
      accept: () => {
        this.submit();
      }
    });
  }

  submit(){
    var playerPerformances: AddPlayerPerformanceToNewMatch[] = [];
    this.model.assignedPlayers.forEach(x => 
      playerPerformances.push(new AddPlayerPerformanceToNewMatch({playerPosition: x.position, playerId: x.player.id}))
    );

    var points: AddMatchPointToNewMatch[] = [];
    this.model.playersPointsAssignments.forEach(x => 
      points.push(new AddMatchPointToNewMatch({playerId: x.player.id, point: x.pointType, minuteOfMatch: x.minuteOfMatch}))
    );

    var playersCards: AddPlayerCardToNewMatch[] = [];
    this.model.playersCardsAssignments.forEach(x =>
      playersCards.push(new AddPlayerCardToNewMatch({playerId: x.player.id, color: x.color, count: x.count}))
    );

    var coachesCards: AddCoachCardToNewMatch[] = [];
    this.model.coachCardsAssignments.forEach(x =>
      coachesCards.push(new AddCoachCardToNewMatch({coachId: x.coach.id, color: x.color, count: x.count}))
    );
    
    let command = new AddMatchCommand({
      date: new Date(Date.UTC(this.model.date.getFullYear(), this.model.date.getMonth(), this.model.date.getDate())),
      opponentsClubName: this.model.opponentsClubName,
      opponentsScore: this.model.opponentsScore,
      location: this.model.location,
      clubScore: this.model.clubScore,
      playersGender: this.model.playersGender,
      matchType: this.model.matchType,
      playerPerformances: playerPerformances,
      playersCards: playersCards,
      matchPoints: points,
      coachesCards: coachesCards,
      description: this.model.description
    })

    this.spinner.show();
    this.matchService.createMatch(command)
    .then(res => {
      this.toastr.success(this.translationProvider.getTranslation('success'));
      this.spinner.hide();
      this.router.navigate(["/results/matches"]);
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  private validateFirstStep() {
    if (this.model.date == null ||this.model.location == null || this.model.location.length == 0 ||
      this.model.opponentsClubName == null || this.model.opponentsClubName.length == 0 ||
      this.model.playersGender == null || this.model.matchType == null
    ){
      window.scroll(0,0);
      this.toastr.warning(this.translationProvider.getTranslation('missing_values'));
    }
    else{
      window.scroll(0,0);
      this.currentStep++;
    }
  }

  private validateThirdStep(){
    if(this.model.assignedPlayers.length < 11){
      this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_players_number'),
        header: this.translationProvider.getTranslation('activate'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          window.scroll(0,0);
          this.currentStep++;
        }
      });
    }
    else{
      window.scroll(0,0);
      this.currentStep++;
    }
  }
}
