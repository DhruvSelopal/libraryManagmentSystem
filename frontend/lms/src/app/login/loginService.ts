import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private http = inject(HttpClient);

  login(username: string, password: string): Observable<any> {
    return this.http.post('http://localhost:5132/user/login', {
        "Username" : username,
        "Password" : password
    });
  }
}