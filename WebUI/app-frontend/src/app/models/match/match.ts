import { MatchType } from "src/app/enums/match-type";
import { PlayersGender } from "src/app/enums/players-gender";

export class Match {
    public id : string;
    public date : Date;
    public opponentsClubName : string;
    public opponentsScore : number;
    public clubScore : number;
    public location : string;
    public playersGender : PlayersGender;
    public matchType : MatchType;
    public description : string;
}
