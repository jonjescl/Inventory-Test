import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductForm } from './product-form';
import { ProductList } from './product-list';

@Component({
  selector: 'app-products',
  imports: [CommonModule,ProductForm, ProductList],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  showForm = false;

  toggleForm() {
    this.showForm = !this.showForm;
  }
}
