FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /src

COPY src/Paqueteria.Comun/ src/Paqueteria.Comun/
COPY src/Clientes/ src/Clientes/

WORKDIR /src/src/Clientes/Paqueteria.WebAssembly

RUN dotnet restore "Paqueteria.WebAssembly.csproj"
RUN dotnet publish "Paqueteria.WebAssembly.csproj" -c Release -o /out

# Etapa 2: Producción usando Nginx
FROM nginx:alpine
EXPOSE 80

# Copiamos el contenido publicado (/out contiene wwwroot con todos los _content y assets)
COPY --from=build-env /out /usr/share/nginx/html

# Configuración limpia de Nginx apuntando al subdirectorio wwwroot y soportando SPA
RUN echo 'server { \
    listen 80; \
    server_name localhost; \
    root /usr/share/nginx/html/wwwroot; \
    index index.html; \
    location / { \
        try_files $uri $uri/ /index.html; \
    } \
    location ~* \.(wasm|clr)$ { \
        types { application/wasm wasm; } \
        add_header Cache-Control "no-cache"; \
    } \
}' > /etc/nginx/conf.d/default.conf