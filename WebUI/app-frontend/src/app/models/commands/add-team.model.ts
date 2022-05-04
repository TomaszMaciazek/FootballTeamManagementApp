export class AddTeamCommand {
    public name: string;
    public mainCoachId: string;

    constructor(data: {
        name: string,
        mainCoachId: string
    }){
        Object.assign(this, data);
    }
}
