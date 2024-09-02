import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ResultModel } from './models/results';
import { RequestModel } from './models/request';
import { map } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  private domain:string;
  private baseUrllFood: string = "/api/foodcreator/";

  constructor(private http: HttpClient, private router: Router) {
    this.domain=environment.domain;
  }

  public Make(ingredients: RequestModel[]):any {

    try {
      
      return this.http.post<any>(this.domain+this.baseUrllFood,ingredients).pipe(
        map(result => {

          return result;

        }, 
        (error:any) => {
          return error;
        }));

    } catch (e) {

    }
  }
}
