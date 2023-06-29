export interface Config {
    authorize_url: string;
    client_id: string;
    redirect_uri: string;
    token_url: string;
    api_url: string;
}

export function getConfig(): Config {
    return {
        authorize_url: import.meta.env.VITE_AUTHORIZE_URL,
        client_id: import.meta.env.VITE_CLIENT_ID,
        redirect_uri: import.meta.env.VITE_REDIRECT_URI,
        token_url: import.meta.env.VITE_TOKEN_URL,
        api_url: import.meta.env.VITE_API_URL
    }
}
