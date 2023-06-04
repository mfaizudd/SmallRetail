import { useAppContext } from '@/context/state';
import { setToken, setRefreshToken, setIdToken } from '@/lib/api';
import { User, exchangeCodeForToken, getCodeVerifier } from '@/lib/auth';
import { useSearchParams, useNavigate } from '@solidjs/router';
import jwtDecode from 'jwt-decode';
import { Component } from "solid-js";

const Redirect: Component = () => {
    const navigate = useNavigate();
    const { user, setUser } = useAppContext();
    const [query] = useSearchParams();
    if (query.code) {
        const code_verifier = getCodeVerifier();
        processToken(query.code, code_verifier);
    }
    async function processToken(code: string, code_verifier: string | null) {
        const token_response = await exchangeCodeForToken(code, code_verifier ?? "");
        if (token_response) {
            setToken(token_response.access_token);
            setRefreshToken(token_response.refresh_token);
            setIdToken(token_response.id_token!)
            const user = jwtDecode<User>(token_response.id_token!)
            setUser(user);
            navigate("/");
        }
    }

    return (
        <div>Redirecting...</div>
    )
}

export default Redirect;