import { Component } from "@angular/core";
import {LoadingService} from "./loadingService"
import { Observable } from "rxjs";
import { AsyncPipe } from "@angular/common";

@Component({
    selector:"loading",
    standalone:true,
    templateUrl:"./loading.html",
    styleUrl:"./loading.css",
    imports: [AsyncPipe]
}) export class loadingComponent{

    shouldload: Observable<boolean> = new Observable<boolean>();
    constructor(private ls: LoadingService){}

    ngOnInit(){
        this.shouldload = this.ls.$loading
    }
}