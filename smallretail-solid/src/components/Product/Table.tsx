import Product from "@/types/Product";
import { Component, For, JSX, Show } from "solid-js";
import Button from "../Button";

interface Props {
    products?: Product[];
    children?: (product: Product, index?: number) => JSX.Element;
}

const ProductTable: Component<Props> = (props) => {
    return <div class="w-full overflow-auto py-4 rounded-lg bg-white dark:bg-slate-800 my-4">
        <table class="table-auto w-full">
            <thead>
                <tr>
                    <th></th>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Barcode</th>
                    <Show when={props.children}>
                        <th></th>
                    </Show>
                </tr>
            </thead>
            <tbody class="overflow-x-scroll">
                <For each={props.products ?? []}>
                    {(product, i) => (
                        <tr class="hover:bg-black/10 p-10">
                            <td class="text-center p-2">{i() + 1}</td>
                            <td class="p-2"> {product.name} </td>
                            <td class="text-center"> {product.price} </td>
                            <td class="text-center"> {product.barcode} </td>
                            <Show when={props.children}>
                                <td class="flex gap-4 justify-center p-2 sm:flex-row">
                                    {props.children?.(product, i())}
                                </td>
                            </Show>
                        </tr>
                    )}
                </For>
            </tbody>
        </table>
    </div>
}

export default ProductTable;