// vehicle-auction.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vehicle } from '../model/Vehicle';
import { CalculationResult } from '../model/calculation-result';
import { AddVehicleCommand } from '../model/AddVehicleCommand';
import { ApiResponse } from '../model/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

    private apiUrl = 'https://localhost:7191/api/vehicle'; // Update with your API URL
  
    constructor(private http: HttpClient) { }
  
    calculateTotalCost(command: AddVehicleCommand): Observable<ApiResponse<CalculationResult>> {
      return this.http.post<ApiResponse<CalculationResult>>(`${this.apiUrl}/calculate`, command);
    }
  }