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
    let messages = this.getMessages();
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

    if(!messages.isEmpty()){
      menuItems.push(messages);
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
    myAccountMenuItem.RequiredPermissions = null;
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
    matches.RequiredPermissions = "matches";

    let trainings = new MenuItem();
    trainings.Title = "trainings_results";
    trainings.Link = "results/trainings";
    trainings.RequiredPermissions = "trainings";

    results.SubMenuItems.push(matches, trainings);
    return results
  }

  private getSurveys() : MenuItem{
    let surveys = new MenuItem;
    surveys.Title = "surveys";
    surveys.FontAwesomeIcon = "fas fa-pencil-ruler"
    surveys.SubMenuItems = new Array<MenuItem>();

    let newSurvey = new MenuItem();
    newSurvey.Title = "create_survey";
    newSurvey.Link = "surveys/create";
    newSurvey.RequiredPermissions = "surveys.add";

    let allSurveys = new MenuItem();
    allSurveys.Title = "all";
    allSurveys.Link = "surveys/all";
    allSurveys.RequiredPermissions = "surveys.all";

    let awaitingSurveys = new MenuItem();
    awaitingSurveys.Title = "created_by_myself";
    awaitingSurveys.Link = "surveys/created";
    awaitingSurveys.RequiredPermissions = "surveys";

    let filledSurveys = new MenuItem();
    filledSurveys.Title = "surveys_assigned_to_me";
    filledSurveys.Link = "surveys/assigned";
    filledSurveys.RequiredPermissions = "surveys";

    surveys.SubMenuItems.push(newSurvey, allSurveys, awaitingSurveys, filledSurveys);
    return surveys
  }

  private getTests() : MenuItem{
    let tests = new MenuItem;
    tests.Title = "tests";
    tests.FontAwesomeIcon = "fas fa-marker"
    tests.RequiredPermissions = "tests.assigned";
    tests.SubMenuItems = new Array<MenuItem>();

    let newTest = new MenuItem();
    newTest.Title = "create_test";
    newTest.Link = "tests/create";
    newTest.RequiredPermissions = "tests.add";

    let allTests = new MenuItem();
    allTests.Title = "all";
    allTests.Link = "tests/all";
    allTests.RequiredPermissions = "tests.all";

    let createdByMeTests = new MenuItem();
    createdByMeTests.Title = "created_by_myself_tests";
    createdByMeTests.Link = "tests/created";
    createdByMeTests.RequiredPermissions = "tests.created";

    let finishedTests = new MenuItem();
    finishedTests.Title = "tests_assigned_to_me";
    finishedTests.Link = "tests/assigned";
    finishedTests.RequiredPermissions = "tests.assigned";

    tests.SubMenuItems.push(newTest, allTests, createdByMeTests, finishedTests);
    return tests
  }

  private getAdmin(): MenuItem{
    let admin = new MenuItem();
    admin.Title = "admin";
    admin.RequiredPermissions = "administration.adminSettings";
    admin.FontAwesomeIcon = "fas fa-cogs";
    admin.SubMenuItems = new Array<MenuItem>();

    let users = new MenuItem();
    users.Title = "users";
    users.Link = "admin/users";
    users.RequiredPermissions = "administration.users";

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
    reports.RequiredPermissions = "reports";
    reports.SubMenuItems = new Array<MenuItem>();

    let playersCount = new MenuItem();
    playersCount.Title = "new_players";
    playersCount.Link = "reports/players/count";
    playersCount.RequiredPermissions = "reports.players.count";

    let coachesCount = new MenuItem();
    coachesCount.Title = "new_coaches";
    coachesCount.Link = "reports/coaches/count";
    coachesCount.RequiredPermissions = "reports.coaches.count";

    let receivedCards = new MenuItem();
    receivedCards.Title = "club_cards";
    receivedCards.Link = "reports/cards/all";
    receivedCards.RequiredPermissions = "reports.club.cards";

    let playersCards = new MenuItem();
    playersCards.Title = "players_cards";
    playersCards.Link = "reports/cards/players";
    playersCards.RequiredPermissions = "reports.players.cards";

    let coachesCards = new MenuItem();
    coachesCards.Title = "coaches_cards";
    coachesCards.Link = "reports/cards/coaches";
    coachesCards.RequiredPermissions = "reports.coaches.cards";

    let teamsCards = new MenuItem();
    teamsCards.Title = "teams_cards";
    teamsCards.Link = "reports/cards/teams";
    teamsCards.RequiredPermissions = "reports.teams.cards";

    let gainedPoints = new MenuItem();
    gainedPoints.Title = "match_points";
    gainedPoints.Link = "reports/points/all";
    gainedPoints.RequiredPermissions = "reports.club.points";

    let playersPoints = new MenuItem();
    playersPoints.Title = "players_points";
    playersPoints.Link = "reports/points/players";
    playersPoints.RequiredPermissions = "reports.players.points";

    let teamsPoints = new MenuItem();
    teamsPoints.Title = "teams_points";
    teamsPoints.Link = "reports/points/teams";
    teamsPoints.RequiredPermissions = "reports.teams.points";

    let clubTrainingsStatistisc = new MenuItem();
    clubTrainingsStatistisc.Title = "club_training_scores";
    clubTrainingsStatistisc.Link = "reports/trainingScores/all";
    clubTrainingsStatistisc.RequiredPermissions = "reports.club.training-scores";

    let clubMatchesStatistisc = new MenuItem();
    clubMatchesStatistisc.Title = "club_match_scores";
    clubMatchesStatistisc.Link = "reports/matchScores/all";
    clubMatchesStatistisc.RequiredPermissions = "reports.club.match-scores";

    let playersMatchesStatistisc = new MenuItem();
    playersMatchesStatistisc.Title = "player_matches_statistics";
    playersMatchesStatistisc.Link = "reports/matchScores/players";
    playersMatchesStatistisc.RequiredPermissions = "reports.players.match-scores";

    let playersTrainingsStatistisc = new MenuItem();
    playersTrainingsStatistisc.Title = "player_trainings_statistics";
    playersTrainingsStatistisc.Link = "reports/trainingScores/players";
    playersTrainingsStatistisc.RequiredPermissions = "reports.players.training-scores";

    let playersTrainingsRanking = new MenuItem();
    playersTrainingsRanking.Title = "training_scores_ranking";
    playersTrainingsRanking.Link = "reports/trainingScores/ranking";
    playersTrainingsRanking.RequiredPermissions = null;

    let playersMatchesRanking = new MenuItem();
    playersMatchesRanking.Title = "match_scores_ranking";
    playersMatchesRanking.Link = "reports/matchScores/ranking";
    playersMatchesRanking.RequiredPermissions = null;

    reports.SubMenuItems.push(playersCount, coachesCount, receivedCards, playersCards, coachesCards, teamsCards, gainedPoints, playersPoints, teamsPoints, clubTrainingsStatistisc, clubMatchesStatistisc, playersMatchesStatistisc, playersTrainingsStatistisc, playersTrainingsRanking, playersMatchesRanking);
    return reports;
  }

  getMessages() : MenuItem{
    let messages = new MenuItem();
    messages.Title = "messages";
    messages.FontAwesomeIcon = "fas fa-comments";
    messages.RequiredPermissions = "messages";
    messages.SubMenuItems = new Array<MenuItem>();

    let box = new MenuItem();
    box.Title = "my_box";
    box.Link = "messages/box";
    box.RequiredPermissions = "messages";

    messages.SubMenuItems.push(box);
    return messages;
  }
}
