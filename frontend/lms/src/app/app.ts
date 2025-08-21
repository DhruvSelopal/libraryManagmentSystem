import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { loadingComponent } from './loadingComponent/loading';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet , loadingComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'lms';
}
