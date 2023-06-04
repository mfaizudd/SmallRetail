import { Button } from "@/components/button";
import { A } from "@solidjs/router";
import { Component } from "solid-js";

const Dashboard: Component = () => {
    return (
        <div class="bg-emerald-200 h-screen flex justify-center items-center flex-col gap-3">
            <h1 class="text-3xl text-white drop-shadow">
                Dashboard
            </h1>
            <A href="/">
                <Button class="bg-white">Home</Button>
            </A>
        </div>
    )
}

export default Dashboard;