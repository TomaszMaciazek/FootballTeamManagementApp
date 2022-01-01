import { Injectable } from '@angular/core';
import { MenuItem } from '../models/menu-item.model';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  constructor() { }

  public getMenuItems() : Array<MenuItem>
  {
    let menuItems = new Array<MenuItem>();
    let news = this.getNews();
    let accountItem = this.getAccount();
    let calendar = this.getTeams();
    let members = this.getClubMembers();
    let results = this.getResults();
    let reports = this.getReports();
    let surveys = this.getSurveys();
    let tests = this.getTests();

    let admin = this.getAdmin();

    if(!news.isEmpty()){
      menuItems.push(news);
    }

    if(!accountItem.isEmpty()){
      menuItems.push(accountItem);
    }

    if(!calendar.isEmpty()){
      menuItems.push(calendar);
    }

    if(!members.isEmpty()){
      menuItems.push(members);
    }

    if(!results.isEmpty()){
      menuItems.push(results);
    }

    if(!reports.isEmpty()){
      menuItems.push(reports);
    }

    if(!tests.isEmpty()){
      menuItems.push(tests);
    }

    if(!surveys.isEmpty()){
      menuItems.push(surveys);
    }

    if(!admin.isEmpty()){
      menuItems.push(admin);
    }

    return menuItems;
  }

  private getNews() : MenuItem
  {
    let news = new MenuItem();

    news.Title = "news";
    news.FontAwesomeIcon = "far fa-newspaper";
    news.SubMenuItems = new Array<MenuItem>();

    let newsPreview = new MenuItem();

    newsPreview.Title = "preview";
    newsPreview.Link = "news/preview";
    newsPreview.RequiredPermissions = "news";

    news.SubMenuItems.push(newsPreview);

    return news;
  }

  private getAccount() : MenuItem
  {
    let account = new MenuItem();

    account.Title = "account";
    account.FontAwesomeIcon = "fas fa-user-circle";
    account.SubMenuItems = new Array<MenuItem>();

    let myAccountMenuItem = new MenuItem();

    myAccountMenuItem.Title = "my_account";
    myAccountMenuItem.Link = "account/myAccount";

    account.SubMenuItems.push(myAccountMenuItem);

    return account;
  }

  private getTeams() : MenuItem{
    let teams = new MenuItem();
    teams.Title = "teams";
    teams.FontAwesomeIcon = "fas fa-tshirt";
    teams.SubMenuItems = new Array<MenuItem>();

    let matches = new MenuItem();
    matches.Title = "preview";
    matches.Link = "teams/preview";
    matches.RequiredPermissions = "teams";


    teams.SubMenuItems.push(matches);

    return teams;
  }

  private getResults() : MenuItem{
    let results = new MenuItem;
    results.Title = "results";
    results.FontAwesomeIcon = "fas fa-trophy";
    results.SubMenuItems = new Array<MenuItem>();

    let matches = new MenuItem();
    matches.Title = "matches_results";
    matches.Link = "results/matches";
    matches.RequiredPermissions = "matches.results";

    let trainings = new MenuItem();
    trainings.Title = "trainings_results";
    trainings.Link = "results/trainings";
    trainings.RequiredPermissions = "trainings.results";

    results.SubMenuItems.push(matches, trainings);
    return results
  }

  private getSurveys() : MenuItem{
    let surveys = new MenuItem;
    surveys.Title = "surveys";
    surveys.FontAwesomeIcon = "fas fa-pencil-ruler"
    surveys.SubMenuItems = new Array<MenuItem>();

    let allSurveys = new MenuItem();
    allSurveys.Title = "all";
    allSurveys.Link = "surveys/all";
    allSurveys.RequiredPermissions = "surveys";

    let awaitingSurveys = new MenuItem();
    awaitingSurveys.Title = "awaiting";
    awaitingSurveys.Link = "surveys/awaiting";
    awaitingSurveys.RequiredPermissions = "surveys";

    let filledSurveys = new MenuItem();
    filledSurveys.Title = "filled";
    filledSurveys.Link = "surveys/filled";
    filledSurveys.RequiredPermissions = "surveys";

    surveys.SubMenuItems.push(allSurveys, awaitingSurveys, filledSurveys);
    return surveys
  }

  private getTests() : MenuItem{
    let tests = new MenuItem;
    tests.Title = "tests";
    tests.FontAwesomeIcon = "fas fa-marker"
    tests.SubMenuItems = new Array<MenuItem>();

    let allTests = new MenuItem();
    allTests.Title = "all";
    allTests.Link = "tests/all";
    allTests.RequiredPermissions = "tests";

    let awaitingTests = new MenuItem();
    awaitingTests.Title = "awaiting";
    awaitingTests.Link = "tests/awaiting";
    awaitingTests.RequiredPermissions = "tests";

    let finishedTests = new MenuItem();
    finishedTests.Title = "finished";
    finishedTests.Link = "tests/finished";
    finishedTests.RequiredPermissions = "tests";

    tests.SubMenuItems.push(allTests, awaitingTests, finishedTests);
    return tests
  }

  private getAdmin(): MenuItem{
    let admin = new MenuItem();
    admin.Title = "admin";
    admin.RequiredPermissions = "admin";
    admin.FontAwesomeIcon = "fas fa-cogs";
    admin.SubMenuItems = new Array<MenuItem>();

    let users = new MenuItem();
    users.Title = "users";
    users.Link = "admin/users";
    users.RequiredPermissions = "admin.users";

    admin.SubMenuItems.push(users);
    return admin;
  }

  private getClubMembers() : MenuItem{
    let members = new MenuItem();
    members.Title = "members";
    members.FontAwesomeIcon = "fas fa-users";
    members.SubMenuItems = new Array<MenuItem>();

    let players = new MenuItem();
    players.Title = "players";
    players.Link = "members/players";
    players.RequiredPermissions = "players";

    let coaches = new MenuItem();
    coaches.Title = "coaches";
    coaches.Link = "members/coaches";
    coaches.RequiredPermissions = "coaches";

    members.SubMenuItems.push(players, coaches);
    return members;
  }

  private getReports(): MenuItem{
    let reports = new MenuItem();
    reports.Title = "reports";
    reports.FontAwesomeIcon = "fas fa-poll";
    reports.SubMenuItems = new Array<MenuItem>();

    let playersCount = new MenuItem();
    playersCount.Title = "new_players";
    playersCount.Link = "reports/players/count";
    playersCount.RequiredPermissions = "reports.players";

    let coachesCount = new MenuItem();
    coachesCount.Title = "new_coaches";
    coachesCount.Link = "reports/coaches/count";
    coachesCount.RequiredPermissions = "reports.coaches";

    let receivedCards = new MenuItem();
    receivedCards.Title = "club_cards";
    receivedCards.Link = "reports/cards/all";
    receivedCards.RequiredPermissions = "reports.cards";

    let playersCards = new MenuItem();
    playersCards.Title = "players_cards";
    playersCards.Link = "reports/cards/players";
    playersCards.RequiredPermissions = "reports.cards";

    let coachesCards = new MenuItem();
    coachesCards.Title = "coaches_cards";
    coachesCards.Link = "reports/cards/coaches";
    coachesCards.RequiredPermissions = "reports.cards";

    let gainedPoints = new MenuItem();
    gainedPoints.Title = "match_points";
    gainedPoints.Link = "reports/points/all";
    gainedPoints.RequiredPermissions = "reports.match.points";

    let playersPoints = new MenuItem();
    playersPoints.Title = "players_match_points";
    playersPoints.Link = "reports/points/players";
    playersPoints.RequiredPermissions = "reports.match.points";

    let playersMatchesStatistisc = new MenuItem();
    playersMatchesStatistisc.Title = "player_matches_statistics";
    playersMatchesStatistisc.Link = "reports/playersMatches";
    playersMatchesStatistisc.RequiredPermissions = "reports.players_matches";

    let playersTrainingsStatistisc = new MenuItem();
    playersTrainingsStatistisc.Title = "player_trainings_statistics";
    playersTrainingsStatistisc.Link = "reports/playersMatches";
    playersTrainingsStatistisc.RequiredPermissions = "reports.players_matches";

    reports.SubMenuItems.push(playersCount, coachesCount, receivedCards, playersCards, coachesCards, gainedPoints, playersPoints, playersMatchesStatistisc, playersTrainingsStatistisc);
    return reports;
  }
}
