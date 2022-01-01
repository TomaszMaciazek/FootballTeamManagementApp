import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { CardColor } from 'src/app/enums/card-color';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { MatchSteps } from 'src/app/models/steps/match-steps';

@Component({
  selector: 'match-step-five',
  templateUrl: './match-step-five.component.html',
  styleUrls: ['./match-step-five.component.scss']
})
export class MatchStepFiveComponent implements OnInit {

  @Input() model: MatchSteps;

  public form: FormGroup;

  players: SimpleSelectPlayer[] = [];

  public cardColors: SelectItem[] = [
    {label: "card_red", value: CardColor.Red},
    {label: "card_yellow", value: CardColor.Yellow},
  ]

  cardColorsLabels = new Map<number, string>([
    [CardColor.Red, 'card_red'],
    [CardColor.Yellow, 'card_yellow']
  ]);

  constructor(
    private formBuilder: FormBuilder,
    private toastr : ToastrService
  ) { }

  ngOnInit(): void {
    this.players = this.model.assignedPlayers.map(x => x.player);
    this.createForm();
  }

  createForm(){
    this.form = this.formBuilder.group({
      'Player': [null, Validators.required],
      'Count': [0, Validators.required],
      'Color': [null, Validators.required]
    });
  }

  addCard(){
    if(this.form.valid){
      let player : SimpleSelectPlayer = this.form.controls['Player'].value;
      let color = this.form.controls['Color'].value;
      let count = this.form.controls['Count'].value;
      this.model.playersCardsAssignments.push({player: player, count: count, color: color});
      this.form.controls['Player'].setValue(null);
      this.form.controls['Count'].setValue(0);
      this.form.controls['Color'].setValue(null);
    }
    else{
      this.toastr.warning('missing_values');
    }
  }

  removeCard(id: string, color: CardColor){
    this.model.playersCardsAssignments = this.model.playersCardsAssignments.filter(x => x.player.id !== id && x.color == color);
  }
}
