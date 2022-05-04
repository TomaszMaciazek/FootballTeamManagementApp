import { PlayerPosition } from "src/app/enums/player-position";

export class AddPlayerPerformanceToNewMatch {
    public playerId : string;
    public playerPosition : PlayerPosition;

    constructor(data: {
        playerId: string,
        playerPosition : PlayerPosition
    }){
        Object.assign(this, data);
    }
}
