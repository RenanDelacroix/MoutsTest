# --- build (Angular) ---
FROM node:20-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .
# build de produção (usa o fileReplacements para environment.prod.ts)
RUN npm run build -- --configuration=production

# --- runtime (Nginx) ---
FROM nginx:1.25-alpine

# copia a config do proxy
COPY nginx.conf /etc/nginx/conf.d/default.conf

# copie o output do Angular para a raiz do Nginx
# ajuste este caminho se o seu outputPath for outro no angular.json
COPY --from=build /app/dist/developer-store.website /usr/share/nginx/html

EXPOSE 80
