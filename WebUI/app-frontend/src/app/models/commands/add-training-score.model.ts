import { TrainingScoreType } from "src/app/enums/training-score-type";

export class AddTrainingScoreCommand {
    public playerId: string;
    public trainingId: string;
    public scoreType: TrainingScoreType;
    public value: number;

    constructor(data: {
        playerId: string,
        trainingId: string,
        scoreType: TrainingScoreType,
        value: number
    }){
        Object.assign(this, data);
    }
}
