
import Button from "@/components/Button";
import { useAppContext } from "@/context/state";
import { getUserInfo, removeRefreshToken, removeToken } from '@/lib/api';
import { oauthSignIn } from '@/lib/auth';
import { Component, Show, createEffect, createSignal, onMount } from 'solid-js';

const Home: Component = () => {
    const { user, setUser } = useAppContext();
    let signInButton = <Button onClick={signIn}>Sign in</Button>;
    onMount(() => {
        const u = getUserInfo();
        setUser(u);
    })
    function signOut() {
        removeToken();
        removeRefreshToken();
        setUser();
    }
    function signIn() {
        oauthSignIn();
    }
    return (
        <div class="bg-slate-200 dark:bg-slate-900 transition-colors h-screen flex dark:text-white justify-center items-center gap-3">
            <div class="w-max bg-white dark:bg-slate-800 p-10 rounded-md shadow-md gap-4 flex flex-col items-center">
                <h1 class="text-3xl mb-8 text-center">Welcome to SmallRetail</h1>
                <Show when={user()} fallback={signInButton}>
                    <Button class="w-1/2" href="/dashboard">Dashboard</Button>
                    <Button class="w-1/2" onClick={signOut}>Sign out</Button>
                </Show>
            </div>
        </div>
    );
};

export default Home;
