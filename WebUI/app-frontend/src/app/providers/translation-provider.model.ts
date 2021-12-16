import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { LanguageCode } from "../enums/language-code";

@Injectable()
export class TranslationProvider {

    private static langId: string = "pl";
    public static get LangId(): string { return this.langId; }

    constructor(private translate: TranslateService){}

    async setDefaultLanguage(langId: LanguageCode) {        
        this.translateLangs.forEach(element => {
            this.translate.setTranslation(element.lang, element.data);
        });
        this.translate.use(langId);
    }

    async setLanguage(langId: LanguageCode) {
        this.translate.use(langId);
    }

    getTranslation(key: string): any {
        let cuurentLangTransalations: any = this.translate.translations[this.translate.currentLang];
        if (!cuurentLangTransalations) {
            let msg: string = "Translations not loaded";
            console.warn(msg);
            return;
        }
        let translation: string = cuurentLangTransalations[key];
        if (translation === undefined || translation === null || translation === "") {
            return key;
        }
        return translation;
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
                "activate": "Aktywuj",
                "actions": "Akcje",
                "add": "Dodaj",
                "admin": "Administrator",
                "administrators": "Administratorzy",
                "admin_add": "Dodawanie administratora",
                "admin_new": "Nowy administrator",
                "all" : "Wszystkie",
                "answer": "Odpowiedź",
                "answers": "Odpowiedzi",
                "any": "Dowolna",
                "apr": "Kwi",
                "april": "Kwiecień",
                "aug": "Sie",
                "august": "Sierpień",
                "author": "Autor",
                "awaiting": "Oczekujące",
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
                "columns_selected": "wybranych kolumn",
                "confirm_activate_training": "Czy na pewno chcesz aktywować ten trening ?",
                "confirm_deactivate_training": "Czy na pewno chcesz deaktywować ten trening ?",
                "confirm_delete_training": "Czy na pewno chcesz usunąć ten trening ?",
                "content": "Treść",
                "country": "Kraj",
                "deactivate": "Dezaktywuj",
                "date": "Data",
                "date_of_birth": "Data urodzenia",
                "dec": "Gr",
                "december": "Grudzień",
                "delete": "Usuń",
                "description": "Opis",
                "details": "Szczegóły",
                "edit": "Edytuj",
                "email": "Email",
                "filled": "Wypełnione",
                "finished": "Zakończone",
                "finished_playing": "Zakończył grę",
                "finished_working": "Zakończył pracę",
                "feb": "Lut",
                "february": "Luty",
                "female": "Kobieta",
                "gender": "Płeć",
                "image_change": "Zmień obraz",
                "incoming_events": "Kalendarz",
                "important": "Ważne",
                "jan": "Sty",
                "january": "Styczeń",
                "jul": "Lip",
                "july": "Lipiec",
                "jun": "Cz",
                "june": "Czerwiec",
                "last_modification": "Ostatnia modyfikacja",
                "login": "Login",
                "logout": "Wyloguj się",
                "male": "Mężczyzna",
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
                "my_account": "Moje konto",
                "name": "Imię",
                "news": "Aktualności",
                "no": "Nie",
                "nov": "Lis",
                "november": "Listopad",
                "oct": "Paź",
                "october": "Październik",
                "opponents_team": "Drużyna przeciwników",
                "owners": "Właściciele",
                "password": "Hasło",
                "player": "Zawodnik",
                "players": "Zawodnicy",
                "player_add": "Dodaj nowego zawodnika",
                "players_addition": "Dodawanie zawodników",
                "player_new": "Nowy zawodnik",
                "player_details": "Szczegóły zawodnika",
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
                "result": "Wynik",
                "results": "Wyniki",
                "role": "Rola",
                "save": "Zapisz",
                "started_playing": "Gra od",
                "started_working": "Pracuje od",
                "select_country": "Wybierz kraj",
                "select_gender": "Wybierz płeć",
                "select_language": "Wybierz język",
                "select_preffered_position": "Wybierz preferowaną pozycję",
                "select_role": "Wybierz rolę",
                "sep": "Wrz",
                "september": "Wrzesień",
                "sign_in": "Zaloguj się",
                "something_went_wrong" : "Operacja zakończona niepowodzeniem",
                "success": "Operacja zakończona powodzeniem",
                "surname": "Nazwisko",
                "survey": "Ankieta",
                "surveys": "Ankiety",
                "survey_add": "Dodawanie ankiety",
                "survey_new": "Nowa ankieta",
                "team": "Zespół",
                "teams": "Zespoły",
                "team_main_coach": "Główny trener zespołu",
                "team_players": "Zawodnicy z zespołu",
                "test": "Test",
                "tests": "Testy",
                "test_add": "Dodawanie testu",
                "test_new": "Nowy test",
                "time_to_unlock": "Pozostały czas blokady wynosi: ",
                "title": "Tytuł",
                "training": "Trening",
                "trainings": "Treningi",
                "training_add": "Dodaj nowy trening",
                "training_new": "Nowy trening",
                "training_details": "Szczegóły treningu",
                "trainings_results": "Wyniki treningów",
                "user": "Użytkownik",
                "users": "Użytkownicy",
                "user_add": "Dodawanie nowego użytkownika",
                "user_edit": "Edycja użytkownika",
                "user_details": "Szczegóły konta użytkownika",
                "wrong_password": "Hasło nieprawidłowe",
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
                "activate": "Activate",
                "actions": "Actions",
                "add": "Add",
                "admin": "Administrator",
                "administrators": "Administrators",
                "admin_add": "Create administrator",
                "admin_new": "New administrator",
                "all" : "All",
                "answer": "Answer",
                "answers": "Answers",
                "any": "Any",
                "apr": "Apr",
                "april": "April",
                "aug": "Aug",
                "august": "August",
                "author": "Author",
                "awaiting" : "Awaiting",
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
                "columns_selected": "columns selected",
                "confirm_activate_training": "Are you sure you want activate this training ?",
                "confirm_deactivate_training": "Are you sure you want deactivate this training ?",
                "confirm_delete_training": "Are you sure you want delete this training ?",
                "content": "Content",
                "country": "Country",
                "date": "Date",
                "date_of_birth": "Date of birth",
                "deactivate": "Deactivate",
                "dec": "Dec",
                "december": "December",
                "delete": "Delete",
                "description": "Description",
                "details": "Details",
                "edit": "Edit",
                "email": "Email",
                "filled": "Filled",
                "finished": "Finished",
                "finished_playing": "Finished playing",
                "finished_working": "Finished working",
                "feb": "Feb",
                "february": "February",
                "female": "Female",
                "gender": "Gender",
                "image_change": "Change image",
                "incoming_events": "Calendar",
                "important": "Important",
                "jan": "Jan",
                "january": "January",
                "jul": "Jul",
                "july": "July",
                "jun": "Jun",
                "june": "June",
                "last_modification": "Last modification",
                "login": "Login",
                "logout": "Log out",
                "male": "Male",
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
                "my_account": "My account",
                "name": "Name",
                "news": "News",
                "no": "No",
                "nov": "nov",
                "november": "November",
                "oct": "Oct",
                "october": "October",
                "opponents_team": "Opponents team",
                "owners": "Owners",
                "password": "Password",
                "player": "Player",
                "players": "Players",
                "player_add": "Add new player",
                "players_addition": "Add players",
                "player_new": "New player",
                "player_details": "Player Details",
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
                "result": "Result",
                "results": "Results",
                "role": "Role",
                "save": "Save",
                "started_playing": "Plays since",
                "started_working": "Works since",
                "select_country": "Select country",
                "select_gender": "Select gender",
                "select_language": "Select language",
                "select_preffered_position": "Select preffered position",
                "select_role": "Select role",
                "sep": "Sep",
                "september": "September",
                "sign_in": "Sign in",
                "something_went_wrong" : "Something went wrong",
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
                "team_main_coach": "Team's main coach",
                "team_players": "Team players",
                "test": "Test",
                "tests": "Tests",
                "test_add": "Add new test",
                "test_new": "New test",
                "training": "Training",
                "trainings": "Trainings",
                "training_add": "Add new training",
                "training_new": "New training",
                "training_details": "Training details",
                "trainings_results": "Trainings results",
                "user": "User",
                "users": "Users",
                "user_add": "Add new user",
                "user_edit": "Edit user",
                "user_details": "User Details",
                "wrong_password": "Incorrect password",
                "yes": "Yes"
            }
        }
    ]
}
