import { Component } from '@angular/core';
import { FoodService } from './core/services/food/food.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import Swal from 'sweetalert2';
import { RequestModel } from './core/services/food/models/request';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Metro Fibre App';

  insertForm!: FormGroup;
  pasta!: FormControl;
  pie!:FormControl;
  pizza!: FormControl;
  salad!: FormControl;
  burger!:FormControl;
  constructor(private foodService: FoodService,
    private spinnerService: NgxSpinnerService,
   private router: Router, private fb: FormBuilder) {

 }

 ngOnInit() {

  


  this.pasta = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.pie = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.pizza = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.salad = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.burger = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);

    this.insertForm = this.fb.group({

      "pasta": this.pasta,
      "pie":this.pie,
      "pizza":this.pizza,
      "salad":this.salad,
      "burger":this.burger

    });


 }

 onSave() 
 {

  this.spinnerService.show();
  let details= this.insertForm.value;

  if(details.pasta===null || details.pasta===undefined || details.pasta==="" || details.namepasta==" " || details.pasta<0)
  {
    this.spinnerService.hide();
    Swal.fire("Category", "Please provide the category", "error");
    return;
  }


  if(details.pie===null || details.pie===undefined || details.pie==="" || details.pie===" " || details.pie<0)
  {
    this.spinnerService.hide();
    Swal.fire("Category", "Please provide the category", "error");
    return;
  }


  if(details.pizza===null || details.pizza===undefined || details.pizza==="" || details.pizza===" " || details.pizza<0)
  {
    this.spinnerService.hide();
    Swal.fire("Category", "Please provide the category", "error");
    return;
  }


  if(details.salad===null || details.salad===undefined || details.salad==="" || details.salad===" " || details.salad<0)
  {
    this.spinnerService.hide();
    Swal.fire("Category", "Please provide the category", "error");
    return;
  }

  let requestModel=new RequestModel();
 
  // this.foodService.Make(category)?.subscribe((result:any) => {
  //   debugger;
  //   this.spinnerService.hide();
    
   
  //   return;


  // },
  // (error:any)=> {
  //     console.log(error);
  //     this.spinnerService.hide();

  //     this.insertForm.controls['name'].enable();
  //     this.insertForm.controls['description'].enable();
  //   }
  // );


 }

}
