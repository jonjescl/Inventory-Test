import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../core/services/productservice';
import { Product } from '../../core/models/product';
import { Transactionproduct } from '../../core/models/transactionproduct';
import { Transactiontype } from '../../core/models/transactiontype';
import { Transaction } from '../../core/models/transaction';
import { TransactionService } from '../../core/services/transactionservice';

@Component({
  selector: 'app-transaction-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './transaction-form.html',
  styleUrl: './transaction-form.css'
})
export class TransactionForm {
  private productService = inject(ProductService);
  private transactionService = inject(TransactionService);
  products: Product[] = [];
  transactionItems:Transactionproduct[]=[];
  selectedProductId: number = 0;
  selectedQuantity: number = 1;
  quantity: number = 1;
  total:number=0;
  transactionType: Transactiontype[] = [
      { id: 1, name: 'Compra' },
      { id: 2, name: 'Venta' }
  ];
  transaction:Transaction= {
    date: '',
    transactionTypeId: 0,
    totalPrice: 0,
    detail: '',
    transactionProducts:[]
  };
  ngOnInit() {
    this.productService.getAll().subscribe(data => {
      this.products = data;
    });
  }
  
  addProduct(): void {
    const product = this.products.find(p => p.id == this.selectedProductId);
    
    if (!product || this.selectedQuantity <= 0) {
      alert('Producto inválido o cantidad incorrecta.');
      return;
    }
    if(product.stock! <= this.selectedQuantity){
      alert('Stock insuficiente, solo quedan '+product.stock+" unidades.");
      return;
    }

    const item: Transactionproduct = {
      productId: product.id,
      productName: product.name,
      quantity: this.selectedQuantity,
      unitPrice: product.price,
      totalPrice: this.selectedQuantity * product.price
    };

    this.transactionItems.push(item);

    // Reset campos
    this.selectedProductId = 0;
    this.selectedQuantity = 1;
  }

  removeProduct(index: number): void {
    this.transactionItems.splice(index, 1);
  }

  get totalAmount(): number {
    this.total=this.transactionItems.reduce((sum, item) => sum + item.totalPrice, 0)
    return this.total;
  }

  saveTransaction(): void {
    if (this.transactionItems.length === 0) {
      alert('Debe agregar al menos un producto.');
      return;
    }
    const now = new Date().toISOString();

    

    this.transaction.transactionProducts=this.transactionItems;
    this.transaction.date=now;
    this.transaction.totalPrice=this.total;
    if(this.transaction.transactionTypeId==0 || this.transaction.detail=="")
    {
       alert("Debe completar todos los campos.");
       return;
    }
    else{
      console.log(this.transaction,"enviar")
      this.transactionService.create(this.transaction).subscribe({
        next: (res) => {
          if (res.isOk) {
            alert(res.message);
          } else {
            alert(res.message);
          }
        },
        error: (err) => {
          console.error('Error al registrar transacción:', err);
        }
      });
    }
  }

}
