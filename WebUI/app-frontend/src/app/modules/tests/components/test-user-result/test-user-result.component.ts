import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { QuestionType } from 'src/app/enums/question-type';
import { TestQuestion } from 'src/app/models/tests/test-question.model';
import { UserTestResult } from 'src/app/models/tests/user-test-result.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TestService } from 'src/app/services/test.service';
import { UserTestResultService } from 'src/app/services/user-test-result.service';

@Component({
  selector: 'app-test-user-result',
  templateUrl: './test-user-result.component.html',
  styleUrls: ['./test-user-result.component.scss']
})
export class TestUserResultComponent implements OnInit {

  id: string;
  result: UserTestResult;
  questions: Array<TestQuestion>;

  constructor(
    private activatedRoute: ActivatedRoute,
    private translationProvider: TranslationProvider,
    private userTestResultService: UserTestResultService,
    private testService: TestService,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.userTestResultService.getById(this.id).then(res => {
        this.result = res;
        this.testService.getTestQuestions(this.result.test.id).then(res2 => {
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

  getQuestionAnswer(question: TestQuestion){
    if(question.type == QuestionType.MutipleChoice){
      return this.result.questionAnswers.filter(x => x.questionId == question.id).map(x => x.answerValue);
    }
    else{
      return this.result.questionAnswers.find(x => x.questionId == question.id).answerValue;
    }
  }

}
