import { Component, JSX, JSXElement } from "solid-js";

interface Props {
    onSubmit?: () => void;
    children?: JSXElement;
    class?: string;
}

const Form: Component<Props> = (props) => {
    const submit: JSX.EventHandler<HTMLElement, Event> = (e) => {
        e.preventDefault();
        props.onSubmit?.();
    }
    return (
        <form onSubmit={submit} class={props.class}>
            {props.children}
        </form>
    )
}

export default Form;