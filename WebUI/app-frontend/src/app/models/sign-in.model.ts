export class SignInModel {
    public login : string;
    public password : string;

    constructor(data: { login: string, password: string }) {
        Object.assign(this, data);
    }
}
