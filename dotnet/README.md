# Keep track .NET solution

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/keeptrack-CI?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=18&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.keep-track)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.keep-track)

## Dependencies

- [.NET Core 3.1 SDK](dot.net)
- MongoDB 4.2 database
  - Local server
  - Docker

  ```bash
  docker run --name mongodb422 -d -p 27017:27017 mongo
  ```

  - MongoDB Atlas cluster

## Configuration

Key | Description | Default value
--- | ----------- | -------------
`Infrastructure:MongoDB:ConnectionString` | MongoDB connection string |
`Infrastructure:MongoDB:DatabaseName` | MongoDB connection string | inventory

This values can be easily provided as environment variables (replace ":" by "__") or by configuration (json).

## Build & debug

- Clone the solution

```bash
git clone https://github.com/devpro/keep-track.git
```

- Build the solution

```bash
dotnet build
```

- Run the tests

```bash
dotnet build
```

- Run the console application:

```bash
dotnet src\ConsoleApp\bin\Debug\netcoreapp3.1\KeepTrack.ConsoleApp.dll -a CarDemo
```

- Run the web api application:

```bash
dotnet run --project src\Api
```
