import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private apiUrl = `${environment.apiUrl}/api/Sale`;

  constructor(private http: HttpClient) { }

  getSales(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?orderBy=createdAt&direction=desc&pageNumber=1&pageSize=10`);
  }

  createSale(sale: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, sale);
  }

  cancelSale(id: string) {
    return this.http.patch<any>(`${this.apiUrl}/${id}/cancel`, {});
  }
}
