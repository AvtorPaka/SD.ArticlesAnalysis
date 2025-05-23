## Storage service

Service for storing and managing articles metadata and content.

### Deployment

#### 0. Change storage_deploy/.env.template variables if needed

| **Variable**                | **Description**                                 | **Default** |
|-----------------------------|-------------------------------------------------|-------------|
| **SD_AA_STORAGE_API_PORT** | Port, forwarded to docker container with an api | 7070        |
| **BUILD_ARCH**              | Your system / docker builder architecture       | arm64       |
| **BUILD_PLATFORM**          | Your system / docker builder OS                 | linux/arm64 |

#### 1. Deploy

```shell
cd SD.ArticlesAnalysis.Storage/storage_deploy && touch .env && cp .env.template .env && docker compose up -d
```

### Api specification presented in [Postman](https://www.postman.com/avtorpaka/sd-articlesanalysis/overview) workspace