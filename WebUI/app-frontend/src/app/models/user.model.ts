import { Role } from "./role.model";

export class User {
    public id: string;
    public email: string;
    public surname: string;
    public names: string
    public username: string;
    public role :Role;
    public playerId : string;
    public coachId : string;
    public isActive: boolean;
}
