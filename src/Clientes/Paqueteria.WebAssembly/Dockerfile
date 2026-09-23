# Etapa 1: Compilación
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build-env
WORKDIR /src

RUN apt-get update && apt-get install -y python3 python3-pip && rm -rf /var/lib/apt/lists/*

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
RUN dotnet workload install wasm-tools

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

# Copiar los archivos publicados de wwwroot al directorio por defecto de Nginx
COPY --from=build-env /app/publish/wwwroot /usr/share/nginx/html

# Copiar la configuración personalizada de Nginx
COPY src/Clientes/Paqueteria.WebAssembly/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80 443
CMD ["nginx", "-g", "daemon off;"]