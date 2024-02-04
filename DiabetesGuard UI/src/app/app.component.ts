import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DiabetesGuard';

  
  compSequence: Array<string> = ['/introduction/', '/introduction/terms-of-services','/medical-background/'];
  currIndex: number = 0;
  private router = inject(Router);

  previous() {

    const len = this.compSequence.length;
    if (this.currIndex <= 0) {
      this.currIndex = len - 1;
    }
    else {
      this.currIndex = (this.currIndex - 1) % len;
    }
    console.log("this.currIndex=", this.currIndex);

    this.router.navigate([this.compSequence[this.currIndex]]);
  }

  next() {

    const len = this.compSequence.length;
    this.currIndex = (this.currIndex + 1) % len;
    console.log("this.currIndex=", this.currIndex); 
    this.router.navigate([this.compSequence[this.currIndex]]);
  }

}
