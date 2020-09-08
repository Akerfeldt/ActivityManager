import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EffectsModule } from '@ngrx/effects';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { OAuthModule, OAuthResourceServerErrorHandler } from 'angular-oauth2-oidc';

import { environment } from '../environments/environment';
import { AppMaterialModule } from './app-material.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppNavComponent, AuthorizedComponent, DashboardComponent, ErrorComponent, LoginComponent } from './components';
import { metaReducers, reducers } from './root-store/reducer';
import { RootStoreModule } from './root-store/root-store.module';
import { UnauthorizedErrorHandlerService } from './services/unauthorized-error-handler.service';

@NgModule({
  declarations: [
    AppComponent,
    AppNavComponent,
    AuthorizedComponent,
    DashboardComponent,
    ErrorComponent,
    LoginComponent
  ],
  imports: [
    AppMaterialModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    HttpClientModule,
    LayoutModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ['https://localhost:44315/api'],
        sendAccessToken: true
      }
    }),
    StoreModule.forRoot(reducers, {
      metaReducers,
      runtimeChecks: {
        strictStateImmutability: false,
        strictActionImmutability: true
      }
    }),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
    EffectsModule.forRoot([]),
    StoreRouterConnectingModule.forRoot(),
    RootStoreModule
  ],
  providers: [{
    provide: OAuthResourceServerErrorHandler,
    useExisting: UnauthorizedErrorHandlerService
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
