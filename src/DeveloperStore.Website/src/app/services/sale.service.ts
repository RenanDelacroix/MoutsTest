// src/app/services/sale.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private apiUrl = 'https://localhost:44345/api/Sale';

  constructor(private http: HttpClient) { }

  getSales(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?orderBy=createdAt&direction=desc&pageNumber=1&pageSize=10`);
  }

  createSale(sale: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, sale);
  }

  cancelSale(id: string) {
    return this.http.patch<any>(`https://localhost:44345/api/Sale/${id}/cancel`, {});
  }
}
