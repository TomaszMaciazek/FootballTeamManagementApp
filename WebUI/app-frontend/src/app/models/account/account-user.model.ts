import { AccountCoach } from "../coach/account-coach.model";
import { AccountPlayer } from "../player/account-player.model";
import { Role } from "../role.model";

export class AccountUser {
    public id : string;
    public email : string;
    public name : string
    public middleName : string;
    public surname : string;
    public isActive : boolean;
    public lastPasswordSet : Date;
    public player: AccountPlayer;
    public coach: AccountCoach;
    public role: Role;
}
