import { Gender } from "src/app/enums/gender";

export class CoachItem {
    public id : string;
    public teamsCount : string;
    public name : string;
    public middleName : string;
    public surname : string;
    public gender : Gender;
    public isStillWorking : boolean;
    public country : string;
    public isActive : boolean;
}
