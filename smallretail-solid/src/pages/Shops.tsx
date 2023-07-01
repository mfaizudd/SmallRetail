import Button from "@/components/Button";
import Form from "@/components/Form";
import Layout from "@/components/Layout";
import Loading from "@/components/Loading";
import Modal from "@/components/Modal";
import TextInput from "@/components/TextInput";
import { getApi, getAuthorizedApi } from "@/lib/api";
import Shop from "@/types/Shop";
import { Component, For, createResource, createSignal, onMount } from "solid-js";

const Shops: Component = () => {
    const [show, setShow] = createSignal(false);
    const [confirm, setConfirm] = createSignal(false);
    const [callback, setCallback] = createSignal<() => void>(() => { });
    const [name, setName] = createSignal("");
    const [editing, setEditing] = createSignal(false);
    const [shopIdx, setShopIdx] = createSignal<number>();
    const fetch = async () => {
        const api = await getAuthorizedApi();
        const response = await api.get<Shop[]>("/shops")
        return response.data;
    }
    const [shops, { refetch }] = createResource<Shop[]>(fetch);
    onMount(async () => {
        fetch();
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
    }
    const edit = (i: number) => {
        const shop = shops()?.[i];
        setName(shop?.name ?? "");
        setShopIdx(i);
        setEditing(true);
        setShow(true);
    }
    const submit = async () => {
        const api = await getAuthorizedApi();
        const data = { name: name() }
        if (!editing()) {
            await api.post("/shops", data);
        } else {
            const shop = shops()?.[shopIdx() ?? -1]
            if (shop) {
                await api.put(`/shops/${shop.id}`, data)
            }
        }
        refetch();
        clear();
        setShow(false);
        setEditing(false);
    }
    const deleteShop = (id: number) => {
        showConfirm(async () => {
            const api = await getAuthorizedApi();
            await api.delete(`/shops/${id}`);
            refetch();
        });
    }
    return (
        <Layout>
            <div class="p-4 w-full">
                <div class="flex flex-row-reverse">
                    <Button onClick={() => {
                        clear();
                        setEditing(false);
                        setShow(true);
                    }}>Add new shop</Button>
                </div>
                <div class="flex flex-wrap gap-4 overflow-x-scroll p-4">
                    <For each={shops()} fallback={<Loading />}>
                        {(shop, i) => (
                            <div class="bg-white dark:bg-slate-700 p-4 rounded-md shadow-md flex gap-2">
                                <div class="w-72">
                                    {shop.name}
                                </div>
                                <Button onClick={() => edit(i())}>Edit</Button>
                                <Button onClick={() => deleteShop(shop.id)} color="danger">Delete</Button>
                            </div>
                        )}
                    </For>
                </div>
            </div>
            <Modal show={show()} onClose={() => setShow(false)}>
                <div class="font-semibold mb-3">Add new shop</div>
                <Form class="flex flex-col gap-4" onSubmit={submit}>
                    <TextInput
                        value={name()}
                        onInput={v => setName(v)}
                        placeholder="Name" />
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

export default Shops;