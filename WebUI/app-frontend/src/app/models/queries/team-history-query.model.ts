export class TeamHistoryQuery {
    public teamId: string;
    public minDate: string;
    public maxDate: string;

    constructor(data: {
        teamId: string,
        minDate: string,
        maxDate: string
    }){
        Object.assign(this,data);
    }
}
