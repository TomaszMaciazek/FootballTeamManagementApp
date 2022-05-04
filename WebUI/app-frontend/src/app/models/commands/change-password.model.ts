export class ChangePassword {
    public userId: string;
    public oldPassword: string;
    public newPassword: string;

    constructor(data: {
        userId: string,
        oldPassword: string,
        newPassword: string
    }){
        Object.assign(this, data);
    }
}
