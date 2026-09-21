# Etapa de compilación
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS build-env
WORKDIR /src

# Copiar proyectos necesarios para el cliente
COPY ["src/Clientes/Paqueteria.WebAssembly/Paqueteria.WebAssembly.csproj", "src/Clientes/Paqueteria.WebAssembly/"]
COPY ["src/Clientes/Paqueteria.UI/Paqueteria.UI.csproj", "src/Clientes/Paqueteria.UI/"]
COPY ["src/Clientes/Paqueteria.Cliente.Core/Paqueteria.Cliente.Core.csproj", "src/Clientes/Paqueteria.Cliente.Core/"]
COPY ["src/Paqueteria.Comun/Paqueteria.Comun.csproj", "src/Paqueteria.Comun/"]

RUN dotnet restore "src/Clientes/Paqueteria.WebAssembly/Paqueteria.WebAssembly.csproj"

# Copiar código y publicar el sitio estático
COPY . .
WORKDIR "/src/src/Clientes/Paqueteria.WebAssembly"
RUN dotnet publish "Paqueteria.WebAssembly.csproj" -c Release -o /out

# Etapa de producción usando un servidor Nginx ultraligero
FROM nginx:alpine
EXPOSE 80
COPY --from=build-env /out/wwwroot /usr/share/nginx/html
# Configuración básica para soportar el ruteo de Blazor SPA (evita errores 404 al recargar)
RUN echo 'server { listen 80; location / { root /usr/share/nginx/html; try_files $uri $uri/ /index.html =404; } }' > /etc/nginx/conf.d/default.conf

