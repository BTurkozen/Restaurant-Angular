import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(private authService: AuthService) {}

  loginData = {
    UserName: '',
    Password: '',
  };

  login() {
    this.authService.login(this.loginData);
    console.log('this.loginData =>', this.loginData);
  }
}
