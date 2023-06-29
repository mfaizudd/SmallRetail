FROM node:18-alpine AS builder
WORKDIR /app
COPY package.json tailwind.config.js tsconfig.json vite.config.ts ./
RUN npm install
ARG VITE_AUTHORIZE_URL
ARG VITE_CLIENT_ID
ARG VITE_REDIRECT_URI
ARG VITE_TOKEN_URL
ARG VITE_API_URL
ENV VITE_AUTHORIZE_URL=${VITE_AUTHORIZE_URL} \
    VITE_CLIENT_ID=${VITE_CLIENT_ID} \
    VITE_REDIRECT_URI=${VITE_REDIRECT_URI} \
    VITE_TOKEN_URL=${VITE_TOKEN_URL} \
    VITE_API_URL=${VITE_API_URL}
COPY . .
RUN npm run build

FROM nginx:alpine AS runner
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /app/dist /usr/share/nginx/html