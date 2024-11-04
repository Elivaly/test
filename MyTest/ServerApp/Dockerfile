# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/runtime:8.0-nanoserver-1809 AS base
WORKDIR /app


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServerApp/ServerApp.csproj", "ServerApp/"]
RUN dotnet restore "./ServerApp/ServerApp.csproj"
COPY . .
WORKDIR "/src/ServerApp"
RUN dotnet build "./ServerApp.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ServerApp.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerApp.dll"]