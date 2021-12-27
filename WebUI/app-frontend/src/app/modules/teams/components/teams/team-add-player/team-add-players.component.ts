import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AddPlayersToTeam } from 'src/app/models/commands/add-player-to-team.model';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { PlayerService } from 'src/app/services/player.service';

@Component({
  selector: 'team-add-player',
  templateUrl: './team-add-players.component.html'
})
export class TeamAddPlayersComponent implements OnInit {

  @Input() teamId : string;
  @Output() confirmPlayersAdded = new EventEmitter<boolean>();

  players : SimpleSelectPlayer[];

  selectedPlayers : SimpleSelectPlayer[];

  constructor(
    private playerService: PlayerService,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider
  ) { }

  ngOnInit(): void {
    this.getPlayersWithoutTeam();
  }

  getPlayersWithoutTeam(){
    this.spinner.show();
    this.playerService.getPlayersWithoutTeam().then(res => {
      this.players = res;
      this.spinner.hide();
    });
  }

  submit(){
    if(this.selectedPlayers != null && this.selectedPlayers.length > 0){
      this.playerService.addPlayerToTeam(new AddPlayersToTeam({playersIds: this.selectedPlayers.map(x => x.id), teamId: this.teamId}))
      .then(res => {
        this.toastr.success(this.translationProvider.getTranslation('success'));
        this.spinner.hide();
        this.confirmPlayersAdded.emit(true);
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    }
  }

  reset(){
    this.selectedPlayers = [];
  }

}
