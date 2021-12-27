import { TrainingScoreType } from "../enums/training-score-type";

export class TrainingScore {
    public id: string;
    public playerId: string;
    public playerName: string;
    public value: number;
    public scoreType: TrainingScoreType;  
    public isActive : boolean;
}
