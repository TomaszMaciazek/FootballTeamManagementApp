import { CardColor } from "src/app/enums/card-color";

export class PlayerCard {
    public id : string;
    public isActive : boolean;
    public playerId : string;
    public matchId : string;
    public color : CardColor;
    public count : number;
}
