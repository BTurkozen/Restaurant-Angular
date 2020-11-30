import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/users/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private authService: AuthService) {}

  isAuthenticated: boolean ;

  ngOnInit(): void {
     this.isAuthenticated = this.authService.isAuthenticated;
  }
}
