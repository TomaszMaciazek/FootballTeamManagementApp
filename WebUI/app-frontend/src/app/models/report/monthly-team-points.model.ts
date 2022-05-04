import { MonthlyMatchPointCount } from "./monthly-match-point-count.model";

export class MonthlyTeamPoints {
    public teamId: string;
    public cupMatchesPoints : Array<MonthlyMatchPointCount>;
    public leagueMatchesPoints : Array<MonthlyMatchPointCount>;
    public friendlyMatchesPoints : Array<MonthlyMatchPointCount>;
}
