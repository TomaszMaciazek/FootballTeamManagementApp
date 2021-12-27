import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { Training } from 'src/app/models/training.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TrainingScoreService } from 'src/app/services/training-score.service';
import { TrainingService } from 'src/app/services/training.service';
import { EditTrainingScoreValueComponent } from '../../trainingScores/edit-training-score-value/edit-training-score-value.component';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html'
})
export class TrainingComponent implements OnInit {

  id: string;
  training: Training = null;
  displayCreateScoreDialog: boolean = false;
  displayEditScoreDialog: boolean = false;

  @ViewChild(EditTrainingScoreValueComponent) editScoreValue: EditTrainingScoreValueComponent;

  constructor(
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private trainingService: TrainingService,
    private trainingScoreService: TrainingScoreService,
    private toastr : ToastrService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private translationProvider: TranslationProvider,
  ) { }


  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.trainingService.getById(this.id).then(res => {
        this.training = res;
        this.spinner.hide();
      });
    });
  }

  reload(event: any){
    this.router.navigate(['/refresh']);
  }

  deleteScore(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_delete_training_score'),
      header: this.translationProvider.getTranslation('delete'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.trainingScoreService.deleteTrainingScore(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('training_score_deleted'));
          this.router.navigate(['/refresh']);
        });
      }
    });
  }

  deactivateScore(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_deactivate_training_score'),
      header: this.translationProvider.getTranslation('deactivate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.trainingScoreService.deactivateTrainingScore(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('training_score_deactivated'));
          this.router.navigate(['/refresh']);
        });
      }
    });
  }

  activateScore(id: string){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_activate_training_score'),
      header: this.translationProvider.getTranslation('activate'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.spinner.show();
        this.trainingScoreService.activateTrainingScore(id).then(x => {
          this.spinner.hide();
          this.toastr.success(this.translationProvider.getTranslation('training_score_activated'));
          this.router.navigate(['/refresh']);
        });
      }
    });
  }

  updateScore(id: string){
    var score = this.training.trainingScores.find(x => x.id === id);

    this.editScoreValue.setValues(id, score.value);

    this.displayCreateScoreDialog = false;
    this.displayEditScoreDialog = true;
  }

}
