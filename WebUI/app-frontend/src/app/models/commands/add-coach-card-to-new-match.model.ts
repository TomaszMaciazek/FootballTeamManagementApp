import { CardColor } from "src/app/enums/card-color";

export class AddCoachCardToNewMatch {
    public coachId : string;
    public color : CardColor;
    public count: number;

    constructor(data :{
        coachId : string;
        color : CardColor,
        count: number
    }){
        Object.assign(this,data);
    }
}
