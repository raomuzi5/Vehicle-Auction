import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddVehicleCommand } from '../model/AddVehicleCommand';
import { CalculationResult } from '../model/calculation-result';
import { VehicleService } from '../service/vehicle-service';
import { ApiResponse } from '../model/ApiResponse';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle-auction.component.html',
  styleUrls: ['./vehicle-auction.component.css']
})
export class VehicleComponent implements OnInit {

  commandForm: FormGroup;
  command: AddVehicleCommand = {
    basePrice: null,
    carType: ''
  };

  calculationResult: CalculationResult;

  constructor(private vehicleService: VehicleService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.commandForm = this.fb.group({
      basePrice: [null, Validators.required],
      carType: ['', Validators.required]
    });

    // Optionally, you can subscribe to value changes in the form
    this.commandForm.valueChanges.subscribe(() => {
      this.calculateTotalCost();
    });
  }

  calculateTotalCost(): void {
    if (this.commandForm.valid) {
      // Update command with form values
      this.command.basePrice = this.commandForm.value.basePrice;
      this.command.carType = this.commandForm.value.carType;

      // Call the API
      this.vehicleService.calculateTotalCost(this.command)
        .subscribe((response: ApiResponse<CalculationResult>) => {
          if (response.success) {
            this.calculationResult = response.data;
          } else {
            console.error('Failed to calculate total cost:', response.message);
          }
        }, error => {
          console.error('API Error:', error);
        });
    }
  }
}
