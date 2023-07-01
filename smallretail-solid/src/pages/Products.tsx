import Button from "@/components/Button";
import ComboBox from "@/components/ComboBox";
import Form from "@/components/Form";
import Layout from "@/components/Layout";
import Modal from "@/components/Modal";
import NumberInput from "@/components/NumberInput";
import TextInput from "@/components/TextInput";
import { getAuthorizedApi } from "@/lib/api";
import ItemsWrapper from "@/types/ItemsWrapper";
import Product from "@/types/Product";
import Shop from "@/types/Shop";
import { Component, For, createResource, createSignal, onMount } from "solid-js";

interface Filter {
    shopId?: string;
    name?: string;
}

const Products: Component = () => {
    const [show, setShow] = createSignal(false);
    const [confirm, setConfirm] = createSignal(false);
    const [callback, setCallback] = createSignal<() => void>(() => { });
    const [name, setName] = createSignal("");
    const [price, setPrice] = createSignal(0);
    const [barcode, setBarcode] = createSignal("");
    const [editing, setEditing] = createSignal(false);
    const [productIdx, setProductIdx] = createSignal<number>();
    const [filter, setFilter] = createSignal<Filter>({});
    const fetch = async () => {
        const api = await getAuthorizedApi();
        let endpoint = "/products?"
        const shopId = filter()?.shopId;
        if (shopId) {
            endpoint += `shop_id=${shopId}&`;
        }
        const name = filter()?.name;
        if (name) {
            endpoint += `name=${name}&`;
        }
        const response = await api.get<ItemsWrapper<Product>>(endpoint);
        return response.data.items;
    }
    const [products, { refetch }] = createResource<Product[]>(fetch);
    onMount(async () => {
        fetch();
    });
    const [shops] = createResource(async () => {
        const api = await getAuthorizedApi();
        const response = await api.get<Shop[]>("/shops");
        return response.data.map(s => {
            return {
                value: s.id.toString(),
                label: s.name,
            };
        });
    });
    const showConfirm = (callback: () => void) => {
        const confirmCallback = () => {
            callback();
            setConfirm(false);
        }
        setCallback(_ => confirmCallback)
        setConfirm(true);
    }
    const clear = () => {
        setName("");
        setPrice(0);
        setBarcode("");
    }
    const edit = (i: number) => {
        const product = products()?.[i];
        setName(product?.name ?? "");
        setPrice(product?.price ?? 0);
        setBarcode(product?.barcode ?? "");
        setProductIdx(i);
        setEditing(true);
        setShow(true);
    }
    const submit = async () => {
        const api = await getAuthorizedApi();
        const data = {
            name: name(),
            price: price(),
            barcode: barcode(),
        }
        if (!editing()) {
            await api.post("/products", data);
        } else {
            const product = products()?.[productIdx() ?? -1]
            if (product) {
                await api.put(`/products/${product.id}`, data)
            }
        }
        refetch();
        clear();
        setShow(false);
        setEditing(false);
    }
    const deleteProduct = (id: number) => {
        showConfirm(async () => {
            const api = await getAuthorizedApi();
            await api.delete(`/products/${id}`);
            refetch();
        });
    }
    const updateFilter = (f: Filter) => {
        setFilter(f);
        refetch();
    }
    return (
        <Layout>
            <div class="p-4 w-full">
                <div class="flex flex-row-reverse gap-4">
                    <Button onClick={() => {
                        clear();
                        setEditing(false);
                        setShow(true);
                    }}>Add new product</Button>
                    <ComboBox
                        items={shops()}
                        placeholder="Shop"
                        onSelect={id => updateFilter({ ...filter(), shopId: id })} />
                    <TextInput
                        onInput={s => updateFilter({...filter(), name: s})}
                        placeholder="Name" />
                </div>
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
                                            <Button onClick={() => edit(i())}>Edit</Button>
                                            <Button onClick={() => deleteProduct(product.id)} color="danger">Delete</Button>
                                        </td>
                                    </tr>
                                )}
                            </For>
                        </tbody>
                    </table>
                </div>
            </div>
            <Modal show={show()} onClose={() => setShow(false)}>
                <div class="font-semibold mb-3">Add new product</div>
                <Form class="flex flex-col gap-4" onSubmit={submit}>
                    <TextInput
                        value={name()}
                        onInput={v => setName(v)}
                        placeholder="Name" />
                    <NumberInput
                        value={price()}
                        onChange={v => setPrice(v)}
                        placeholder="Price" />
                    <TextInput
                        value={barcode()}
                        onInput={v => setBarcode(v)}
                        placeholder="Barcode" />
                    <Button type="submit">Submit</Button>
                </Form>
            </Modal>
            <Modal show={confirm()} onClose={() => setConfirm(false)}>
                <div class="font-semibold mb-3">Are you sure?</div>
                <div class="flex gap-2 flex-row-reverse">
                    <Button color="danger" onClick={callback()}>Yes</Button>
                    <Button color="secondary" onClick={() => setConfirm(false)}>No</Button>
                </div>
            </Modal>
        </Layout>
    )
}

export default Products;