import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';




@Component({
  selector: 'app-diabetes-status',
  templateUrl: './diabetes-status.component.html',
  styleUrl: './diabetes-status.component.css'
})
export class DiabetesStatusComponent implements OnInit {



// reactive form

  diabetesStatus!: FormGroup;

private fb = inject(FormBuilder);

ngOnInit(): void { 
  this.diabetesStatus=this.fb.group(
    {
      isDiabetic:['',Validators.required],
      diabetesType:['',Validators.required]
    }
  )
}

Save( ){
  if(this.diabetesStatus.valid){
    console.log("this.diabetesStatus.value=",this.diabetesStatus.value);
  }
  else{
    console.warn("Invalid form");
  }
  
}


}
