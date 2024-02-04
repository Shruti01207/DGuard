import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './core/navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { SidebarComponent } from "./core/sidebar/sidebar.component";
import { AppRoutingModule } from './app-routing.module';
import { IntroductionModule } from './introduction/introduction.module';
import { IntroductionRoutingModule } from './introduction/introduction-routing.module';
import { IntroductionComponent } from './introduction/introduction.component';
import { MedicalBackgroundModule } from './medical-background/medical-background.module';

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        SignUpComponent,
        NavbarComponent,
        HomeComponent,
        SidebarComponent
       
    ],
    providers: [],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        FormsModule,
        BrowserAnimationsModule,
        MaterialModule,
        AppRoutingModule,
        IntroductionModule,
        IntroductionRoutingModule,
        MedicalBackgroundModule

       
    ],
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
      ],
})
export class AppModule { }
