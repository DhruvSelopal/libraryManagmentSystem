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
  constructor(public homePageServ : homePageService,public cd:ChangeDetectorRef,private router:Router){}
  
  ngOnInit(): void {
    this.homePageServ.initializeScreen().subscribe({
  next: (books: book[]) => {
    this.allBooks = books;
    for(let book of this.allBooks){
      console.log(book.bookName + " " + book.bookCount);
    }
  },
  error: (error:any) => {
    alert(error);
    console.error('Error:', error);
  },

  complete:()=>{
    this.isLoading = false;
    this.cd.detectChanges()
  }

  });
  
  }

  signout():void{
    this.router.navigate(['/login'])
  }
}
