import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { IntroductionComponent } from './introduction.component';
import { AboutComponent } from './about/about.component';
import { IntroductionRoutingModule } from './introduction-routing.module';
import { NavbarComponent } from '../core/navbar/navbar.component';
import { MaterialModule } from '../material/material.module';
import { FooterComponent } from './shared/footer/footer.component';
import { TermsOfServiceComponent } from './terms-of-service/terms-of-service.component';


@NgModule({
  declarations: [
   IntroductionComponent,
   AboutComponent,
   FooterComponent,
   TermsOfServiceComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    IntroductionRoutingModule,
    MaterialModule
  ],
  exports:[
   
  ]
})
export class IntroductionModule { }
