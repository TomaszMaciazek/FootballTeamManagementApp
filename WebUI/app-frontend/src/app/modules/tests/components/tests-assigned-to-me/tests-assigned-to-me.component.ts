import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { UserTestResultListItem } from 'src/app/models/listItems/user-test-result-list-item.model';
import { UserTestResultQuery } from 'src/app/models/queries/user-test-result-query.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { UserTestResultService } from 'src/app/services/user-test-result.service';

@Component({
  selector: 'app-tests-assigned-to-me',
  templateUrl: './tests-assigned-to-me.component.html',
  styleUrls: ['./tests-assigned-to-me.component.scss']
})
export class TestsAssignedToMeComponent implements OnInit {

  form: FormGroup;

  results: UserTestResultListItem[];

  titleSearch: string = null;
  isCompleated: boolean = null;

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
    private userTestResultService: UserTestResultService,
    private formBuilder: FormBuilder,
    private tokenStorageProvider: TokenStorageProvider,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    let query = new UserTestResultQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      title: this.titleSearch,
      userId: this.tokenStorageProvider.getUserId(),
      isCompleated : this.isCompleated
    })
    this.userTestResultService.getResults(query).then(res => {
      this.results = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  isCompleatedOptions = [
    {label: 'all', value: null},
    {label: 'only_compleated', value: true},
    {label: 'only_not_compleated', value: false}
  ]

  createForm(){
    this.form = this.formBuilder.group({
      'Title': [null, Validators.nullValidator],
      'IsCompleated': [null, Validators.nullValidator],
    });
  }
  
  getResults(){
    this.spinner.show();
    let query = new UserTestResultQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      title: this.titleSearch,
      userId: this.tokenStorageProvider.getUserId(),
      isCompleated : this.isCompleated
    })
    this.userTestResultService.getResults(query).then(res => {
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

  filter(){
    if(this.form.controls['Title'].value != null && this.form.controls['Title'].value.length > 0){
      this.titleSearch = this.form.controls['Title'].value;
    }
    else{
      this.titleSearch = null;
    }

    if(this.form.controls['IsCompleated'].value != null){
      this.isCompleated = this.form.controls['IsCompleated'].value;
    }
    else{
      this.isCompleated = null;
    }

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Title";
    this.getResults();
  }

  resetFilter() {
    this.titleSearch = null;
    this.isCompleated = null

    this.form.controls['Title'].setValue(null);
    this.form.controls['IsCompleated'].setValue(null);

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Title";
    this.getResults();
  }

}
