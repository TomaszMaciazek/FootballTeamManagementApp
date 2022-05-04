import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { QuestionType } from 'src/app/enums/question-type';
import { SurveyQuestion } from 'src/app/models/surveys/survey-question.model';
import { UserSurveyResult } from 'src/app/models/surveys/user-survey-result.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { SurveyService } from 'src/app/services/survey.service';
import { UserSurveyResultService } from 'src/app/services/user-survey-result.service';

@Component({
  selector: 'app-survey-user-result',
  templateUrl: './survey-user-result.component.html',
  styleUrls: ['./survey-user-result.component.scss']
})
export class SurveyUserResultComponent implements OnInit {

  id: string;
  result: UserSurveyResult;
  questions: Array<SurveyQuestion>;

  constructor(
    private activatedRoute: ActivatedRoute,
    private translationProvider: TranslationProvider,
    private userSurveyResultService: UserSurveyResultService,
    private surveyService: SurveyService,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.userSurveyResultService.getById(this.id).then(res => {
        this.result = res;
        this.surveyService.getSurveyQuestions(this.result.survey.id).then(res2 => {
          this.questions = res2;
          this.spinner.hide();
        })
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    });
  }

  getQuestionAnswer(question: SurveyQuestion){
    if(question.type == QuestionType.Text){
      return this.result.textQuestionAnswers.find(x => x.questionId == question.id).answerValue;
    }
    else if(question.type == QuestionType.MutipleChoice){
      return this.result.selectQuestionAnswers.filter(x => x.questionId == question.id).map(x => x.answerValue);
    }
    else{
      return this.result.selectQuestionAnswers.find(x => x.questionId == question.id).answerValue;
    }
  }

}
