import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { PlayerPosition } from 'src/app/enums/player-position';
import { AddMatchCommand } from 'src/app/models/commands/add-match.model';
import { AddPlayerPerformanceToNewMatch } from 'src/app/models/commands/add-player-performance-to-new-match.model';
import { PlayerPositionAssignment } from 'src/app/models/player/player-assignment.model';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { PlayingPlayerQuery } from 'src/app/models/queries/playing-player-query.model';
import { MatchSteps } from 'src/app/models/steps/match-steps';
import { PlayerService } from 'src/app/services/player.service';

@Component({
  selector: 'match-step-three',
  templateUrl: './match-step-three.component.html',
  styleUrls: ['./match-step-three.component.scss']
})
export class MatchStepThreeComponent implements OnInit {

  @Input() model: MatchSteps;

  public form: FormGroup;

  players: SimpleSelectPlayer[] = [];
  filteredPlayers: SimpleSelectPlayer[] = [];
  playersAssignments: PlayerPositionAssignment[] = [];

  comparePlayer = (a: SimpleSelectPlayer, b: SimpleSelectPlayer) => {
    if (a.name < b.name)
      return -1;
    if (a.name > b.name)
      return 1;
    return 0;
  };

  public playerPositions: SelectItem[] = [
    {label: "position_goalkeeper", value: PlayerPosition.Goalkeeper},
    {label: "position_rightBack", value: PlayerPosition.RightBack},
    {label: "position_leftBack", value: PlayerPosition.LeftBack},
    {label: "position_centerBack", value: PlayerPosition.CenterBack},
    {label: "position_attackingMidfielder", value: PlayerPosition.AttackingMidfielder},
    {label: "position_defensiveMiedfielder", value: PlayerPosition.DefensiveMiedfielder},
    {label: "position_striker", value: PlayerPosition.Striker},
    {label: "position_reserve", value: PlayerPosition.Reserve}
  ]

  playerPositionsLabel = new Map<number, string>([
    [PlayerPosition.Goalkeeper, 'position_goalkeeper'],
    [PlayerPosition.RightBack, 'position_rightBack'],
    [PlayerPosition.LeftBack, 'position_leftBack'],
    [PlayerPosition.CenterBack, 'position_centerBack'],
    [PlayerPosition.AttackingMidfielder, 'position_attackingMidfielder'],
    [PlayerPosition.DefensiveMiedfielder, 'position_defensiveMiedfielder'],
    [PlayerPosition.Striker, 'position_striker'],
    [PlayerPosition.Reserve, 'position_reserve']
  ]);

  constructor(
    private spinner: NgxSpinnerService,
    private formBuilder: FormBuilder,
    private playerService: PlayerService,
    private toastr : ToastrService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    var date : string = null;
    if(this.model.date != null || this.model.date != undefined){
      date = this.model.date.toISOString();
    }
    this.playerService.getPlayingPlayers(new PlayingPlayerQuery({playersGender: this.model.playersGender, date: date})).then(res => {
      this.players = res;
      this.filteredPlayers = JSON.parse(JSON.stringify(res));;
      this.filteredPlayers = this.filteredPlayers.sort(this.comparePlayer);
      this.spinner.hide();
    })
  }

  createForm(){
    this.form = this.formBuilder.group({
      'Player': [null, Validators.required],
      'Position': [null, Validators.required]
    });
  }

  addPlayer(){
    if(this.form.valid){
      let player : SimpleSelectPlayer = this.form.controls['Player'].value;
      let position = this.form.controls['Position'].value;
      this.model.assignedPlayers.push({player: player, position: position});

      this.filteredPlayers = this.filteredPlayers.filter(x => x.id !== this.form.controls['Player'].value.id);
      this.form.controls['Player'].setValue(null);
      this.form.controls['Position'].setValue(null);
    }
    else{
      this.toastr.warning('missing_values');
    }
  }

  removePlayer(id: string){
    this.model.assignedPlayers = this.model.assignedPlayers.filter(x => x.player.id !== id);
    let player = this.players.find(x => x.id === id);
    this.filteredPlayers.push(player);
    this.filteredPlayers = this.filteredPlayers.sort(this.comparePlayer);
  }

}
