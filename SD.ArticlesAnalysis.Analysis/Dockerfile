FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM --platform=${BUILDPLATFORM} mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src

COPY ["src/SD.ArticlesAnalysis.Analysis.Domain/SD.ArticlesAnalysis.Analysis.Domain.csproj", "SD.ArticlesAnalysis.Analysis.Domain/"]
COPY ["src/SD.ArticlesAnalysis.Analysis.Infrastructure/SD.ArticlesAnalysis.Analysis.Infrastructure.csproj", "SD.ArticlesAnalysis.Analysis.Infrastructure/"]
COPY ["src/SD.ArticlesAnalysis.Analysis.Api/SD.ArticlesAnalysis.Analysis.Api.csproj", "SD.ArticlesAnalysis.Analysis.Api/"]

RUN dotnet restore "SD.ArticlesAnalysis.Analysis.Api/SD.ArticlesAnalysis.Analysis.Api.csproj" --arch ${TARGETARCH}
COPY src/. .
RUN dotnet build "SD.ArticlesAnalysis.Analysis.Api/SD.ArticlesAnalysis.Analysis.Api.csproj" --arch ${TARGETARCH} -c Release --no-restore -o /app/build

WORKDIR "/src/SD.ArticlesAnalysis.Analysis.Api"
FROM build AS publish
RUN dotnet publish "SD.ArticlesAnalysis.Analysis.Api.csproj" --arch ${TARGETARCH}  -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN mkdir -p /wordcloud_data/img && chmod -R 777 /wordcloud_data
    
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SD.ArticlesAnalysis.Analysis.Api.dll"]