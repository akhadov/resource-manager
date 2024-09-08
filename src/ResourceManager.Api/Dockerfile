FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ResourceManager.Api/ResourceManager.Api.csproj", "src/ResourceManager.Api/"]
COPY ["src/ResourceManager.Infrastructure/ResourceManager.Infrastructure.csproj", "src/ResourceManager.Infrastructure/"]
COPY ["src/ResourceManager.Application/ResourceManager.Application.csproj", "src/ResourceManager.Application/"]
COPY ["src/ResourceManager.Domain/ResourceManager.Domain.csproj", "src/ResourceManager.Domain/"]
COPY ["src/ResourceManager.SharedKernel/ResourceManager.SharedKernel.csproj", "src/ResourceManager.SharedKernel/"]
RUN dotnet restore "./src/ResourceManager.Api/ResourceManager.Api.csproj"
COPY . .
WORKDIR "/src/src/ResourceManager.Api"
RUN dotnet build "./ResourceManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ResourceManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResourceManager.Api.dll"]