import { MonthlyMatchPointCount } from "./monthly-match-point-count.model";

export class MonthlyPlayerPoints {
    public playerId: string;
    public cupMatchesPoints : Array<MonthlyMatchPointCount>;
    public leagueMatchesPoints : Array<MonthlyMatchPointCount>;
    public friendlyMatchesPoints : Array<MonthlyMatchPointCount>;
}
