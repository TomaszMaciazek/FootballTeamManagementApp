import { MatchPointCount } from "./match-point-count.model";

export class PlayerPoints {
    public playerId : string;
    public cupMatchesPoints : Array<MatchPointCount>;
    public leagueMatchesPoints : Array<MatchPointCount>;
    public friendlyMatchPoints : Array<MatchPointCount>;
}
