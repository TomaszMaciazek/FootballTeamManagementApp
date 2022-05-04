import { Gender } from "src/app/enums/gender";
import { PlayerPosition } from "src/app/enums/player-position";

export class AddPlayerCommand {
    public email: string;
    public password: string;
    public name: string;
    public middleName: string;
    public surname: string;
    public birthDate: Date;
    public countryId: number;
    public startedPlaying: Date;
    public gender: Gender;
    public prefferedPosition: PlayerPosition;
    public teamId: string;

    constructor(data: {
        email: string,
        password: string,
        name: string,
        middleName: string,
        surname: string,
        birthDate: Date,
        countryId: number,
        startedPlaying: Date,
        gender: Gender,
        prefferedPosition: PlayerPosition,
        teamId: string,
    }){
        Object.assign(this, data);
    }
}
