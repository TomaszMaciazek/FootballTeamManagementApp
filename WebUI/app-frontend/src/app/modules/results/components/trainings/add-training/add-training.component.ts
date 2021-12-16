import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AddTrainingCommand } from 'src/app/models/commands/add-training.model';
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

  public pl = { 
    closeText: 'Zamknij',
    prevText: 'Poprzedni',
    nextText: 'Następny',
    monthNames: ['Styczeń','Luty','Marzec','Kwiecień','Maj','Czerwiec','Lipiec','Sierpień','Wrzesień','Październik','Listopad','Grudzień'],
    monthNamesShort: ['Sty','Lut','Mar','Kwi','Maj','Cze', 'Lip','Sie','Wrz','Paź','Lis','Gru'],
    dayNames: ['Niedziela','Poniedziałek','Wtorek','Środa','Czwartek','Piątek','Sobota'],
    dayNamesShort: ['Nie','Pon','Wt','Śr','Czw','Pt','So'],
    dayNamesMin: ['N','P','W','Ś','Cz','P','S'],
    weekHeader: 'Tydzień',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: 'r',
    timeOnlyTitle: 'Tylko czas',
    timeText: 'Czas',
    hourText: 'Godzina',
    minuteText: 'Minuta',
    secondText: 'Sekunda',
    currentText: 'Teraz',
    ampm: false,
    month: 'Miesiąc',
    week: 'Tydzień',
    day: 'Dzień',
    allDayText : 'Cały dzień'
  };
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
      debugger;
      this.spinner.show();
      var training = new AddTrainingCommand({
        title: this.form.controls['Title'].value,
        description: this.form.controls['Description'].value,
        date: this.form.controls['Date'].value
      });
      this.trainingService.createTraining(training)
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
    this.form.controls['Description'].setValue("");
    this.form.controls['Title'].setValue("");
    this.form.controls['Date'].setValue(new Date());
  }
}
