import { TestQuestion } from "./test-question.model";

export class Test {
    public id : string;
    public title : string;
    public description : string;
    public questions : Array<TestQuestion>;
}
