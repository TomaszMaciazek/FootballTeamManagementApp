import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent, SelectItem } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { Gender } from 'src/app/enums/gender';
import { PlayerPosition } from 'src/app/enums/player-position';
import { Country } from 'src/app/models/country.model';
import { PlayerItem } from 'src/app/models/listItems/player-item.model';
import { PlayerQuery } from 'src/app/models/queries/player-query.model';
import { SimpleTeam } from 'src/app/models/team/simple-team.model';
import { CountryService } from 'src/app/services/country.service';
import { PlayerService } from 'src/app/services/player.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html'
})
export class PlayersComponent implements OnInit {

  public form: FormGroup;

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "asc";
  sortColumn: string = "PlayerName";
  players: PlayerItem[];
  teams: SimpleTeam[];
  selectedTeam : SimpleTeam = null;;
  countries: Country[];
  selectedCountry: Country = null;
  isStillPlaying: boolean = null;

  genders : SelectItem[] = [
    {label: 'female', value: Gender.Female},
    {label: 'male', value: Gender.Male}
  ];

  genderLabel = new Map<number, string>([
    [Gender.Female, 'female'],
    [Gender.Male, 'male']
  ]);

  selectedGender: Gender = null;
  searchQuery: string = null;
  isActive: boolean = null;

  playerPositionLabel = new Map<number, string>([
    [PlayerPosition.AttackingMidfielder, 'position_attackingMidfielder'],
    [PlayerPosition.CenterBack, 'position_center_back'],
    [PlayerPosition.DefensiveMiedfielder, 'position_defensiveMidfielder'],
    [PlayerPosition.Goalkeeper, 'position_goalkeeper'],
    [PlayerPosition.LeftBack, 'position_left_back'],
    [PlayerPosition.RightBack, 'position_right_back'],
    [PlayerPosition.Striker, 'position_striker']
  ]);

  boolFilterOptions = [
    {label: 'both', value: null},
    {label: 'yes', value: true},
    {label: 'no', value: false}
  ];

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private playerService: PlayerService,
    private countryService: CountryService,
    private teamService: TeamService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.createForm();

    var query = new PlayerQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      queryString : null,
      isActive : null,
      countryId: null,
      gender: null,
      teamId : null,
      isStillPlaying: null
    });

    forkJoin([
      this.teamService.getAllTeams().then(res => this.teams = res),
      this.countryService.getCountries().then(res => this.countries = res.sort((a, b) => (a.code > b.code) ? 1 : -1)),
      this.playerService.getPlayers(query).then(res => {
        this.players = res.items;
        this.totalCount = res.totalCount;
      })
    ])
    .subscribe(res => this.spinner.hide());
  }

  createForm(){
    this.form = this.formBuilder.group({
      'SearchQuery': [null, Validators.nullValidator],
      'Country': [null, Validators.nullValidator],
      'IsActive': [null, Validators.nullValidator],
      'Team': [null, Validators.nullValidator],
      'Gender': [null, Validators.nullValidator],
      'StillPlaying': [null, Validators.nullValidator]
    });
  }

  getPlayers(query: PlayerQuery){
    this.spinner.show();
    this.playerService.getPlayers(query)
    .then(res => {
      this.players = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    });
  }

  loadPlayers(event: LazyLoadEvent) {
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
    
    var query = this.getPlayersQuery();
    this.getPlayers(query);
  }

  filter(){
    if(this.form.controls['SearchQuery'].value != null && this.form.controls['SearchQuery'].value.length > 0){
      this.searchQuery = this.form.controls['SearchQuery'].value;
    }
    else{
      this.searchQuery = null;
    }

    if(this.form.controls['Team'].value != null){
      this.selectedTeam = this.form.controls['Team'].value;
    }
    else{
      this.selectedTeam = null;
    }

    if(this.form.controls['Country'].value != null){
      this.selectedCountry = this.form.controls['Country'].value;
    }
    else{
      this.selectedCountry = null;
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
    
    if(this.form.controls['StillPlaying'].value != null){
      this.isStillPlaying = this.form.controls['StillPlaying'].value;
    }
    else{
      this.isStillPlaying = null;
    }

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "PlayerName";

    var query = this.getPlayersQuery();
    this.getPlayers(query);
  }

  resetFilter() {
    this.searchQuery = null;
    this.isActive = null;
    this.selectedCountry = null;
    this.selectedTeam = null;
    this.selectedGender = null;
    this.isStillPlaying = null;

    this.form.controls['SearchQuery'].setValue(null);
    this.form.controls['Team'].setValue(null);
    this.form.controls['Country'].setValue(null);
    this.form.controls['IsActive'].setValue(null);
    this.form.controls['Gender'].setValue(null);
    this.form.controls['StillPlaying'].setValue(null);

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "PlayerName";

    let query = this.getPlayersQuery();
    this.getPlayers(query);
  }

  getPlayersQuery(){
    let countryId = this.selectedCountry ? this.selectedCountry.id : null;

    let teamId = this.selectedTeam  ? this.selectedTeam.id : null;
    return new PlayerQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      queryString : this.searchQuery,
      isActive : this.isActive,
      countryId: countryId,
      gender: this.selectedGender,
      teamId : teamId,
      isStillPlaying: this.isStillPlaying
    });
  }


}
