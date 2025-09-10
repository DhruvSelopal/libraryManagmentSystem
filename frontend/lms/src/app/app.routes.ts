import { Mybooks } from './mybooks/mybooks';
import { Routes } from '@angular/router';
import { LoginComponent } from './login/login';
import { Signup } from './signup/signup';
import { Homepage } from './homepage/homepage';
import { dataResolver } from './mybooks/resolver'

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {path:'mybooks',component:Mybooks,resolve:{userIssuedBooks:dataResolver}},
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: Signup},
  { path: 'homepage/:username', component: Homepage }
];
