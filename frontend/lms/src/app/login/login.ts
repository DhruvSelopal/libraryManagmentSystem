import { homePageService } from './../homepage/homepageService';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { LoginService } from './loginService';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent {
  username: string = 'dhruv';
  password: string = 'dhruv@123';

  private loginService = inject(LoginService);

  constructor(private hs : homePageService){}

  login() {
    console.log(this.username);
    
    if(this.username.trim().length === 0 || this.password.trim().length === 0) {
      alert("Fields can't be empty");
      return;
    }
    this.loginService.login(this.username,this.password)

  }
}