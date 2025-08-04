import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../core/services/transactionservice';
import { Transaction } from '../../core/models/transaction';
import { Transactiontype } from '../../core/models/transactiontype';
import { Transactionproduct } from '../../core/models/transactionproduct';

@Component({
  selector: 'app-transaction-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css'
})
export class TransactionList {
  private transactionService = inject(TransactionService);
  filterText = '';
  transaction: Transaction[] = [];
  pageSize = 3;
  currentPage = 1;
  selectedTransaction: Transaction = {} as Transaction;
  showEditModal = false;
  showDetailModal = false;
  transactionType: Transactiontype[] = [
    { id: 1, name: 'Compra' },
    { id: 2, name: 'Venta' }
  ];
  unitTransaction: Transaction = {} as Transaction;
  transactionProducts?:Transactionproduct[]=[]
  loadingDetails = false;
  constructor() {
    this.loadTransactions();
  }
  loadTransactions() {
    this.transactionService.getAll().subscribe({
      next: (data) => {
        this.transaction = data;
        console.log('Transacciones desde el servicio:', data);
      },
      error: (err) => {
        console.error('Error al cargar transacciones:', err);
      }
    });
  }
  get filteredProducts(): Transaction[] {
    return this.transaction.filter(p =>
      p.detail.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }
  get totalPages(): number {
    return Math.ceil(this.filteredProducts.length / this.pageSize);
  }
  
  get paginatedProducts(): Transaction[] {
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
  deleteTransaction(id: number) {
    if (confirm('¿Está seguro que desea eliminar transacción?')) {
      this.transactionService.delete(id).subscribe({
        next: res => {
          if (res.isOk) {
            alert(res.message);
            this.loadTransactions();
          } else {
            alert(res.message);
          }
        },
        error: err => {
          alert('Error del servidor al eliminar transacción.');
          console.error(err);
        }
      });
    }
  }
  openEditModal(transaction: Transaction) {
    this.selectedTransaction = { ...transaction };
    this.showEditModal = true;
  }
  closeEditModal() {
    this.showEditModal = false;
  }
  editTransaction() {
    if (
      !this.selectedTransaction.detail?.trim() ||
      this.selectedTransaction.totalPrice==0 ||
      !this.selectedTransaction.date?.trim() ||
      this.selectedTransaction.transactionTypeId == 0
    ) {
      alert("Debe completar todos los campos.");
      return;
    }

    this.transactionService.update(this.selectedTransaction.id!, this.selectedTransaction).subscribe({
      next: (res) => {
        if (res.isOk) {
          alert(res.message || "Producto actualizado con éxito");
          this.loadTransactions(); 
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
  openDetailsModal(transaction: Transaction) {
    this.selectedTransaction = { ...transaction };
    this.showDetailModal = true;
    this.selectedTransaction = transaction;
    this.transactionProducts = [];
    this.loadingDetails = true;

    this.transactionService.getById(transaction.id!).subscribe({
      next: (data) => {
        console.log(data)
        this.unitTransaction=data;
        this.transactionProducts = data.transactionProducts;
        this.loadingDetails = false;
      },
      error: () => {
        alert('Error al cargar los productos de la transacción');
        this.loadingDetails = false;
      }
    });

  }

  closeDetailsModal() {
    this.showDetailModal = false;
  }
}
