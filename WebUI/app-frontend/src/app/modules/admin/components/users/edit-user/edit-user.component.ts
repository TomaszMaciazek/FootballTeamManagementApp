import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { BehaviorSubject, forkJoin, Observable } from 'rxjs';
import { Gender } from 'src/app/enums/gender';
import { Country } from 'src/app/models/country.model';
import { Role } from 'src/app/models/role.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { CountryService } from 'src/app/services/country.service';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  public form: FormGroup;
  public isEdit: boolean;
  private id: string;
  public role: string;

  public prefferedPositionControl: FormControl;
  public startedPlayingControl: FormControl;
  public finishedPlayingControl: FormControl;
  public teamIdControl: FormControl;
  public startedWorkingControl: FormControl;
  public finishedWorkingControl: FormControl;
  public genderControl: FormControl;
  public countryIdControl: FormControl;
  public birthDateControl: FormControl;

  public roles: Role[];
  public countries: Country[];
  public genders: SelectItem[] =[{label: "female" ,value: Gender.Female}, {label: "male", value: Gender.Male}];

  private _showPlayerControls: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private _showCoachControls: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public showPlayerControls: Observable<boolean> = this._showPlayerControls.asObservable();
  public showCoachControls: Observable<boolean> = this._showCoachControls.asObservable();

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private roleService: RoleService,
    private countryService: CountryService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      debugger;
      this.isEdit = !(routeParams['id'] == '0')
      this.id = routeParams['id'];
      this.createForm();
      if(this.isEdit){

      }
      forkJoin([
        this.roleService.getRoles().then(res => this.roles = res),
        this.countryService.getCountries().then(res => this.countries = res)
      ])
      .subscribe(res => this.spinner.hide());
    });
  }

  createForm() {
    this.teamIdControl = this.formBuilder.control("TeamId");
    this.countryIdControl = this.formBuilder.control("CountryId", Validators.required);
    this.prefferedPositionControl = this.formBuilder.control("PrefferedPosition", Validators.required);
    this.startedPlayingControl = this.formBuilder.control("StartedPlaying", Validators.required);
    this.startedWorkingControl = this.formBuilder.control("StartedWorking", Validators.required);
    this.genderControl = this.formBuilder.control("Gender", Validators.required);
    this.birthDateControl = this.formBuilder.control("BirthDate", Validators.required);
    if(this.isEdit){
      this.finishedPlayingControl = this.formBuilder.control("FinishedPlaying", Validators.required);
      this.finishedWorkingControl = this.formBuilder.control("FinishedWorking", Validators.required);
    }
    this.form = this.formBuilder.group({
        'Email': ["", Validators.required],
        'Name': ["", Validators.required],
        'Password': ["", Validators.required],
        'Middlename': [""],
        'Surname': ["", Validators.required],
        'Role': ["", Validators.required]
    });
  }

  setRole(event){
    debugger;
    if(event.value.name != this.role){
      if(this.role === "player"){
        this.form.removeControl("PrefferedPosition");
        this.form.removeControl("StartedPlaying");
        this.form.removeControl("TeamId");
        this._showPlayerControls.next(false);
        if(this.isEdit){
          this.form.removeControl("FinishedPlaying");
        }
      }
      else if(this.role === "coach"){
        this.form.removeControl("StartedWorking");
        if(this.isEdit){
          this.form.removeControl("FinishedWorking");
        }
        this._showCoachControls.next(false);
      }

      if(event.value.name === "admin"){
        this.form.removeControl("Gender");
        this.form.removeControl("BirthDate");
        this.form.removeControl("CountryId");
      }
      else if(event.value.name === "player"){
        this.form.addControl("PrefferedPosition", this.prefferedPositionControl);
        this.form.addControl("StartedPlaying", this.startedPlayingControl);
        this.form.addControl("TeamId", this.teamIdControl);
        this.form.addControl("CountryId", this.countryIdControl);
        this.form.addControl("Gender", this.genderControl);
        this.form.addControl("BirthDate", this.birthDateControl);
        this._showPlayerControls.next(true);
      }
      else if(event.value.name === "coach"){
        this.form.addControl("StartedWorking", this.startedWorkingControl);
        this.form.addControl("CountryId", this.countryIdControl);
        this.form.addControl("Gender", this.genderControl);
        this.form.addControl("BirthDate", this.birthDateControl);
        this._showCoachControls.next(true);
      }
    }
  }

}
