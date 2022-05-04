import { CardColor } from "src/app/enums/card-color";
import { SimpleCoach } from "../coach/simple-coach.model";

export class CoachCard {
    public id : string;
    public isActive : boolean;
    public coach : SimpleCoach;
    public matchId : string;
    public color : CardColor;
    public count : number;
}
