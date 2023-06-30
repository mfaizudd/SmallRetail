import { Route, Routes } from '@solidjs/router';
import type { Component } from 'solid-js';
import Home from './pages/Home';
import Redirect from './pages/Redirect';
import Dashboard from './pages/Dashboard';
import { MetaProvider, Title } from '@solidjs/meta';
import Shops from './pages/Shops';
import Products from './pages/Products';

const App: Component = () => {
    return (
        <MetaProvider>
            <Title>SmallRetail</Title>
            <Routes>
                <Route path="/" component={Home} />
                <Route path="/redirect" component={Redirect} />
                <Route path="/dashboard" component={Dashboard} />
                <Route path="/shops" component={Shops} />
                <Route path="/products" component={Products} />
            </Routes>
        </MetaProvider>
    );
};

export default App;
