import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent, SelectItem } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { Gender } from 'src/app/enums/gender';
import { Country } from 'src/app/models/country.model';
import { CoachItem } from 'src/app/models/listItems/coach-item.model';
import { CoachQuery } from 'src/app/models/queries/coach-query.model';
import { SimpleTeam } from 'src/app/models/team/simple-team.model';
import { CoachService } from 'src/app/services/coach.service';
import { CountryService } from 'src/app/services/country.service';
import { PlayerService } from 'src/app/services/player.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-coaches',
  templateUrl: './coaches.component.html'
})
export class CoachesComponent implements OnInit {

  public form: FormGroup;

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "asc";
  sortColumn: string = "CoachName";
  coaches: CoachItem[];
  teams: SimpleTeam[];
  selectedTeams : SimpleTeam[] = null;
  countries: Country[];
  selectedCountries: Country[] = null;
  selectedGender: Gender = null;
  isStillWorking : boolean = null;
  isActive : boolean = null;
  searchQuery: string = null;

  genders = [
    {label: 'gender_both', value: null},
    {label: 'female', value: Gender.Female},
    {label: 'male', value: Gender.Male}
  ];

  genderLabel = new Map<number, string>([
    [Gender.Female, 'female'],
    [Gender.Male, 'male']
  ]);

  boolFilterOptions = [
    {label: 'both', value: null},
    {label: 'yes', value: true},
    {label: 'no', value: false}
  ];

  constructor(
    private spinner: NgxSpinnerService,
    private playerService: PlayerService,
    private countryService: CountryService,
    private coachService: CoachService,
    private teamService: TeamService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();

    let query = new CoachQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      queryString : null,
      isActive : null,
      countriesIds:  null,
      gender: null,
      teamsIds : null,
      isStillWorking: null
    });

    forkJoin([
      this.teamService.getAllTeams().then(res => this.teams = res),
      this.countryService.getCountries().then(res => this.countries = res.sort((a, b) => (a.code > b.code) ? 1 : -1)),
      this.coachService.getCoaches(query).then(res => {
        this.coaches = res.items;
        this.totalCount = res.totalCount;
      })
    ])
    .subscribe(res => this.spinner.hide());
  }

  createForm(){
    this.form = this.formBuilder.group({
      'SearchQuery': [null, Validators.nullValidator],
      'Countries': [null, Validators.nullValidator],
      'IsActive': [null, Validators.nullValidator],
      'Teams': [null, Validators.nullValidator],
      'Gender': [null, Validators.nullValidator],
      'StillWorking': [null, Validators.nullValidator]
    });
  }

  getCoaches(query: CoachQuery){
    this.spinner.show();
    this.coachService.getCoaches(query)
    .then(res => {
      this.coaches = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    });
  }

  loadCoaches(event: LazyLoadEvent) {
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
    
    let query = this.getCoachesQuery();
    this.getCoaches(query);
  }

  filter(){
    if(this.form.controls['SearchQuery'].value != null && this.form.controls['SearchQuery'].value.length > 0){
      this.searchQuery = this.form.controls['SearchQuery'].value;
    }
    else{
      this.searchQuery = null;
    }

    if(this.form.controls['Teams'].value != null){
      this.selectedTeams = this.form.controls['Teams'].value;
    }
    else{
      this.selectedTeams = null;
    }

    if(this.form.controls['Countries'].value != null){
      this.selectedCountries = this.form.controls['Countries'].value;
    }
    else{
      this.selectedCountries = null;
    }

    if(this.form.controls['IsActive'].value != null){
      this.isActive = this.form.controls['IsActive'].value;
    }
    else{
      this.isActive = null;
    }

    if(this.form.controls['Gender'].value != null){
      this.selectedGender = this.form.controls['Gender'].value;
    }
    else{
      this.selectedGender = null;
    }
    
    if(this.form.controls['StillWorking'].value != null){
      this.isStillWorking = this.form.controls['StillWorking'].value;
    }
    else{
      this.isStillWorking = null;
    }

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "CoachName";

    let query = this.getCoachesQuery();
    this.getCoaches(query);
  }

  resetFilter() {
    this.searchQuery = null;
    this.isActive = null;
    this.selectedCountries = null;
    this.selectedTeams = null;
    this.selectedGender = null;
    this.isStillWorking = null;

    this.form.controls['SearchQuery'].setValue(null);
    this.form.controls['Teams'].setValue(null);
    this.form.controls['Countries'].setValue(null);
    this.form.controls['IsActive'].setValue(null);
    this.form.controls['Gender'].setValue(null);
    this.form.controls['StillWorking'].setValue(null);

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "CoachName";

    let query = this.getCoachesQuery();
    this.getCoaches(query);
  }

  getCoachesQuery(){
    let countriesIds = this.selectedCountries != null && this.selectedCountries.length > 0
    ? this.selectedCountries.map(x => x.id)
    : null;

    let teamsIds = this.selectedTeams != null && this.selectedTeams.length > 0  
    ? this.selectedTeams.map(x => x.id) 
    : null;
    return new CoachQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      queryString : this.searchQuery,
      isActive : this.isActive,
      countriesIds: countriesIds,
      gender: this.selectedGender,
      teamsIds : teamsIds,
      isStillWorking: this.isStillWorking
    });
  }

}
