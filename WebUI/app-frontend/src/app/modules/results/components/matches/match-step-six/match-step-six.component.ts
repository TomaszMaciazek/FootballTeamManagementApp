import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { CardColor } from 'src/app/enums/card-color';
import { SimpleCoach } from 'src/app/models/coach/simple-coach.model';
import { MatchSteps } from 'src/app/models/steps/match-steps';
import { CoachService } from 'src/app/services/coach.service';

@Component({
  selector: 'match-step-six',
  templateUrl: './match-step-six.component.html',
  styleUrls: ['./match-step-six.component.scss']
})
export class MatchStepSixComponent implements OnInit {

  @Input() model: MatchSteps;

  public form: FormGroup;

  coaches: SimpleCoach[] = [];

  public cardColors: SelectItem[] = [
    {label: "card_red", value: CardColor.Red},
    {label: "card_yellow", value: CardColor.Yellow},
  ]

  cardColorsLabels = new Map<number, string>([
    [CardColor.Red, 'card_red'],
    [CardColor.Yellow, 'card_yellow']
  ]);

  constructor(
    private spinner: NgxSpinnerService,
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private coachService: CoachService
  ) { }

  ngOnInit(): void {
    this.spinner.show()
    this.createForm();
    var date : string = null;
    if(this.model.date != null || this.model.date != undefined){
      date = this.model.date.toISOString();
    }
    this.coachService.getWorkingCoaches(date).then(res =>{
      this.coaches = res;
      this.spinner.hide();
    });
  }

  createForm(){
    this.form = this.formBuilder.group({
      'Coach': [null, Validators.required],
      'Count': [0, Validators.required],
      'Color': [null, Validators.required]
    });
  }

  addCard(){
    if(this.form.valid){
      let coach : SimpleCoach = this.form.controls['Coach'].value;
      let color = this.form.controls['Color'].value;
      let count = this.form.controls['Count'].value;
      this.model.coachCardsAssignments.push({coach: coach, count: count, color: color});
      this.form.controls['Coach'].setValue(null);
      this.form.controls['Count'].setValue(0);
      this.form.controls['Color'].setValue(null);
    }
    else{
      this.toastr.warning('missing_values');
    }
  }

  removeCard(id: string, color: CardColor){
    this.model.coachCardsAssignments = this.model.coachCardsAssignments.filter(x => x.coach.id !== id && x.color == color);
  }

}
