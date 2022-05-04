import { Directive, ElementRef, Input, NgModule, OnInit } from '@angular/core';
import { TokenStorageProvider } from '../providers/token-storage-provider.model';
import { UserContextProvider } from '../providers/user-context-provider.model';

@Directive({
  selector: '[security]'
})
export class UserSecurityDirective implements OnInit {

  private el: HTMLElement;
    @Input('security') permission: string;
    constructor(
        el: ElementRef,
        private tokenStorageProvider: TokenStorageProvider
        ) {
        this.el = el.nativeElement;
    }

    ngOnInit() {
        if (this.permission == null && !this.tokenStorageProvider.isLogged()) {
            this.el.parentNode.removeChild(this.el);
        }

        if (this.permission != null) {
            var list = this.permission.split(";");
            var hasAccess = false;
            for (var i = 0; i < list.length; i++) {
                if (this.tokenStorageProvider.hasPermission(list[i])) {
                    hasAccess = true;
                    break;
                }
            }
            
            if ((this.el.parentNode != null) && (!hasAccess)) {
                this.el.parentNode.removeChild(this.el);
                this.el.className = "hidden";
            }
            else if(!hasAccess){
                this.el.remove();
            }

        }
    }

}


@NgModule({
  declarations: [
      UserSecurityDirective
  ],
  exports: [
      UserSecurityDirective
  ],
  imports: []
})
export class UserSecurityModule { }