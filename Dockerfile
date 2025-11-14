# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# copia tudo
COPY . .

# restaura e publica
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

# Render usa a variável $PORT automaticamente
ENV ASPNETCORE_URLS=http://0.0.0.0:$PORT

EXPOSE 8080

ENTRYPOINT ["dotnet", "FuturoDoTrabalho.Api.dll"]
