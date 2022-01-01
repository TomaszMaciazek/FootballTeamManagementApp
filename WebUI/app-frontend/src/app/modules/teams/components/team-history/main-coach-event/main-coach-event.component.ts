import { Component, Input, OnInit } from '@angular/core';
import { CoachAssignmentEvent } from 'src/app/models/history/coach-assignment-event.model';

@Component({
  selector: 'main-coach-event',
  templateUrl: './main-coach-event.component.html',
  styleUrls: ['./main-coach-event.component.scss']
})
export class MainCoachEventComponent {

  @Input() event : CoachAssignmentEvent;

}
