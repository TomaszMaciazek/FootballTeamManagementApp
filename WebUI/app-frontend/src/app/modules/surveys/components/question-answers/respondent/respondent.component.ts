import { Component, Input, OnInit } from '@angular/core';
import { SimpleUser } from 'src/app/models/user/simple-user.model';

@Component({
  selector: 'respondent',
  templateUrl: './respondent.component.html'
})
export class RespondentComponent {

  @Input() user: SimpleUser;

}
