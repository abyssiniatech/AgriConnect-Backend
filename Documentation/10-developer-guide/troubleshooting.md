# AgriConnect Ethiopia — Troubleshooting Guide

## 1. Introduction

This guide provides solutions for common problems encountered while developing, running, testing, and configuring AgriConnect Ethiopia.

The main areas covered are:

* Git
* .NET backend
* Angular frontend
* PostgreSQL
* Entity Framework Core
* API communication
* Authentication
* Configuration
* Build and runtime errors

---

# 2. General Troubleshooting Process

When a problem occurs, follow this sequence:

```text
Identify the Error
       ↓
Read the Complete Error Message
       ↓
Check Recent Changes
       ↓
Check Configuration
       ↓
Check Dependencies
       ↓
Check Logs
       ↓
Reproduce the Problem
       ↓
Apply a Targeted Fix
       ↓
Run Tests
       ↓
Verify the Fix
```

Avoid making multiple unrelated changes at the same time.

---

# 3. Git Troubleshooting

## 3.1 Check Repository Status

Run:

```bash
git status
```

This shows:

* Current branch.
* Modified files.
* Untracked files.
* Staged files.

---

## 3.2 Check Current Branch

```bash
git branch --show-current
```

Make sure you are working on the intended branch.

---

## 3.3 Changes Are Not Showing

Run:

```bash
git status
git diff
```

If the file is ignored by Git, check:

```bash
git check-ignore -v path/to/file
```

---

## 3.4 Accidentally Staged a File

Remove it from staging without deleting it:

```bash
git restore --staged path/to/file
```

---

## 3.5 Discard Local Changes

For a specific file:

```bash
git restore path/to/file
```

> Warning: this discards uncommitted changes to that file.

Review the file carefully before using this command.

---

# 4. .NET Build Problems

## 4.1 `dotnet` Command Not Found

Run:

```bash
dotnet --version
```

If the command is unavailable:

1. Install the required .NET SDK.
2. Restart Git Bash.
3. Verify the PATH configuration.
4. Run `dotnet --version` again.

---

## 4.2 Restore Dependencies

If packages are missing:

```bash
dotnet restore
```

Then:

```bash
dotnet build
```

---

## 4.3 Clean and Rebuild

If stale build artifacts are causing problems:

```bash
dotnet clean
dotnet build
```

If necessary, remove generated build directories and restore dependencies again.

Do not delete source files or migration files.

---

# 5. C# Compilation Errors

## 5.1 Missing Type or Namespace

Example:

```text
CS0246: The type or namespace name could not be found
```

Check:

* Namespace.
* Project reference.
* Required `using` statement.
* File location.
* Package dependency.
* Type name spelling.

Search the repository:

```bash
grep -R "TypeName" -n . --include="*.cs"
```

---

## 5.2 Interface Implementation Error

If a class does not implement an interface correctly:

1. Open the interface.
2. Compare all method signatures.
3. Check return types.
4. Check parameters.
5. Check access modifiers.
6. Check namespaces.

Then rebuild:

```bash
dotnet build
```

---

## 5.3 Dependency Injection Error

Typical runtime error:

```text
Unable to resolve service for type ...
```

Check that the service has been registered.

Examples:

```csharp
builder.Services.AddScoped<IExampleService, ExampleService>();
```

or:

```csharp
builder.Services.AddTransient<IExampleService, ExampleService>();
```

Use the lifetime appropriate to the service.

---

# 6. ASP.NET Core Runtime Problems

## 6.1 Application Does Not Start

Run:

```bash
dotnet run
```

Read the complete terminal output.

Check:

* Configuration.
* Database connection.
* Dependency injection.
* Port conflicts.
* Missing environment variables.
* Invalid application settings.

---

## 6.2 Port Already in Use

Check which process is using the port.

On Windows:

```bash
netstat -ano | findstr :5071
```

Replace `5071` with the actual port.

Terminate the process only if you are certain it is safe to do so.

---

# 7. PostgreSQL Problems

## 7.1 PostgreSQL Is Not Running

Verify:

```bash
psql --version
```

Then attempt to connect:

```bash
psql -U postgres
```

If connection fails, verify that the PostgreSQL service is running.

---

## 7.2 Database Does Not Exist

List databases:

```sql
\l
```

Create the development database if necessary:

```sql
CREATE DATABASE agric_connect;
```

---

## 7.3 Authentication Failed

Check:

* Username.
* Password.
* Host.
* Port.
* PostgreSQL authentication configuration.
* Application connection string.

Do not expose database credentials in source control.

---

## 7.4 Connection Refused

Check:

* PostgreSQL service.
* Host.
* Port.
* Firewall.
* Connection string.

The default PostgreSQL port is commonly:

```text
5432
```

---

# 8. Entity Framework Core Problems

## 8.1 `dotnet ef` Not Found

Run:

```bash
dotnet ef --version
```

If unavailable:

```bash
dotnet tool install --global dotnet-ef
```

Then restart the terminal if required.

---

## 8.2 List Migrations

```bash
dotnet ef migrations list
```

Review which migrations exist and which have been applied.

---

## 8.3 Apply Migrations

```bash
dotnet ef database update
```

If the command fails, inspect the complete database error before changing migration files.

---

## 8.4 Migration Conflicts

If a migration fails:

1. Read the complete error.
2. Check the current database schema.
3. Check migration history.
4. Review the migration code.
5. Determine whether the problem is caused by existing data or schema.
6. Fix the underlying issue.
7. Re-run the migration.

Avoid deleting migrations as a first response.

---

# 9. Angular Problems

## 9.1 `ng` Command Not Found

Check:

```bash
ng version
```

If Angular CLI is installed locally, use:

```bash
npx ng version
```

You can also run the project's npm scripts:

```bash
npm start
```

---

## 9.2 Frontend Dependencies Missing

Run:

```bash
npm install
```

Then:

```bash
npm start
```

If dependency installation fails, inspect the exact npm error before changing package versions.

---

## 9.3 Angular Build Errors

Run:

```bash
npm run build
```

or the project's configured build command.

Check:

* TypeScript errors.
* Missing imports.
* Component declarations.
* Standalone component imports.
* Route configuration.
* Template errors.
* Dependency versions.

---

# 10. Frontend Cannot Connect to API

If the browser shows an API connection error:

Check:

1. Backend is running.
2. API URL is correct.
3. API port is correct.
4. HTTP/HTTPS protocol matches.
5. CORS configuration is correct.
6. Browser network request URL is correct.
7. Backend endpoint exists.

Use browser developer tools:

```text
Developer Tools
→ Network
→ Failed Request
→ Request URL
→ Status Code
```

---

# 11. HTTP vs HTTPS Errors

A common local development problem occurs when the frontend calls:

```text
https://localhost:5071
```

while the backend is actually running on:

```text
http://localhost:5071
```

This can result in connection errors such as:

```text
ERR_SSL_PROTOCOL_ERROR
```

Verify the backend URL displayed by:

```bash
dotnet run
```

Then update the frontend API configuration to use the correct protocol and port.

---

# 12. CORS Errors

A browser may report a CORS error when the frontend origin is not allowed by the backend.

Example frontend origin:

```text
http://localhost:4200
```

The backend should allow the required development origin.

Check the backend CORS configuration and make sure it matches the actual frontend URL.

Avoid unrestricted CORS configurations for protected applications.

---

# 13. API Returns 404 Not Found

If an endpoint returns:

```text
404 Not Found
```

check:

* HTTP method.
* Route.
* Controller route.
* API version.
* Endpoint path.
* Route parameters.
* Backend application version.

For example, verify whether the application expects:

```text
/api/v1/courses
```

or:

```text
/api/v2/courses
```

Also verify that the requested resource actually exists.

---

# 14. API Returns 400 Bad Request

A `400` response usually indicates invalid request data.

Check:

* JSON structure.
* Required fields.
* Data types.
* Validation rules.
* Request headers.
* Content-Type.

Example:

```http
Content-Type: application/json
```

Review the response body for validation details.

---

# 15. API Returns 401 Unauthorized

A `401` response usually means authentication is missing or invalid.

Check:

* Login status.
* Access token.
* Authorization header.
* Token expiration.
* Authentication configuration.

Typical header:

```http
Authorization: Bearer <token>
```

Never share real tokens in logs, screenshots, commits, or support requests.

---

# 16. API Returns 403 Forbidden

A `403` response usually means the user is authenticated but does not have permission to perform the requested operation.

Check:

* User role.
* Authorization policy.
* Required permission.
* Resource ownership.
* Backend authorization configuration.

---

# 17. API Returns 409 Conflict

A `409 Conflict` may indicate that an operation conflicts with the current application state.

Examples:

* Duplicate record.
* Duplicate enrollment.
* Duplicate product operation.
* Idempotency conflict.
* Resource state conflict.

Review the response body and application logs for the exact reason.

---

# 18. API Returns 500 Internal Server Error

A `500` response indicates a server-side failure.

Check backend logs immediately.

Look for:

* Exception type.
* Stack trace.
* Database errors.
* Null references.
* Dependency injection failures.
* Configuration errors.
* External service failures.

Do not expose detailed stack traces to production users.

---

# 19. Scalar/OpenAPI Problems

If the API documentation does not appear:

Check:

1. Backend is running.
2. Scalar package is installed.
3. OpenAPI is configured.
4. Correct endpoint URL is being used.
5. API versioning configuration is correct.
6. Browser can reach the backend.

Example:

```text
http://localhost:<port>/scalar/v1
```

Use the actual port and configured API version.

---

# 20. Authentication Problems

If login does not work:

Check:

* User exists.
* Password is correct.
* Authentication endpoint is correct.
* JWT configuration is valid.
* Token is returned.
* Token is stored correctly.
* Authorization header is sent.
* Token has not expired.

Never log passwords or authentication tokens.

---

# 21. Database Data Problems

If expected data is missing:

Check:

```bash
dotnet ef database update
```

Then inspect the database:

```bash
psql -U postgres -d agric_connect
```

List tables:

```sql
\dt
```

Check records:

```sql
SELECT COUNT(*) FROM table_name;
```

Replace `table_name` with the actual table.

---

# 22. Seed Data Problems

If development seed data is missing:

1. Verify the seeder exists.
2. Verify the application executes the seeder.
3. Check database connection.
4. Check migration status.
5. Review application logs.
6. Check the target database.

Do not manually insert large amounts of data unless required by the project's development procedure.

---

# 23. Environment Configuration Problems

Check the active environment.

For .NET:

```bash
echo $ASPNETCORE_ENVIRONMENT
```

On Windows PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT
```

Verify that the expected configuration file is being loaded.

---

# 24. Node and npm Problems

Check versions:

```bash
node --version
npm --version
```

Check installed dependencies:

```bash
npm list --depth=0
```

If dependencies are inconsistent, review:

```text
package.json
package-lock.json
```

Avoid deleting the lock file unless there is a specific reason and the team agrees.

---

# 25. Browser Debugging

When frontend behavior is incorrect, open browser developer tools.

Useful tabs:

```text
Console
Network
Application
Sources
```

### Console

Look for:

* JavaScript errors.
* Angular errors.
* Runtime exceptions.

### Network

Check:

* Request URL.
* HTTP method.
* Status code.
* Request payload.
* Response body.
* Request headers.

---

# 26. Logging and Diagnostics

When troubleshooting backend problems, inspect application logs.

Useful information includes:

* Timestamp.
* Request path.
* HTTP method.
* Status code.
* Exception type.
* Error message.
* Correlation/request ID where available.

Avoid logging sensitive information.

---

# 27. Clean Restart Procedure

When the application behaves unexpectedly:

### Backend

```bash
dotnet clean
dotnet restore
dotnet build
dotnet run
```

### Frontend

Stop the development server and restart:

```bash
npm start
```

### Database

Verify PostgreSQL is running and the correct database is being used.

Do not immediately delete databases, migrations, or source files.

---

# 28. Reporting a Bug

When reporting a problem, include:

* Problem description.
* Expected behavior.
* Actual behavior.
* Steps to reproduce.
* Environment.
* Relevant command.
* Error message.
* HTTP status code if applicable.
* Screenshot when useful.
* Relevant logs without secrets.

Example:

```text
Problem:
GET /api/v2/products returns 404.

Expected:
The product details should be returned.

Actual:
The API returns 404 Not Found.

Steps:
1. Start backend.
2. Open API documentation.
3. Send GET request.
4. Observe response.

Environment:
Development
```

---

# 29. Troubleshooting Checklist

Before asking for assistance:

* [ ] Read the complete error message.
* [ ] Checked recent code changes.
* [ ] Checked Git status.
* [ ] Checked configuration.
* [ ] Checked database connection.
* [ ] Checked backend logs.
* [ ] Checked browser console.
* [ ] Checked network requests.
* [ ] Checked API route.
* [ ] Checked authentication.
* [ ] Checked dependencies.
* [ ] Reproduced the issue.
* [ ] Confirmed the exact command that fails.

---

# 30. Important Safety Rules

Never solve a development problem by blindly:

* Deleting the database.
* Deleting migrations.
* Removing authentication.
* Disabling authorization.
* Disabling CORS security.
* Committing secrets.
* Replacing dependencies without checking compatibility.
* Copying production credentials into development.

Understand the cause before applying a destructive or security-related change.

---

# 31. Conclusion

Effective troubleshooting starts with the complete error message and a clear understanding of the environment.

Developers should diagnose problems systematically, verify configuration and dependencies, inspect logs, test individual components, and confirm the fix before continuing development.

For unresolved issues, document the exact error and reproduction steps so that other team members can investigate efficiently.
