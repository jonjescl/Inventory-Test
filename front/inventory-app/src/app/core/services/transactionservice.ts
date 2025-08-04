import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction';
import { Response } from '../models/response';


@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  constructor() {}
  private http=inject(HttpClient);
  private apiUrl=environment.apiTransactionUrl;

  getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.apiUrl}Transactions`);
  }

  getById(id: number): Observable<Transaction> {
    return this.http.get<Transaction>(`${this.apiUrl}Transactions/${id}`);
  }

  create(product: Transaction): Observable<Response> {
    return this.http.post<Response>(`${this.apiUrl}Transactions`, product);
  }

  update(id: number, product: Transaction): Observable<Response> {
    return this.http.put<Response>(`${this.apiUrl}Transactions/${id}`, product);
  }

  delete(id: number): Observable<Response> {
    return this.http.delete<Response>(`${this.apiUrl}Transactions/${id}`);
  }

}
