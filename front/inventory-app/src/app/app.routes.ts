import { Routes } from '@angular/router';
import { Products } from './pages/products/products';
import { Transactions } from './pages/transactions/transactions';
import { Home } from './pages/home/home';

export const routes: Routes = [
    {path: 'products' , component: Products },
    {path: 'transactions' , component: Transactions},
    {path: '' , component: Home},
    {path: '**' , redirectTo: ''}
];
