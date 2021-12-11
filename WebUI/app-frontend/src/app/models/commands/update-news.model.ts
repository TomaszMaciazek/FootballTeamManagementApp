export class UpdateNewsCommand {
    public id: string;
    public title: string;
    public content: string;
    public isImportant: boolean;

    constructor(data: {
        id: string,
        title: string,
        content: string,
        isImportant: boolean
    }){
        Object.assign(this, data);
    }
}
