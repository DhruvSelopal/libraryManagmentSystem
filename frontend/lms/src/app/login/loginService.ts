import { error } from 'node:console';
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { book } from '../homepage/bookModel';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private http = inject(HttpClient);

  private userSubject = new BehaviorSubject<string>("");
  user$ = this.userSubject.asObservable();

    private router = inject(Router);


  login(username: string, password: string): string {
    this.http.post('http://localhost:5132/user/login', {
        "Username" : username,
        "Password" : password
    }).subscribe(
      {
      next: (response) => {
        console.log("login successful")
        this.userSubject.next(username)
        this.router.navigate(["/homepage"])
      },
      error: (error) => {
        alert("Error occurred");
        console.error('Login error:', error);
      }
    })

    return "";
  }
}