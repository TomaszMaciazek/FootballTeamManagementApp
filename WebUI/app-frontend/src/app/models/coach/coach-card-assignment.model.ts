import { CardColor } from "src/app/enums/card-color";
import { SimpleCoach } from "./simple-coach.model";

export class CoachCardAssignment {
    public coach: SimpleCoach;
    public color: CardColor;
    public count: number;

    constructor(data:{
        coach: SimpleCoach,
        color : CardColor,
        count: number
    }){
        Object.assign(this,data);
    }
}
