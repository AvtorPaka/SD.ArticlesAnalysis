## Api-gateway

Nginx, working as api-gateway for SD.AA services.

### Deployment

#### 0. Change .env.template variables if needed

| **Variable**                | **Description**                                 | **Default** |
|-----------------------------|-------------------------------------------------|-------------|
| **SD_AA_GATEWAY_PORT** | Port, forwarded to docker container with an nginx api-gateway | 7071 |
| **SD_AA_A_URL**              | Base Url of [SD.AA.Analysis](https://github.com/AvtorPaka/SD.ArticlesAnalysis/tree/master/SD.ArticlesAnalysis.Analysis)  service     | http://host.docker.internal:7077  |
| **SD_AA_S_URL**       | Base Url of [SD.AA.Storage](https://github.com/AvtorPaka/SD.ArticlesAnalysis/tree/master/SD.ArticlesAnalysis.Storage) service           | http://host.docker.internal:7070 |


#### 1. Deploy

```shell
cd SD.ArticlesAnalysis.Gateway/ && touch .env && cp .env.template .env && docker compose up -d
```