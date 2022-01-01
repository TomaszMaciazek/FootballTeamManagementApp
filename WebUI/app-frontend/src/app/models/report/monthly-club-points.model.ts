import { MonthlyMatchPointCount } from "./monthly-match-point-count.model";

export class MonthlyClubPoints {
    public cupMatchesPoints : Array<MonthlyMatchPointCount>;
    public leagueMatchesPoints : Array<MonthlyMatchPointCount>;
    public friendlyMatchesPoints : Array<MonthlyMatchPointCount>;
}
