import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BranchService {
  private apiUrl = 'https://localhost:44345/api/Branches'; // ajuste conforme seu backend

  constructor(private http: HttpClient) { }

  getBranches(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
