import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {}

  Token_Key = 'token';
  Name_Key = 'name';

  get isAuthenticated() {
    return !!localStorage.getItem(this.Token_Key);
  }

  logOut() {
    localStorage.removeItem(this.Token_Key);
    localStorage.removeItem(this.Name_Key);
  }

  login(loginData: any) {
    this.http
      .post(environment.apiUrl + '/api/auth/login', loginData)
      .subscribe((res: any) => {
        var authResponse = res;
        if (authResponse != null) {
          localStorage.setItem(this.Token_Key, authResponse.token);
          localStorage.setItem(this.Name_Key, authResponse.username);
          console.log(localStorage.getItem(this.Token_Key));
          this.router.navigate(['/']);
        }
      });
  }

  register(user: any) {
    delete user.confirmPassword;
    this.http
      .post(environment.apiUrl + '/api/auth/register', user)
      .subscribe((res: any) => {
        console.log('res =>', res);
        var authResponse = res;
        // localStorage.setItem(this.Token_Key, res.token);
        // localStorage.setItem(this.Name_Key, res.usename);
        console.log(localStorage.getItem(this.Token_Key));
        this.router.navigate(['/']);
      });
  }
}
