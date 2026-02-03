FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Paqueteria.Core/Paqueteria.Core.csproj", "Paqueteria.Core/"]
RUN dotnet restore "Paqueteria.Core/Paqueteria.Core.csproj"
COPY . .
WORKDIR "/src/Paqueteria.Core"
RUN dotnet build "./Paqueteria.Core.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Paqueteria.Core.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paqueteria.Core.dll"]
