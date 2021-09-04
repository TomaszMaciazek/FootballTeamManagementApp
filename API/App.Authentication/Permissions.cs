using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace App.Authentication
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

        static Permissions()
        {
            AllPermissions = new List<Permission>
            {
                // Administration
                Users,
                AdminSettings,

                //Trainings
                Trainings,
                TrainingsEdit,
                TrainingsActivate,
                TrainingsDeactivate,
                TrainingsAdd,
                TrainingsDelete,

                //Matches
                Matches,
                MatchesEdit,
                MatchesActivate,
                MatchesDeactivate,
                MatchesAdd,
                MatchesDelete,

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
                ChatsDelete

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
    }
}
