import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { TestListItem } from 'src/app/models/listItems/test-list-item.model';
import { TestTemplateQuery } from 'src/app/models/queries/test-template-query.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-tests-all',
  templateUrl: './tests-all.component.html',
  styleUrls: ['./tests-all.component.scss']
})
export class TestsAllComponent implements OnInit {

  form: FormGroup;

  tests: TestListItem[];

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
    private testService: TestService,
    private formBuilder: FormBuilder,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    let query = new TestTemplateQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      title: this.titleSearch,
      authorId: null
    })
    this.testService.getTests(query).then(res => {
      this.tests = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  createForm(){
    this.form = this.formBuilder.group({
      'Title': [null, Validators.nullValidator]
    });
  }
  
  getTests(){
    this.spinner.show();
    let query = new TestTemplateQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      title: this.titleSearch,
      authorId: null
    });
    this.testService.getTests(query).then(res => {
      this.tests = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  loadTests(event: LazyLoadEvent) {
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
    this.getTests();
  }

  filter(){
    if(this.form.controls['Title'].value != null && this.form.controls['Title'].value.length > 0){
      this.titleSearch = this.form.controls['Title'].value;
    }
    else{
      this.titleSearch = null;
    }

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Title";
    this.getTests();
  }

  resetFilter() {
    this.titleSearch = null;
    this.isAnonymous = null

    this.form.controls['Title'].setValue(null);
    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Title";
    this.getTests();
  }

  deleteTest(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_test'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.testService.delete(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.router.navigate(['/refresh']);
        });
      }
    });
  }

}
