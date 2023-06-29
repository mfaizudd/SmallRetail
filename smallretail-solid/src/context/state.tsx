import { User } from "@/lib/auth";
import { createSignal, createContext, useContext, JSX, Component, Accessor } from "solid-js";

interface Props {
    children?: JSX.Element
}

interface State {
    user: Accessor<User | undefined>;
    setUser: (user?: User) => void;
}

const AppContext = createContext<State>({ user: () => undefined, setUser: () => { } });

export const AppWrapper: Component<Props> = (props) => {
    const [user, setUser] = createSignal<User>()
    const state: State = { user, setUser }
    return (
        <AppContext.Provider value={state}>
            {props.children}
        </AppContext.Provider>
    )
}

export function useAppContext() {
    return useContext(AppContext);
}