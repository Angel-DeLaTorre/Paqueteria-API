# Etapa 1: Compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /src

COPY ["src/Clientes/Paqueteria.WebAssembly/Paqueteria.WebAssembly.csproj", "src/Clientes/Paqueteria.WebAssembly/"]
COPY ["src/Clientes/Paqueteria.UI/Paqueteria.UI.csproj", "src/Clientes/Paqueteria.UI/"]
COPY ["src/Clientes/Paqueteria.Cliente.Core/Paqueteria.Cliente.Core.csproj", "src/Clientes/Paqueteria.Cliente.Core/"]
COPY ["src/Paqueteria.Comun/Paqueteria.Comun.csproj", "src/Paqueteria.Comun/"]

RUN dotnet restore "src/Clientes/Paqueteria.WebAssembly/Paqueteria.WebAssembly.csproj"

COPY . .
WORKDIR "/src/src/Clientes/Paqueteria.WebAssembly"
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Producción usando Nginx
FROM nginx:alpine

COPY --from=build-env /app/publish/wwwroot /usr/share/nginx/html

COPY src/Clientes/Paqueteria.WebAssembly/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80 443
CMD ["nginx", "-g", "daemon off;"]