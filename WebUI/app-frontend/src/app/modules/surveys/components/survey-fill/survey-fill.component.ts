import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { QuestionType } from 'src/app/enums/question-type';
import { FillSurvey } from 'src/app/models/commands/fill-survey.model';
import { SelectQuestionAnswer } from 'src/app/models/commands/select-question-answer.model';
import { TextQuestionAnswer } from 'src/app/models/commands/text-question-answer.model';
import { SurveyFillTemplate } from 'src/app/models/surveys/survey-fill-template.model';
import { SurveyQuestion } from 'src/app/models/surveys/survey-question.model';
import { UserSurveyResult } from 'src/app/models/surveys/user-survey-result.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { SurveyService } from 'src/app/services/survey.service';
import { UserSurveyResultService } from 'src/app/services/user-survey-result.service';

@Component({
  selector: 'app-survey-fill',
  templateUrl: './survey-fill.component.html',
  styleUrls: ['./survey-fill.component.scss']
})
export class SurveyFillComponent implements OnInit {

  id: string;

  form: FormGroup;
  
  result : SurveyFillTemplate = null;

  questionType = QuestionType;

  constructor(
    private activatedRoute: ActivatedRoute,
    private translationProvider: TranslationProvider,
    private userSurveyResultService: UserSurveyResultService,
    private tokenStorageProvider: TokenStorageProvider,
    private confirmationService: ConfirmationService,
    private surveyService: SurveyService,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.createForm();
      this.id = routeParams['id'];
      this.surveyService.getTemplateToFillById(this.id).then(res => {
        this.result = res;
        this.result.questions.forEach(x => this.addQuestionAnswerForm(x));
        this.spinner.hide();
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    });
  }

  createForm(){
    this.form = new FormGroup({
      'QuestionsAnswers': new FormArray([])
    });
  }

  addQuestionAnswerForm(question: SurveyQuestion) {
    var answerItem = null;

    if(question.type == QuestionType.Text){
      answerItem = new FormGroup({
        'questionId': new FormControl(question.id, Validators.nullValidator),
        'answer': new FormControl('', question.isRequired ? Validators.required : Validators.nullValidator),
      });
    }
    else{
      answerItem = new FormGroup({
        'questionId': new FormControl(question.id, Validators.nullValidator),
        'answer': new FormControl(null, question.isRequired ? Validators.required : Validators.nullValidator),
      });
    }
    (<FormArray>this.form.get('QuestionsAnswers')).push(answerItem);
  }

  confirmSubmit(){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_fill_survey'),
      header: this.translationProvider.getTranslation('compleate_filling'),
      icon: 'pi pi-info-circle',
      accept: () => {
        this.submit();
      }
    });
  }

  submit(){
    this.spinner.show();
    let formData = this.form.value;

    var fillCommand = new FillSurvey();
    fillCommand.respondentId = this.tokenStorageProvider.getUserId();
    fillCommand.surveyId = this.id;


    let surveyQuestions = formData.QuestionsAnswers;

    var textQuestionAnswers = [];
    var selectQuestionAnswers = [];

    surveyQuestions.forEach((question, index, array) => {
      let questionTemplate = this.result.questions.find(x => x.id == question.questionId);

      if(questionTemplate.type == QuestionType.Text){
        textQuestionAnswers.push(new TextQuestionAnswer({questionId: questionTemplate.id, value: question.answer}));
      }
      else if(questionTemplate.type == QuestionType.MutipleChoice){
        if(question.answer != null){
          if(question.answer instanceof Array){
            question.answer.forEach(x => {
              selectQuestionAnswers.push(new SelectQuestionAnswer({questionId: questionTemplate.id, value: x}));
            });
          }
          else{
            selectQuestionAnswers.push(new SelectQuestionAnswer({questionId: questionTemplate.id, value: question.answer}));
          }
        }
        else{
          selectQuestionAnswers.push(new SelectQuestionAnswer({questionId: questionTemplate.id, value: null}));
        }
      }
      else{
        selectQuestionAnswers.push(new SelectQuestionAnswer({questionId: questionTemplate.id, value: question.answer}));
      }
    });

    fillCommand.selectQuestionAnswers = selectQuestionAnswers;
    fillCommand.textQuestionAnswers = textQuestionAnswers;

    this.userSurveyResultService.fillSurvey(fillCommand)
    .then(res => {
      this.toastr.success(this.translationProvider.getTranslation('success'));
      this.spinner.hide();
      this.router.navigate(["/surveys/assigned"]);
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }
}
