import { CreateTestQuestion } from "./create-test-question.model";

export class CreateTestCommand {
    public authorId : string;
    public title : string;
    public description : string;
    public questions : Array<CreateTestQuestion>;
    public respondentsIds : Array<string>;
    public passedMinimalValue : number;
}
