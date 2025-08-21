import { homePageService } from './homepageService';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Bookcard } from './bookcard/bookcard';
import { book } from './bookModel';
import { ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, RouterModule , Bookcard],
  templateUrl: './homepage.html',
  styleUrls: ['./homepage.css']
})
export class Homepage implements OnInit {
  isLoading:boolean = true
  slowReadingBook:string = "https://covers.openlibrary.org/b/id/5546156-M.jpg"
  allBooks : book[] = []
  constructor(public homePageServ : homePageService,public cd:ChangeDetectorRef){}

  ngOnInit(): void {
    this.homePageServ.initializeScreen()
  }
  
  signout(){
    this.homePageServ.signout()
  }

  issueBook(bookId:number){
    this.homePageServ.issueBook(bookId).subscribe({
      next:()=>{
        alert("book issue");
      },
      error:()=>{
        alert("error occured");
      }
    })
  }

}
