export class UpdateUserPassword {
    public userId: string;
    public newPassword: string;

    constructor(data: {
        userId: string,
        newPassword: string
    }){
        Object.assign(this, data);
    }
}
