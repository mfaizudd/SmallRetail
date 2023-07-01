import { Component, For, Show, createEffect, createSignal } from "solid-js";
import TextInput from "@/components/TextInput";
import Button from "./Button";

interface Props {
    items?: Item[]
    placeholder?: string;
    onSelect?: (value?: string) => void;
}

export interface Item {
    value: string;
    label: string;
}

const ComboBox: Component<Props> = (props) => {
    const [items, setItems] = createSignal(props.items ?? []);
    const [focused, setFocused] = createSignal(false);
    const [value, setValue] = createSignal("");
    createEffect(() => {
        setItems(props.items ?? []);
    });
    const onChange = (text: string) => {
        if (!items()) return;
        if (text == "") {
            setItems(props.items ?? []);
            return;
        }
        const filtered = items().filter(x => x.label.includes(text));
        setItems(filtered);
        setValue(text);
    }
    const onFocus = (focus: boolean) => {
        setFocused(focus);
        if (focus) return;
        if (items().length > 1) return;
        const item = items()[0];
        if (item) {
            onChange(item.label);
            props.onSelect?.(item.value);
        }
    }
    const onSelect = (item: Item) => {
        onChange(item.label);
        props.onSelect?.(item.value);
    }
    const clear = () => {
        setValue("");
        onChange("");
        props.onSelect?.();
    }
    return (
        <div>
            <div class="flex gap-2">
                <TextInput
                    value={value()}
                    onInput={onChange}
                    placeholder={props.placeholder}
                    onFocus={onFocus} />
            </div>
            <div classList={{
                "h-0 flex flex-col transition-all": true,
                "opacity-0": !focused()
            }}>
                <div class="my-2 z-10 shadow-lg rounded-md bg-white dark:bg-slate-800 p-2">
                    <Show when={value() != ""}>
                        <div
                            class="p-2 hover:bg-white/10 rounded-md cursor-pointer"
                            onClick={clear}>Clear</div>
                    </Show>
                    <For each={items()}>
                        {item => (
                            <div
                                class="p-2 hover:bg-white/10 rounded-md cursor-pointer"
                                onClick={_ => onSelect(item)}
                            >
                                {item.label}
                            </div>
                        )}
                    </For>
                </div>
            </div>
        </div>
    )
}

export default ComboBox;