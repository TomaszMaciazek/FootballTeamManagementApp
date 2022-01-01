import { MatchPointCount } from "./match-point-count.model";

export class MonthlyMatchPointCount {
    public pointsByType : Array<MatchPointCount>;
    public month : number;
    public year : number;
}
