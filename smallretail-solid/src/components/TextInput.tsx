import { Component } from "solid-js";

interface Props {
    value?: string;
    onChange?: (v: string) => void;
    name?: string;
    placeholder?: string;
    id?: string;
}

const TextInput: Component<Props> = (props) => {
    return (
        <input
            class="p-2 rounded-md border dark:bg-slate-900"
            type="text"
            value={props.value}
            placeholder={props.placeholder}
            onChange={e => props.onChange?.(e.currentTarget.value)}
            name={props.name}
            id={props.id} />
    )
}

export default TextInput;