import { Component, inject } from '@angular/core';
import { ProductService } from '../../core/services/productservice';
import { Product } from '../../core/models/product';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { Category } from '../../core/models/category';
@Component({
  selector: 'app-product-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css'
})
export class ProductList {
  private productService = inject(ProductService);
  filterText = '';
  products: Product[] = [];
  pageSize = 3;
  currentPage = 1;
  selectedProduct: Product = {} as Product;
  showEditModal = false;
  categories: Category[] = [];

  constructor() {
    this.loadProducts();
    this.loadCategories();
  }
  loadProducts() {
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        console.log('Productos desde el servicio:', data);
      },
      error: (err) => {
        console.error('Error al cargar productos:', err);
      }
    });
  }
  loadCategories() {
    this.productService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
      }
    });
  }

  get filteredProducts(): Product[] {
    return this.products.filter(p =>
      p.name.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }

  get totalPages(): number {
    return Math.ceil(this.filteredProducts.length / this.pageSize);
  }

  get paginatedProducts(): Product[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredProducts.slice(start, start + this.pageSize);
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  editProduct2(product: Product) {
    if(product.categoryId==0 || product.name=="" || product.description
      || product.price==0 || product.stock ==0
    ){
       alert("Debe completar todos los campos.");
    }
    else{
      
      this.productService.update(product.id!, product).subscribe({
        next: (res) => {
          if (res.isOk) {
            alert(res.message);
          } else {
            alert(res.message);
          }
        },
        error: (err) => {
          console.error('Error al registrar producto:', err);
          alert('Error inesperado al registrar producto.');
        }
      });
    }
  }

  deleteProduct(id: number) {
    if (confirm('¿Está seguro que desea eliminar este producto?')) {
      this.productService.delete(id).subscribe({
        next: res => {
          if (res.isOk) {
            alert('Producto eliminado correctamente.');
            this.loadProducts();
          } else {
            alert(res.message);
          }
        },
        error: err => {
          alert('Error del servidor al eliminar producto.');
          console.error(err);
        }
      });
    }
  }
  openEditModal(product: Product) {
    this.selectedProduct = { ...product };
    this.showEditModal = true;
  }

  closeEditModal() {
    this.showEditModal = false;
  }
  editProduct() {
    if (
      !this.selectedProduct.name?.trim() ||
      !this.selectedProduct.description?.trim() ||
      !this.selectedProduct.categoryId ||
      this.selectedProduct.price <= 0 ||
      this.selectedProduct.stock <= 0
    ) {
      alert("Debe completar todos los campos.");
      return;
    }

    this.productService.update(this.selectedProduct.id!, this.selectedProduct).subscribe({
      next: (res) => {
        if (res.isOk) {
          alert(res.message || "Producto actualizado con éxito");
          this.loadProducts(); 
          this.closeEditModal();
        } else {
          alert("Error al actualizar el producto.");
        }
      },
      error: (err) => {
        console.error(err);
        alert("Error del servidor.");
      }
    });
  }
}
