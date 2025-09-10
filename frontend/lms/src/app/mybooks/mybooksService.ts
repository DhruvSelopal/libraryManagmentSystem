import { HttpClient, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "express";
import { Observable } from "rxjs";

@Injectable({
    providedIn:"root"
}) export class MyBooksService{
    constructor(private http:HttpClient){}
    returnBookApi(bookid:Number,username:string):Observable<HttpResponse<any>>{
        return this.http.get<HttpResponse<any>>(`http://192.168.6.55:5000/user/bookreturn/${username}/${bookid}`);
    }
}