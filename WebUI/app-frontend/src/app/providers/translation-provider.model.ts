import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { PrimeNGConfig } from "primeng/api";
import { LanguageCode } from "../enums/language-code";

@Injectable()
export class TranslationProvider {

    private static langId: string = "pl";
    public static get LangId(): string { return this.langId; }

    constructor(private translate: TranslateService, private config: PrimeNGConfig){}

    async setDefaultLanguage(langId: LanguageCode) {        
        this.translateLangs.forEach(element => {
            this.translate.setTranslation(element.lang, element.data);
        });
        this.translate.use(langId);
        var primengTranslationsData = this.primengTranslations.find(x => x.lang === this.translate.currentLang).data;
        this.config.setTranslation(primengTranslationsData);
    }

    async setLanguage(langId: LanguageCode) {
        this.translate.use(langId);
        var primengTranslationsData = this.primengTranslations.find(x => x.lang === this.translate.currentLang).data;
        this.config.setTranslation(primengTranslationsData);
    }

    getTranslation(key: string): any {
        let currentLangTransalations: any = this.translate.translations[this.translate.currentLang];
        if (!currentLangTransalations) {
            let msg: string = "Translations not loaded";
            console.warn(msg);
            return;
        }
        let translation: string = currentLangTransalations[key];
        if (translation === undefined || translation === null || translation === "") {
            return key;
        }
        return translation;
    }

    getCurrentLangTransalations() : any{
        let currentLangTransalations: any = this.translate.translations[this.translate.currentLang];
        if (!currentLangTransalations) {
            let msg: string = "Translations not loaded";
            return;
        }
        return currentLangTransalations;
    }

    translateLangs: any = [
        {
            "lang": "pl",
            "data": {
                "account": "Konto",
                "account_is_locked": "Konto zostało tymczasowo zablokowane",
                "account_not_found": "Konto o danym loginie nie zostało znalezione",
                "account_remain_password_attempts": "Ilość pozostałych prób przed blokadą: ",
                "account_last_logon": "Ostatnie logowanie",
                "active": "Aktywny",
                "active_team": "Aktywna",
                "activate": "Aktywuj",
                "actions": "Akcje",
                "add": "Dodaj",
                "add_new_training_score": "Dodaj nowy rezultat",
                "add_player_to_team" : "Dodaj zawodnika do drużyny",
                "admin": "Administrator",
                "administrators": "Administratorzy",
                "admin_add": "Dodawanie administratora",
                "admin_new": "Nowy administrator",
                "agility": "Zwinność",
                "all" : "Wszystkie",
                "answer": "Odpowiedź",
                "answers": "Odpowiedzi",
                "any": "Dowolna",
                "apr": "Kwi",
                "april": "Kwiecień",
                "attitude": "Nastawienie",
                "aug": "Sie",
                "august": "Sierpień",
                "author": "Autor",
                "awaiting": "Oczekujące",
                "awareness": "Świadomość",
                "ball_control": "Kontrola piłki",
                "both": "Oba",
                "browse": "Przeglądaj",
                "card": "Kartka",
                "cards": "Kartki",
                "cards_addition": "Dodawanie kartek",
                "cards_red": "Czerwone kartki",
                "cards_yellow": "Żółte kartki",
                "choose_columns": "Wybierz kolumny",
                "clear": "Wyczyść",
                "coach": "Trener",
                "coaches": "Trenerzy",
                "coach_add": "Dodaj nowego trenera",
                "coach_new": "Nowy trener",
                "coach_teams_count": "Przypisane drużyny",
                "columns_selected": "wybranych kolumn",
                "communication": "Komunikacja",
                "confirm_activate_team": "Czy na pewno chcesz aktywować tę drużynę ?",
                "confirm_activate_training": "Czy na pewno chcesz aktywować ten trening ?",
                "confirm_activate_training_score": "Czy na pewno chcesz aktywować ten rezultat ?",
                "confirm_deactivate_team": "Czy na pewno chcesz dezaktywować tę drużynę ?",
                "confirm_deactivate_training": "Czy na pewno chcesz dezaktywować ten trening ?",
                "confirm_delete_team": "Czy na pewno chcesz usunąć tę drużynę ?",
                "confirm_delete_training": "Czy na pewno chcesz usunąć ten trening ?",
                "confirm_delete_training_score": "Czy na pewno chcesz usunąć ten rezultat ?",
                "confirm_deactivate_training_score": "Czy na pewno chcesz dezaktywować ten rezultat ?",
                "confirm_remove_player_from_team": "Czy na pewno chcesz usunąć tego zawodnika z drużyny ?",
                "content": "Treść",
                "concentration": "Koncentracja na zadaniach",
                "cooperation": "Współpraca",
                "coordination": "Koordynacja",
                "country": "Kraj",
                "countries": "Kraje",
                "creativity": "Kreatywność",
                "deactivate": "Dezaktywuj",
                "date": "Data",
                "date_of_birth": "Data urodzenia",
                "dec": "Gr",
                "december": "Grudzień",
                "decisiveness": "Decyzyjność",
                "delete": "Usuń",
                "description": "Opis",
                "details": "Szczegóły",
                "determination": "Determinacja",
                "discipline": "Dyscyplina",
                "edit": "Edytuj",
                "email": "Email",
                "emotions_control": "Kontrola emocji",
                "endurance": "Wytrzymałość",
                "engagement": "Zaangażowanie",
                "existing_scores": "Aktualne wyniki",
                "filled": "Wypełnione",
                "filter": "Filtruj",
                "finished": "Zakończone",
                "finished_playing": "Zakończył grę",
                "finished_working": "Zakończył pracę",
                "feb": "Lut",
                "february": "Luty",
                "female": "K",
                "gender": "Płeć",
                "gender_both": "Obie",
                "heading_accuracy": "Skuteczność zagrań głową",
                "image_change": "Zmień obraz",
                "incoming_events": "Kalendarz",
                "important": "Ważne",
                "is_still_playing": "Nadal gra",
                "is_still_working": "Nadal pracuje",
                "jan": "Sty",
                "january": "Styczeń",
                "jul": "Lip",
                "july": "Lipiec",
                "jun": "Cz",
                "june": "Czerwiec",
                "last_modification": "Ostatnia modyfikacja",
                "left_foot_ball_receiving_accuracy": "Skuteczność przyjęcia piłki lewą nogą",
                "left_foot_dribbling_accuracy": "Skuteczność prowadzenia piłki lewą nogą",
                "left_foot_pass_accuracy": "Skuteczność podań lewą nogą",
                "left_foot_shots_accuracy": "Skuteczność strzałów lewą nogą",
                "login": "Login",
                "logout": "Wyloguj się",
                "male": "M",
                "mar": "Mrz",
                "march": "Marzec",
                "match": "Mecz",
                "matches": "Mecze",
                "match_add": "Dodaj nowy mecz",
                "match_details": "Szczegóły meczu",
                "match_new": "Nowy mecz",
                "matches_results": "Wyniki meczów",
                "may": "Maj",
                "members": "Członkowie klubu",
                "message": "Wiadomość",
                "messages": "Wiadomości",
                "message_add": "Dodaj wiadomość",
                "message_edit": "Edycja wiadomości",
                "message_new": "Nowa wiadomość",
                "middlename": "Drugie imię",
                "mobility": "Mobilność",
                "my_account": "Moje konto",
                "name": "Imię",
                "name_team": "Nazwa",
                "news": "Aktualności",
                "no": "Nie",
                "nov": "Lis",
                "november": "Listopad",
                "no_scores": "Brak wyników",
                "no_players": "Brak zawodników przypisanych do tej drużyny",
                "oct": "Paź",
                "october": "Październik",
                "one_vs_one": "Skuteczność gry 1 vs 1",
                "opponents_team": "Drużyna przeciwników",
                "optional": "opcjonalne",
                "owners": "Właściciele",
                "password": "Hasło",
                "player": "Zawodnik",
                "players": "Zawodnicy",
                "player_add": "Dodaj nowego zawodnika",
                "players_addition": "Dodawanie zawodników",
                "players_count": "Liczba zawodników",
                "player_new": "Nowy zawodnik",
                "player_details": "Szczegóły zawodnika",
                "player_removed_from_team": "Zawodnik został usunięty z drużyny",
                "player_scores": "Rezultaty zawodnika",
                "player_without_team": "brak przypsanej drużyny",
                "players_scores": "Rezultaty zawodników",
                "points": "Zdobyte punkty",
                "points_addition": "Dodawanie zdobytych punktów",
                "position_goalkeeper": "Bramkarz",
                "position_rightBack": "Prawy obrońca",
                "position_leftBack": "Lewy obrońca",
                "position_centerBack": "Środkowy obrońca",
                "position_attackingMidfielder": "Pomocnik ofensywny",
                "position_defensiveMiedfielder": "Pomocnik defensywny",
                "position_striker" : "Napastnik",
                "preferred_position": "Preferowana pozycja",
                "preview" : "Przegląd",
                "question": "Pytanie",
                "questions": "Pytania",
                "report": "Raport",
                "reports": "Raporty",
                "right_foot_ball_receiving_accuracy": "Skuteczność przyjęcia piłki prawą nogą",
                "right_foot_dribbling_accuracy": "Skuteczność prowadzenia piłki prawą nogą",
                "right_foot_pass_accuracy": "Skuteczność podań prawą nogą",
                "right_foot_shots_accuracy": "Skuteczność strzałów prawą nogą",
                "remove_player_from_team": "Usuwanie zawodnika z drużyny",
                "result": "Wynik",
                "results": "Wyniki",
                "role": "Rola",
                "save": "Zapisz",
                "score_type": "Typ wyniku",
                "select_country": "Wybierz kraj",
                "select_countries": "Wybierz kraje",
                "select_coach": "Wybierz trenera",
                "select_gender": "Wybierz płeć",
                "select_language": "Wybierz język",
                "select_player": "Wybierz zawodnika",
                "select_preffered_position": "Wybierz preferowaną pozycję",
                "select_role": "Wybierz rolę",
                "select_team": "Wybierz drużynę",
                "select_teams": "Wybierz drużyny",
                "select_training_score_type": "Wybierz typ wyniku",
                "selfconfidence": "Pewność siebie",
                "sep": "Wrz",
                "september": "Wrzesień",
                "sign_in": "Zaloguj się",
                "something_went_wrong" : "Operacja zakończona niepowodzeniem",
                "started_playing": "Gra od",
                "started_working": "Pracuje od",
                "strength": "Siła",
                "stress_control": "Radzenie sobie ze stresem",
                "success": "Operacja zakończona powodzeniem",
                "surname": "Nazwisko",
                "survey": "Ankieta",
                "surveys": "Ankiety",
                "survey_add": "Dodawanie ankiety",
                "survey_new": "Nowa ankieta",
                "team": "Drużyna",
                "teams": "Drużyny",
                "team_activated": "Drużyna została aktywowana",
                "team_add_new": "Dodawanie drużyny",
                "team_deactivated": "Drużyna została dezaktywowana",
                "team_deleted": "Drużyna została usunięta",
                "team_has_no_main_coach": "Drużyna nie ma przypisanego głównego trenera",
                "team_history": "Historia drużyny",
                "team_history_is_not_empty": "Historia drużyny nie jest pusta", 
                "team_main_coach": "Główny trener drużyny",
                "team_players": "Zawodnicy z drużyny",
                "test": "Test",
                "tests": "Testy",
                "test_add": "Dodawanie testu",
                "test_new": "Nowy test",
                "time_to_unlock": "Pozostały czas blokady wynosi: ",
                "title": "Tytuł",
                "training": "Trening",
                "trainings": "Treningi",
                "training_activated": "Trening został aktywowany",
                "training_add": "Dodaj nowy trening",
                "training_new": "Nowy trening",
                "training_deactivated": "Trening został dezaktywowany",
                "training_deleted": "Trening został usunięty",
                "training_description": "Opis treningu",
                "training_details": "Szczegóły treningu",
                "trainings_results": "Wyniki treningów",
                "training_score_activated": "Rezultat został aktywowany",
                "training_score_add_new": "Dodawanie nowego rezultatu",
                "training_score_already_exists": "Rezultat dla danego typu i zawodnika już istnieje",
                "training_score_deactivated": "Rezultat został dezaktywowany",
                "training_score_deleted": "Rezultat został usunięty",
                "training_score_edit_value": "Edycja wartości rezultatu",
                "user": "Użytkownik",
                "users": "Użytkownicy",
                "user_add": "Dodawanie nowego użytkownika",
                "user_edit": "Edycja użytkownika",
                "user_details": "Szczegóły konta użytkownika",
                "value": "Wartość",
                "wrong_password": "Hasło nieprawidłowe",
                "year_of_birth": "Rocznik",
                "yes": "Tak"
            }

        },
        {
            "lang": "en",
            "data": {
                "account": "Account",
                "account_is_locked": "This account is currently locked",
                "account_not_found": "Account with given login was not found",
                "account_remain_password_attempts": "Number of remain attempts: ",
                "account_last_logon": "Last logon",
                "active": "Active",
                "active_team": "Active",
                "activate": "Activate",
                "actions": "Actions",
                "add": "Add",
                "add_new_training_score": "Add new training score",
                "add_player_to_team" : "Add player to team",
                "admin": "Administrator",
                "administrators": "Administrators",
                "admin_add": "Create administrator",
                "admin_new": "New administrator",
                "agility": "Agility",
                "all" : "All",
                "answer": "Answer",
                "answers": "Answers",
                "any": "Any",
                "apr": "Apr",
                "april": "April",
                "attitude": "Attitude",
                "aug": "Aug",
                "august": "August",
                "author": "Author",
                "awaiting" : "Awaiting",
                "awareness": "Awareness",
                "ball_control": "Ball control",
                "both": "Both",
                "browse": "Browse",
                "card": "Card",
                "cards": "Cards",
                "cards_addition": "Cards addition",
                "cards_red": "Red cards",
                "cards_yellow": "Yellow cards",
                "choose_columns": "Choose columns",
                "clear": "Clear",
                "coach": "Coach",
                "coaches": "Coaches",
                "coach_add": "Add new coach",
                "coach_new": "New coach",
                "coach_teams_count": "Assigned teams",
                "columns_selected": "columns selected",
                "communication": "Communication",
                "confirm_activate_team": "Are you sure you want to activate this team ?",
                "confirm_activate_training": "Are you sure you want to activate this training ?",
                "confirm_activate_training_score": "Are you sure you want to activate this training score ?",
                "confirm_deactivate_team": "Are you sure you want to deactivate this team ?",
                "confirm_deactivate_training": "Are you sure you want to deactivate this training ?",
                "confirm_delete_team": "Are you sure you want to delete this team ?",
                "confirm_delete_training": "Are you sure you want to delete this training ?",
                "confirm_delete_training_score": "Are you sure you want to delete this training score ?",
                "confirm_deactivate_training_score": "Are you sure you want to deactivate this training score ?",
                "confirm_remove_player_from_team": "Are you sure you want to remove this player from the team ?",
                "content": "Content",
                "concentration": "Concentration",
                "cooperation": "Cooperation",
                "coordination": "Coordination",
                "country": "Country",
                "countries": "Countries",
                "creativity": "Creativity",
                "date": "Date",
                "date_of_birth": "Date of birth",
                "deactivate": "Deactivate",
                "dec": "Dec",
                "december": "December",
                "decisiveness": "Decisiveness",
                "delete": "Delete",
                "description": "Description",
                "details": "Details",
                "determination": "Determination",
                "discipline": "Discipline",
                "edit": "Edit",
                "email": "Email",
                "emotions_control": "Emotions control",
                "endurance": "Endurance",
                "engagement": "Engagement",
                "existing_scores": "Existing scores",
                "filled": "Filled",
                "filter": "Filter",
                "finished": "Finished",
                "finished_playing": "Finished playing",
                "finished_working": "Finished working",
                "feb": "Feb",
                "february": "February",
                "female": "F",
                "gender": "Gender",
                "gender_both": "Both",
                "heading_accuracy": "Heading accuracy",
                "image_change": "Change image",
                "incoming_events": "Calendar",
                "important": "Important",
                "is_still_playing": "Still playing",
                "is_still_working": "Still working",
                "jan": "Jan",
                "january": "January",
                "jul": "Jul",
                "july": "July",
                "jun": "Jun",
                "june": "June",
                "last_modification": "Last modification",
                "left_foot_ball_receiving_accuracy": "Left foot ball receiving accuracy",
                "left_foot_dribbling_accuracy": "Left foot dribbling accuracy",
                "left_foot_pass_accuracy": "Left foot pass accuracy",
                "left_foot_shots_accuracy": "Left foot shots accuracy",
                "login": "Login",
                "logout": "Log out",
                "male": "M",
                "mar": "Mar",
                "march": "March",
                "match": "Match",
                "matches": "Matches",
                "match_add": "Add new match",
                "match_details": "Match details",
                "match_new": "New match",
                "matches_results": "Matches results",
                "may": "May",
                "members": "Club members",
                "message": "Message",
                "messages": "Messages",
                "message_add": "Add new message",
                "message_edit": "Edit message",
                "message_new": "New message",
                "middlename": "Middlename",
                "mobility": "Mobility",
                "my_account": "My account",
                "name": "Name",
                "name_team": "Name",
                "news": "News",
                "no": "No",
                "nov": "nov",
                "november": "November",
                "no_scores": "No scores",
                "no_players": "This team has no players assigned",
                "oct": "Oct",
                "october": "October",
                "one_vs_one": "One vs one performance",
                "opponents_team": "Opponents team",
                "optional": "optional",
                "owners": "Owners",
                "password": "Password",
                "player": "Player",
                "players": "Players",
                "player_add": "Add new player",
                "players_addition": "Add players",
                "players_count": "Players count",
                "player_new": "New player",
                "player_details": "Player Details",
                "player_removed_from_team": "Player successfully removed from the team",
                "player_scores": "Player scores",
                "players_scores": "Players scores",
                "player_without_team": "player is not a member of any team",
                "points": "Gained points",
                "points_addition": "Match points addition",
                "position_goalkeeper": "Goalkeeper",
                "position_rightBack": "Rightback defender",
                "position_leftBack": "Leftback defender",
                "position_centerBack": "Centerback defender",
                "position_attackingMidfielder": "Attacking midfielder",
                "position_defensiveMiedfielder": "Defensive miedfielder",
                "position_striker" : "Striker",
                "preferred_position": "Preferred position",
                "preview" : "Preview",
                "question": "Question",
                "questions": "Questions",
                "report": "Report",
                "reports": "Reports",
                "right_foot_ball_receiving_accuracy": "Right foot ball receiving accuracy",
                "right_foot_dribbling_accuracy": "Right foot dribbling accuracy",
                "right_foot_pass_accuracy": "Right foot pass accuracy",
                "right_foot_shots_accuracy": "Right foot shots accuracy",
                "remove_player_from_team": "Remove player from the team",
                "result": "Result",
                "results": "Results",
                "role": "Role",
                "save": "Save",
                "score_type": "Score type",
                "select_country": "Select country",
                "select_countries": "Select countries",
                "select_coach": "Select coach",
                "select_gender": "Select gender",
                "select_language": "Select language",
                "select_player": "Select player",
                "select_preffered_position": "Select preffered position",
                "select_role": "Select role",
                "select_team": "Select team",
                "select_teams": "Select teams",
                "select_training_score_type": "Select score type",
                "selfconfidence": "Selfconfidence",
                "sep": "Sep",
                "september": "September",
                "sign_in": "Sign in",
                "something_went_wrong" : "Something went wrong",
                "started_playing": "Plays since",
                "started_working": "Works since",
                "strength": "Strength",
                "stress_control": "Stress control",
                "success": "Successfully completed",
                "surname": "Surname",
                "survey": "Survey",
                "surveys": "Surveys",
                "survey_add": "Add new survey",
                "survey_new": "New survey",
                "time_to_unlock": "Remain lock time: ",
                "title": "Title",
                "team": "Team",
                "teams": "Teams",
                "team_activated": "Team was activated successfully",
                "team_add_new": "Create new team",
                "team_deactivated": "Team was deactivated successfully",
                "team_deleted": "Team was deleted successfully",
                "team_has_no_main_coach": "Team has no main coach assigned to it",
                "team_history": "Team history",
                "team_history_is_not_empty": "Team history is not empty", 
                "team_main_coach": "Team's main coach",
                "team_players": "Team players",
                "test": "Test",
                "tests": "Tests",
                "test_add": "Add new test",
                "test_new": "New test",
                "training": "Training",
                "trainings": "Trainings",
                "training_activated": "Training was activated successfully",
                "training_add": "Add new training",
                "training_new": "New training",
                "training_deactivated": "Training was deactivated successfully",
                "training_deleted": "Training was deleted successfully",
                "training_description": "Training details",
                "training_details": "Training details",
                "trainings_results": "Trainings results",
                "training_score_activated": "Training score was activated successfully",
                "training_score_add_new": "Create new training score",
                "training_score_already_exists": "Score with given type and player already exist",
                "training_score_deactivated": "Training score was deactivated successfully",
                "training_score_deleted": "Training score was deleted successfully",
                "training_score_edit_value": "Edit training score value",
                "user": "User",
                "users": "Users",
                "user_add": "Add new user",
                "user_edit": "Edit user",
                "user_details": "User Details",
                "value": "Value",
                "wrong_password": "Incorrect password",
                "year_of_birth": "Year of birth",
                "yes": "Yes"
            }
        }
    ]

    primengTranslations = [
        {
        lang: "en",
        data: {
            "startsWith": "Starts with",
            "contains": "Contains",
            "notContains": "Not contains",
            "endsWith": "Ends with",
            "equals": "Equals",
            "notEquals": "Not equals",
            "noFilter": "No Filter",
            "lt": "Less than",
            "lte": "Less than or equal to",
            "gt": "Greater than",
            "gte": "Greater than or equal to",
            "is": "Is",
            "isNot": "Is not",
            "before": "Before",
            "after": "After",
            "dateIs": "Date is",
            "dateIsNot": "Date is not",
            "dateBefore": "Date is before",
            "dateAfter": "Date is after",
            "clear": "Clear",
            "apply": "Apply",
            "matchAll": "Match All",
            "matchAny": "Match Any",
            "addRule": "Add Rule",
            "removeRule": "Remove Rule",
            "accept": "Yes",
            "reject": "No",
            "choose": "Choose",
            "upload": "Upload",
            "cancel": "Cancel",
            "dayNames": ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            "dayNamesShort": ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
            "dayNamesMin": ["Su","Mo","Tu","We","Th","Fr","Sa"],
            "monthNames": ["January","February","March","April","May","June","July","August","September","October","November","December"],
            "monthNamesShort": ["Jan", "Feb", "Mar", "Apr", "May", "Jun","Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            "dateFormat": "mm/dd/yy",
            "today": "Today",
            "weekHeader": "Wk",
            "weak": 'Weak',
            "medium": 'Medium',
            "strong": 'Strong',
            "passwordPrompt": 'Enter a password',
            "emptyMessage": 'No results found',
            "emptyFilterMessage": 'No results found'
        }
    },
    {
        lang: "pl",
        data: {
            "startsWith": "zaczyna się od",
            "contains": "zawiera",
            "notContains": "nie zawiera",
            "endsWith": "kończy się na",
            "equals": "równa się",
            "notEquals": "rie równa się",
            "noFilter": "brak filtrów",
            "lt": "mniej niż",
            "lte": "mniejsze lub równe",
            "gt": "większe",
            "gte": "większe lub równe",
            "is": "jest",
            "isNot": "nie jest",
            "before": "przed",
            "after": "po",
            "dateIs": "Data",
            "dateIsNot": "Data nie jest",
            "dateBefore": "Data przed",
            "dateAfter": "Data po",
            "clear": "Wyczyść",
            "apply": "Zastosuj",
            "matchAll": "Dopasuj wszystkie",
            "matchAny": "Dopasuj dowolny",
            "addRule": "Dodaj regułę",
            "removeRule": "Usuń regułę",
            "accept": "Tak",
            "reject": "Nie",
            "choose": "Wybierz",
            "upload": "Wyślij",
            "cancel": "Anuluj",
            "dayNames": ["Niedziela", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota"],
            "dayNamesShort": ["Niedz", "Pon", "Wt", "Śr", "Czw", "Pt", "Sob"],
            "dayNamesMin": ["Niedz","Pon","Wt","Śr","Czw","Pt","Sob"],
            "monthNames": ["Styczeń","Luty","Marzec","Kwiecień","Maj","Czerwiec","Lipiec","Sierpień","Wrzesień","Październik","Listopad","Grudzień"],
            "monthNamesShort": ["Sty", "Lut", "Mar", "Kwi", "Maj", "Cze","Lip", "Sie", "Wrz", "Paź", "Lis", "Gr"],
            "dateFormat": "mm-dd-yy",
            "today": "Dziś",
            "weekHeader": "Tyg",
            "weak": 'Tydzień',
            "medium": 'Średni',
            "strong": 'Mocno',
            "passwordPrompt": 'Podaj hasło',
            "emptyMessage": 'Brak wyników',
            "emptyFilterMessage": 'Brak wyników'
        }
    }];
}
