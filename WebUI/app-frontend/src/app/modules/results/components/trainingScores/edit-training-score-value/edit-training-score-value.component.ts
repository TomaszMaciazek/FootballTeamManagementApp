import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { UpdateTrainingScoreCommand } from 'src/app/models/commands/update-training-score.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TrainingScoreService } from 'src/app/services/training-score.service';

@Component({
  selector: 'edit-training-score-value',
  templateUrl: './edit-training-score-value.component.html'
})
export class EditTrainingScoreValueComponent {

  id: string;
  value: number;

  @Output() confirmUpdateScore : EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private toastr : ToastrService,
    private trainingScoreService: TrainingScoreService,
    private translationProvider: TranslationProvider,
    private spinner: NgxSpinnerService
  ) {}

  submit(){
    var command = new UpdateTrainingScoreCommand({id: this.id, value: this.value});
    this.spinner.show();
    this.trainingScoreService.editScoreValue(command).then(res => {
      this.toastr.success(this.translationProvider.getTranslation('success'));
      this.spinner.hide();
      this.confirmUpdateScore.emit(true);
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  setValues(id: string, value: number){
    this.id = id;
    this.value = value;
  }



}
