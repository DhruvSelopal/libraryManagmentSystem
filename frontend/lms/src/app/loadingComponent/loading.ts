import { Component } from "@angular/core";
import {LoadingService} from "./loadingService"

@Component({
    selector:"loading",
    standalone:true,
    templateUrl:"./loading.html",
    styleUrl:"./loading.css"
}) export class loadingComponent{
    shouldLoad:boolean = false;
    constructor(private ls: LoadingService){}

    ngOnInit(){
        this.ls.$loading.subscribe(data=>{
            this.shouldLoad = data;
        })
    }
}