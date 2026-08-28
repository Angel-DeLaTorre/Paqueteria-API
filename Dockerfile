# =========================================================
# Etapa 1: Compilación de la solución (SDK de .NET 10)
# =========================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copiamos todos los archivos .csproj replicando su estructura exacta de carpetas
# Esto permite aprovechar la caché de Docker para acelerar futuras compilaciones
COPY ["Paqueteria.Api/Paqueteria.Api.csproj", "Paqueteria.Api/"]
COPY ["Paqueteria.Aplicacion/Paqueteria.Aplicacion.csproj", "Paqueteria.Aplicacion/"]
COPY ["Paqueteria.Core/Paqueteria.Core.csproj", "Paqueteria.Core/"]
COPY ["Paqueteria.Infraestructura/Paqueteria.Infraestructura.csproj", "Paqueteria.Infraestructura/"]

# 2. Restauramos las dependencias NuGet basándonos en el proyecto ejecutable principal
RUN dotnet restore "Paqueteria.Api/Paqueteria.Api.csproj"

# 3. Copiamos absolutamente el resto del código fuente del repositorio
COPY . .

# 4. Nos movemos a la capa de la API y compilamos en modo Release sin generar el ejecutable nativo del Host
WORKDIR "/src/Paqueteria.Api"
RUN dotnet build "Paqueteria.Api.csproj" -c Release -o /app/build

# =========================================================
# Etapa 2: Publicación de los binarios optimizados
# =========================================================
FROM build AS publish
RUN dotnet publish "Paqueteria.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================================================
# Etapa 3: Imagen de ejecución final (Runtime ligero de .NET 10)
# =========================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Exponer el puerto por defecto de .NET en ambientes de contenedores Linux
EXPOSE 8080

# Copiamos solo los archivos publicados (sin código fuente, compiladores ni SDK)
COPY --from=publish /app/publish .

# Comando de arranque del contenedor
ENTRYPOINT ["dotnet", "Paqueteria.Api.dll"]