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
                "account_is_locked": "Konto zostało tymczasowo zablokowane",
                "account_not_found": "Konto o danym loginie nie zostało znalezione",
                "account_remain_password_attempts": "Ilość pozostałych prób przed blokadą: ",
                "account_last_logon": "Ostatnie logowanie",
                "activate": "Aktywuj",
                "admin": "Administrator",
                "administrators": "Administratorzy",
                "admin_add": "Dodawanie administratora",
                "admin_new": "Nowy administrator",
                "answer": "Odpowiedź",
                "answers": "Odpowiedzi",
                "author": "Autor",
                "birthday": "Data urodzenia",
                "browse": "Przeglądaj",
                "card": "Kartka",
                "cards": "Kartki",
                "cards_addition": "Dodawanie kartek",
                "cards_red": "Czerwone kartki",
                "cards_yellow": "Żółte kartki",
                "coach": "Trener",
                "coaches": "Trenerzy",
                "coach_add": "Dodaj nowego trenera",
                "coach_new": "Nowy trener",
                "country": "Kraj",
                "deactivate": "Dezaktuwuj",
                "delete": "Usuń",
                "details": "Szczegóły",
                "edit": "Edytuj",
                "email": "Email",
                "finished_playing": "Zakończył grę",
                "finished_working": "Zakończył pracę",
                "gender": "Płeć",
                "image_change": "Zmień obraz",
                "login": "Login",
                "match": "Mecz",
                "matches": "Mecze",
                "match_add": "Dodaj nowy mecz",
                "match_details": "Szczegóły meczu",
                "match_new": "Nowy mecz",
                "message": "Wiadomość",
                "messages": "Wiadomości",
                "message_add": "Dodaj wiadomość",
                "message_new": "Nowa wiadomość",
                "middlename": "Drugie imię",
                "name": "Imię",
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
                "preferred_position": "Preferowana pozycja",
                "question": "Pytanie",
                "questions": "Pytania",
                "report": "Raport",
                "reports": "Raporty",
                "result": "Wynik",
                "results": "Wyniki",
                "started_playing": "Gra od",
                "started_working": "Pracuje od",
                "select_language": "Wybierz język",
                "sign_in": "Zaloguj się",
                "something_went_wrong" : "Operacja zakończona niepowodzeniem",
                "success": "Sukces",
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
                "training": "Trening",
                "trainings": "Treningi",
                "training_add": "Dodaj nowy trening",
                "training_new": "Nowy trening",
                "training_details": "Szczegóły treningu",
                "users": "Użytkownicy",
                "user_details": "Szczegóły konta użytkownika",
                "wrong_password": "Hasło nieprawidłowe"
            }

        },
        {
            "lang": "en",
            "data": {
                "account_is_locked": "This account is currently locked",
                "account_not_found": "Account with given login was not found",
                "account_remain_password_attempts": "Number of remain attempts: ",
                "account_last_logon": "Last logon",
                "activate": "Activate",
                "admin": "Administrator",
                "administrators": "Administrators",
                "admin_add": "Create administrator",
                "admin_new": "New administrator",
                "answer": "Answer",
                "answers": "Answers",
                "author": "Author",
                "birthday": "Birthday",
                "browse": "Browse",
                "card": "Card",
                "cards": "Cards",
                "cards_addition": "Cards addition",
                "cards_red": "Red cards",
                "cards_yellow": "Yellow cards",
                "coach": "Coach",
                "coaches": "Coaches",
                "coach_add": "Add new coach",
                "coach_new": "New coach",
                "country": "Country",
                "deactivate": "Deactivate",
                "delete": "Delete",
                "details": "Details",
                "edit": "Edit",
                "email": "Email",
                "finished_playing": "Finished playing",
                "finished_working": "Finished working",
                "gender": "Gender",
                "image_change": "Change image",
                "login": "Login",
                "match": "Match",
                "matches": "Matches",
                "match_add": "Add new match",
                "match_details": "Match details",
                "match_new": "New match",
                "message": "Message",
                "messages": "Messages",
                "message_add": "Add new message",
                "message_new": "New message",
                "middlename": "Middlename",
                "name": "Name",
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
                "preferred_position": "Preferred position",
                "question": "Question",
                "questions": "Questions",
                "report": "Report",
                "reports": "Reports",
                "result": "Result",
                "results": "Results",
                "started_playing": "Plays since",
                "started_working": "Works since",
                "select_language": "Select language",
                "sign_in": "Sign in",
                "something_went_wrong" : "Something went wrong",
                "success": "Success",
                "surname": "Surname",
                "survey": "Survey",
                "surveys": "Surveys",
                "survey_add": "Add new survey",
                "survey_new": "New survey",
                "time_to_unlock": "Remain lock time: ",
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
                "training_details": "Training Details",
                "users": "Users",
                "user_details": "User Details",
                "wrong_password": "Incorrect password"
            }
        }
    ]
}
