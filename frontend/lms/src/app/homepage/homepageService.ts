import { Injectable } from "@angular/core";
import { routes } from "../app.routes";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { book } from "./bookModel";
import { error } from "node:console";
import { Observable } from "rxjs";
import { Router } from '@angular/router';
import { LoginService } from "../login/loginService";
import { FormsModule } from "@angular/forms";

@Injectable({
    providedIn:"root"
})
export class homePageService{
    username:string | null = ""
    constructor (private http : HttpClient,private router:Router,private ls : LoginService){}


    getusername(username:string | null):void{
        this.username = username
        console.log(this.username)
    }

    initializeScreen():Observable<book[]>{
        return this.http.get<book[]>("http://localhost:5132/books");
    }

    signout():void{
    this.router.navigate(['/login'])
    }

    issueBook(bookId:number):Observable<void>{
        return this.http.get<void>(`http://localhost:5132/user/bookissue/${this.username}/${bookId}`)
    }
}