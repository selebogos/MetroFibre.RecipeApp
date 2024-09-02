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
  cucumber!: FormControl;
  tomato!: FormControl;
  meat!: FormControl;
  cheese!: FormControl;
  dough!: FormControl;
  olives!: FormControl;
  lettuce!: FormControl;

  constructor(private foodService: FoodService,
    private spinnerService: NgxSpinnerService,
    private router: Router, private fb: FormBuilder) {

  }

  ngOnInit() {

    this.cucumber = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.meat = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.dough = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.tomato = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.olives = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.tomato = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);
    this.lettuce = new FormControl('', [Validators.required, Validators.maxLength(25), Validators.minLength(2)]);

    this.insertForm = this.fb.group({

      "cucumber": this.cucumber,
      "meat": this.meat,
      "olives": this.olives,
      "dough": this.dough,
      "tomato": this.tomato,
      "cheese": this.cheese,
      "lettuce": this.lettuce
      
    });
  }

  onSave() {

    this.spinnerService.show();
    let details = this.insertForm.value;

    if (details.cucumber === null || details.cucumber === undefined || details.cucumber === "" || details.cucumber == " " || details.cucumber < 0) {
      this.spinnerService.hide();
      Swal.fire("Recipe Optimizer", "Please provide the Qty for cucumber Ingredient", "error");
      return;
    }


    if (details.olives === null || details.olives === undefined || details.olives === "" || details.olives === " " || details.olives < 0) {
      this.spinnerService.hide();
      Swal.fire("Recipe Optimizer", "Please provide the Qty for olives Ingredients", "error");
      return;
    }


    if (details.lettuce === null || details.lettuce === undefined || details.lettuce === "" || details.lettuce === " " || details.lettuce < 0) {
      this.spinnerService.hide();
      Swal.fire("Recipe Optimizer", "Please provide the Qty for lettuce Ingredients", "error");
      return;
    }


    if (details.meat === null || details.meat === undefined || details.meat === "" || details.meat === " " || details.meat < 0) {
      this.spinnerService.hide();
      Swal.fire("Recipe Optimizer", "Please provide the Qty for meat Ingredients", "error");
      return;
    }
    if (details.tomato === null || details.tomato === undefined || details.tomato === "" || details.tomato === " " || details.tomato < 0) {
      this.spinnerService.hide();
      Swal.fire("Recipe Optimizer", "Please provide the Qty for tomato Ingredients", "error");
      return;
    }

    if (details.cheese === null || details.cheese === undefined || details.cheese === "" || details.cheese === " " || details.cheese < 0) {
      this.spinnerService.hide();
      Swal.fire("Recipe Optimizer", "Please provide the Qty for cheese Ingredients", "error");
      return;
    }
    if (details.dough === null || details.dough === undefined || details.dough === "" || details.dough === " " || details.dough < 0) {
      this.spinnerService.hide();
      Swal.fire("Recipe Optimizer", "Please provide the Qty for dough Ingredients", "error");
      return;
    }

    let ingredients: Array<RequestModel> = [];
    let requestModel = new RequestModel();

    requestModel.name = "dough";
    requestModel.quantity = details.dough;
    ingredients.push(requestModel);

    requestModel = new RequestModel();
    requestModel.name = "cheese";
    requestModel.quantity = details.cheese;
    ingredients.push(requestModel);

    requestModel = new RequestModel();
    requestModel.name = "tomato";
    requestModel.quantity = details.tomato;
    ingredients.push(requestModel);

    requestModel = new RequestModel();
    requestModel.name = "lettuce";
    requestModel.quantity = details.lettuce;
    ingredients.push(requestModel);

    requestModel = new RequestModel();
    requestModel.name = "olives";
    requestModel.quantity = details.olives;
    ingredients.push(requestModel);

    requestModel = new RequestModel();
    requestModel.name = "cucumber";
    requestModel.quantity = details.cucumber;
    ingredients.push(requestModel);


    this.foodService.Make(ingredients)?.subscribe((result: any) => {
      debugger;
      this.spinnerService.hide();

    
      Swal.fire("Recipe Optimizer", result.message.results, "success");
      return;


    },
      (error: any) => {
        console.log(error);
        this.spinnerService.hide();

      }
    );


  }

}
