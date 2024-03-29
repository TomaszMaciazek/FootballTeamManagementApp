﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace App.UserMiddleware
{
    public static class Permissions
    {

        public const string ClaimType = "permission";
        public static ReadOnlyCollection<Permission> AllPermissions { get; set; }

        #region Administration
        public const string AdministrationGroup = "Administracja";
        public const string UsersPolicy = "administration.users";
        public static Permission Users { get => new("Użytkownicy", UsersPolicy, AdministrationGroup, ""); }
        public const string AdminSettingsPolicy = "administration.adminSettings";
        public static Permission AdminSettings { get => new("AdminSettings", AdminSettingsPolicy, AdministrationGroup, ""); }
        #endregion

        #region Players
        public const string PlayersGroup = "Zawodnicy";
        public const string PlayersPolicy = "players";
        public static Permission Players { get => new("Zawodnicy", PlayersPolicy, PlayersGroup, ""); }
        public const string PlayersTeamsEditPolicy = "players.teams.edit";
        public static Permission PlayersTeamsEdit { get => new("Edycja drużyn zawodników", PlayersTeamsEditPolicy, PlayersGroup, ""); }
        public const string PlayersHistoriesPolicy = "players.history";
        public static Permission PlayersHistories { get => new("Przeglądanie historii zawodników", PlayersHistoriesPolicy, PlayersGroup, ""); }
        #endregion

        #region Coaches
        public const string CoachesGroup = "Trenerzy";
        public const string CoachesPolicy = "coaches";
        public static Permission Coaches { get => new("Trenerzy", CoachesPolicy, CoachesGroup, ""); }
        public const string CoachesTeamsEditPolicy = "coaches.teams.edit";
        public static Permission CoachesTeamsEdit { get => new("Edycja drużyn trenerów", CoachesTeamsEditPolicy, CoachesGroup, ""); }
        #endregion

        #region Trainings
        public const string TrainingsGroup = "Treningi";
        public const string TrainingsPolicy = "trainings";
        public static Permission Trainings { get => new("Treningi", TrainingsPolicy, TrainingsGroup, ""); }
        public const string TrainingsEditPolicy = "trainings.edit";
        public static Permission TrainingsEdit { get => new("Edycja treningu", TrainingsEditPolicy, TrainingsGroup, ""); }
        public const string TrainingsActivatePolicy = "trainings.activate";
        public static Permission TrainingsActivate { get => new("Aktywacja treningu", TrainingsActivatePolicy, TrainingsGroup, "");}
        public const string TrainingsDeactivatePolicy = "trainings.deactivate";
        public static Permission TrainingsDeactivate { get => new("Dezaktywacja treningu", TrainingsDeactivatePolicy, TrainingsGroup, ""); }
        public const string TrainingsAddPolicy = "trainings.add";
        public static Permission TrainingsAdd { get => new("Dodanie treningu", TrainingsAddPolicy, TrainingsGroup, ""); }
        public const string TrainingsDeletePolicy = "trainings.delete";
        public static Permission TrainingsDelete { get => new("Usuwanie treningu", TrainingsDeletePolicy, TrainingsGroup, ""); }
        #endregion

        #region TrainingScores
        public const string TrainingScoresGroup = "Rezultaty treningow";
        public const string TrainingScoresPolicy = "trainings_scores";
        public static Permission TrainingScores { get => new("Rezultaty treningów", TrainingScoresPolicy, TrainingScoresGroup, "Rezultaty treningów"); }
        public const string TrainingScoresEditPolicy = "trainings_scores.edit";
        public static Permission TrainingScoresEdit { get => new("Edycja rezultatów treningów", TrainingScoresEditPolicy, TrainingScoresGroup, ""); }
        public const string TrainingScoresActivatePolicy = "trainings_scores.activate";
        public static Permission TrainingScoresActivate { get => new("Aktywacja rezultatów treningów", TrainingScoresActivatePolicy, TrainingScoresGroup, ""); }
        public const string TrainingScoresDeactivatePolicy = "trainings_scores.deactivate";
        public static Permission TrainingScoresDeactivate { get => new("Dezaktywacja rezultatów treningów", TrainingScoresDeactivatePolicy, TrainingScoresGroup, ""); }
        public const string TrainingScoresAddPolicy = "trainings_scores.add";
        public static Permission TrainingScoresAdd { get => new("Dodanie rezultatów treningów", TrainingScoresAddPolicy, TrainingScoresGroup, ""); }
        public const string TrainingScoresDeletePolicy = "trainings_scores.delete";
        public static Permission TrainingScoresDelete { get => new("Usuwanie rezultatów treningów", TrainingScoresDeletePolicy, TrainingScoresGroup, ""); }
        #endregion

        #region MatchScores
        public const string MatchScoresGroup = "Rezultaty meczów";
        public const string MatchScoresPolicy = "matches_scores";
        public static Permission MatchScores { get => new("Rezultaty meczów", MatchScoresPolicy, MatchScoresGroup, "Rezultaty meczów"); }
        public const string MatchScoresEditPolicy = "matches_scores.edit";
        public static Permission MatchScoresEdit { get => new("Edycja rezultatów meczów", MatchScoresEditPolicy, MatchScoresGroup, ""); }
        public const string MatchScoresActivatePolicy = "matches_scores.activate";
        public static Permission MatchScoresActivate { get => new("Aktywacja rezultatów meczów", MatchScoresActivatePolicy, MatchScoresGroup, ""); }
        public const string MatchScoresDeactivatePolicy = "matches_scores.deactivate";
        public static Permission MatchScoresDeactivate { get => new("Dezaktywacja rezultatów meczów", MatchScoresDeactivatePolicy, MatchScoresGroup, ""); }
        public const string MatchScoresAddPolicy = "matches_scores.add";
        public static Permission MatchScoresAdd { get => new("Dodanie rezultatów meczów", MatchScoresAddPolicy, MatchScoresGroup, ""); }
        public const string MatchScoresDeletePolicy = "matches_scores.delete";
        public static Permission MatchScoresDelete { get => new("Usuwanie rezultatów mecżów", MatchScoresDeletePolicy, MatchScoresGroup, ""); }
        #endregion


        #region Matches
        public const string MatchesGroup = "Mecze";
        public const string MatchesPolicy = "matches";
        public static Permission Matches { get => new("Mecze", MatchesPolicy, MatchesGroup, ""); }
        public const string MatchesEditPolicy = "matches.edit";
        public static Permission MatchesEdit { get => new("Edycja meczu", MatchesEditPolicy, MatchesGroup, ""); }
        public const string MatchesActivatePolicy = "matches.activate";
        public static Permission MatchesActivate { get => new("Aktywacja meczu", MatchesActivatePolicy, MatchesGroup, ""); }
        public const string MatchesDeactivatePolicy = "matches.deactivate";
        public static Permission MatchesDeactivate { get => new("Dezaktywacja meczu", MatchesDeactivatePolicy, MatchesGroup, ""); }
        public const string MatchesAddPolicy = "matches.add";
        public static Permission MatchesAdd { get => new("Dodanie meczu", MatchesAddPolicy, MatchesGroup, ""); }
        public const string MatchesDeletePolicy = "matches.delete";
        public static Permission MatchesDelete { get => new("Usuwanie meczu", MatchesDeletePolicy, MatchesGroup, ""); }
        public const string MatchesResultsPolicy = "matches.results";
        public static Permission MatchesResults { get => new("Wyniki meczow", MatchesResultsPolicy, MatchesGroup, ""); }
        #endregion

        #region Cards
        public const string CardsGroup = "Kartki";
        public const string CardsPolicy = "cards";
        public static Permission Cards { get => new("Kartki", CardsPolicy, CardsGroup, "Kartki"); }
        public const string CardsEditPolicy = "cards.edit";
        public static Permission CardsEdit { get => new("Edycja kartek", CardsEditPolicy, CardsGroup, ""); }
        public const string CardsActivatePolicy = "cards.activate";
        public static Permission CardsActivate { get => new("Aktywacja kartek", CardsActivatePolicy, CardsGroup, ""); }
        public const string CardsDeactivatePolicy = "cards.deactivate";
        public static Permission CardsDeactivate { get => new("Dezaktywacja kartek", CardsDeactivatePolicy, CardsGroup, ""); }
        public const string CardsAddPolicy = "cards.add";
        public static Permission CardsAdd { get => new("Dodanie kartek", CardsAddPolicy, CardsGroup, ""); }
        public const string CardsDeletePolicy = "cards.delete";
        public static Permission CardsDelete { get => new("Usuwanie kartek", CardsDeletePolicy, CardsGroup, ""); }
        #endregion

        #region Surveys
        public const string SurveysGroup = "Ankiety";
        public const string SurveysPolicy = "surveys";
        public static Permission Surveys { get => new("Ankiety", SurveysPolicy, SurveysGroup, ""); }
        public const string SurveysAllPolicy = "surveys.all";
        public static Permission SurveysAll { get => new("Ankiety wszystkie", SurveysAllPolicy, SurveysGroup, ""); }
        public const string SurveysCreatedByMePolicy = "surveys.created";
        public static Permission SurveysCreatedByMe { get => new("Ankiety autorskie", SurveysCreatedByMePolicy, SurveysGroup, ""); }
        public const string SurveysAssignedToMePolicy = "surveys.assigned";
        public static Permission SurveysAssignedToMe { get => new("Ankiety przypisane", SurveysAssignedToMePolicy, SurveysGroup, ""); }
        public const string SurveysEditPolicy = "surveys.edit";
        public static Permission SurveysEdit { get => new("Edycja ankiety", SurveysEditPolicy, SurveysGroup, ""); }
        public const string SurveysAddPolicy = "surveys.add";
        public static Permission SurveysAdd { get => new("Dodanie ankiety", SurveysAddPolicy, SurveysGroup, ""); }
        public const string SurveysDeletePolicy = "surveys.delete";
        public static Permission SurveysDelete { get => new("Usuwanie ankiety", SurveysDeletePolicy, SurveysGroup, ""); }
        #endregion

        #region Player Performances
        public const string MatchPlayerPerformancesGroup = "Udziały zawodników w meczach";
        public const string MatchPlayerPerformancesPolicy = "match_performances";
        public static Permission MatchPlayerPerformances { get => new("Udziały", MatchPlayerPerformancesPolicy, MatchPlayerPerformancesGroup, ""); }
        public const string MatchPlayerPerformancesEditPolicy = "match_performances.edit";
        public static Permission MatchPlayerPerformancesEdit { get => new("Edycja udziałów", MatchPlayerPerformancesEditPolicy, MatchPlayerPerformancesGroup, ""); }
        public const string MatchPlayerPerformancesAddPolicy = "match_performances.add";
        public static Permission MatchPlayerPerformancesAdd { get => new("Dodanie udziałów", MatchPlayerPerformancesAddPolicy, MatchPlayerPerformancesGroup, ""); }
        public const string MatchPlayerPerformancesDeletePolicy = "match_performances.delete";
        public static Permission MatchPlayerPerformancesDelete { get => new("Usuwanie udziałów", MatchPlayerPerformancesDeletePolicy, MatchPlayerPerformancesGroup, ""); }
        public const string MatchPlayerPerformancesActivatePolicy = "match_performances.activate";
        public static Permission MatchPlayerPerformancesActivate { get => new("Aktywacja udziałów", MatchPlayerPerformancesActivatePolicy, MatchPlayerPerformancesGroup, ""); }
        public const string MatchPlayerPerformancesDeactivatePolicy = "match_performances.deactivate";
        public static Permission MatchPlayerPerformancesDeactivate { get => new("Dezaktywacja udziałów", MatchPlayerPerformancesDeactivatePolicy, MatchPlayerPerformancesGroup, ""); }
        #endregion

        #region Match Points
        public const string MatchPointsGroup = "Zdobyte bramki";
        public const string MatchPointsPolicy = "match_performances";
        public static Permission MatchPoints { get => new("Zdobyte bramki", MatchPointsPolicy, MatchPointsGroup, ""); }
        public const string MatchPointsEditPolicy = "match_performances.edit";
        public static Permission MatchPointsEdit { get => new("Edycja bramek", MatchPointsEditPolicy, MatchPointsGroup, ""); }
        public const string MatchPointsAddPolicy = "match_performances.add";
        public static Permission MatchPointsAdd { get => new("Dodanie bramek", MatchPointsAddPolicy, MatchPointsGroup, ""); }
        public const string MatchPointsDeletePolicy = "match_performances.delete";
        public static Permission MatchPointsDelete { get => new("Usuwanie bramek", MatchPointsDeletePolicy, MatchPointsGroup, ""); }
        public const string MatchPointsActivatePolicy = "match_performances.activate";
        public static Permission MatchPointsActivate { get => new("Aktywacja bramek", MatchPointsActivatePolicy, MatchPointsGroup, ""); }
        public const string MatchPointsDeactivatePolicy = "match_performances.deactivate";
        public static Permission MatchPointsDeactivate { get => new("Dezaktywacja bramek", MatchPointsDeactivatePolicy, MatchPointsGroup, ""); }
        #endregion

        #region Reports
        public const string ReportsGroup = "Raporty";
        public const string ReportsPolicy = "reports";
        public static Permission Reports { get => new("Raporty", ReportsPolicy, ReportsGroup, ""); }
        public const string ReportsPlayersCountPolicy = "reports.players.count";
        public static Permission ReportsPlayersCount { get => new("Liczba nowych zawodników", ReportsPlayersCountPolicy, ReportsGroup, ""); }
        public const string ReportsCoachesCountPolicy = "reports.coaches.count";
        public static Permission ReportsCoachesCount { get => new("Liczba nowych trenerów", ReportsCoachesCountPolicy, ReportsGroup, ""); }
        public const string ReportsClubCardsPolicy = "reports.club.cards";
        public static Permission ReportsClubCards { get => new("Kartki klubu", ReportsClubCardsPolicy, ReportsGroup, ""); }
        public const string ReportsPlayersCardsPolicy = "reports.players.cards";
        public static Permission ReportsPlayersCards { get => new("Kartki zawodników", ReportsPlayersCardsPolicy, ReportsGroup, ""); }
        public const string ReportsCoachesCardsPolicy = "reports.coaches.cards";
        public static Permission ReportsCoachesCards { get => new("Kartki trenerów", ReportsCoachesCardsPolicy, ReportsGroup, ""); }
        public const string ReportsTeamsCardsPolicy = "reports.teams.cards";
        public static Permission ReportsTeamsCards { get => new("Kartki drużyn", ReportsTeamsCardsPolicy, ReportsGroup, ""); }
        public const string ReportsClubPointsPolicy = "reports.club.points";
        public static Permission ReportsClubPoints { get => new("Bramki klubu", ReportsClubPointsPolicy, ReportsGroup, ""); }
        public const string ReportsPlayersPointsPolicy = "reports.players.points";
        public static Permission ReportsPlayersPoints { get => new("Bramki zawodników", ReportsPlayersPointsPolicy, ReportsGroup, ""); }
        public const string ReportsTeamsPointsPolicy = "reports.teams.points";
        public static Permission ReportsTeamsPoints { get => new("Bramki drużyn", ReportsTeamsPointsPolicy, ReportsGroup, ""); }
        public const string ReportsClubTrainingScoresPolicy = "reports.club.training-scores";
        public static Permission ReportsClubTrainingScores { get => new("Klubowe wyniki treningów", ReportsClubTrainingScoresPolicy, ReportsGroup, ""); }
        public const string ReportsPlayersTrainingScoresPolicy = "reports.players.training-scores";
        public static Permission ReportsPlayersTrainingScores { get => new("Wyniki treningów poszczególnych zawodników", ReportsPlayersTrainingScoresPolicy, ReportsGroup, ""); }
        public const string ReportsClubMatchScoresPolicy = "reports.club.match-scores";
        public static Permission ReportsClubMatchScores { get => new("Klubowe wyniki treningów", ReportsClubMatchScoresPolicy, ReportsGroup, ""); }
        public const string ReportsPlayersMatchScoresPolicy = "reports.players.match-scores";
        public static Permission ReportsPlayersMatchScores { get => new("Wyniki treningów poszczególnych zawodników", ReportsPlayersMatchScoresPolicy, ReportsGroup, ""); }
        #endregion

        #region Tests
        public const string TestsGroup = "Testy";
        public const string TestsPolicy = "tests";
        public static Permission Tests { get => new("Testy", TestsPolicy, TestsGroup, ""); }
        public const string TestsAllPolicy = "tests.all";
        public static Permission TestsAll { get => new("Testy wszystkie", TestsAllPolicy, TestsGroup, ""); }
        public const string TestsCreatedByMePolicy = "tests.created";
        public static Permission TestsCreatedByMe { get => new("Testy autorskie", TestsCreatedByMePolicy, TestsGroup, ""); }
        public const string TestsAssignedToMePolicy = "tests.assigned";
        public static Permission TestsAssignedToMe { get => new("Testy przypisane", TestsAssignedToMePolicy, TestsGroup, ""); }
        public const string TestsEditPolicy = "tests.edit";
        public static Permission TestsEdit { get => new("Edycja testu", TestsEditPolicy, TestsGroup, ""); }
        public const string TestsActivatePolicy = "tests.activate";
        public static Permission TestsActivate { get => new("Aktywacja testu", TestsActivatePolicy, TestsGroup, ""); }
        public const string TestsDeactivatePolicy = "tests.deactivate";
        public static Permission TestsDeactivate { get => new("Dezaktywacja testu", TestsDeactivatePolicy, TestsGroup, ""); }
        public const string TestsAddPolicy = "tests.add";
        public static Permission TestsAdd { get => new("Dodanie testu", TestsAddPolicy, TestsGroup, ""); }
        public const string TestsDeletePolicy = "tests.delete";
        public static Permission TestsDelete { get => new("Usuwanie testu", TestsDeletePolicy, TestsGroup, ""); }
        #endregion

        #region Chats
        public const string MessagesGroup = "Wiadomości";
        public const string MessagesPolicy = "messages";
        public static Permission Messages { get => new("Wiadomości", MessagesPolicy, MessagesGroup, ""); }
        #endregion

        #region News
        public const string NewsGroup = "Aktualnosci";
        public const string NewsPolicy = "news";
        public static Permission News { get => new("Aktualnosci", NewsPolicy, NewsGroup, ""); }
        public const string NewsAddPolicy = "news.add";
        public static Permission NewsAdd { get => new("Dodawanie aktualności", NewsAddPolicy, NewsGroup, ""); }
        public const string NewsEditPolicy = "news.edit";
        public static Permission NewsEdit { get => new("Edycja aktualności", NewsEditPolicy, NewsGroup, ""); }
        public const string NewsDeletePolicy = "news.delete";
        public static Permission NewsDelete { get => new("Usuwanie aktualności", NewsDeletePolicy, NewsGroup, ""); }
        public const string NewsActivatePolicy = "news.activate";
        public static Permission NewsActivate { get => new("Aktywacja aktualności", NewsActivatePolicy, NewsGroup, ""); }
        public const string NewsDeactivatePolicy = "news.deactivate";
        public static Permission NewsDeactivate { get => new("Dezaktywacja aktualności", NewsDeactivatePolicy, NewsGroup, ""); }

        #endregion

        public const string RespondentsPolicy = "users.respondents";
        public static Permission GetRespondents { get => new("Respondenci", RespondentsPolicy, SurveysGroup, ""); }

        #region Teams
        public const string TeamsGroup = "Drużyny";
        public const string TeamsPolicy = "teams";
        public static Permission Teams { get => new("Drużyny", TeamsPolicy, TeamsGroup, ""); }
        public const string TeamsEditPolicy = "teams.edit";
        public static Permission TeamsEdit { get => new("Edycja drużyny", TeamsEditPolicy, TeamsGroup, ""); }
        public const string TeamsActivatePolicy = "teams.activate";
        public static Permission TeamsActivate { get => new("Aktywacja drużyny", TeamsActivatePolicy, TeamsGroup, ""); }
        public const string TeamsDeactivatePolicy = "teams.deactivate";
        public static Permission TeamsDeactivate { get => new("Dezaktywacja drużyny", TeamsDeactivatePolicy, TeamsGroup, ""); }
        public const string TeamsAddPolicy = "teams.add";
        public static Permission TeamsAdd { get => new("Dodanie drużyny", TeamsAddPolicy, TeamsGroup, ""); }
        public const string TeamsDeletePolicy = "teams.delete";
        public static Permission TeamsDelete { get => new("Usuwanie drużyny", TeamsDeletePolicy, TeamsGroup, ""); }
        public const string TeamsHistoriesPolicy = "teams.history";
        public static Permission TeamsHistories { get => new("Historia drużyny", TeamsHistoriesPolicy, TeamsGroup, ""); }
        #endregion

        static Permissions()
        {
            AllPermissions = new List<Permission>
            {
                // Administration
                Users,
                AdminSettings,

                // Players
                Players,
                PlayersTeamsEdit,
                PlayersHistories,

                //Coaches
                Coaches,
                CoachesTeamsEdit,

                //Trainings
                Trainings,
                TrainingsEdit,
                TrainingsActivate,
                TrainingsDeactivate,
                TrainingsAdd,
                TrainingsDelete,

                //TrainingScores
                TrainingScores,
                TrainingScoresActivate,
                TrainingScoresAdd,
                TrainingScoresDeactivate,
                TrainingScoresEdit,
                TrainingScoresDelete,

                //MatchScores
                MatchScores,
                MatchScoresActivate,
                MatchScoresAdd,
                MatchScoresDeactivate,
                MatchScoresEdit,
                MatchScoresDelete,

                //Matches
                Matches,
                MatchesEdit,
                MatchesActivate,
                MatchesDeactivate,
                MatchesAdd,
                MatchesDelete,
                MatchesResults,

                //Reports
                Reports,
                ReportsPlayersCount,
                ReportsCoachesCount,
                ReportsClubCards,
                ReportsPlayersCards,
                ReportsCoachesCards,
                ReportsClubPoints,
                ReportsPlayersPoints,
                ReportsClubTrainingScores,
                ReportsPlayersTrainingScores,
                ReportsClubMatchScores,
                ReportsPlayersMatchScores,

                //Surveys
                Surveys,
                SurveysAll,
                GetRespondents,
                SurveysEdit,
                SurveysCreatedByMe,
                SurveysAssignedToMe,
                SurveysAdd,
                SurveysDelete,

                //Tests
                Tests,
                TestsAll,
                TestsCreatedByMe,
                TestsAssignedToMe,
                TestsEdit,
                TestsActivate,
                TestsDeactivate,
                TestsAdd,
                TestsDelete,

                //Chats
                Messages,

                //News
                News,
                NewsAdd,
                NewsActivate,
                NewsDeactivate,
                NewsEdit,
                NewsDelete,

                //Teams
                Teams,
                TeamsEdit,
                TeamsActivate,
                TeamsDeactivate,
                TeamsAdd,
                TeamsDelete,
                TeamsHistories,

                //Cards
                Cards,
                CardsEdit,
                CardsActivate,
                CardsDeactivate,
                CardsAdd,
                CardsDelete,

                //Match Player Performances
                MatchPlayerPerformances,
                MatchPlayerPerformancesEdit,
                MatchPlayerPerformancesActivate,
                MatchPlayerPerformancesDeactivate,
                MatchPlayerPerformancesAdd,
                MatchPlayerPerformancesDelete,

                //Match Points
                MatchPoints,
                MatchPointsEdit,
                MatchPointsActivate,
                MatchPointsDeactivate,
                MatchPointsAdd,
                MatchPointsDelete

            }.AsReadOnly();
        }

        public static Permission GetPermissionByName(string permissionName)
        {
            return AllPermissions.Where(p => p.Name == permissionName).FirstOrDefault();
        }
        public static Permission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.Where(p => p.Value == permissionValue).FirstOrDefault();
        }
        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }
    }

    public class Permission
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public Permission()
        { }
        public Permission(string name, string value, string groupName, string description = null)
        {
            Name = name;
            Value = value;
            GroupName = groupName;
            Description = description;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Permission permission)
        {
            return permission.Value;
        }
    }
}
