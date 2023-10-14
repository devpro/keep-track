# Keeptrack

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/keeptrack-ci?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=26&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.keep-track)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.keep-track&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.keep-track)

> Would you like to save somewhere the book you read, the movie you watched or the video game you completed?

Keeptrack is a an open source application to save and review what you like.

## Instances

This code can be deployed to any kind of infrastructure. It is Cloud native.

A SaaS free version is currently available for early adopters. Contact the main developer for an access.

## Software design

Frontend is a [web application](angular-bootstrap/README.md) (Single Page Application) written in Angular/TypeScript (16+).

Backend is a [web API](dotnet/README.md) (REST) written in .NET/C# (7+).

Data is persisted in a MongoDB database.

Authentication can be done from multiple providers (Google, GitHub, etc.) and is federated by Firebase Authentication.
