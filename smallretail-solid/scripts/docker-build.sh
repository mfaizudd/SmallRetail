#!/bin/bash
. ./.envrc
docker build -t mfaizudd/smallretail-web \
    --build-arg VITE_AUTHORIZE_URL=${VITE_AUTHORIZE_URL} \
    --build-arg VITE_CLIENT_ID=${VITE_CLIENT_ID} \
    --build-arg VITE_REDIRECT_URI=${VITE_REDIRECT_URI} \
    --build-arg VITE_TOKEN_URL=${VITE_TOKEN_URL} \
    --build-arg VITE_API_URL=${VITE_API_URL} .