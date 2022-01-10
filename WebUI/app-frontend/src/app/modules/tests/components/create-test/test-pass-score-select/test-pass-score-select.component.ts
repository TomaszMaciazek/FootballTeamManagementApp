import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { QuestionType } from 'src/app/enums/question-type';

@Component({
  selector: 'test-pass-score-select',
  templateUrl: './test-pass-score-select.component.html',
  styleUrls: ['./test-pass-score-select.component.scss']
})
export class TestPassScoreSelectComponent {

  @Input() value: number;
  @Input() maxScore : number;
  @Output() scoreSet = new EventEmitter<number>();

  submit(){
    if(this.value <= this.maxScore){
      this.scoreSet.emit(this.value);
    }
  }
}
