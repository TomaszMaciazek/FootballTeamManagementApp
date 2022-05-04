export class RemovePlayerFromTeam {
    public playerId: string;
    public teamId: string;

    constructor(data: {
        playerId: string,
        teamId: string
    }){
        Object.assign(this, data);
    }
}
