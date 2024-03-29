import Product from "@/types/Product";

export default interface Shop {
    id: number;
    user_id: number;
    name: string;
    invite_code?: string;
    products?: Product[];
}