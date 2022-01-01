import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { MatchPointType } from 'src/app/enums/match-point-type';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { MatchSteps } from 'src/app/models/steps/match-steps';

@Component({
  selector: 'match-step-four',
  templateUrl: './match-step-four.component.html',
  styleUrls: ['./match-step-four.component.scss']
})
export class MatchStepFourComponent implements OnInit {

  @Input() model: MatchSteps;

  public form: FormGroup;

  players: SimpleSelectPlayer[] = [];

  public pointTypes: SelectItem[] = [
    {label: "corner_kick_goal", value: MatchPointType.CornerKick},
    {label: "free_kick_goal", value: MatchPointType.FreeKick},
    {label: "in_game_goal", value: MatchPointType.InGame},
    {label: "own_goal", value: MatchPointType.Own},
    {label: "penalty_goal", value: MatchPointType.Penalty}
  ]

  pointTypesLabels = new Map<number, string>([
    [MatchPointType.CornerKick, 'corner_kick_goal'],
    [MatchPointType.FreeKick, 'free_kick_goal'],
    [MatchPointType.InGame, 'in_game_goal'],
    [MatchPointType.Own, 'own_goal'],
    [MatchPointType.Penalty, 'penalty_goal']
  ]);

  constructor(
    private formBuilder: FormBuilder,
    private toastr : ToastrService
  ) { }

  ngOnInit(): void {
    this.players = this.model.assignedPlayers.map(x => x.player);
    this.createForm();
  }

  createForm(){
    this.form = this.formBuilder.group({
      'Player': [null, Validators.required],
      'Minute': [0, Validators.required],
      'Point': [null, Validators.required]
    });
  }

  addPoint(){
    if(this.form.valid){
      let player : SimpleSelectPlayer = this.form.controls['Player'].value;
      let point = this.form.controls['Point'].value;
      let minute = this.form.controls['Minute'].value;
      this.model.playersPointsAssignments.push({player: player, minuteOfMatch: minute, pointType: point});
      this.form.controls['Player'].setValue(null);
      this.form.controls['Minute'].setValue(0);
      this.form.controls['Point'].setValue(null);
    }
    else{
      this.toastr.warning('missing_values');
    }
  }

  removePoint(id: string, minute: number){
    this.model.playersPointsAssignments = this.model.playersPointsAssignments.filter(x => x.player.id !== id && x.minuteOfMatch == minute);
  }

}
