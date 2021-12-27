import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { SimpleCoach } from 'src/app/models/coach/simple-coach.model';
import { TeamItem } from 'src/app/models/listItems/team-item.model';
import { TeamQuery } from 'src/app/models/queries/team-query.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { CoachService } from 'src/app/services/coach.service';
import { TeamService } from 'src/app/services/team.service';
import { EditTeamComponent } from './edit-team/edit-team.component';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html'
})
export class TeamsComponent implements OnInit {

  @ViewChild(EditTeamComponent) editTeam: EditTeamComponent;

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "asc";
  sortColumn: string = "";
  teams : TeamItem[];
  coaches: SimpleCoach[];

  displayEditTeamDialog : boolean = false;
  displayCreateTeamDialog : boolean = false;

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private teamService: TeamService,
    private coachService: CoachService,
    private translationProvider: TranslationProvider,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    var query = new TeamQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      name: null,
      coachId: null
    });

    forkJoin([
      this.teamService.getTeams(query).then(res => {
        this.teams = res.items;
        this.totalCount = res.totalCount;
      }),
      this.coachService.getWorkingCoaches().then(res => this.prepareCoaches(res))
    ])
    .subscribe(res => this.spinner.hide());
  }

  getTeams(){
    this.spinner.show();
    var query = new TeamQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      name: null,
      coachId: null
    });
    this.teamService.getTeams(query)
    .then(res => {
      this.teams = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    });
  }

  loadTeams(event: LazyLoadEvent) {
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
    this.getTeams();
  }

  prepareCoaches(res : SimpleCoach[]){
    const compareCoach = (a: SimpleCoach, b: SimpleCoach) => {
      if ((a.surname + a.name) < (b.surname + b.name))
        return -1;
      if ((a.surname + a.name) > (b.surname + b.name))
        return 1;
      return 0;
    };
    this.coaches = res.sort(compareCoach);
  }

  confirmDeactivate(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_deactivate_team'),
        header: this.translationProvider.getTranslation('deactivate'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.spinner.show();
            this.teamService.deactivateTeam(id).then(x => {
              this.spinner.hide();
              this.toastr.success(this.translationProvider.getTranslation('team_deactivated'));
              this.router.navigate(['/refresh']);
            });
        }
    });
  }

  confirmActivate(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_activate_team'),
        header: this.translationProvider.getTranslation('activate'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.spinner.show();
            this.teamService.activateTeam(id).then(x => {
              this.spinner.hide();
              this.toastr.success(this.translationProvider.getTranslation('team_activated'));
              this.router.navigate(['/refresh']);
            });
        }
    });
  }

  confirmDelete(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_delete_team'),
        header: this.translationProvider.getTranslation('delete'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          this.spinner.show();
          this.teamService.deleteTeam(id).then(x => {
            this.spinner.hide();
            this.toastr.success(this.translationProvider.getTranslation('team_deleted'));
            this.router.navigate(['/refresh']);
          });
        }
    });
  }

  reload(event: any){
    this.router.navigate(['/refresh']);
  }

  showEditDialog(id : string){
    var team = this.teams.find(x => x.id === id);
    this.editTeam.setValues(id, team.mainCoach, team.name);
    this.displayEditTeamDialog = true;
  }

}
