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

  getSales(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`, {
      params: {
        orderBy: 'number',
        direction: 'desc',
        pageNumber: pageNumber.toString(),
        pageSize: pageSize.toString()
      }
    });
  }

  createSale(sale: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, sale);
  }

  cancelSale(id: string) {
    return this.http.patch<any>(`${this.apiUrl}/${id}/cancel`, {});
  }

  paySale(id: string) {
    return this.http.patch<{ message: string }>(`${this.apiUrl}/${id}/pay`, {});
  }

  getSaleById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
}
