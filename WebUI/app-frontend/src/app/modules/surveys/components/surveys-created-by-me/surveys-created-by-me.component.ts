import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { MySurveyListItem } from 'src/app/models/listItems/my-survey-list-item.model';
import { SurveyTemplateQuery } from 'src/app/models/queries/survey-template-query.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { SurveyService } from 'src/app/services/survey.service';

@Component({
  selector: 'app-surveys-created-by-me',
  templateUrl: './surveys-created-by-me.component.html',
  styleUrls: ['./surveys-created-by-me.component.scss']
})
export class SurveysCreatedByMeComponent implements OnInit {

  form: FormGroup;

  surveys: MySurveyListItem[];

  titleSearch: string = null;
  isAnonymous: boolean = null;

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "asc";
  sortColumn: string = "Title";

  showFilters : boolean = false;

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private translationProvider: TranslationProvider,
    private surveyService: SurveyService,
    private formBuilder: FormBuilder,
    private tokenStorageProvider: TokenStorageProvider,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    let query = new SurveyTemplateQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      title: this.titleSearch,
      authorId: this.tokenStorageProvider.getUserId(),
      isAnonymous : this.isAnonymous
    })
    this.surveyService.getSurveysCreatedByMe(query).then(res => {
      this.surveys = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  isAnonymousOptions = [
    {label: 'all', value: null},
    {label: 'only_anonymous', value: true},
    {label: 'only_not_anonymous', value: false}
  ]

  createForm(){
    this.form = this.formBuilder.group({
      'Title': [null, Validators.nullValidator],
      'IsAnonymous': [null, Validators.nullValidator],
    });
  }
  
  getSurveys(){
    this.spinner.show();
    let query = new SurveyTemplateQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      title: this.titleSearch,
      authorId: this.tokenStorageProvider.getUserId(),
      isAnonymous : this.isAnonymous
    });
    this.surveyService.getSurveysCreatedByMe(query).then(res => {
      this.surveys = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  loadSurveys(event: LazyLoadEvent) {
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
    this.getSurveys();
  }

  filter(){
    if(this.form.controls['Title'].value != null && this.form.controls['Title'].value.length > 0){
      this.titleSearch = this.form.controls['Title'].value;
    }
    else{
      this.titleSearch = null;
    }

    if(this.form.controls['IsAnonymous'].value != null){
      this.isAnonymous = this.form.controls['IsAnonymous'].value;
    }
    else{
      this.isAnonymous = null;
    }

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Title";
    this.getSurveys();
  }

  resetFilter() {
    this.titleSearch = null;
    this.isAnonymous = null

    this.form.controls['Title'].setValue(null);
    this.form.controls['IsAnonymous'].setValue(null);

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Title";
    this.getSurveys();
  }

  deleteSurvey(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_survey'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.surveyService.deleteSurvey(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('survey_deleted'));
          this.router.navigate(['/refresh']);
        });
      }
    });
  }

}
