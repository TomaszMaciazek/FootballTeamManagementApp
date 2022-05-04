import { MonthDataCountStatistics } from "./month-data-count-statistics.model";

export class MonthlyTeamCards {
    public teamId : string;
    public yellowCards : Array<MonthDataCountStatistics>;
    public redCards : Array<MonthDataCountStatistics>;
}
