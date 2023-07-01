import Button from "@/components/Button";
import Layout from "@/components/Layout";
import Loading from "@/components/Loading";
import ProductPicker from "@/components/Product/Picker";
import { getAuthorizedApi } from "@/lib/api";
import ItemsWrapper from "@/types/ItemsWrapper";
import Product from "@/types/Product";
import { useParams } from "@solidjs/router";
import { Component, For, Suspense, createResource, createSignal } from "solid-js";

const ShopDetail: Component = () => {
    const params = useParams();
    const [products, { refetch }] = createResource(async () => {
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
            <div class="p-4 w-full">
                <div class="flex flex-row-reverse">
                    <Button onClick={() => setShowPicker(true)}>Add product</Button>
                </div>
                <Suspense fallback={<Loading />}>
                    <For each={products()}>
                        {product => (
                            <div>{product.name}</div>
                        )}
                    </For>
                </Suspense>
            </div>
            <ProductPicker
                onPick={p => addProduct(p)}
                show={showPicker()}
                onClose={() => setShowPicker(false)} />
        </Layout>
    )
}

export default ShopDetail;