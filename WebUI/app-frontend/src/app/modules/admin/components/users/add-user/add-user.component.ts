import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { BehaviorSubject, forkJoin, Observable } from 'rxjs';
import { Gender } from 'src/app/enums/gender';
import { PlayerPosition } from 'src/app/enums/player-position';
import { AddAdminCommand } from 'src/app/models/commands/add-admin.model';
import { AddCoachCommand } from 'src/app/models/commands/add-coach.model';
import { AddPlayerCommand } from 'src/app/models/commands/add-player.model';
import { Country } from 'src/app/models/country.model';
import { Role } from 'src/app/models/role.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { CountryService } from 'src/app/services/country.service';
import { RoleService } from 'src/app/services/role.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html'
})
export class AddUserComponent implements OnInit {

  public form: FormGroup;
  public role: string;

  public preferredPositionControl: FormControl;
  public startedPlayingControl: FormControl;
  public finishedPlayingControl: FormControl;
  public teamIdControl: FormControl;
  public startedWorkingControl: FormControl;
  public finishedWorkingControl: FormControl;
  public genderControl: FormControl;
  public countryIdControl: FormControl;
  public dateOfBirthControl: FormControl;

  public roles: Role[];
  public countries: Country[];
  public genders: SelectItem[] =[
    {label: "female" ,value: Gender.Female}, 
    {label: "male", value: Gender.Male}
  ];

  public playerPositions: SelectItem[] = [
    {label: "position_goalkeeper", value: PlayerPosition.Goalkeeper},
    {label: "position_rightBack", value: PlayerPosition.RightBack},
    {label: "position_leftBack", value: PlayerPosition.LeftBack},
    {label: "position_centerBack", value: PlayerPosition.CenterBack},
    {label: "position_attackingMidfielder", value: PlayerPosition.AttackingMidfielder},
    {label: "position_defensiveMiedfielder", value: PlayerPosition.DefensiveMiedfielder},
    {label: "position_striker", value: PlayerPosition.Striker}
  ]

  private _showPlayerControls: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private _showCoachControls: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public showPlayerControls: Observable<boolean> = this._showPlayerControls.asObservable();
  public showCoachControls: Observable<boolean> = this._showCoachControls.asObservable();

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private roleService: RoleService,
    private countryService: CountryService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    forkJoin([
      this.roleService.getRoles().then(res => this.roles = res),
      this.countryService.getCountries().then(res => this.countries = res.sort((a, b) => (a.code > b.code) ? 1 : -1))
    ])
    .subscribe(res => this.spinner.hide());
  }

  createForm() {
    this.teamIdControl = this.formBuilder.control(null, Validators.nullValidator);
    this.countryIdControl = this.formBuilder.control(null, Validators.required);
    this.preferredPositionControl = this.formBuilder.control(null, Validators.required);
    this.startedPlayingControl = this.formBuilder.control(new Date(), Validators.required);
    this.startedWorkingControl = this.formBuilder.control(new Date(), Validators.required);
    this.genderControl = this.formBuilder.control(null, Validators.required);
    this.dateOfBirthControl = this.formBuilder.control(new Date(), Validators.required);
    this.form = this.formBuilder.group({
        'Email': ["", Validators.required],
        'Name': ["", Validators.required],
        'Password': ["", Validators.required],
        'Middlename': ["", Validators.nullValidator],
        'Surname': ["", Validators.required],
        'Role': ["", Validators.required]
    });
  }

  setRole(event){
    if(event.value.name != this.role){
      if(this.role === "player"){
        this.form.removeControl("PreferredPosition");
        this.form.removeControl("StartedPlaying");
        this.form.removeControl("TeamId");
        this._showPlayerControls.next(false);
      }
      else if(this.role === "coach"){
        this.form.removeControl("StartedWorking");
        this._showCoachControls.next(false);
      }

      this.role = event.value.name;

      if(event.value.name === "admin"){
        this.form.removeControl("Gender");
        this.form.removeControl("DateOfBirth");
        this.form.removeControl("CountryId");
      }
      else if(event.value.name === "player"){
        this.form.addControl("PreferredPosition", this.preferredPositionControl);
        this.form.addControl("StartedPlaying", this.startedPlayingControl);
        this.form.addControl("TeamId", this.teamIdControl);
        this.form.addControl("CountryId", this.countryIdControl);
        this.form.addControl("Gender", this.genderControl);
        this.form.addControl("DateOfBirth", this.dateOfBirthControl);
        this._showPlayerControls.next(true);
      }
      else if(event.value.name === "coach"){
        this.form.addControl("StartedWorking", this.startedWorkingControl);
        this.form.addControl("CountryId", this.countryIdControl);
        this.form.addControl("Gender", this.genderControl);
        this.form.addControl("DateOfBirth", this.dateOfBirthControl);
        this._showCoachControls.next(true);
      }
    }
  }

  submit(){
    if(this.form.valid){
      debugger;
      this.spinner.show();
      if(this.role == "player"){
        var addPlayerCommand = new AddPlayerCommand({
          email: this.form.controls['Email'].value,
          password: this.form.controls['Password'].value,
          name: this.form.controls['Name'].value,
          middleName: this.form.controls['Middlename'].value,
          surname: this.form.controls['Surname'].value,
          birthDate: this.dateOfBirthControl.value,
          countryId: this.countryIdControl.value.id,
          startedPlaying: this.startedPlayingControl.value,
          gender: this.genderControl.value,
          prefferedPosition: this.preferredPositionControl.value,
          teamId: null
        });
        this.userService.createPlayer(addPlayerCommand).then(res => {
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.spinner.hide();
          this.router.navigate(['/admin/users'])
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
      else if(this.role == "coach"){
        var addCoachCommand = new AddCoachCommand({
          email: this.form.controls['Email'].value,
          password: this.form.controls['Password'].value,
          name: this.form.controls['Name'].value,
          middleName: this.form.controls['Middlename'].value,
          surname: this.form.controls['Surname'].value,
          birthDate: this.dateOfBirthControl.value,
          countryId: this.countryIdControl.value.id,
          startedWorking: this.startedWorkingControl.value,
          gender: this.genderControl.value,
          teamsIds: new Array<string>()
        });
        this.userService.createCoach(addCoachCommand).then(res => {
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.spinner.hide();
          this.router.navigate(['/admin/users'])
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
      else if(this.role == "admin"){
        var addAdminCommand = new AddAdminCommand({
          email: this.form.controls['Email'].value,
          password: this.form.controls['Password'].value,
          name: this.form.controls['Name'].value,
          middleName: this.form.controls['Middlename'].value,
          surname: this.form.controls['Surname'].value
        });
        this.userService.createAdmin(addAdminCommand).then(res => {
          this.toastr.success(this.translationProvider.getTranslation('success'));
          this.spinner.hide();
          this.router.navigate(['/admin/users'])
        })
        .catch(error => {
          this.toastr.error(this.translationProvider.getTranslation(error));
          this.spinner.hide();
        });
      }
    }
  }

}
