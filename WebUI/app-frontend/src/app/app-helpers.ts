import { HttpParams } from "@angular/common/http";

export function detectBody() {
    if (window.innerWidth < 769) {
        document.body.classList.add('body-small');
    }
    else {
        document.body.classList.remove('body-small');
    }
}

export function goTop() {
    document.body.animate({scrollTop: 0}, 200);
    document.documentElement.animate({scrollTop: 0}, 200);
}

export function toParams(data: any){
    let httpParams = new HttpParams();
    Object.keys(data).forEach(function (key) {
        if(data[key] != null && data[key] != undefined){
            if(data[key] instanceof Array){
                data[key].forEach(element => {
                    httpParams = httpParams.append(key, element);
                });
            }
            else{
                httpParams = httpParams.append(key, data[key]);
            }
        }
    });
    return httpParams;
}