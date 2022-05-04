export class MenuItem {
    Title : string;
    FontAwesomeIcon: string;
    RequiredPermissions: string;
    Link: string;
    Expanded: boolean;
    SubMenuItems: Array<MenuItem>;

    constructor() {
        this.Expanded = false;
    }

    public isEmpty() : boolean {
        return !this.Title && !this.FontAwesomeIcon && !this.RequiredPermissions &&
            !this.Link && !this.SubMenuItems;
    }
}
