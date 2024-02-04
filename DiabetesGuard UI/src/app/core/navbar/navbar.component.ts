import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/auth/models/user.model';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
 
})
export class NavbarComponent implements OnInit {

  loggedInUser?: User;
 
  private route=inject(Router);

  ngOnInit(): void {
    this.authService.user().subscribe({
      next: (response) => {
        if (response == undefined && localStorage.getItem('user-email')) {
          this.loggedInUser = {
            email: localStorage.getItem('user-email') as string,
            roles: localStorage.getItem('user-roles')?.split(',') as string[]
          }
        }
        else {
          this.loggedInUser = response;
        }
        console.log("current user", response);
      }
    });
  }

  private authService = inject(AuthService);

  logOut( ){
    this.authService.logOut( );
    this.route.navigateByUrl('login');
  }








}
