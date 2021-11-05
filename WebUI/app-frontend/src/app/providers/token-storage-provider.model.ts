import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable()
export class TokenStorageProvider {

    constructor(private jwtHelper: JwtHelperService){}

    public getToken(): string {
        return localStorage.getItem("jwt");
    }

    public setToken(token : string){
        localStorage.setItem("jwt", token);
    }

    public removeToken(){
        localStorage.removeItem("jwt");
    }

    public getTokenDecoded(): string {
        var token = this.getToken();
        if (token == null) return "";
        var info = this.jwtHelper.decodeToken(token);
        return info;
    }

    public getPermissions(): Array<string> {
        var token = <any>this.getTokenDecoded();
        var keys = Object.keys(token);
        var filed = keys.find(x => x == "permission");
        return token[filed];
    }

    public hasPermission(role: string): boolean {
        var permissions = this.getPermissions();

        var myPermissions: any[] = [];

        if (!Array.isArray(permissions))
            myPermissions = [permissions];
        else
            myPermissions = permissions;

        return myPermissions != null ? myPermissions.indexOf(role) !== -1 : false;
    }

    public getValueByName(name: string): any {
        var token = <any>this.getTokenDecoded();
        var keys = Object.keys(token);
        var filed = keys.find(num => (num.indexOf(name) !== -1));
        return token[filed];
    }

    public getUserId(): string {
        var userid = this.getValueByName("userId");
        return userid != null ? userid.toString() : "";
    }

    public getEndSessionDate(): Date {
        var token = this.getToken();
        return this.jwtHelper.getTokenExpirationDate(token);
    }

    public isLogged(): boolean {
        return localStorage.getItem("jwt") != null;
    }

    public isExpired(): boolean {
        var token = this.getToken();
        return this.jwtHelper.isTokenExpired(token);
    }

    public getPasswordChangeRequired(): boolean {
        return this.getValueByName('passwordChangeRequired') === true;
    }
}
