import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationResult } from "../models/authentication-result.model";
import { TokenStorageProvider } from "./token-storage-provider.model";

@Injectable()
export class UserContextProvider {

    constructor(
        private tokenStorageProvider : TokenStorageProvider,
        private router: Router,
        private http: HttpClient,
        ){}

    public hasPermission(permission: string): boolean {
        return this.tokenStorageProvider.hasPermission(permission);
    }

    public getDefaultPageUrl() : string{
        //if (this.getPasswordChangeRequired()) { return "/changePasswordRequired"; }
        if (this.hasPermission("news")) { return "/news/preview"; }
        else { return "/noAccess"; }
    }

    public getPasswordChangeRequired(): boolean {
        return this.tokenStorageProvider.getPasswordChangeRequired();
    }
}
