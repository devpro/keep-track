# Keep track .NET solution

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/keeptrack-CI?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=18&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.keep-track)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.keep-track)

## Dependencies

- SDK: .NET Core 3.0
- DB: MongoDB 4.2

## Configuration

- Value for key `KeepTrack_MongoDbConnectionString`: .NET connection string to access MongoDB cluster, ideally set as an environment variable.

## Local run

- Clone the solution: `git clone ...`
- Build the solution: `dotnet build`
- Run the console: `dotnet dotnet src\ConsoleApp\bin\Debug\netcoreapp3.0\KeepTrack.ConsoleApp.dll ...`
- Run the web api: `dotnet run --project src\Api`
