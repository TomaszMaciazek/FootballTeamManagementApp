export class UpdateTrainingScoreCommand {
    public id: string;
    public value: number;

    constructor(data: {
        id: string,
        value: number
    }){
        Object.assign(this, data);
    }
}
