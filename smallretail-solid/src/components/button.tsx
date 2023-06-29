import { A } from "@solidjs/router";
import { Component, JSX } from "solid-js"
import { Switch, Match } from "solid-js";
interface Props {
    children?: JSX.Element;
    onClick?: JSX.EventHandlerUnion<HTMLElement, MouseEvent>
    color?: ButtonColor;
    type?: ButtonType;
    href?: string;
    class?: string;
}

type ButtonColor =
    | "primary"
    | "secondary"
    | "danger"
    | "warning"
    | "success"

type ButtonType =
    | "submit"
    | "button"

const Button: Component<Props> = (props) => {
    props.color = props.color ?? "primary"
    props.type = props.type ?? "button"

    const classList = {
        "p-2 transition-all rounded-md shadow hover:shadow-lg focus:shadow-sm text-white text-center": true,
        "bg-blue-500": props.color == "primary",
        "bg-slate-700": props.color == "secondary",
        "bg-red-500": props.color == "danger",
        "bg-amber-400": props.color == "warning",
        "bg-teal-400": props.color == "success",
        [props.class ?? ""]: true
    }

    return (
        <Switch>
            <Match when={!props.href}>
                <button
                    classList={classList}
                    type={props.type}
                    onClick={props.onClick}>
                    {props.children}
                </button>
            </Match>
            <Match when={props.href}>
                <A
                    href={props.href!}
                    classList={classList}
                    onClick={props.onClick}>
                    {props.children}
                </A>
            </Match>
        </Switch>
    )
}

export default Button;