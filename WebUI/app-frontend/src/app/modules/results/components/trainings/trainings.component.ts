import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent, MenuItem } from 'primeng/api';
import { TrainingItem } from 'src/app/models/listItems/training-item.model';
import { TrainingQuery } from 'src/app/models/queries/training-query.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TrainingService } from 'src/app/services/training.service';

@Component({
  selector: 'app-trainings',
  templateUrl: './trainings.component.html',
  styleUrls: ['./trainings.component.scss']
})
export class TrainingsComponent implements OnInit {

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "desc";
  sortColumn: string = "";
  trainings : TrainingItem[];
  trainingButtons: Map<string,MenuItem[]>;

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private trainigService: TrainingService,
    private translationProvider: TranslationProvider,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.trainingButtons = new Map<string, MenuItem[]>();
    this.getTrainings();
  }


  getTrainings(){
    this.spinner.show();
    this.trainingButtons.clear();
    var query = new TrainingQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      isActive: null,
      title: null,
      minDate : null,
      maxDate: null
    });
    this.trainigService.getTrainings(query)
    .then(res => {
      this.trainings = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    });
  }

  addNewTraining(event: any) {
    this.router.navigate(['/results/trainings/add']);
  }

  editTraining(id: string, event: any) {
    this.router.navigate(['/results/trainings/edit', { id: id }]);
  }

  loadTrainings(event: LazyLoadEvent) {
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
    this.getTrainings();
  }

  goToTraining(id: string){
    this.router.navigate([`/results/training/${id}`]);
  }

  confirmDeactivate(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_deactivate_training'),
        header: this.translationProvider.getTranslation('deactivate'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.spinner.show();
            this.trainigService.deactivateTraining(id).then(x => {
              this.spinner.hide();
              this.toastr.success(id, 'tmp');
              this.getTrainings();
            });
        }
    });
  }

  confirmActivate(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_activate_training'),
        header: this.translationProvider.getTranslation('activate'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.spinner.show();
            this.trainigService.activateTraining(id).then(x => {
              this.spinner.hide();
              this.toastr.success(id, 'tmp');
              this.getTrainings();
            });
        }
    });
  }

  confirmDelete(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_delete_training'),
        header: this.translationProvider.getTranslation('delete'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          this.spinner.show();
          this.trainigService.deleteTraining(id).then(x => {
            this.spinner.hide();
            this.toastr.success(id, 'tmp');
            this.getTrainings();
          });
        }
    });
  }
}
