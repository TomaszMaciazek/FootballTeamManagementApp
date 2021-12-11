import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UpdateNewsCommand } from 'src/app/models/commands/update-news.model';
import { News } from 'src/app/models/news.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-edit-news',
  templateUrl: './edit-news.component.html'
})
export class EditNewsComponent implements OnInit {

  public form: FormGroup;
  private id: string;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private newsService: NewsService
    ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.id = routeParams['id'];
      this.createForm();
      this.newsService.getById(this.id).then( res =>{
        this.form.controls['Content'].setValue(res.content);
        this.form.controls['Title'].setValue(res.title);
        this.form.controls['IsImportant'].setValue(res.isImportant);
      });
    });

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
      var command = new UpdateNewsCommand({
        id: this.id,
        title: this.form.controls['Title'].value,
        content: this.form.controls['Content'].value,
        isImportant: this.form.controls['IsImportant'].value
      });
      this.newsService.updateNews(command)
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
