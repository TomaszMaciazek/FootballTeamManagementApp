import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatchSteps } from 'src/app/models/steps/match-steps';


@Component({
  selector: 'match-step-two',
  templateUrl: './match-step-two.component.html',
  styleUrls: ['./match-step-two.component.scss']
})
export class MatchStepTwoComponent {

  @Input() model: MatchSteps;

  constructor() { }

}
