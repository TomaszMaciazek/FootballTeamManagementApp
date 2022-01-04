import { SelectUserCoachDetails } from "../coach/select-user-coach-details.model";
import { SelectUserPlayerDetails } from "../player/select-user-player-details.model";
import { Role } from "../role.model";

export class SelectUser {
    public id : string;
    public name : string;
    public middleName : string;
    public surname : string;
    public userName : string;
    public role: Role;
    public player : SelectUserPlayerDetails;
    public coach : SelectUserCoachDetails;
}
