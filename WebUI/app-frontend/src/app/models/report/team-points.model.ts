import { MatchPointCount } from "./match-point-count.model";

export class TeamPoints {
    public teamId : string;
    public cupMatchesPoints : Array<MatchPointCount>;
    public leagueMatchesPoints : Array<MatchPointCount>;
    public friendlyMatchPoints : Array<MatchPointCount>;
}
