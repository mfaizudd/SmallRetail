import { Component, JSX } from "solid-js"

interface Props {
    class?: string;
    children?: JSX.Element;
    onClick?: JSX.EventHandlerUnion<HTMLButtonElement, MouseEvent>
}

export const Button: Component<Props> = (props) => {
    return (
        <button
            class={`shadow hover:shadow-lg p-2 rounded-md ${props.class ?? ""} transition-all`}
            onClick={props.onClick}>
            {props.children}
        </button>
    )
}