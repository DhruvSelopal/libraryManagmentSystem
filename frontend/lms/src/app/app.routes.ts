import { Routes } from '@angular/router';
import { LoginComponent } from './login/login';
import { Signup } from './signup/signup';
import { Homepage } from './homepage/homepage';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: Signup},
  { path: 'homepage', component: Homepage}
];
