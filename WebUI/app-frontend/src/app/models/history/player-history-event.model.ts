import { PlayerHistoryEventType } from "src/app/enums/player-history-event-type";

export class PlayerHistoryEvent {
    public id : string;
    public date: Date;
    public eventType: PlayerHistoryEventType;

    constructor(data: {
        id: string,
        date: Date,
        eventType: PlayerHistoryEventType
    }){
        Object.assign(this,data);
    }
}
