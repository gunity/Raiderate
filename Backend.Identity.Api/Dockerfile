FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend.Identity.Api/Backend.Identity.Api.csproj", "Backend.Identity.Api/"]
COPY ["Backend.Identity.Application/Backend.Identity.Application.csproj", "Backend.Identity.Application/"]
COPY ["Backend.Identity.Domain/Backend.Identity.Domain.csproj", "Backend.Identity.Domain/"]
COPY ["Backend.Shared/Backend.Shared.csproj", "Backend.Shared/"]
COPY ["Backend.Identity.Infrastructure/Backend.Identity.Infrastructure.csproj", "Backend.Identity.Infrastructure/"]
RUN dotnet restore "Backend.Identity.Api/Backend.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Backend.Identity.Api"
RUN dotnet build "./Backend.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Backend.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.Identity.Api.dll"]
