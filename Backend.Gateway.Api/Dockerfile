FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend.Gateway.Api/Backend.Gateway.Api.csproj", "Backend.Gateway.Api/"]
COPY ["Backend.Gateway.Infrastructure/Backend.Gateway.Infrastructure.csproj", "Backend.Gateway.Infrastructure/"]
COPY ["Backend.Shared/Backend.Shared.csproj", "Backend.Shared/"]
RUN dotnet restore "Backend.Gateway.Api/Backend.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/Backend.Gateway.Api"
RUN dotnet build "./Backend.Gateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Backend.Gateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.Gateway.Api.dll"]
