import { Component, Input, OnInit } from '@angular/core';
import { QuestionType } from 'src/app/enums/question-type';
import { SurveyQuestion } from 'src/app/models/surveys/survey-question.model';

@Component({
  selector: 'question-answer',
  templateUrl: './question-answer.component.html',
  styleUrls: ['./question-answer.component.scss']
})
export class QuestionAnswerComponent {

  @Input() question: SurveyQuestion;
  @Input() value : any;

  questionType = QuestionType;

  checkEquality(num : number){
    return this.value.filter(x => x == num).length > 0;
  }
}
