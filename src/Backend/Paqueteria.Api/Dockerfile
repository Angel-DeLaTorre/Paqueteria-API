# =========================================================
# Etapa 1: Compilación de la solución (SDK de .NET 10)
# =========================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /src

# Copiamos todos los archivos .csproj con la nueva estructura de carpetas
COPY ["src/Backend/Paqueteria.Api/Paqueteria.Api.csproj", "src/Backend/Paqueteria.Api/"]
COPY ["src/Backend/Paqueteria.Aplicacion/Paqueteria.Aplicacion.csproj", "src/Backend/Paqueteria.Aplicacion/"]
COPY ["src/Backend/Paqueteria.Dominio/Paqueteria.Dominio.csproj", "src/Backend/Paqueteria.Dominio/"]
COPY ["src/Backend/Paqueteria.Infraestructura/Paqueteria.Infraestructura.csproj", "src/Backend/Paqueteria.Infraestructura/"]
COPY ["src/Paqueteria.Comun/Paqueteria.Comun.csproj", "src/Paqueteria.Comun/"]

# Restauramos las dependencias NuGet basándonos en el proyecto ejecutable principal
RUN dotnet restore "src/Backend/Paqueteria.Api/Paqueteria.Api.csproj"

# Copiamos absolutamente el resto del código fuente del repositorio
COPY . .

# Nos movemos a la capa de la API y compilamos en modo Release
WORKDIR "/src/src/Backend/Paqueteria.Api"
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

EXPOSE 8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Paqueteria.Api.dll"]
