import { Component, For, createResource, createSignal } from "solid-js";
import Modal from "@/components/Modal";
import { Portal } from "solid-js/web";
import Button from "../Button";
import { getAuthorizedApi } from "@/lib/api";
import ItemsWrapper from "@/types/ItemsWrapper";
import Product from "@/types/Product";
import ProductTable from "./Table";

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
        <ProductTable products={products()}>
            {(product) => (
                <Button onClick={() => pick(product)}>Pick</Button>
            )}
        </ProductTable>
    </Modal>
}

export default ProductPicker;