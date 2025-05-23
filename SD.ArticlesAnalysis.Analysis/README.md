## Analysis service

Service for analyzing articles data from [SD.AA.Storage](https://github.com/AvtorPaka/SD.ArticlesAnalysis/tree/master/SD.ArticlesAnalysis.Storage) service. Counts number of paragraphs, words, characters in article text. Creates word cloud images for articles content. 

### Deployment 

#### 0. Change analysis_deploy/.env.template variables if needed

| **Variable**                | **Description**                                 | **Default** |
|-----------------------------|-------------------------------------------------|--------|
| **SD_AA_ANALYSIS_API_PORT** | Port, forwarded to docker container with an api | 7077   |
| **SD_AA_STORAGE_URL**       | Base url of [SD.AA.Storage](https://github.com/AvtorPaka/SD.ArticlesAnalysis/tree/master/SD.ArticlesAnalysis.Storage) service           | http://host.docker.internal:7070 |
| **BUILD_ARCH**              | Your system / docker builder architecture       | arm64  |
| **BUILD_PLATFORM**          | Your system / docker builder OS                 | linux/arm64 |

#### 1. Deploy 

```shell
cd SD.ArticlesAnalysis.Analysis/analysis_deploy && touch .env && cp .env.template .env && docker compose up -d
```

### Api specification presented in [Postman](https://www.postman.com/avtorpaka/sd-articlesanalysis/overview) workspace