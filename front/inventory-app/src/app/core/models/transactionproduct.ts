export interface Transactionproduct {
    id?: number;
    transactionId?: number;
    productId?: number;
    productName: string;
    quantity: number;
    unitPrice: number;
    totalPrice: number;
    createdAt?: string;
    updatedAt?: string;

}
