# Stock Stats API

Tiny Web API (ASP.NET Core 8) that demonstrates how we query **Elasticsearch** with the official .NET client to fetch real‑time stock‑market data.

### Prerequisites
* **Elasticsearch 8.x** running at `http://localhost:9200` and containing an index called `stocks`.
* .NET 8 SDK.

### Run Locally
```bash
# restore & build
$ dotnet build

# start the API (defaults to http://localhost:5000)
$ dotnet run --project src/StockApi