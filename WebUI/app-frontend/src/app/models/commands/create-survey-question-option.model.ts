export class CreateSurveyQuestionOption {
    public label : string;
    public value : number;

    constructor(data: {
        label : string,
        value : number
    }){
        Object.assign(this, data);
    }
}
