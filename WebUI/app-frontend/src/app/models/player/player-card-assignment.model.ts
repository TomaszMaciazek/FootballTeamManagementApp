import { CardColor } from "src/app/enums/card-color";
import { SimpleSelectPlayer } from "./simple-select-player.model";

export class PlayerCardAssignment {
    public player : SimpleSelectPlayer;
    public color : CardColor;
    public count: number;

    constructor(data:{
        player : SimpleSelectPlayer,
        color : CardColor,
        count: number
    }){
        Object.assign(this,data);
    }
}
