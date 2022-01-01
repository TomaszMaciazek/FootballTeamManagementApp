import { PlayersGender } from "src/app/enums/players-gender";

export class PlayingPlayerQuery {
    public playersGender: PlayersGender;
    public date: string;

    constructor(data: {
        playersGender: PlayersGender,
        date: string
    }){
        Object.assign(this, data);
    }
}
