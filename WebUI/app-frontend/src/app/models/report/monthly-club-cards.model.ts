import { MonthDataCountStatistics } from "./month-data-count-statistics.model";

export class MonthlyClubCards {
    public playersYellowCards : Array<MonthDataCountStatistics>;
    public playersRedCards : Array<MonthDataCountStatistics>;
    public coachesYellowCards : Array<MonthDataCountStatistics>;
    public coachesRedCards : Array<MonthDataCountStatistics>;
}
