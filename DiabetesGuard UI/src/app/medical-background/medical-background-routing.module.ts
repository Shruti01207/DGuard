import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicalBackgroundComponent } from './medical-background.component';
import { DiabetesStatusComponent } from './diabetes-status/diabetes-status.component';

const routes: Routes = [

 {
  path:'medical-background',
  component: MedicalBackgroundComponent,
  children:[
    {
      path:'',
      component: DiabetesStatusComponent
    }
  ]
 }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicalBackgroundRoutingModule { }
