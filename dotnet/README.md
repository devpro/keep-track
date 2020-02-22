# Keep track .NET solution

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/keeptrack-ci?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=26&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.keep-track)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.keep-track)

## Design

This design of API has been inspired by the [Hexagonal Architecture](https://blog.octo.com/en/hexagonal-architecture-three-principles-and-an-implementation-example/).

## Dependencies

- [.NET Core 3.1 SDK](dot.net)
- MongoDB 4.2 database
  - Local server
  - Docker

  ```bash
  docker run --name mongodb422 -d -p 27017:27017 mongo
  ```

  - [MongoDB Atlas](https://cloud.mongodb.com/) cluster

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

## Deploy

- Add the outbout IP to the MongoDB Atlas cluster
- Add the application url to Firebase domains
- Create web project in Firebase and grab ids to be set to environment.ts file
- Create a GitHub OAuth application ([firebase.google.com](https://firebase.google.com/docs/auth/web/github-auth), [github.com](https://github.com/settings/applications/new))
- Add urls in Azure web app CORS page
