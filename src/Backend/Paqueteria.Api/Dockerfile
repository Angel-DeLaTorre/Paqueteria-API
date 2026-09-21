# Etapa 1: Compilación de la solución (SDK de .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# 1. Copiamos ÚNICAMENTE la carpeta Comun y la carpeta Backend completa
COPY src/Paqueteria.Comun/ src/Paqueteria.Comun/
COPY src/Backend/ src/Backend/

# 2. Nos movemos físicamente al directorio de la API dentro del contenedor
WORKDIR /src/src/Backend/Paqueteria.Api

# 3. Restauramos y compilamos de forma local y aislada
RUN dotnet restore "Paqueteria.Api.csproj"
RUN dotnet build "Paqueteria.Api.csproj" -c Release -o /app/build

# Etapa 2: Publicación de los binarios de la API
FROM build AS publish
RUN dotnet publish "Paqueteria.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Imagen de producción ligera (Runtime de ASP.NET Core 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paqueteria.Api.dll"]
