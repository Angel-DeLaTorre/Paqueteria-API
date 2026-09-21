# Etapa 1: Compilación de la solución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS build
WORKDIR /src

# Copiamos todos los archivos .csproj con la estructura de carpetas real
COPY ["src/Backend/Paqueteria.Api/Paqueteria.Api.csproj", "src/Backend/Paqueteria.Api/"]
COPY ["src/Backend/Paqueteria.Aplicacion/Paqueteria.Aplicacion.csproj", "src/Backend/Paqueteria.Aplicacion/"]
COPY ["src/Backend/Paqueteria.Dominio/Paqueteria.Dominio.csproj", "src/Backend/Paqueteria.Dominio/"]
COPY ["src/Backend/Paqueteria.Infraestructura/Paqueteria.Infraestructura.csproj", "src/Backend/Paqueteria.Infraestructura/"]
COPY ["src/Paqueteria.Comun/Paqueteria.Comun.csproj", "src/Paqueteria.Comun/"]

# Restauramos las dependencias NuGet
RUN dotnet restore "src/Backend/Paqueteria.Api/Paqueteria.Api.csproj"

# Copiamos el resto del código fuente
COPY . .

# Nos movemos al directorio de la API y compilamos
WORKDIR "/src/src/Backend/Paqueteria.Api"
RUN dotnet build "Paqueteria.Api.csproj" -c Release -o /app/build

# Etapa 2: Publicación de los binarios optimizados
FROM build AS publish
RUN dotnet publish "Paqueteria.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Imagen de ejecución final
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paqueteria.Api.dll"]
