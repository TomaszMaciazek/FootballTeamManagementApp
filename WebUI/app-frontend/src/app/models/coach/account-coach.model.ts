import { Gender } from "src/app/enums/gender";
import { SimpleTeam } from "../team/simple-team.model";

export class AccountCoach {
    public id : string;
    public birthDate : Date;
    public country : string;
    public gender : Gender;
    public startedWorking : Date;
    public finishedWorking : Date;
    public teams : Array<SimpleTeam>;
}
