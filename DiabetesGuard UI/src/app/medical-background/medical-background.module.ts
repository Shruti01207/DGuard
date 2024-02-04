import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MedicalBackgroundRoutingModule } from './medical-background-routing.module';
import { RouterModule } from '@angular/router';
import { MedicalBackgroundComponent } from './medical-background.component';
import { DiabetesStatusComponent } from './diabetes-status/diabetes-status.component';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    MedicalBackgroundComponent,
    DiabetesStatusComponent
  ],
  imports: [
    CommonModule,
    MedicalBackgroundRoutingModule,
    RouterModule,
    ReactiveFormsModule,
    MaterialModule
  ]
})
export class MedicalBackgroundModule { }
