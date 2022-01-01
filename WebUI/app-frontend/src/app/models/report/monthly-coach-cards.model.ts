import { MonthDataCountStatistics } from "./month-data-count-statistics.model";

export class MonthlyCoachCards {
    public coachId : string;
    public yellowCards : Array<MonthDataCountStatistics>;
    public redCards : Array<MonthDataCountStatistics>;
}
