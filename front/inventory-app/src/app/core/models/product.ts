import { Category } from "./category";
export interface  Product {
    id?: number;
    name: string;
    description: string;
    image: string;
    price: number;
    stock: number;
    categoryId: number;
    category?: Category;
    createdAt?: string;
    updatedAt?: string;
}
