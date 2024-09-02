import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})

export class RequestModel {

    name!:string;
    quantity!:number;
}