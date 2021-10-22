import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { TokenStorageProvider } from "./token-storage-provider.model";

@Injectable()
export class UserContextProvider {

    constructor(
        private tokenStorage : TokenStorageProvider,
        private router: Router,
        ){}
}
