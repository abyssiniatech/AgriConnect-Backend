# AgriConnect Ethiopia — Database Setup Guide

## 1. Introduction

This document explains how to configure the PostgreSQL database required by AgriConnect Ethiopia for local development and testing.

The database setup process includes:

1. Installing PostgreSQL.
2. Creating the development database.
3. Configuring the connection string.
4. Applying Entity Framework Core migrations.
5. Seeding development data.
6. Verifying the database.
7. Troubleshooting common database problems.

---

# 2. Database Technology

AgriConnect uses:

| Component           | Technology            |
| ------------------- | --------------------- |
| Database            | PostgreSQL            |
| ORM                 | Entity Framework Core |
| Backend             | ASP.NET Core          |
| Database migrations | EF Core Migrations    |

The exact PostgreSQL and EF Core versions should match the project configuration.

---

# 3. PostgreSQL Requirements

Verify PostgreSQL:

```bash
psql --version
```

Example:

```text
psql (PostgreSQL) 18.x
```

Make sure the PostgreSQL server is running before continuing.

---

# 4. Connect to PostgreSQL

Connect using the PostgreSQL administrator account:

```bash
psql -U postgres
```

If PostgreSQL is configured on another host or port:

```bash
psql -h localhost -p 5432 -U postgres
```

Enter the PostgreSQL password when prompted.

---

# 5. Create the Development Database

Create the AgriConnect development database:

```sql
CREATE DATABASE agric_connect;
```

Verify that the database exists:

```sql
\l
```

Connect to it:

```sql
\c agric_connect
```

Verify the current database:

```sql
SELECT current_database();
```

The result should be:

```text
agric_connect
```

---

# 6. Database Naming

The development database name should match the application's configured connection string.

Recommended development name:

```text
agric_connect
```

If a different database name is used, update the backend configuration accordingly.

---

# 7. Database Connection String

Configure the backend connection string.

Example:

```text
Host=localhost;Port=5432;Database=agric_connect;Username=postgres;Password=YOUR_PASSWORD
```

The connection string should be stored using the project's approved configuration mechanism.

Do not commit a real database password to Git.

---

# 8. Configure appsettings

A development configuration may contain:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=agric_connect;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

Use the actual connection string expected by the application's DbContext configuration.

---

# 9. Verify Entity Framework Core

From the backend project directory, run:

```bash
dotnet ef --version
```

If the command is unavailable:

```bash
dotnet tool install --global dotnet-ef
```

Then verify:

```bash
dotnet ef --version
```

---

# 10. Restore Backend Dependencies

Before running migrations:

```bash
dotnet restore
```

Then build the project:

```bash
dotnet build
```

The project should build successfully before database migration commands are executed.

---

# 11. Review Existing Migrations

List the available migrations:

```bash
dotnet ef migrations list
```

Review the migration history before applying changes to the database.

Do not delete existing migrations simply because a migration fails.

---

# 12. Apply Migrations

Apply all pending migrations:

```bash
dotnet ef database update
```

Entity Framework Core will create or update the database schema according to the migration history.

---

# 13. Verify Database Tables

Connect to the database:

```bash
psql -U postgres -d agric_connect
```

List tables:

```sql
\dt
```

You should see the tables created by the application's migrations.

The exact table names depend on the current implementation.

---

# 14. Inspect the Database Schema

To inspect a table:

```sql
\d table_name
```

For example:

```sql
\d users
```

If the project uses different table names, replace `users` with the actual table name.

---

# 15. Verify Migration History

Entity Framework Core normally maintains a migration history table.

Check:

```sql
SELECT * FROM "__EFMigrationsHistory";
```

This shows which migrations have been applied to the database.

---

# 16. Database Seeding

AgriConnect may use a database seeder to create development data.

Seeded data may include:

* Users.
* Farmers.
* Buyers.
* Experts.
* Logistics providers.
* Products.
* Categories.
* Orders.
* Other required reference data.

The actual seed data depends on the implementation.

---

# 17. Run the Application Seeder

If the project performs seeding during application startup:

```bash
dotnet run
```

Monitor the console output for database initialization and seeding messages.

If the project provides a dedicated seed command, use the command documented by the implementation.

---

# 18. Verify Seeded Data

After the application starts, connect to PostgreSQL:

```bash
psql -U postgres -d agric_connect
```

Inspect the relevant tables:

```sql
SELECT * FROM users;
```

or:

```sql
SELECT COUNT(*) FROM users;
```

Use the actual table names from the project schema.

---

# 19. Reset Development Database

When a complete database reset is intentionally required during local development, the database can be dropped and recreated.

First disconnect from the database:

```sql
\c postgres
```

Then:

```sql
DROP DATABASE agric_connect;
```

Recreate it:

```sql
CREATE DATABASE agric_connect;
```

Apply migrations again:

```bash
dotnet ef database update
```

> **Warning:** Dropping the database permanently deletes all data in that database. Never perform this operation against production.

---

# 20. Development Database Backup

Create a PostgreSQL backup:

```bash
pg_dump -U postgres -d agric_connect > agric_connect_backup.sql
```

Restore a backup:

```bash
psql -U postgres -d agric_connect < agric_connect_backup.sql
```

Use appropriate credentials and connection parameters for the environment.

---

# 21. Database Permissions

The application database user should have only the permissions required by the application.

For local development, the PostgreSQL administrator account may be used if that matches the project's setup.

Production environments should use a dedicated database user with restricted permissions.

---

# 22. Database Security

Developers must:

* Never commit database passwords.
* Never expose production credentials.
* Never use production data unnecessarily in development.
* Protect database backups.
* Use secure credentials.
* Restrict database access.
* Avoid exposing PostgreSQL directly to the public internet.

---

# 23. Common Database Problems

## PostgreSQL Connection Refused

Check:

```bash
psql -U postgres -h localhost -p 5432
```

Verify that:

* PostgreSQL is running.
* Host is correct.
* Port is correct.
* Firewall rules are appropriate.

---

## Database Does Not Exist

List databases:

```sql
\l
```

Create the database if required:

```sql
CREATE DATABASE agric_connect;
```

---

## Authentication Failed

Verify:

* PostgreSQL username.
* Password.
* Host.
* Port.
* Authentication configuration.

Do not change PostgreSQL authentication settings without understanding their security implications.

---

## Migration Fails

First inspect:

```bash
dotnet ef migrations list
```

Then verify:

* Connection string.
* PostgreSQL availability.
* Current migration state.
* Migration dependencies.
* Database permissions.

Review the complete error message before changing migration files.

---

## Table Does Not Exist

Check:

```sql
\dt
```

Then verify whether migrations have been applied:

```bash
dotnet ef database update
```

---

# 24. Database Verification Checklist

Before considering the database ready:

* [ ] PostgreSQL is installed.
* [ ] PostgreSQL service is running.
* [ ] `psql` works.
* [ ] Development database exists.
* [ ] Connection string is configured.
* [ ] EF Core CLI is available.
* [ ] Backend dependencies are restored.
* [ ] Backend builds successfully.
* [ ] Migrations are available.
* [ ] Migrations are applied.
* [ ] Required tables exist.
* [ ] Seed data is available when required.
* [ ] Application can connect successfully.

---

# 25. Recommended Development Workflow

Use this sequence when setting up a fresh development environment:

```text
Install PostgreSQL
       ↓
Start PostgreSQL
       ↓
Create agric_connect Database
       ↓
Configure Connection String
       ↓
Restore .NET Dependencies
       ↓
Build Backend
       ↓
Check EF Core Migrations
       ↓
Apply Database Migrations
       ↓
Run Seeder
       ↓
Verify Tables and Data
       ↓
Start Backend
       ↓
Test API
```

---

# 26. Conclusion

A correctly configured PostgreSQL database is required for AgriConnect backend development.

Developers should use Entity Framework Core migrations to maintain the schema, use seed data only where appropriate, protect database credentials, and never perform destructive database operations against production.
