export class TextQuestionAnswer {
    public questionId : string;
    public value : string;

    constructor(data: {
        questionId : string,
        value : string
    }){
        Object.assign(this,data);
    }
}
