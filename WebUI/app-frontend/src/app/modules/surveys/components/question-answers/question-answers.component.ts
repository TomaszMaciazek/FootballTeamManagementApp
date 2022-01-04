import { Component, Input, OnInit } from '@angular/core';
import { QuestionType } from 'src/app/enums/question-type';
import { SurveyQuestionWithAnswers } from 'src/app/models/surveys/survey-question-with-answers.model';
import { SimpleUser } from 'src/app/models/user/simple-user.model';

@Component({
  selector: 'question-answers',
  templateUrl: './question-answers.component.html',
  styleUrls: ['./question-answers.component.scss']
})
export class QuestionAnswersComponent {

  @Input() question : SurveyQuestionWithAnswers;
  @Input() respondents : SimpleUser[];

  questionType = QuestionType;

  getRespondent(id: string){
    return this.respondents.find(x => x.id == id);
  }

  getAnswerLabel(value: number){
    return this.question.options.find(x => x.value == value).label;
  }
}
