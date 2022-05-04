import { Component, Input, OnInit } from '@angular/core';
import { QuestionType } from 'src/app/enums/question-type';
import { TestQuestion } from 'src/app/models/tests/test-question.model';

@Component({
  selector: 'test-question-answer',
  templateUrl: './test-question-answer.component.html',
  styleUrls: ['./test-question-answer.component.scss']
})
export class TestQuestionAnswerComponent {

  @Input() question: TestQuestion;
  @Input() value : any;

  questionType = QuestionType;

  checkEquality(num : number){
    return this.value.filter(x => x == num).length > 0;
  }

}
