# LibraryManagementSystem

A simple .NET web API for managing Library.

This repository is built with .NET 8 and uses Entity Framework Core with a PostgreSQL database by default. The project includes a `DbInitializer` under `src/Data/DbInitializer.cs` to seed initial data.

---

## Table of contents

- [Prerequisites](#prerequisites)
- [Dependencies](#dependencies)
- [Configuration](#configuration)
  - [PostgreSQL (default)](#postgresql-default)
  - [Reconfigure to MySQL](#reconfigure-to-mysql)
  - [Reconfigure to MongoDB](#reconfigure-to-mongodb)
- [Database migrations](#database-migrations)
- [Seeding data](#seeding-data)
- [Run the application](#run-the-application)

---

## Prerequisites

- .NET SDK 8.0 (install from https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A running PostgreSQL server (local or remote)
- Optional for alternatives:
  - MySQL server (for MySQL provider)
  - MongoDB server (for MongoDB alternative)

Note: development was performed targeting .NET 8 (see `bin/Debug/net8.0`).

## Dependencies

Key dependencies used by the application (NuGet packages) include:

- Microsoft.EntityFrameworkCore (EF Core)
- Npgsql.EntityFrameworkCore.PostgreSQL (Postgres EF Core provider)
- Microsoft.AspNetCore.Authentication.JwtBearer (JWT authentication)
- DotNetEnv (optional if environment variables are used)
- Humanizer (utilities)

You can see exact versions in the project file `LibraryManagementSystem/LibraryManagementSystem.csproj`.


## Configuration

The application reads configuration from `appsettings.json` and environment variables. The most important setting is the database connection string. You can either store the connection string in `appsettings.json` under `ConnectionStrings:DefaultConnection` or set an environment variable.

### PostgreSQL (default)

Example `appsettings.json` connection string (replace values):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=<db_host>;Port=<db_port>;Database=<yourdbname>;Username=<yourusername>;Password=<yourpassword>"
  }
}
```

Alternatively set an environment variable in Windows (cmd.exe):

```cmd
setx POSTGRES_CONNECTION_STRING "Host=localhost;Port=5432;Database=librarydb;Username=postgres;Password=YourPassword"
```

If your project uses a different environment variable name (for example an ApplicationDbContext fallback), follow the name used in your code (look for `Environment.GetEnvironmentVariable(...)` in `src/Data` or the `Program.cs` configuration code).

### Reconfigure to MySQL

To use MySQL instead of PostgreSQL:

1. Install the MySQL EF Core provider (Pomelo):

```cmd
cd LibraryManagementSystem
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

2. Update `ApplicationDbContext` to use the MySQL provider. Replace the Npgsql `UseNpgsql(...)` call with something like:

```csharp
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
```

3. Update your connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=librarydb;User=root;Password=YourPassword;"
}
```

4. Recreate/add migrations for MySQL (recommended to remove old Postgres migrations or create a fresh migration):

```cmd
dotnet ef migrations add InitialForMySql
dotnet ef database update
```

Note: provider-specific SQL differences can require recreating migrations when switching providers.

### Reconfigure to MongoDB

Entity Framework Core does not support MongoDB. To switch to MongoDB you must replace EF Core usage with a MongoDB driver-based implementation. High-level steps:

1. Install the official MongoDB driver:

```cmd
cd LibraryManagementSystem
dotnet add package MongoDB.Driver
```

2. Add MongoDB settings to `appsettings.json`:

```json
"Mongo": {
  "ConnectionString": "mongodb://localhost:27017",
  "Database": "librarydb"
}
```

3. Register MongoDB services in `Program.cs` (example):

```csharp
// ...existing code...
using MongoDB.Driver;

var mongoConn = configuration.GetSection("Mongo:ConnectionString").Value;
var mongoDbName = configuration.GetSection("Mongo:Database").Value;
var mongoClient = new MongoClient(mongoConn);
var database = mongoClient.GetDatabase(mongoDbName);

builder.Services.AddSingleton<IMongoClient>(mongoClient);
// If you want typed collections, register them:
builder.Services.AddScoped(sp => database.GetCollection<Book>("Books"));
// ...existing code...
```

4. Replace EF-based repositories with implementations that use `IMongoCollection<T>` and the MongoDB.Driver query API.

5. Update seeding logic to use the MongoDB driver (InsertMany/InsertOne) instead of DbContext.

This is a non-trivial change; plan for repository/service refactors and tests.


## Database migrations

This project uses EF Core migrations which are stored in the `Migrations/` folder.

If you haven't installed the EF CLI tool, install it globally (optional):

```cmd
dotnet tool install --global dotnet-ef --version 8.*
```

Or rely on the `Microsoft.EntityFrameworkCore.Design` package and run `dotnet ef` from the project folder.

Common commands (run from the `LibraryManagementSystem` project folder):

```cmd
cd LibraryManagementSystem

:: Add a migration (after model changes)
dotnet ef migrations add YourMigrationName

:: Apply migrations to the database
dotnet ef database update
```

If you need to target a specific project/startup project when the solution has multiple projects, use the `--project` and `--startup-project` flags.

## Seeding data

A seeding helper is available at `src/Data/DbInitializer.cs`. The initializer inserts several users, books, and a rent record when the database is empty. If you want to force reseeding, you can clear tables or recreate the database, then rerun migrations and the app.

## Run the application

From the repository root, run the following (Windows `cmd.exe`):

```cmd
cd LibraryManagementSystem

:: Restore packages and build
dotnet restore
dotnet build

:: Apply migrations (creates/upgrades the database)
dotnet ef database update

:: Run the app (this will start the web API)
dotnet run
```

By default the application will listen on the ports configured in `Properties/launchSettings.json` or via environment variables. Open your browser or API client at the printed URL (e.g., `https://localhost:5001`) and test endpoints under `src/Controllers`.

---

If you want, I can also:

- Add example `appsettings.Development.json` entries for Postgres/MySQL/MongoDB.
- Add explicit instructions/code snippets showing how to register the Postgres `ApplicationDbContext` in `Program.cs`.
- Provide a small migration rollback/cleanup guide.

Tell me which extras you'd like and I'll add them.

