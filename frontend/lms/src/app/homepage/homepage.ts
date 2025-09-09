import { homePageService } from './homepageService';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Bookcard } from './bookcard/bookcard';
import { book } from './bookModel';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, RouterModule , Bookcard ],
  templateUrl: './homepage.html',
  styleUrls: ['./homepage.css']
})
export class Homepage implements OnInit {
  username:string | null  = ""
  isLoading:boolean = true
  slowReadingBook:string = "https://covers.openlibrary.org/b/id/5546156-M.jpg"
  allBooks : book[] = []
  constructor(public homePageServ : homePageService,public cd:ChangeDetectorRef,private route : ActivatedRoute ){}

  ngOnInit(): void {
    debugger
    this.username = this.route.snapshot.paramMap.get("username")
    console.log(this.username)
     this.homePageServ.getusername(this.username);

    this.homePageServ.initializeScreen().subscribe({
      next:(data:book[])=>{
        for(let i:number =  0; i < data.length;i++){
          console.log(data[i].bookName + "    " + data[i].bookDescription)
        }
        this.allBooks = data;
        this.cd.detectChanges()
      },
      error:(err:HttpErrorResponse)=>{
        alert(err.message);
      }
    });
  }
  
  signout(){
    this.homePageServ.signout()
  }

  issueBook(bookId:number,i:number){
    debugger
    this.homePageServ.issueBook(bookId).subscribe({
      next:()=>{
        console.log("books issued")
        this.allBooks[i].bookCount--;
        console.log(this.allBooks[i].bookCount)

      },
      error:(err:HttpErrorResponse)=>{
        alert(err.statusText);
      }
    })
  }

}
