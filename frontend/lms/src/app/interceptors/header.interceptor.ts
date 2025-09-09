import { HttpClient, HttpErrorResponse, HttpEvent, HttpHandler, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Inject, inject } from '@angular/core';
import { catchError, finalize, switchMap } from 'rxjs/operators'; // Import finalize
import { error } from 'console';
import { GetAcessToken , AccessTokenResponse } from '../getAcessToken';
import { Observable,throwError } from 'rxjs';

export const HeaderInterceptor:HttpInterceptorFn =  (req:HttpRequest<any>,next:HttpHandlerFn) =>{
    if(req.url.includes('/getaccesstoken')){
        return next(req);
    }
    const getaccesstoken : GetAcessToken = Inject(GetAcessToken);
    let AcessToken:string |null  = localStorage.getItem("AcessToken");
    let RefreshToken:string | null = localStorage.getItem("RefreshToken");
    if(!AcessToken || AcessToken === ''){
        console.log("No token in local storage configure a token");
        return next(req);
    }
    let reqClone:HttpRequest<any> = req.clone({
        headers : req.headers.append('Authorization',`Bearer ${AcessToken}`)
    });
    return next(reqClone).pipe(
  catchError(err => {
    if (err.status === 401) {
      // Step 1: call the token refresh method
      return getaccesstoken.getaccesstoken().pipe(

        // Step 2: switch to retrying the original request
        switchMap((newTokenResponse:AccessTokenResponse) => {
          const newAccessToken = newTokenResponse.accesstoken;

          // Step 3: clone the original request with the new token
          const newReq = req.clone({
            headers: req.headers.set('Authorization', `Bearer ${newAccessToken}`)
          });

          // Step 4: resend the request
          return next(newReq);
        })
      );
    }

    // For other errors, just rethrow
    return throwError(() => err);
  })
);


}