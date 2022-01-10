import { TrainingScoreType } from "src/app/enums/training-score-type";

export class TrainingScoreStatistics {
    public scoreType : TrainingScoreType;
    public avg: number;
    public min : number;
    public max : number;
    public median : number;
}
