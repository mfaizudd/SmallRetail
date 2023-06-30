import Shop from "@/types/Shop";

export default interface Product {
    id: number;
    name: string;
    price: number;
    barcode: string;
    user_id: string;
    shops?: Shop[];
}