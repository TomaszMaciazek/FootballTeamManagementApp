import { MonthDataCountStatistics } from "./month-data-count-statistics.model";

export class MonthlyPlayerCards {
    public playerId : string;
    public yellowCards : Array<MonthDataCountStatistics>;
    public redCards : Array<MonthDataCountStatistics>;
}
