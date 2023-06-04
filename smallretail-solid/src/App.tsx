import { Route, Routes } from '@solidjs/router';
import type { Component } from 'solid-js';
import Home from './pages/Home';
import Redirect from './pages/Redirect';
import Dashboard from './pages/Dashboard';

const App: Component = () => {
    return (
        <Routes>
            <Route path="/" component={Home} />
            <Route path="/redirect" component={Redirect} />
            <Route path="/dashboard" component={Dashboard} />
        </Routes>
    );
};

export default App;
