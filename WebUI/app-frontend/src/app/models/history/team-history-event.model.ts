import { TeamHistoryEventType } from "src/app/enums/team-history-event-type";

export class TeamHistoryEvent {
    public id : string;
    public date: Date;
    public eventType: TeamHistoryEventType;

    constructor(data: {
        id: string,
        date: Date,
        eventType: TeamHistoryEventType
    }){
        Object.assign(this,data);
    }
}
