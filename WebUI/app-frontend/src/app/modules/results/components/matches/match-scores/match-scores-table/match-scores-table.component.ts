import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatchScore } from 'src/app/models/match-score.model';
import { MatchPlayer } from 'src/app/models/player/match-player';

@Component({
  selector: 'match-scores-table',
  templateUrl: './match-scores-table.component.html',
  styleUrls: ['./match-scores-table.component.scss']
})
export class MatchScoresTableComponent {

  @Input() matchScores: MatchScore[];
  @Input() players: MatchPlayer[];
  @Output() deleteScore = new EventEmitter<string>();
  @Output() updateScore = new EventEmitter<string>();
  @Output() activateScore = new EventEmitter<string>();
  @Output() deactivateScore = new EventEmitter<string>();

  confirmDeleteScore(id: string){
    this.deleteScore.emit(id);
  }

  confirmEditScore(id: string){
    this.updateScore.emit(id);
  }

  confirmActivateScore(id: string){
    this.activateScore.emit(id);
  }

  confirmDeactivateScore(id: string){
    this.deactivateScore.emit(id);
  }

  getPlayerName(id : string){
    if(this.players && this.players.length > 0){
      let player = this.players.find(x => x.playerId == id);
      if(player){
        return player.name + ' ' + player.middleName + ' ' + player.surname;
      }
    }
    return '';
  }

}
