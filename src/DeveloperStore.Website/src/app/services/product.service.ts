import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = 'https://localhost:44345/api/Product';

  constructor(private http: HttpClient) { }

  getProducts(pageNumber = 1, pageSize = 10, orderBy = 'name', direction = 'desc') {
    const params = new HttpParams()
      .set('orderBy', orderBy)
      .set('direction', direction)
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    return this.http.get<any>(this.apiUrl, { params });
  }
}
