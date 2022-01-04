import { Gender } from "src/app/enums/gender";
import { SimpleTeam } from "../team/simple-team.model";

export class Coach {
    public id : string;
    public isActive : boolean;
    public email: string;
    public name : string;
    public middleName : string;
    public surname : string;
    public birthDate : Date;
    public country : string;
    public gender : Gender;
    public startedWorking : Date;
    public finishedWorking : Date;
    public teams : Array<SimpleTeam>;
}
