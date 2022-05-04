import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AddNewsCommand } from 'src/app/models/commands/add-news.model';
import { News } from 'src/app/models/news.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-add-news',
  templateUrl: './add-news.component.html'
})
export class AddNewsComponent implements OnInit {

  public form: FormGroup;
  
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private newsService: NewsService
  ) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.form = this.formBuilder.group({
        'Content': ["", Validators.required],
        'Title': ["", Validators.required],
        'IsImportant': [false]
    });
  }

  submit(){
    if(this.form.valid){
      this.spinner.show();
      var news = new AddNewsCommand({
        title: this.form.controls['Title'].value,
        content: this.form.controls['Content'].value,
        isImportant: this.form.controls['IsImportant'].value
      });
      this.newsService.createNews(news)
        .then(res => {
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.spinner.hide();
          this.router.navigate(["/news/preview"]);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
    }
  }

  reset(){
    this.form.controls['Content'].setValue("");
    this.form.controls['Title'].setValue("");
    this.form.controls['IsImportant'].setValue(false);
  }

}
