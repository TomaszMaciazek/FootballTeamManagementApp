import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { News } from 'src/app/models/news.model';
import { NewsQuery } from 'src/app/models/queries/news-query.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { NewsService } from 'src/app/services/news.service';
import { DomSanitizer, SafeResourceUrl, SafeUrl} from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Paginator } from 'primeng/paginator';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {

  pageNumber : number = 1;
  rowNumbers : number;
  rowNumbersOptions = [10, 20, 30];
  news: News[] = [];
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;

  @ViewChild('paginator', { static: true }) paginator: Paginator

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private newsService: NewsService,
    private toastr : ToastrService,
    private confirmationService: ConfirmationService,
    private translationProvider: TranslationProvider,
    ) { }

  ngOnInit(): void {
    this.rowNumbers = this.rowNumbersOptions[0];
    this.getNews();
  }

  getNews(){
    this.spinner.show();
    this.newsService.getNews(new NewsQuery({pageNumber: this.pageNumber, pageSize: this.rowNumbers, orderByColumnName: "CreatedDate", orderByDirection: "desc"}))
    .then(res => {
      this.news = res.items;
      this.totalCount = res.totalCount;
      this.totalPages = res.totalPages;
      this.hasNextPage = res.hasNextPage;
      this.hasPreviousPage = res.hasPreviousPage;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  onPageChange(event){
    if(+event.rows != this.rowNumbers){
      this.rowNumbers = event.rows;
      this.pageNumber = 1;
      this.paginator.changePage(0);
    }
    else{
      this.pageNumber = event.page + 1;
    }
    this.getNews()
  }

  addNewNews(event: any) {
    this.router.navigate(['/news/add']);
  }

  editNews(id: string, event: any) {
    this.router.navigate(['/news/edit', { id: id }]);
  }

  deleteNews(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_news'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-info-circle',
      accept: () => {
        this.spinner.show();
        this.newsService.delete(id)
        .then(res => {
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.spinner.hide();
          this.getNews();
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    });
  }
}
