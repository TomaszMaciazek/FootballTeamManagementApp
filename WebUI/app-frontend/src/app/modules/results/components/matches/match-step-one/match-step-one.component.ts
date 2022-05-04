import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatchType } from 'src/app/enums/match-type';
import { PlayersGender } from 'src/app/enums/players-gender';
import { AddMatchCommand } from 'src/app/models/commands/add-match.model';
import { MatchSteps } from 'src/app/models/steps/match-steps';

@Component({
  selector: 'match-step-one',
  templateUrl: './match-step-one.component.html',
  styleUrls: ['./match-step-one.component.scss']
})
export class MatchStepOneComponent{

  @Input() model: MatchSteps;

  constructor(
    private toastr : ToastrService
  ){}

  matchTypes = [
    {label : 'cup_match' ,value : MatchType.Cup},
    {label : 'league_match' ,value : MatchType.League},
    {label : 'friendly_match' ,value : MatchType.Friendly},
  ]

  playersGendersOptions = [
    {label: 'male', value : PlayersGender.Males},
    {label: 'female', value : PlayersGender.Females},
    {label: 'players_gender_both', value : PlayersGender.Both},
  ]
}
