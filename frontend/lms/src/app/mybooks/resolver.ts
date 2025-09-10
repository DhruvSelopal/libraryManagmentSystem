import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { book } from "../homepage/bookModel";
import { inject } from "@angular/core";
import { HttpClient,HttpErrorResponse,HttpResponse } from "@angular/common/http";
import { catchError, map, throwError,tap } from "rxjs";
import { error } from "console";

export const dataResolver: ResolveFn<book[]> = (
    route:ActivatedRouteSnapshot,
    state:RouterStateSnapshot
) =>{
    console.log("resolver running")
    const http:HttpClient = inject(HttpClient);

    const username:string | null  = localStorage.getItem("username")
    const user:string = username? username : ''
    console.log(user)
    return http.get<book[]>(`http://192.168.6.55:5000/user/getbooks/${user}`)
    .pipe(
        tap((data:book[]) => console.log((data)))
    )
}