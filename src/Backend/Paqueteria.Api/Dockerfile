# Etapa 1: Compilación de la solución (SDK de .NET 10)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS build
WORKDIR /app

# Copiamos los archivos de proyecto para aprovechar la caché de capas de Docker
COPY ["src/Backend/Paqueteria.Api/Paqueteria.Api.csproj", "src/Backend/Paqueteria.Api/"]
COPY ["src/Backend/Paqueteria.Aplicacion/Paqueteria.Aplicacion.csproj", "src/Backend/Paqueteria.Aplicacion/"]
COPY ["src/Backend/Paqueteria.Dominio/Paqueteria.Dominio.csproj", "src/Backend/Paqueteria.Dominio/"]
COPY ["src/Backend/Paqueteria.Infraestructura/Paqueteria.Infraestructura.csproj", "src/Backend/Paqueteria.Infraestructura/"]
COPY ["src/Paqueteria.Comun/Paqueteria.Comun.csproj", "src/Paqueteria.Comun/"]

# Restauramos las dependencias NuGet de la API Web
RUN dotnet restore "src/Backend/Paqueteria.Api/Paqueteria.Api.csproj"

# Copiamos todo el resto del código fuente del backend
COPY . .

# Compilamos el proyecto ejecutable de la API
RUN dotnet build "src/Backend/Paqueteria.Api/Paqueteria.Api.csproj" -c Release -o /app/build

# Etapa 2: Publicación de los binarios de la API
FROM build AS publish
RUN dotnet publish "src/Backend/Paqueteria.Api/Paqueteria.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Imagen de producción ligera (Runtime de ASP.NET Core 10)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paqueteria.Api.dll"]
