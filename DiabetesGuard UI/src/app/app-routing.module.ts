import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './auth/guard/auth.guard';
import { AboutComponent } from './introduction/about/about.component';
import { IntroductionComponent } from './introduction/introduction.component';
import { MedicalBackgroundComponent } from './medical-background/medical-background.component';

const routes: Routes = [

    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },

    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'signup',
        component: SignUpComponent
    },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [authGuard]
    },
    {
        path: 'introduction',
        redirectTo:'introduction',
        pathMatch:'full'
    },
    {
     path:'medical-background',
     redirectTo:'medical-background',
        pathMatch:'full'
     
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }