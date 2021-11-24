import { Role } from "./role.model";

export class User {
    public id: string;
    public email: string;
    public name: string
    public username: string;
    public role :Role;
    public isActive: boolean;
}
