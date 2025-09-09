import { error } from 'node:console';
import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { book } from '../homepage/bookModel';

export interface LoginResponse {
  acesstoken:string,
  refreshtoken:string
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private http = inject(HttpClient);

  private userSubject = new BehaviorSubject<string>("");
  user$ = this.userSubject.asObservable();



  login(username: string, password: string): Observable<LoginResponse> {
    debugger
    return this.http.post<LoginResponse>('http://192.168.6.55:5000/user/login', {
        "Username" : username,
        "Password" : password
    });
  }

}