import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AddTrainingCommand } from 'src/app/models/commands/add-training.model';
import { LocaleCalendarProvider } from 'src/app/providers/locale-calendar-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TrainingService } from 'src/app/services/training.service';

@Component({
  selector: 'app-add-training',
  templateUrl: './add-training.component.html'
})
export class AddTrainingComponent implements OnInit {

  public form: FormGroup;
  
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private trainingService: TrainingService
  ) { }


  
  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.form = this.formBuilder.group({
        'Description': [null, Validators.required],
        'Title': [null, Validators.required],
        'Date': [new Date(), Validators.required]
    });
  }

  submit(){
    if(this.form.valid){
      let date = this.form.controls['Date'].value;
      this.spinner.show();
      var training = new AddTrainingCommand({
        title: this.form.controls['Title'].value,
        description: this.form.controls['Description'].value,
        date:  new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds()))
      });
      this.trainingService.createTraining(training)
        .then(res => {
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.spinner.hide();
          this.router.navigate(["/results/trainings"]);
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
    }
  }

  reset(){
    this.form.controls['Description'].setValue("");
    this.form.controls['Title'].setValue("");
    this.form.controls['Date'].setValue(new Date());
  }
}
