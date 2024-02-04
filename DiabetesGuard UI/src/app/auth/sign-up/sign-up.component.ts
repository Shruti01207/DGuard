import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit, OnDestroy {
  
  
 
  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      
      email: ['', [Validators.required,Validators.email]],
      password: ['', Validators.required],
      username:['', Validators.required], 
      gender:['female', [Validators.required]],
      DateOfBirth: ['', Validators.required]

    });
  }
   ngOnDestroy(): void {
    this.authServiceSubscription?.unsubscribe();
  }
  passwordType: string = 'password';
  visibilityIcon: string = 'fa-eye-slash';

  authServiceSubscription?: Subscription

  signUpForm!: FormGroup;

  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private route = inject(Router);


  showHidePassword() {
    this.passwordType = (this.passwordType == 'password') ? 'text' : 'password';
    this.visibilityIcon = (this.passwordType == 'text') ? 'fa-eye' : 'fa-eye-slash';
  }
  onSubmit() {
    if (this.signUpForm.valid) {
      // send data to database
      this.authServiceSubscription = this.authService.signUp(this.signUpForm.value).
        subscribe({
          next: (response) => {
            console.log("response", "signup done");
            this.authService.logOut();
            this.authServiceSubscription = this.authService.login(this.signUpForm.value).
              subscribe({
                next: (response) => {
                  this.authService.setAuthCookieAndUser(response);
                  this.route.navigateByUrl('home');
                }
              });
          }
        });
      //console.log("form-data",this.signUpForm.value);
    }
    else {
      console.log("form is not valid");
      this.validateAllFormFields(this.signUpForm);
    }
  }

  private validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {

      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsDirty({ onlySelf: true })
      }
      else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }
  register( ){
    if(this.signUpForm.valid){
      this.authServiceSubscription = this.authService.signUp(this.signUpForm.value).
      subscribe({
        next: (response) => {
          console.log("register response",response );
          this.authService.logOut();
          // this.authServiceSubscription = this.authService.login(this.signUpForm.value).
          //   subscribe({
          //     next: (response) => {
          //       this.authService.setAuthCookieAndUser(response);
          //       this.route.navigateByUrl('home');
          //     }
          //   });
          this.route.navigateByUrl("/login");
        }
      });
    }
    else{
      console.warn("Invalid form");
    }
    
  }



}
