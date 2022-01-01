import { CardColor } from "src/app/enums/card-color";

export class AddPlayerCardToNewMatch {
    public playerId : string;
    public color : CardColor;
    public count : number;

    constructor(data: {
        playerId : string,
        color : CardColor,
        count : number
    }){
        Object.assign(this, data);
    }
}
