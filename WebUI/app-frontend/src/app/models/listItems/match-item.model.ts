import { MatchType } from "src/app/enums/match-type";
import { PlayersGender } from "src/app/enums/players-gender";

export class MatchItem {
    public id : string;
    public date : Date;
    public opponentsClubName : string;
    public opponentsScore : number
    public clubScore : number;
    public playersGender : PlayersGender;
    public matchType : MatchType;
    public isActive : boolean;
}
