import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TransactionForm } from './transaction-form';
import { TransactionList } from './transaction-list';

@Component({
  selector: 'app-transactions',
  imports: [CommonModule,TransactionForm, TransactionList],
  templateUrl: './transactions.html',
  styleUrl: './transactions.css'
})
export class Transactions {
  showForm = false;

  toggleForm() {
    this.showForm = !this.showForm;
  }
}
