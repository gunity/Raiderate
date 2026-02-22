FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend.Ratings.Api/Backend.Ratings.Api.csproj", "Backend.Ratings.Api/"]
COPY ["Backend.Ratings.Application/Backend.Ratings.Application.csproj", "Backend.Ratings.Application/"]
COPY ["Backend.Ratings.Domain/Backend.Ratings.Domain.csproj", "Backend.Ratings.Domain/"]
COPY ["Backend.Shared/Backend.Shared.csproj", "Backend.Shared/"]
COPY ["Backend.Ratings.Infrastructure/Backend.Ratings.Infrastructure.csproj", "Backend.Ratings.Infrastructure/"]
RUN dotnet restore "Backend.Ratings.Api/Backend.Ratings.Api.csproj"
COPY . .
WORKDIR "/src/Backend.Ratings.Api"
RUN dotnet build "./Backend.Ratings.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Backend.Ratings.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.Ratings.Api.dll"]
