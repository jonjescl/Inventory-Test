import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Category } from '../../core/models/category';
import { ProductService } from '../../core/services/productservice';
import { Product } from '../../core/models/product';
@Component({
  selector: 'app-product-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-form.html',
  styleUrl: './product-form.css'
})
export class ProductForm {
  name = '';
  price = 0;
  stock = 0;
  private productService = inject(ProductService);
  categories: Category[] = [];
  product: Product = {
    name: '',
    price: 0,
    stock: 0,
    categoryId: 0,
    description: '',
    image: ''
  };
  ngOnInit(): void {
    this.productService.getAllCategories().subscribe({
      next: (res) => this.categories = res,
      error: (err) => console.error('Error loading categories', err)
    });
  }
  submitForm() {
    if(this.product.categoryId==0 || this.product.name=="" || this.product.description
      || this.product.price==0 || this.product.stock ==0
    ){
       alert("Debe completar todos los campos.");
    }
    else{
      this.productService.create(this.product).subscribe({
        next: (res) => {
          if (res.isOk) {
            alert(res.message);
          } else {
            alert('Error: ' + res.message);
          }
        },
        error: (err) => {
          console.error('Error al registrar producto:', err);
          alert('Error inesperado al registrar producto.');
        }
      });
    }
  }
}
