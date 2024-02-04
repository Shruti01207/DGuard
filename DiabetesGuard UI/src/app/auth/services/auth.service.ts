import { Injectable, inject } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';
import { SignupRequest } from '../models/signup-request.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor() { }

  private http = inject(HttpClient);
  private cookieService = inject(CookieService);

  login(request: LoginRequest): Observable<LoginResponse> {

    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/Auth/login`, {
      
      email: request.email,
      password: request.password
    });
  }
  setAuthCookieAndUser(response: LoginResponse) {
    this.cookieService.set('Authorization', `Bearer ${response.token}`,
      undefined, '/', undefined, true, 'Strict');

    this.setUser({
      email: response.email,
      roles: response.roles
    });

  }

  setUser(user: User): void {
    this.$user.next(user);// new values will
    // be emitted to any suscriber of this observable
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-roles', user.roles.join(','));

  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable()
  }

  logOut(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }


  signUp(request: SignupRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Auth/register`, request);
  }

  getUser(): User | undefined {

    const email = localStorage.getItem("user-email");
    const roles = localStorage.getItem("user-roles");

    if (email && roles) {
      const user: User = {
        email: email,
        roles: roles.split(',')
      };
      return user;
    }

    return undefined;

  }



}
