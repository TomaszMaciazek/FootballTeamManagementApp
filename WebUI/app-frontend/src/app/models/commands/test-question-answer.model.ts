export class TestQuestionAnswerCommand {
    public questionId : string;
    public value : number;

    constructor(data: {
        questionId : string,
        value : string
    }){
        Object.assign(this,data);
    }
}
