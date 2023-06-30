import { Component } from "solid-js";

interface Props {
    value?: number;
    onChange?: (v: number) => void;
    name?: string;
    placeholder?: string;
    id?: string;
}

const NumberInput: Component<Props> = (props) => {
    return (
        <input
            class="p-2 rounded-md border dark:bg-slate-900"
            type="number"
            value={props.value}
            placeholder={props.placeholder}
            onChange={e => props.onChange?.(+e.currentTarget.value)}
            name={props.name}
            id={props.id} />
    )
}

export default NumberInput;