import { Injectable } from "@angular/core";
import { TranslationProvider } from "./translation-provider.model";

@Injectable()
export class LocaleCalendarProvider {
    private _locale = {
        firstDayOfWeek: 1,
        dayNamesShort: ["", "", "", "", "", "", ""],
        dayNamesMin: ["", "", "", "", "", "", ""],
        monthNames: ["", "", "", "", "", "", "", "", "", "", "", ""]
    };
    private _consts = {
        dayNamesShortConsts: ["sun", "mon", "tue", "wed", "thu", "fri", "sat"],
        dayNamesMinConsts: ["su", "mo", "tu", "we", "th", "fr", "sa"],
        monthNamesConsts: ["january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"]
    }

    constructor(private translationProvider: TranslationProvider) {
        
    }

    get locale(): any {

        this.translate();

        return this._locale;
    }

    translate() {
        this.translateArray(this._consts.dayNamesShortConsts, this._locale.dayNamesShort);
        this.translateArray(this._consts.dayNamesMinConsts, this._locale.dayNamesMin);
        this.translateArray(this._consts.monthNamesConsts, this._locale.monthNames);
    }

    private translateArray(arrayConsts: string[], targetArray: string[]) {
        for (var i = 0; i < targetArray.length; i++) {
            var translatedElem = this.translationProvider.getTranslation(arrayConsts[i]);
            targetArray[i] = translatedElem;
        }
    }
}
