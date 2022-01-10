import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { QuestionType } from 'src/app/enums/question-type';
import { FillTestCommand } from 'src/app/models/commands/fill-test.model';
import { TestQuestionAnswerCommand } from 'src/app/models/commands/test-question-answer.model';
import { TestToFillQuestion } from 'src/app/models/tests/test-to-fill-question.model';
import { TestToFill } from 'src/app/models/tests/test-to-fill.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TestService } from 'src/app/services/test.service';
import { UserTestResultService } from 'src/app/services/user-test-result.service';

@Component({
  selector: 'app-test-fill',
  templateUrl: './test-fill.component.html',
  styleUrls: ['./test-fill.component.scss']
})
export class TestFillComponent implements OnInit {

  id: string;

  form: FormGroup;
  
  result : TestToFill = null;

  questionType = QuestionType;

  constructor(
    private activatedRoute: ActivatedRoute,
    private translationProvider: TranslationProvider,
    private userTestResultService: UserTestResultService,
    private tokenStorageProvider: TokenStorageProvider,
    private confirmationService: ConfirmationService,
    private testService: TestService,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.createForm();
      this.id = routeParams['id'];
      this.testService.getTemplateToFillById(this.id).then(res => {
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

  addQuestionAnswerForm(question: TestToFillQuestion) {
    var answerItem = null;
    answerItem = new FormGroup({
      'questionId': new FormControl(question.id, Validators.nullValidator),
      'answer': new FormControl(null, Validators.required),
    });
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

    var fillCommand = new FillTestCommand();
    fillCommand.respondentId = this.tokenStorageProvider.getUserId();
    fillCommand.testId = this.id;


    let testQuestions = formData.QuestionsAnswers;
    var questionAnswers = [];

    testQuestions.forEach((question, index, array) => {
      let questionTemplate = this.result.questions.find(x => x.id == question.questionId);

      if(questionTemplate.type == QuestionType.MutipleChoice){
        if(question.answer != null){
          if(question.answer instanceof Array){
            question.answer.forEach(x => {
              questionAnswers.push(new TestQuestionAnswerCommand({questionId: questionTemplate.id, value: x}));
            });
          }
          else{
            questionAnswers.push(new TestQuestionAnswerCommand({questionId: questionTemplate.id, value: question.answer}));
          }
        }
        else{
          questionAnswers.push(new TestQuestionAnswerCommand({questionId: questionTemplate.id, value: null}));
        }
      }
      else{
        questionAnswers.push(new TestQuestionAnswerCommand({questionId: questionTemplate.id, value: question.answer}));
      }
    });

    fillCommand.questionAnswers = questionAnswers;

    this.userTestResultService.fillTest(fillCommand)
    .then(res => {
      this.toastr.success(this.translationProvider.getTranslation('success'));
      this.spinner.hide();
      this.router.navigate(["/tests/result/" + res]);
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
   }

}
