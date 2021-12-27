using System.Collections.Generic;
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
        public static Permission TrainingScores { get => new("Re", TrainingsPolicy, TrainingsGroup, "Rezultaty treningów"); }
        public const string TrainingScoresEditPolicy = "trainings_scores.edit";
        public static Permission TrainingScoresEdit { get => new("Edycja rezultatów treningów", TrainingsEditPolicy, TrainingsGroup, ""); }
        public const string TrainingScoresActivatePolicy = "trainings_scores.activate";
        public static Permission TrainingScoresActivate { get => new("Aktywacja rezultatów treningów", TrainingsActivatePolicy, TrainingsGroup, ""); }
        public const string TrainingScoresDeactivatePolicy = "trainings_scores.deactivate";
        public static Permission TrainingScoresDeactivate { get => new("Dezaktywacja rezultatów treningów", TrainingsDeactivatePolicy, TrainingsGroup, ""); }
        public const string TrainingScoresAddPolicy = "trainings_scores.add";
        public static Permission TrainingScoresAdd { get => new("Dodanie rezultatów treningów", TrainingsAddPolicy, TrainingsGroup, ""); }
        public const string TrainingScoresDeletePolicy = "trainings_scores.delete";
        public static Permission TrainingScoresDelete { get => new("Usuwanie rezultatów treningów", TrainingsDeletePolicy, TrainingsGroup, ""); }
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

        #region Surveys
        public const string SurveysGroup = "Ankiety";
        public const string SurveysPolicy = "surveys";
        public static Permission Surveys { get => new("Ankiety", SurveysPolicy, SurveysGroup, ""); }
        public const string SurveysEditPolicy = "surveys.edit";
        public static Permission SurveysEdit { get => new("Edycja ankiety", SurveysEditPolicy, SurveysGroup, ""); }
        public const string SurveysActivatePolicy = "surveys.activate";
        public static Permission SurveysActivate { get => new("Aktywacja ankiety", SurveysActivatePolicy, SurveysGroup, ""); }
        public const string SurveysDeactivatePolicy = "surveys.deactivate";
        public static Permission SurveysDeactivate { get => new("Dezaktywacja ankiety", SurveysDeactivatePolicy, SurveysGroup, ""); }
        public const string SurveysAddPolicy = "surveys.add";
        public static Permission SurveysAdd { get => new("Dodanie ankiety", SurveysAddPolicy, SurveysGroup, ""); }
        public const string SurveysDeletePolicy = "surveys.delete";
        public static Permission SurveysDelete { get => new("Usuwanie ankiety", SurveysDeletePolicy, SurveysGroup, ""); }
        #endregion

        #region Tests
        public const string TestsGroup = "Testy";
        public const string TestsPolicy = "tests";
        public static Permission Tests { get => new("Testy", TestsPolicy, TestsGroup, ""); }
        public const string TestsEditPolicy = "surveys.edit";
        public static Permission TestsEdit { get => new("Edycja testu", TestsEditPolicy, TestsGroup, ""); }
        public const string TestsActivatePolicy = "surveys.activate";
        public static Permission TestsActivate { get => new("Aktywacja testu", TestsActivatePolicy, TestsGroup, ""); }
        public const string TestsDeactivatePolicy = "surveys.deactivate";
        public static Permission TestsDeactivate { get => new("Dezaktywacja testu", TestsDeactivatePolicy, TestsGroup, ""); }
        public const string TestsAddPolicy = "surveys.add";
        public static Permission TestsAdd { get => new("Dodanie testu", TestsAddPolicy, TestsGroup, ""); }
        public const string TestsDeletePolicy = "surveys.delete";
        public static Permission TestsDelete { get => new("Usuwanie testu", TestsDeletePolicy, TestsGroup, ""); }
        #endregion

        #region Chats
        public const string ChatsGroup = "Konwersacje";
        public const string ChatsPolicy = "chats";
        public static Permission Chats { get => new("Konwersacje", ChatsPolicy, ChatsGroup, ""); }
        public const string ChatsEditPolicy = "chats.edit";
        public static Permission ChatsEdit { get => new("Edycja konwersacji", ChatsEditPolicy, ChatsGroup, ""); }
        public const string ChatsActivatePolicy = "chats.activate";
        public static Permission ChatsActivate { get => new("Aktywacja konwersacji", ChatsActivatePolicy, ChatsGroup, ""); }
        public const string ChatsDeactivatePolicy = "chats.deactivate";
        public static Permission ChatsDeactivate { get => new("Dezaktywacja konwersacji", ChatsDeactivatePolicy, ChatsGroup, ""); }
        public const string ChatsAddPolicy = "chats.add";
        public static Permission ChatsAdd { get => new("Dodanie testu", ChatsAddPolicy, ChatsGroup, ""); }
        public const string ChatsDeletePolicy = "chats.delete";
        public static Permission ChatsDelete { get => new("Usuwanie testu", ChatsDeletePolicy, ChatsGroup, ""); }
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
        public static Permission NewsDelete { get => new("Usuwanie aktualności", NewsAddPolicy, NewsGroup, ""); }
        public const string NewsActivatePolicy = "news.activate";
        public static Permission NewsActivate { get => new("Aktywacja aktualności", NewsActivatePolicy, NewsGroup, ""); }
        public const string NewsDeactivatePolicy = "news.deactivate";
        public static Permission NewsDeactivate { get => new("Dezaktywacja aktualności", NewsDeactivatePolicy, NewsGroup, ""); }

        #endregion

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
        public const string TeamsResultsPolicy = "teams.results";
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

                //Matches
                Matches,
                MatchesEdit,
                MatchesActivate,
                MatchesDeactivate,
                MatchesAdd,
                MatchesDelete,
                MatchesResults,

                //Surveys
                Surveys,
                SurveysEdit,
                SurveysActivate,
                SurveysDeactivate,
                SurveysAdd,
                SurveysDelete,

                //Tests
                Tests,
                TestsEdit,
                TestsActivate,
                TestsDeactivate,
                TestsAdd,
                TestsDelete,

                //Chats
                Chats,
                ChatsEdit,
                ChatsActivate,
                ChatsDeactivate,
                ChatsAdd,
                ChatsDelete,

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
