import { TestToFillQuestion } from "./test-to-fill-question.model";

export class TestToFill {
    public id : string;
    public questions : Array<TestToFillQuestion>;
    public title : string;
    public description : string;
    public passedMinimalValue : number;
}
