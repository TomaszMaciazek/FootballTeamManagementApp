import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { MatchScoreType } from 'src/app/enums/match-score-type';
import { AddMatchScoreCommand } from 'src/app/models/commands/add-match-score.model';
import { MatchScore } from 'src/app/models/match-score.model';
import { MatchPlayer } from 'src/app/models/player/match-player';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { MatchScoreService } from 'src/app/services/match-score.service';

@Component({
  selector: 'add-match-score',
  templateUrl: './add-match-score.component.html',
  styleUrls: ['./add-match-score.component.scss']
})
export class AddMatchScoreComponent implements OnInit {

  @Input() players: Array<MatchPlayer>;
  @Input() matchId: string;
  @Input() existingMatchScores: Array<MatchScore>;
  @Output() confirmUpdateScore = new EventEmitter<boolean>();

  public addScoreForm: FormGroup;

  public typeOptions: SelectItem[] = [
    {label: "tactical_performance", value: MatchScoreType.TacticalPerformance},
    {label: "ball_control", value: MatchScoreType.BallControl},
    {label: "left_foot_pass_accuracy", value: MatchScoreType.LeftFootPassAccuracy},
    {label: "right_foot_pass_accuracy", value: MatchScoreType.RightFootPassAccuracy},
    {label: "left_foot_dribbling_accuracy", value: MatchScoreType.LeftFootDribblingAccuracy},
    {label: "right_foot_dribbling_accuracy", value: MatchScoreType.RightFootDribblingAccuracy},
    {label: "left_foot_ball_receiving_accuracy", value: MatchScoreType.LeftFootBallReceivingAccuracy},
    {label: "right_foot_ball_receiving_accuracy", value: MatchScoreType.RightFootBallReceivingAccuracy},
    {label: "left_foot_shots_accuracy", value: MatchScoreType.LeftFootShotsAccuracy},
    {label: "right_foot_shots_accuracy", value: MatchScoreType.RightFootShotsAccuracy},
    {label: "heading_accuracy", value: MatchScoreType.HeadingAccuracy},
    {label: "one_vs_one", value: MatchScoreType.OneVsOneScore},
    {label: "mobility", value: MatchScoreType.Mobility},
    {label: "strength", value: MatchScoreType.Strength},
    {label: "endurance", value: MatchScoreType.Endurance},
    {label: "agility", value: MatchScoreType.Agility},
    {label: "coordination", value: MatchScoreType.Coordination},
    {label: "concentration", value: MatchScoreType.Concentration},
    {label: "emotions_control", value: MatchScoreType.EmotionsControl},
    {label: "selfconfidence", value: MatchScoreType.Selfconfidence},
    {label: "stress_control", value: MatchScoreType.StressControl},
    {label: "attitude", value: MatchScoreType.Attitude},
    {label: "communication", value: MatchScoreType.Communication},
    {label: "cooperation", value: MatchScoreType.Cooperation},
    {label: "determination", value: MatchScoreType.Determination},
    {label: "discipline", value: MatchScoreType.Discipline},
    {label: "engagement", value: MatchScoreType.Engagement},
    {label: "creativity", value: MatchScoreType.Creativity},
    {label: "decisiveness", value: MatchScoreType.Decisiveness},
    {label: "awareness", value: MatchScoreType.Awareness}
  ].sort((a: SelectItem, b: SelectItem) => {
    if (this.translationProvider.getTranslation(a.label) < this.translationProvider.getTranslation(b.label))
        return -1;
      if (this.translationProvider.getTranslation(a.label) > this.translationProvider.getTranslation(b.label))
        return 1;
      return 0;
  });

  constructor(
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private matchScoreService: MatchScoreService
    ) { }
  
    ngOnInit(): void {
      this.createAddScoreForm();
    }
  
    createAddScoreForm() {
      this.addScoreForm = this.formBuilder.group({
          'Player': [null, Validators.required],
          'ScoreType': [null, Validators.required],
          'Value': [5, Validators.required]
      });
    }

    submit(){
      if(this.addScoreForm.valid){
        if(this.existingMatchScores && this.existingMatchScores.filter(
            x => x.playerId == this.addScoreForm.controls['Player'].value.playerId && x.scoreType ==this.addScoreForm.controls['ScoreType'].value
            ).length > 0){
              this.toastr.warning(this.translationProvider.getTranslation('match_score_already_exists'));
          }
        else{
          var command = new AddMatchScoreCommand({
            matchId: this.matchId,
            playerId: this.addScoreForm.controls['Player'].value.playerId,
            scoreType: this.addScoreForm.controls['ScoreType'].value,
            value: this.addScoreForm.controls['Value'].value
          });
          this.spinner.show();
          this.matchScoreService.addScore(command).then(res => {
            this.toastr.success(this.translationProvider.getTranslation('success'));
            this.spinner.hide();
            this.confirmUpdateScore.emit(true);
          })
          .catch(error => {
            this.toastr.error(this.translationProvider.getTranslation(error));
            this.spinner.hide();
          });
        }
      }
    }
    
    reset(){
      this.addScoreForm.controls['Player'].setValue(null);
      this.addScoreForm.controls['ScoreType'].setValue(null);
      this.addScoreForm.controls['Value'].setValue(5);
    }



}
