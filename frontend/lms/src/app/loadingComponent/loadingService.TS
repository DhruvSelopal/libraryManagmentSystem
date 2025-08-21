import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn:"root"
}) export class LoadingService{

    private _loadinCount = 0;

    private loading = new BehaviorSubject<boolean>(false);
    $loading = this.loading.asObservable();

    loadingOn():void{

        this._loadinCount++;

        if(this._loadinCount === 1) this.loading.next(true); // turn on loading only when req becomes1
    }
    loadingOf():void{

        this._loadinCount--;

        if(this._loadinCount === 0) this.loading.next(false);

        if(this._loadinCount <0) this._loadinCount = 0;
    }
}