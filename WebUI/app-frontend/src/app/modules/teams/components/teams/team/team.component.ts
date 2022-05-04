import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { Gender } from 'src/app/enums/gender';
import { RemovePlayerFromTeam } from 'src/app/models/commands/remove-player-from-team.model';
import { Team } from 'src/app/models/team/team.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { PlayerService } from 'src/app/services/player.service';
import { TeamService } from 'src/app/services/team.service';
import { TeamAddPlayersComponent } from '../team-add-player/team-add-players.component';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.scss']
})
export class TeamComponent implements OnInit {

  @ViewChild(TeamAddPlayersComponent) addPlayers: TeamAddPlayersComponent;

  id: string;
  team: Team;
  displayAddPlayersToTeamDialog : boolean = false;

  genderLabel = new Map<number, string>([
    [Gender.Male, 'male'],
    [Gender.Female, 'female']
  ]);


  constructor(
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private teamService: TeamService,
    private toastr : ToastrService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private translationProvider: TranslationProvider,
    private tokenStorageProvider: TokenStorageProvider,
    private playerService: PlayerService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.teamService.getById(this.id).then(res => {
        this.team = res;
        this.spinner.hide();
      });
    });
  }

  confirmRemove(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_remove_player_from_team'),
        header: this.translationProvider.getTranslation('remove_player_from_team'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          this.spinner.show();
          this.playerService.removePlayerFromTeam(new RemovePlayerFromTeam({playerId: id, teamId: this.id})).then(x => {
            this.spinner.hide();
            this.toastr.success(this.translationProvider.getTranslation('player_removed_from_team'));
            this.router.navigate(['/refresh']);
          });
        }
    });
  }

  reload(event: any){
    this.router.navigate(['/refresh']);
  }

  showAddPlayersDialog(){
    this.addPlayers.reset();
    this.displayAddPlayersToTeamDialog = true;
  }

}
