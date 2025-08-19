import { Injectable } from "@angular/core";
import { routes } from "../app.routes";
import { HttpClient } from "@angular/common/http";
import { book } from "./bookModel";
import { error } from "node:console";
import { Observable } from "rxjs";

@Injectable({
    providedIn:"root"
})
export class homePageService{
    username:string | null = null;
    constructor (private http : HttpClient){}
    initializeScreen():Observable<book[]>{
        return this.http.get<book[]>("http://localhost:5132/books")
    }
}