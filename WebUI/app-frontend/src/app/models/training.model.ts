import { TrainingScore } from "./training-score.model";

export class Training {
    public date: Date;
    public description: string;
    public title: string;
    public trainingScores: Array<TrainingScore>
}

