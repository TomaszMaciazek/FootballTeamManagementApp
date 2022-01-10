import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { QuestionType } from 'src/app/enums/question-type';
import { SimpleUserTestResultListItem } from 'src/app/models/listItems/simple-user-test-result-list-item.model';
import { TestResultsQuery } from 'src/app/models/queries/test-results-query.model';
import { UserTestResultQuery } from 'src/app/models/queries/user-test-result-query.model';
import { TestQuestion } from 'src/app/models/tests/test-question.model';
import { Test } from 'src/app/models/tests/test.model';
import { SimpleUser } from 'src/app/models/user/simple-user.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TestService } from 'src/app/services/test.service';
import { UserSurveyResultService } from 'src/app/services/user-survey-result.service';
import { UserTestResultService } from 'src/app/services/user-test-result.service';

@Component({
  selector: 'app-test-results',
  templateUrl: './test-results.component.html',
  styleUrls: ['./test-results.component.scss']
})
export class TestResultsComponent implements OnInit {

  id: string;
  results : SimpleUserTestResultListItem[];
  test: Test;

  questionType = QuestionType;

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "asc";
  sortColumn: string = "Name";

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

      let query = new TestResultsQuery({
        pageNumber: this.pageNumber,
        pageSize: this.rowNumbers,
        orderByColumnName: this.sortColumn,
        orderByDirection: this.sortOrder,
        testId: this.id
      });

      forkJoin([
        this.testService.getById(this.id)
        .then(res => this.test = res)
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        }),
        this.userTestResultService.getTestResults(query).then(res => {
          this.results = res.items;
          this.totalCount = res.totalCount;
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        })
      ])
      .subscribe(res => this.spinner.hide());
    });
  }

  getResults(){
    this.spinner.show();
    let query = new TestResultsQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      testId: this.id
    });
    this.userTestResultService.getTestResults(query).then(res => {
      this.results = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  loadResults(event: LazyLoadEvent) {
    if(event.sortField){
      this.sortColumn = event.sortField;
    }
    if(event.sortOrder == -1){
      this.sortOrder = "desc";
    }
    else{
      this.sortOrder = "asc";
    }
    if(event.rows){
      this.rowNumbers = event.rows;
    }
    this.pageNumber = Math.floor(event.first / this.rowNumbers) + 1;
    this.getResults();
  }

}
