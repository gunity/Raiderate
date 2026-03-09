FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend.Players.Api/Backend.Players.Api.csproj", "Backend.Players.Api/"]
COPY ["Backend.Players.Application/Backend.Players.Application.csproj", "Backend.Players.Application/"]
COPY ["Backend.Players.Domain/Backend.Players.Domain.csproj", "Backend.Players.Domain/"]
COPY ["Backend.Shared/Backend.Shared.csproj", "Backend.Shared/"]
COPY ["Backend.Players.Infrastructure/Backend.Players.Infrastructure.csproj", "Backend.Players.Infrastructure/"]
RUN dotnet restore "Backend.Players.Api/Backend.Players.Api.csproj"
COPY . .
WORKDIR "/src/Backend.Players.Api"
RUN dotnet build "./Backend.Players.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Backend.Players.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.Players.Api.dll"]
