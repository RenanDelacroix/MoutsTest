# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todas as pastas do projeto
COPY . .

# Restaura o projeto da API
WORKDIR /src/DeveloperStore.API
RUN dotnet restore

# Build com publish
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "DeveloperStore.API.dll"]
