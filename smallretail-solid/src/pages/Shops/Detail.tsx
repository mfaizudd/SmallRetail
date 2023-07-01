import Button from "@/components/Button";
import Layout from "@/components/Layout";
import ProductPicker from "@/components/ProductPicker";
import { getAuthorizedApi } from "@/lib/api";
import ItemsWrapper from "@/types/ItemsWrapper";
import Product from "@/types/Product";
import { useParams } from "@solidjs/router";
import { Component, For, createResource, createSignal } from "solid-js";

const ShopDetail: Component = () => {
    const params = useParams();
    const [products, {refetch}] = createResource(async () => {
        const api = await getAuthorizedApi();
        const response = await api.get<ItemsWrapper<Product>>(`/products?shop_id=${params.id}`)
        return response.data.items;
    });
    const [showPicker, setShowPicker] = createSignal(false);
    const addProduct = async (product: Product) => {
        const api = await getAuthorizedApi();
        await api.post(`/shops/${params.id}/products`, {
            product_id: product.id,
            stock: 1,
        });
        refetch();
    }
    return (
        <Layout>
            <Button onClick={() => setShowPicker(true)}>Add product</Button>
            <ul>
                <For each={products()}>
                    {product => (
                        <li>{product.name}</li>
                    )}
                </For>
            </ul>
            <ProductPicker
                onPick={p => addProduct(p)}
                show={showPicker()}
                onClose={() => setShowPicker(false)} />
        </Layout>
    )
}

export default ShopDetail;