FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM --platform=${BUILDPLATFORM} mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src

COPY ["src/SD.ArticlesAnalysis.Storage.Domain/SD.ArticlesAnalysis.Storage.Domain.csproj", "SD.ArticlesAnalysis.Storage.Domain/"]
COPY ["src/SD.ArticlesAnalysis.Storage.Infrastructure/SD.ArticlesAnalysis.Storage.Infrastructure.csproj", "SD.ArticlesAnalysis.Storage.Infrastructure/"]
COPY ["src/SD.ArticlesAnalysis.Storage.Api/SD.ArticlesAnalysis.Storage.Api.csproj", "SD.ArticlesAnalysis.Storage.Api/"]

RUN dotnet restore "SD.ArticlesAnalysis.Storage.Api/SD.ArticlesAnalysis.Storage.Api.csproj" --arch ${TARGETARCH}
COPY src/. .
RUN dotnet build "SD.ArticlesAnalysis.Storage.Api/SD.ArticlesAnalysis.Storage.Api.csproj" --arch ${TARGETARCH} -c Release --no-restore -o /app/build

WORKDIR "/src/SD.ArticlesAnalysis.Storage.Api"
FROM build AS publish
RUN dotnet publish "SD.ArticlesAnalysis.Storage.Api.csproj" --arch ${TARGETARCH}  -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN mkdir -p /file_data/articles && chmod -R 777 /file_data
    
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SD.ArticlesAnalysis.Storage.Api.dll"]