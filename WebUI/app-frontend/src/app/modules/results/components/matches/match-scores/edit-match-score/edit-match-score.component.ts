import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UpdateMatchScoreCommand } from 'src/app/models/commands/update-match-score.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { MatchScoreService } from 'src/app/services/match-score.service';

@Component({
  selector: 'edit-match-score',
  templateUrl: './edit-match-score.component.html',
  styleUrls: ['./edit-match-score.component.scss']
})
export class EditMatchScoreComponent {

  id: string;
  value: number;

  @Output() confirmUpdateScore : EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private toastr : ToastrService,
    private matchScoreService: MatchScoreService,
    private translationProvider: TranslationProvider,
    private spinner: NgxSpinnerService
  ) {}

  submit(){
    var command = new UpdateMatchScoreCommand({id: this.id, value: this.value});
    this.spinner.show();
    this.matchScoreService.editScoreValue(command).then(res => {
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
