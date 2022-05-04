export class SelectQuestionAnswer {
    public questionId : string;
    public value : number;

    constructor(data: {
        questionId : string,
        value : number
    }){
        Object.assign(this,data);
    }
}
