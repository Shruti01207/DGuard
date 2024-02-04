import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IntroductionComponent } from './introduction.component';
import { AboutComponent } from './about/about.component';
import { TermsOfServiceComponent } from './terms-of-service/terms-of-service.component';


const routes: Routes = [

    {
        path: 'introduction',
        component: IntroductionComponent,
        children:[
          {
            path:'',
            component: AboutComponent
          },
          {
            path:'terms-of-services',
            component: TermsOfServiceComponent
          }
        ]
   
    },   
    
     { path: '', component: AboutComponent  }
    
      
  
   
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class IntroductionRoutingModule { }