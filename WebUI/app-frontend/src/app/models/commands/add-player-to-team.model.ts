export class AddPlayersToTeam {
    public playersIds: Array<string>;
    public teamId: string;

    constructor(data: {
        playersIds: Array<string>,
        teamId: string
    }){
        Object.assign(this, data);
    }
}
