import { error } from 'node:console';
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { book } from '../homepage/bookModel';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private http = inject(HttpClient);

  private userSubject = new BehaviorSubject<string>("");
  user$ = this.userSubject.asObservable();



  login(username: string, password: string): Observable<object> {
    return this.http.post('http://localhost:5132/user/login', {
        "Username" : username,
        "Password" : password
    });
  }
}