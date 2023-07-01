import { Component, For, createResource, createSignal } from "solid-js";
import Modal from "@/components/Modal";
import { Portal } from "solid-js/web";
import Button from "./Button";
import { getAuthorizedApi } from "@/lib/api";
import ItemsWrapper from "@/types/ItemsWrapper";
import Product from "@/types/Product";

interface Props {
    show?: boolean;
    onClose?: () => void;
    onPick?: (product: Product) => void;
}

const ProductPicker: Component<Props> = (props) => {
    const [products] = createResource(async () => {
        const api = await getAuthorizedApi();
        const response = await api.get<ItemsWrapper<Product>>("/products?limit=1000");
        return response.data.items;
    });
    const pick = (product: Product) => {
        props.onClose?.();
        props.onPick?.(product);
    }

    return <Modal show={props.show ?? false} onClose={props.onClose}>
        <div class="w-full overflow-auto py-4 rounded-lg bg-white dark:bg-slate-800 my-4">
            <table class="table-auto w-full">
                <thead>
                    <tr>
                        <th></th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Barcode</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody class="overflow-x-scroll">
                    <For each={products()}>
                        {(product, i) => (
                            <tr class="hover:bg-black/10 p-10">
                                <td class="text-center p-2">{i() + 1}</td>
                                <td class="p-2"> {product.name} </td>
                                <td class="text-center"> {product.price} </td>
                                <td class="text-center"> {product.barcode} </td>
                                <td class="flex gap-4 justify-center p-2 sm:flex-row">
                                    <Button onClick={() => pick(product)}>Pick</Button>
                                </td>
                            </tr>
                        )}
                    </For>
                </tbody>
            </table>
        </div>
    </Modal>
}

export default ProductPicker;