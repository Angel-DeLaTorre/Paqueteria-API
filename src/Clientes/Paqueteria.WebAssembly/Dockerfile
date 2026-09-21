# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /src

COPY src/Paqueteria.Comun/ src/Paqueteria.Comun/
COPY src/Clientes/ src/Clientes/

WORKDIR /src/src/Clientes/Paqueteria.WebAssembly

# 3. Restauramos y publicamos el sitio de forma totalmente aislada
RUN dotnet restore "Paqueteria.WebAssembly.csproj"
RUN dotnet publish "Paqueteria.WebAssembly.csproj" -c Release -o /out

# Etapa 2: Producción usando un servidor Nginx ultraligero
FROM nginx:alpine
EXPOSE 80

COPY --from=build-env /out/wwwroot /usr/share/nginx/html

#  Inyectamos una configuración completa de Nginx para soportar tipos de archivos .wasm y SPA Routing
RUN echo 'server { \
    listen 80; \
    server_name localhost; \
    root /usr/share/nginx/html/wwwroot; \
    index index.html; \
    location / { \
        try_files $uri $uri/ /index.html; \
    } \
    location ~* \.wasm$ { \
        types { application/wasm wasm; } \
        add_header Cache-Control "no-cache"; \
    } \
}' > /etc/nginx/conf.d/default.conf