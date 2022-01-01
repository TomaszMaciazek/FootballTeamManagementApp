import { SimpleCoach } from "../coach/simple-coach.model";

export class CoachAssignmentEvent {
    public id : string;
    public coach : SimpleCoach;
    public date : Date;
}
