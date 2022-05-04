export class CreateTestQuestionOption {
    public label : string;
    public value : number;
    public points : number;

    constructor(data: {
        label : string,
        value : number,
        points : number
    }){
        Object.assign(this, data);
    }
}
