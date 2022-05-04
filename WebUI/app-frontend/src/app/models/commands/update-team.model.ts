export class UpdateTeamCommand {
    public id: string;
    public name: string;
    public mainCoachId: string;

    constructor(data: {
        id: string,
        name: string,
        mainCoachId: string
    }){
        Object.assign(this, data);
    }
}
