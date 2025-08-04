import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { Response } from '../models/response';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor() {}
  private http=inject(HttpClient);
  private apiUrl=environment.apiProductsUrl;
  
  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}Products`);
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}Products/${id}`);
  }

  create(product: Product): Observable<Response> {
    return this.http.post<Response>(`${this.apiUrl}Products`, product);
  }

  update(id: number, product: Product): Observable<Response> {
    return this.http.put<Response>(`${this.apiUrl}Products/${id}`, product);
  }

  delete(id: number): Observable<Response> {
    return this.http.delete<Response>(`${this.apiUrl}Products/${id}`);
  }
  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}Categories`);
  }
}
