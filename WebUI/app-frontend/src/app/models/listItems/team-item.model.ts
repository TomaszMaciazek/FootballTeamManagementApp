import { SimpleCoach } from "../coach/simple-coach.model";

export class TeamItem {
    public id : string;
    public name : string;
    public mainCoach : SimpleCoach;
    public membersCount : number;
    public isActive: boolean;
    public canBeDeleted: boolean;
}
