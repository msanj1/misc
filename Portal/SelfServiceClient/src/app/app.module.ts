import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { SigningRedirectCallbackComponent } from './home/signin-redirect-callback.components';
import { SignoutRedirectCallbackComponent } from './home/singout-redirect-callback.components';
import { UnAuthorizedComponent } from './home/unauthorized.component';
import { CoreModule } from '../core/core.module';
import { MaterialDesignModule } from '../material-design/material-design.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SigningRedirectCallbackComponent,
    SignoutRedirectCallbackComponent,
    UnAuthorizedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoreModule,
    MaterialDesignModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
