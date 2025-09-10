import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { book } from '../homepage/bookModel';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { RouterModule,Router } from '@angular/router';
import { MyBooksService } from './mybooksService';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-mybooks',
  imports: [],
  templateUrl: './mybooks.html',
  styleUrl: './mybooks.css'
})
export class Mybooks implements OnInit{
    issuedBooks : book[] = []
    constructor(private route: ActivatedRoute,private router:Router,private mybooksservice: MyBooksService){}
    ngOnInit(): void {
        this.route.data.subscribe((data) =>{
            this.issuedBooks = data['userIssuedBooks']
            
        })
    }

    returnBook(dabook:book):void{
        let username:string | null = localStorage.getItem("username")
        let user:string = username? username: '';
        
        this.mybooksservice.returnBookApi(dabook.bookId,user).subscribe((res : HttpResponse<any>) =>{
            console.log(res)
        })
    }
    
}
