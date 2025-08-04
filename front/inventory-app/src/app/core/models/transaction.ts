import { Transactionproduct } from "./transactionproduct";
import { Transactiontype } from "./transactiontype";

export interface  Transaction {
    id?: number;
    date: string;
    transactionTypeId: number;
    transactionType?: Transactiontype;
    totalPrice: number;
    detail: string;
    createdAt?: string;
    updatedAt?: string;
    transactionProducts?: Transactionproduct[];
}

