import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { MatchType } from 'src/app/enums/match-type';
import { PlayersGender } from 'src/app/enums/players-gender';
import { MatchItem } from 'src/app/models/listItems/match-item.model';
import { MatchQuery } from 'src/app/models/queries/match-query.model';
import { SimpleTeam } from 'src/app/models/team/simple-team.model';
import { MatchService } from 'src/app/services/match.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.scss']
})
export class MatchesComponent implements OnInit {

  public form: FormGroup;

  showFilters: boolean = false;

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "desc";
  sortColumn: string = "Date";
  matches : MatchItem[];
  teams : SimpleTeam[];

  private queryString : string = null;
  private minDate : string = null;
  private maxDate : string = null;
  private isActive : boolean = null;
  private selectedPlayersGenders : PlayersGender[] = null;
  private selectedTeams : SimpleTeam[] = null;
  private selectedMatchTypes : MatchType[] = null;

  boolFilterOptions = [
    {label: 'both', value: null},
    {label: 'yes', value: true},
    {label: 'no', value: false}
  ];

  matchTypeOptions = [
    {label: 'cup_match', value : MatchType.Cup},
    {label: 'league_match', value : MatchType.League},
    {label: 'friendly_match', value : MatchType.Friendly}
  ]

  playersGendersOptions = [
    {label: 'male', value : PlayersGender.Males},
    {label: 'female', value : PlayersGender.Females},
    {label: 'players_gender_both', value : PlayersGender.Both},
  ]

  playerPositionsLabel = new Map<number, string>([
    [PlayersGender.Males, 'male'],
    [PlayersGender.Females, 'female'],
    [PlayersGender.Both, 'players_gender_both']
  ]);

  matchTypeOptionsLabel = new Map<number, string>([
    [MatchType.Cup, 'cup_match'],
    [MatchType.League, 'league_match'],
    [MatchType.Friendly, 'friendly_match']
  ]);

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private matchService: MatchService,
    private teamService : TeamService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();

    let query = new MatchQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      queryString : null,
      isActive : null,
      playersGenders: null,
      teamsIds : null,
      minDate: null,
      maxDate: null,
      matchTypes: null
    });

    forkJoin([
      this.teamService.getAllTeams().then(res => this.teams = res),
      this.matchService.getMatches(query).then(res => {
        this.matches = res.items;
        this.totalCount = res.totalCount;
      })
    ])
    .subscribe(res => this.spinner.hide());
  }

  createForm(){
    this.form = this.formBuilder.group({
      'SearchQuery': [null, Validators.nullValidator],
      'IsActive': [null, Validators.nullValidator],
      'Teams': [null, Validators.nullValidator],
      'Genders': [null, Validators.nullValidator],
      'MatchTypes': [null, Validators.nullValidator],
      'MinDate': [null, Validators.nullValidator],
      'MaxDate': [null, Validators.nullValidator]
    });
  }

  getMatches(query: MatchQuery){
    this.spinner.show();
    this.matchService.getMatches(query)
    .then(res => {
      this.matches = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    });
  }

  loadMatches(event: LazyLoadEvent) {
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
    
    let query = this.getMatchesQuery();
    this.getMatches(query);
  }

  filter(){
    if(this.form.controls['SearchQuery'].value != null && this.form.controls['SearchQuery'].value.length > 0){
      this.queryString = this.form.controls['SearchQuery'].value;
    }
    else{
      this.queryString = null;
    }

    if(this.form.controls['Teams'].value != null){
      this.selectedTeams = this.form.controls['Teams'].value;
    }
    else{
      this.selectedTeams = null;
    }

    if(this.form.controls['IsActive'].value != null){
      this.isActive = this.form.controls['IsActive'].value;
    }
    else{
      this.isActive = null;
    }

    if(this.form.controls['MatchTypes'].value != null){
      this.selectedMatchTypes = this.form.controls['MatchTypes'].value;
  }
  else{
    this.selectedMatchTypes = null;
  }

    if(this.form.controls['Genders'].value != null){
        this.selectedPlayersGenders = this.form.controls['Genders'].value;
    }
    else{
      this.selectedPlayersGenders = null;
    }
    
    if(this.form.controls['MinDate'].value != null){
      let minDateValue = this.form.controls['MinDate'].value;
      this.minDate = new Date(Date.UTC(minDateValue.getFullYear(), minDateValue.getMonth(), minDateValue.getDate())).toISOString();
    }
    else{
      this.minDate = null;
    }

    if(this.form.controls['MaxDate'].value != null){
      let maxDateValue = this.form.controls['MaxDate'].value;
      this.maxDate = new Date(Date.UTC(maxDateValue.getFullYear(), maxDateValue.getMonth(), maxDateValue.getDate())).toISOString();
    }
    else{
      this.maxDate = null;
    }

    this.pageNumber = 1;
    this.sortOrder = "desc";
    this.sortColumn= "Date";

    let query = this.getMatchesQuery();
    this.getMatches(query);
  }

  resetFilter() {
    this.queryString = null;
    this.isActive = null;
    this.selectedTeams = null;
    this.selectedPlayersGenders = null;
    this.minDate = null;
    this.maxDate = null;
    this.selectedMatchTypes = null;

    this.form.controls['SearchQuery'].setValue(null);
    this.form.controls['Teams'].setValue(null);
    this.form.controls['IsActive'].setValue(null);
    this.form.controls['Genders'].setValue(null);
    this.form.controls['MinDate'].setValue(null);
    this.form.controls['MaxDate'].setValue(null);
    this.form.controls['MatchTypes'].setValue(null);

    this.pageNumber = 1;
    this.sortOrder = "desc";
    this.sortColumn= "Date";

    let query = this.getMatchesQuery();
    this.getMatches(query);
  }

  getMatchesQuery(){

    let teamsIds = this.selectedTeams != null && this.selectedTeams.length > 0  
    ? this.selectedTeams.map(x => x.id) 
    : null;

    return new MatchQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      queryString : this.queryString,
      isActive : this.isActive,
      playersGenders: this.selectedPlayersGenders,
      teamsIds : teamsIds,
      minDate: this.minDate,
      maxDate: this.maxDate,
      matchTypes: this.selectedMatchTypes
    });
  }

  addNewMatch(){
    this.router.navigate(['/results/matches/add']);
  }



}
