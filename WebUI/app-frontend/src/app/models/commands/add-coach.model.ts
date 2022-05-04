import { Gender } from "src/app/enums/gender";

export class AddCoachCommand {
    public email: string;
    public password: string;
    public name: string;
    public middleName: string;
    public surname: string;
    public birthDate: Date;
    public countryId: number;
    public startedWorking: Date;
    public gender: Gender;
    public teamsIds: Array<string>;

    constructor(data: {
        email: string,
        password: string,
        name: string,
        middleName: string,
        surname: string,
        birthDate: Date,
        countryId: number,
        startedWorking: Date,
        gender: Gender,
        teamsIds: Array<string>
    }){
        Object.assign(this, data);
    }
}
