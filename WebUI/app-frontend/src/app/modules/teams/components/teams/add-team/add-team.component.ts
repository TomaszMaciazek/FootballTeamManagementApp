import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SimpleCoach } from 'src/app/models/coach/simple-coach.model';
import { AddTeamCommand } from 'src/app/models/commands/add-team.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { CoachService } from 'src/app/services/coach.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'add-team',
  templateUrl: './add-team.component.html'
})
export class AddTeamComponent implements OnInit {

  @Output() confirmCreateTeam = new EventEmitter<boolean>();
  
  @Input() coaches: SimpleCoach[];
  
  public addTeamForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private teamService: TeamService
  ) { }

  ngOnInit(): void {
    this.createTeamForm();
  }

  createTeamForm() {
    this.addTeamForm = this.formBuilder.group({
        'Coach': [null, Validators.nullValidator],
        'Name': [null, Validators.required]
    });
  }

  submit(){
    if(this.addTeamForm.valid){
      var coachId = null;
      if(this.addTeamForm.controls['Coach'].value){
        coachId = this.addTeamForm.controls['Coach'].value.id;
      }
      var command = new AddTeamCommand({
        mainCoachId: coachId,
        name: this.addTeamForm.controls['Name'].value
      });
      this.spinner.show();
      this.teamService.createTeam(command).then(res => {
        this.toastr.success(this.translationProvider.getTranslation('success'));
        this.spinner.hide();
        this.confirmCreateTeam.emit(true);
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    }
  }

  reset(){
    this.addTeamForm.controls['Coach'].setValue(null);
    this.addTeamForm.controls['Name'].setValue(null);
  }
}
