# Etapa 1 — Build do Angular
FROM node:20 AS build
WORKDIR /app

# Copia dependências e instala
COPY package*.json ./
RUN npm install

# Copia todo o código
COPY . .

# Build usando environment.prod.ts
RUN npm run build -- --configuration production

FROM nginx:1.27

# Remove configuração padrão do nginx
RUN rm /etc/nginx/conf.d/default.conf

# Copia configuração personalizada
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copia os arquivos buildados para o nginx
COPY --from=build /app/dist/* /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
