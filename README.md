# Developer Store API

Este projeto é uma aplicação em .NET 8 com PostgreSQL rodando via Docker.
Siga as instruções abaixo para configurar e executar o ambiente localmente.

📋 **Pré-requisitos**

Antes de iniciar, você precisa ter instalado no seu computador:

.NET 8 SDK
.Angular CLI: 16.2.16
.Node: 18.17.1
.Package Manager: npm 9.6.7
.OS: win32 x64
.npm 9.6.7
.Server: Docker Desktop 4.12.0 (85629)
        - Version: 20.10.17

**Docker** — última versão instalada e configurada corretamente

🚀 **Executando a aplicação via Docker**  *Rodando API em 'localhost:5000/swagger' e Website em 'localhost:4200'*

No terminal, navegue até a pasta raiz do projeto onde está a sln e docker-compose e execute:

**- docker compose up --build**

### *Tudo vai subir para o container e ficar exposto:*
 - API no [localhost:5000/swagger]
 - Website ANgular no [localhost:4200]


🚀 **Executando a aplicação local no Visual Studio 2022**
    - Apontar o projeto DeveloperStore.API como *start-up project* no VisualStudio
    - Executar normalmente com debug no IISExpress
    - Angular: Ir para a pasta raiz do projeto DeveloperStore.Website e executar npm start no terminal de comando

NESTE CASO:
 - API no [localhost:44345/swagger]
 - Website Angular no [localhost:4200] também

🔄 **Reiniciando o ambiente do zero**

### Caso seja necessário parar e reiniciar tudo, use:

docker compose down
docker compose down -v
docker compose build
docker compose up

_____________________________________________________________________________

🗄️** Acessando o banco de dados no container**

Para acessar o banco de dados via terminal:

docker exec -it developerstore_postgres psql -U postgres -d developerstore

*Se estiver dentro do container via Docker Desktop, basta executar:*

psql -U postgres -d developerstore

🔍 **Consultando dados no PostgreSQL**
Exemplo de consulta:

SELECT * FROM saleitem;


