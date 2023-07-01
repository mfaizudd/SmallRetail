import { Component, JSX, Show, createComputed, createSignal } from "solid-js";
import Button from "@/components/Button";
import { Transition } from "solid-transition-group";
import { A } from "@solidjs/router";

interface Props {
    children: JSX.Element
}

const Layout: Component<Props> = (props) => {
    const buttons = () => (
        [
            { link: "/dashboard", label: "Home" },
            { link: "/shops", label: "Shops" },
            { link: "/products", label: "Products" },
        ].map(v => (
            <A
                class="p-2 hover:bg-gray-200/50 dark:hover:bg-white/20 active:bg-black/10 dark:active:bg-white/10 transition-colors rounded-full text-center dark:text-white"
                href={v.link}>
                {v.label}
            </A>
        ))
    )
    const [menuActive, setMenuActive] = createSignal(false);
    return (
        <div class="flex min-h-screen flex-col sm:flex-row transition-all bg-slate-200 dark:text-white dark:bg-slate-900">
            <div class="sm:w-60 bg-white dark:bg-slate-800 sm:flex flex-col gap-4 p-4 hidden shadow-md">
                <div class="flex text-center flex-col p-4">
                    <h2 class="text-lg font-bold">SmallRetail</h2>
                </div>
                {buttons()}
            </div>
            <div class="flex flex-row align-middle items-center sm:hidden">
                <Button class="sm:hidden m-4" onClick={() => setMenuActive(!menuActive())}>Menu</Button>
                <div class="font-semibold">SmallRetail</div>
            </div>
            <div classList={{
                "flex flex-col px-4 gap-4 transition-all sm:hidden": true,
                "invisible opacity-0 -translate-y-10 h-0": !menuActive(),
                "visible translate-y-0": menuActive(),
            }}>
                {buttons()}
            </div>
            <div class="sm:grow w-full">
                {props.children}
            </div>
        </div>
    )
}

export default Layout;