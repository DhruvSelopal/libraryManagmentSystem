import { Inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

export interface AccessTokenResponse{
    accesstoken:string
}

@Injectable({
    providedIn:"root"
}) export class GetAcessToken{
    http:HttpClient = Inject(HttpClient);
    url:string = "http://192.168.6.55:5000/user/getaccesstoken";

    getaccesstoken():Observable<AccessTokenResponse>{
        let headers:HttpHeaders = new HttpHeaders({
            "refreshtoken": `Bearer ${localStorage.getItem("RefreshToken")}`
        })
        return this.http.get<AccessTokenResponse>(this.url,{headers});
    }
}