export class AddNewsCommand {
    public title: string;
    public content: string;
    public isImportant: boolean;

    constructor(data: {
        title: string,
        content: string,
        isImportant: boolean
    }){
        Object.assign(this, data);
    }
}
