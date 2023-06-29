import { Component, JSX, createSignal } from "solid-js";

interface Props {
    show: boolean;
    children?: JSX.Element;
    onClose?: () => void;
}

const Modal: Component<Props> = (props) => {
    const handleClick: JSX.EventHandler<HTMLElement, Event> = (e) => {
        e.stopPropagation();
    }
    return (
        <div classList={{
            "h-screen w-screen fixed inset-0 transition-all": true,
            "opacity-0 invisible": !props.show,
            "opacity-100 visible": props.show,
        }}>
            <div
                class="flex items-center h-full justify-center bg-black/50 backdrop-blur"
                onClick={props.onClose}>
                <div class="bg-white dark:bg-slate-900 rounded-lg p-4" onClick={handleClick}>
                    {props.children}
                </div>
            </div>
        </div>
    )
}

export default Modal;