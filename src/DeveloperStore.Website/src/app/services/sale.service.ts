import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateSaleRequest } from '../models/sale.model';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private apiUrl = 'https://localhost:44345/api/Sale'; 

  constructor(private http: HttpClient) {}

  getSales(pageNumber: number = 1, pageSize: number = 10, orderBy: string = 'createdAt', direction: string = 'desc') {
    const url = `${this.apiUrl}?orderBy=${orderBy}&direction=${direction}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
    return this.http.get<any>(url);
  }

  createSale(sale: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, sale);
  }
  
}
