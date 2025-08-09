# Developer Store API

Este projeto é uma aplicação em .NET 8 com PostgreSQL rodando via Docker.
Siga as instruções abaixo para configurar e executar o ambiente localmente.

📋 **Pré-requisitos**

Antes de iniciar, você precisa ter instalado no seu computador:

.NET 8 SDK

**Docker** — última versão instalada e configurada corretamente

🚀 **Executando a aplicação**  *API Rodando em localhost:5000/swagger e Site rodando em localhost:4200*

No terminal, navegue até a pasta raiz do projeto e execute:

docker compose up --build

🔄 **Reiniciando o ambiente do zero**

Caso seja necessário parar e reiniciar tudo, use:

docker compose down
docker compose down -v
docker compose build
docker compose up

🗄️** Acessando o banco de dados no container**

Para acessar o banco de dados via terminal:

docker exec -it developerstore_postgres psql -U postgres -d developerstore

*Se estiver dentro do container via Docker Desktop, basta executar:*

psql -U postgres -d developerstore

🔍 **Consultando dados no PostgreSQL**
Exemplo de consulta:

SELECT * FROM saleitem;


