import { NgModule, Renderer2, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { ToastrModule } from 'ngx-toastr';
import { FormErrorComponent } from './components/form/form-error/form-error.component';
import { AuthenticationService } from './services/authentication.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { BlankLayoutComponent } from './components/layout/blank-layout/blank-layout.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RefreshComponent } from './components/refresh/refresh.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { TokenStorageProvider } from './providers/token-storage-provider.model';
import { UserContextProvider } from './providers/user-context-provider.model';
import { BasicLayoutComponent } from './components/layout/basic-layout/basic-layout.component';
import { NavigationService } from './services/navigation.service';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { TranslationProvider } from './providers/translation-provider.model';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { SharedModule } from './shared/shared.module';
import { FooterComponent } from './components/footer/footer.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { TopbarComponent } from './components/topbar/topbar.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { LocaleCalendarProvider } from './providers/locale-calendar-provider.model';
import { ConfirmationService } from 'primeng/api';
import { UserSecurityModule } from './directives/user-security.directive';
import { MenuComponent } from './components/layout/basic-layout/menu/menu.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    FormErrorComponent,
    BlankLayoutComponent,
    RefreshComponent,
    BasicLayoutComponent,
    FooterComponent,
    TopbarComponent,
    MenuComponent
  ],
  imports: [
    UserSecurityModule,
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    NgbModule,
    FontAwesomeModule,
    HttpClientModule,
    NgxSpinnerModule,
    NgScrollbarModule,
    FlexLayoutModule,
    TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
      }
    }),
    ToastrModule.forRoot({
      timeOut: 1500,
      progressBar: true,
      preventDuplicates: true
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5000","localhost:44363"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    AuthenticationService,
    NavigationService, 
    TranslationProvider,
    TokenStorageProvider,
    UserContextProvider,
    LocaleCalendarProvider,
    HttpClient,
    ConfirmationService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
