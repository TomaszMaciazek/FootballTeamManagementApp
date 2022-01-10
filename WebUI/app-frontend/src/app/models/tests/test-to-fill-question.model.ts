import { QuestionType } from "src/app/enums/question-type";

export class TestToFillQuestion {
    public id : string;
    public number : number;
    public type : QuestionType;
    public description : string;
    public pointsToScore : number;
    public options : Array<TestToFillQuestion>;
}
