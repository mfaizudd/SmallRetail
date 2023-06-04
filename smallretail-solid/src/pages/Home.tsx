
import { Button } from '@/components/button';
import { useAppContext } from '@/context/state';
import { getUserInfo, removeRefreshToken, removeToken } from '@/lib/api';
import { oauthSignIn } from '@/lib/auth';
import { A } from '@solidjs/router';
import { Component, Show, createEffect, createSignal, onMount } from 'solid-js';

const Home: Component = () => {
    const { user, setUser } = useAppContext();
    let signInButton = <Button class="bg-white" onClick={signIn}>Sign in</Button>;
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
        <div class="bg-slate-200 h-screen flex justify-center items-center flex-col gap-3">
            <h1 class="text-3xl text-white drop-shadow">
                Hello world!
            </h1>
            <A href="/dashboard" class="bg-white rounded-md p-2">Dashboard</A>
            <Show when={user()} fallback={signInButton}>
                <Button class="bg-white" onClick={signOut}>Sign out</Button>
            </Show>
        </div>
    );
};

export default Home;
