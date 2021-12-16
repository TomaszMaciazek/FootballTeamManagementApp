export class AddTrainingCommand {
    public date: Date;
    public description: string;
    public title: string;

    constructor(data: {
        title: string,
        description: string,
        date: Date
    }){
        Object.assign(this, data);
    }
}
