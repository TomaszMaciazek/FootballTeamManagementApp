import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SimpleCoach } from 'src/app/models/coach/simple-coach.model';
import { UpdateTeamCommand } from 'src/app/models/commands/update-team.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { CoachService } from 'src/app/services/coach.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'edit-team',
  templateUrl: './edit-team.component.html'
})
export class EditTeamComponent implements OnInit {

  @Input() coaches: SimpleCoach[];
  @Output() confirmUpdateTeam : EventEmitter<boolean> = new EventEmitter<boolean>();

  private id : string;
  private oldName : string = null;
  private oldCoach : SimpleCoach = null;

  public editTeamForm: FormGroup;

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
    this.editTeamForm = this.formBuilder.group({
        'Coach': [null, Validators.nullValidator],
        'Name': [null, Validators.required]
    });
  }

  setValues(id: string, coach: SimpleCoach, name: string){
    this.id = id;
    this.oldCoach = coach,
    this.oldName = name;
    this.editTeamForm.controls['Coach'].setValue(coach);
    this.editTeamForm.controls['Name'].setValue(name);
  }

  submit(){
    if(this.editTeamForm.valid){
      var coachId = null;
      if(this.editTeamForm.controls['Coach'].value){
        coachId = this.editTeamForm.controls['Coach'].value.id;
      }
      var command = new UpdateTeamCommand({
        id: this.id,
        mainCoachId: coachId,
        name: this.editTeamForm.controls['Name'].value
      });
      this.spinner.show();
      this.teamService.editTeam(command).then(res => {
        this.toastr.success(this.translationProvider.getTranslation('success'));
        this.spinner.hide();
        this.confirmUpdateTeam.emit(true);
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    }
  }

  reset(){
    this.editTeamForm.controls['Coach'].setValue(this.oldCoach);
    this.editTeamForm.controls['Name'].setValue(this.oldName);
  }

}
