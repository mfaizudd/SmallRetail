import { Component, JSX, createEffect, createSignal } from "solid-js";

interface Props {
    value?: string;
    onInput?: (v: string) => void;
    onFocus?: (focus: boolean) => void;
    name?: string;
    placeholder?: string;
    id?: string;
    class?: string
}

const TextInput: Component<Props> = (props) => {
    return (
        <input
            class={ `p-2 rounded-md border dark:bg-slate-900 ${props.class ?? ""}` }
            type="text"
            value={props.value ?? ""}
            placeholder={props.placeholder}
            onInput={e => props.onInput?.(e.currentTarget.value)}
            onFocusIn={() => props.onFocus?.(true)}
            onFocusOut={() => props.onFocus?.(false)}
            name={props.name}
            id={props.id} />
    )
}

export default TextInput;