import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { TrainingScoreType } from 'src/app/enums/training-score-type';
import { AddTrainingScoreCommand } from 'src/app/models/commands/add-training-score.model';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { TrainingScore } from 'src/app/models/training-score.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { PlayerService } from 'src/app/services/player.service';
import { TrainingScoreService } from 'src/app/services/training-score.service';

@Component({
  selector: 'add-training-score',
  templateUrl: './add-training-score.component.html'
})
export class AddTrainingScoreComponent implements OnInit {

  @Input() trainingId: string;
  @Input() existingTrainingScores: Array<TrainingScore>;
  @Output() confirmUpdateScore = new EventEmitter<boolean>();

  public addScoreForm: FormGroup;
  public players: SimpleSelectPlayer[];

  public typeOptions: SelectItem[] = [
    {label: "ball_control", value :TrainingScoreType.BallControl},
    {label: "left_foot_pass_accuracy", value :TrainingScoreType.LeftFootPassAccuracy},
    {label: "right_foot_pass_accuracy", value :TrainingScoreType.RightFootPassAccuracy},
    {label: "left_foot_dribbling_accuracy", value :TrainingScoreType.LeftFootDribblingAccuracy},
    {label: "right_foot_dribbling_accuracy", value :TrainingScoreType.RightFootDribblingAccuracy},
    {label: "left_foot_ball_receiving_accuracy", value :TrainingScoreType.LeftFootBallReceivingAccuracy},
    {label: "right_foot_ball_receiving_accuracy", value :TrainingScoreType.RightFootBallReceivingAccuracy},
    {label: "left_foot_shots_accuracy", value :TrainingScoreType.LeftFootShotsAccuracy},
    {label: "right_foot_shots_accuracy", value :TrainingScoreType.RightFootShotsAccuracy},
    {label: "heading_accuracy", value :TrainingScoreType.HeadingAccuracy},
    {label: "one_vs_one", value :TrainingScoreType.OneVsOneScore},
    {label: "mobility", value :TrainingScoreType.Mobility},
    {label: "strength", value :TrainingScoreType.Strength},
    {label: "endurance", value :TrainingScoreType.Endurance},
    {label: "agility", value :TrainingScoreType.Agility},
    {label: "coordination", value :TrainingScoreType.Coordination},
    {label: "concentration", value :TrainingScoreType.Concentration},
    {label: "emotions_control", value :TrainingScoreType.EmotionsControl},
    {label: "selfconfidence", value :TrainingScoreType.Selfconfidence},
    {label: "stress_control", value :TrainingScoreType.StressControl},
    {label: "attitude", value :TrainingScoreType.Attitude},
    {label: "communication", value :TrainingScoreType.Communication},
    {label: "cooperation", value :TrainingScoreType.Cooperation},
    {label: "determination", value :TrainingScoreType.Determination},
    {label: "discipline", value :TrainingScoreType.Discipline},
    {label: "engagement", value :TrainingScoreType.Engagement},
    {label: "creativity", value :TrainingScoreType.Creativity},
    {label: "decisiveness", value :TrainingScoreType.Decisiveness},
    {label: "awareness", value :TrainingScoreType.Awareness}
  ].sort((a: SelectItem, b: SelectItem) => {
    if (this.translationProvider.getTranslation(a.label) < this.translationProvider.getTranslation(b.label))
        return -1;
      if (this.translationProvider.getTranslation(a.label) > this.translationProvider.getTranslation(b.label))
        return 1;
      return 0;
  })

  constructor(
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private playerService: PlayerService,
    private translationProvider: TranslationProvider,
    private trainingScoreService: TrainingScoreService
  ) { }

  ngOnInit(): void {
    this.createAddScoreForm();
    this.getPlayers();
  }

  createAddScoreForm() {
    this.addScoreForm = this.formBuilder.group({
        'Player': [null, Validators.required],
        'ScoreType': [null, Validators.required],
        'Value': [5, Validators.required]
    });
  }

  getPlayers(){
    this.spinner.show();
    this.playerService.getPlayingPlayers().then(res => {
      this.preparePlayers(res);
      this.spinner.hide()
    });
  }

  preparePlayers(res : SimpleSelectPlayer[]){
    const comparePlayer = (a: SimpleSelectPlayer, b: SimpleSelectPlayer) => {
      if (a.name < b.name)
        return -1;
      if (a.name > b.name)
        return 1;
      return 0;
    };
    this.players = res.sort(comparePlayer);
  }

  submit(){
    if(this.addScoreForm.valid){
      if(this.existingTrainingScores.filter(
          x => x.playerId == this.addScoreForm.controls['Player'].value.id && x.scoreType ==this.addScoreForm.controls['ScoreType'].value
          ).length > 0){
            this.toastr.warning(this.translationProvider.getTranslation('training_score_already_exists'));
        }
      else{
        var command = new AddTrainingScoreCommand({
          trainingId: this.trainingId,
          playerId: this.addScoreForm.controls['Player'].value.id,
          scoreType: this.addScoreForm.controls['ScoreType'].value,
          value: this.addScoreForm.controls['Value'].value
        });
        this.spinner.show();
        this.trainingScoreService.addScore(command).then(res => {
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
