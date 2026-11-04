# Weather API

## Requirements

- .NET SDK 10.0.403 (see `global.json`)
- C# 14 / .NET 10

## Build and run

```sh
dotnet restore weather-parser.sln
dotnet build weather-parser.sln -c Release
cd Weather/Weather.Api
dotnet run
```

The API uses SQLite; configure `ConnectionStrings:DefaultConnection` in appsettings
or via `ConnectionStrings__DefaultConnection`. The database is created at startup.
Health check: `/health`. Swagger UI: `/swagger`.

## Dependency checks

```sh
dotnet list weather-parser.sln package --outdated
dotnet list weather-parser.sln package --vulnerable --include-transitive
```
