export class AddAdminCommand {
    public email: string;
    public password: string;
    public name: string;
    public middleName: string;
    public surname: string;

    constructor(data: {
        email: string,
        password: string,
        name: string,
        middleName: string,
        surname: string,
    }){
        Object.assign(this, data);
    }
}
