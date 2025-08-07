import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateSaleRequest } from '../models/sale.model';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private apiUrl = 'http://localhost:5000/api/sales'; 

  constructor(private http: HttpClient) {}

  getSales(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  createSale(sale: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, sale);
  }
  
}
