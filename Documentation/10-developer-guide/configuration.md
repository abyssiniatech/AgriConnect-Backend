# AgriConnect Ethiopia — Configuration Guide

## 1. Introduction

This document explains how to configure the AgriConnect Ethiopia application for local development, testing, and deployment.

Configuration controls application behavior such as:

* Database connectivity.
* API settings.
* Frontend API URLs.
* Authentication.
* Logging.
* External services.
* AI services.
* Notifications.
* Environment-specific settings.

---

# 2. Configuration Principles

AgriConnect should use environment-specific configuration.

The main environments are:

```text
Development
Testing
Production
```

Development settings should never be used as production settings.

Sensitive values such as passwords, API keys, and authentication secrets must not be committed to source control.

---

# 3. Backend Configuration

The .NET backend commonly uses:

```text
appsettings.json
appsettings.Development.json
appsettings.Production.json
```

The exact files depend on the implementation.

A typical configuration structure is:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=agric_connect;Username=postgres;Password=YOUR_PASSWORD"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Do not commit real credentials.

---

# 4. Database Configuration

AgriConnect uses PostgreSQL for persistent application data.

A development connection string may contain:

```text
Host
Port
Database
Username
Password
```

Example:

```text
Host=localhost;Port=5432;Database=agric_connect;Username=postgres;Password=YOUR_PASSWORD
```

The actual values must match the local PostgreSQL installation.

---

# 5. Database Environment Variables

For environments where secrets should not be stored in configuration files, use environment variables.

Example:

```bash
export DATABASE_HOST=localhost
export DATABASE_PORT=5432
export DATABASE_NAME=agric_connect
export DATABASE_USER=postgres
export DATABASE_PASSWORD=YOUR_PASSWORD
```

The application should read these values according to its implemented configuration strategy.

Never commit the actual password.

---

# 6. Frontend Configuration

The Angular frontend may use environment configuration files.

Typical files include:

```text
src/environments/environment.ts
src/environments/environment.development.ts
src/environments/environment.production.ts
```

A development configuration may contain:

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000'
};
```

Use the actual backend URL configured for the project.

---

# 7. API Base URL

The frontend must know where the backend API is running.

Example:

```typescript
apiUrl: 'http://localhost:5000/api'
```

If the backend uses another port or HTTPS, update the URL accordingly.

For example:

```text
http://localhost:5071/api
```

or:

```text
https://localhost:7001/api
```

The frontend and backend protocol and port must match the actual running services.

---

# 8. Authentication Configuration

Authentication configuration may include:

* JWT settings.
* Token expiration.
* Issuer.
* Audience.
* Signing key.
* Cookie settings where applicable.

Example structure:

```json
{
  "Jwt": {
    "Issuer": "AgriConnect",
    "Audience": "AgriConnectClient",
    "ExpirationMinutes": 60
  }
}
```

Signing secrets must be stored securely and must not be committed to Git.

---

# 9. JWT Secret

A development JWT secret may be supplied through an environment variable or secure local configuration.

Example:

```bash
export JWT_SECRET="development-secret-change-me"
```

For production, use a strong randomly generated secret and a secure secret-management solution.

Never use example secrets in production.

---

# 10. CORS Configuration

Cross-Origin Resource Sharing allows the frontend to communicate with the backend when they run on different origins.

A development frontend may run at:

```text
http://localhost:4200
```

The backend should allow only the required development origin.

Example conceptual configuration:

```text
Allowed Origins:
http://localhost:4200
```

Avoid using unrestricted origins such as `*` when credentials or protected APIs are involved.

---

# 11. Logging Configuration

Logging helps developers diagnose application problems.

Development logging may use:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Production logging should avoid exposing:

* Passwords.
* Authentication tokens.
* Database credentials.
* Personal information.
* Other sensitive data.

---

# 12. AI Service Configuration

If AgriConnect integrates an external AI service, configuration may include:

```text
AI provider
API endpoint
API key
Model
Timeout
Request limits
```

Example environment variable:

```bash
export AI_API_KEY="YOUR_API_KEY"
```

Never commit an actual AI API key.

If AI functionality is optional, the application should continue to operate correctly when the service is unavailable.

---

# 13. Notification Configuration

Notification services may require configuration for:

* Email.
* SMS.
* Push notifications.
* In-app notifications.

Configuration should contain only the values required by the implemented notification provider.

Sensitive credentials must be stored securely.

---

# 14. File and Image Storage

If the application supports file or image uploads, configuration may include:

* Upload directory.
* Maximum file size.
* Allowed file types.
* Storage provider.
* Public URL.
* Access permissions.

Example:

```text
Maximum file size: project-defined limit
Allowed types: JPG, JPEG, PNG, WebP
```

Only approved file types should be accepted.

---

# 15. API Documentation Configuration

AgriConnect may expose OpenAPI documentation through Scalar or another API documentation interface.

During development, verify that the configured documentation endpoint is available.

Example:

```text
http://localhost:<port>/scalar/v1
```

The exact URL depends on the backend configuration.

API documentation should normally be restricted or disabled in production when appropriate.

---

# 16. Environment Selection

The .NET environment can be selected using:

```bash
export ASPNETCORE_ENVIRONMENT=Development
```

On Windows PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
```

The application should load the appropriate environment-specific configuration.

---

# 17. Frontend Development Environment

Start the Angular development server using the project's configured command.

Common examples:

```bash
npm start
```

or:

```bash
ng serve
```

The application will normally be available at:

```text
http://localhost:4200
```

Use the URL printed by the terminal.

---

# 18. Configuration Validation

After configuration, verify:

### Backend

```bash
dotnet build
dotnet run
```

### Frontend

```bash
npm install
npm start
```

### Database

```bash
psql -U postgres -d agric_connect
```

### API

Open the configured Scalar/OpenAPI endpoint.

---

# 19. Configuration Checklist

Before starting development:

* [ ] Database connection is configured.
* [ ] PostgreSQL is running.
* [ ] Backend environment is set.
* [ ] Frontend API URL is correct.
* [ ] Authentication configuration is available.
* [ ] JWT secret is configured securely.
* [ ] CORS allows the required frontend origin.
* [ ] Logging is configured.
* [ ] AI configuration is available if required.
* [ ] Notification configuration is available if required.
* [ ] File-storage configuration is available if required.
* [ ] API documentation is accessible.
* [ ] No secrets are committed to Git.

---

# 20. Security Rules

Never commit configuration containing:

```text
Passwords
API keys
JWT secrets
Database credentials
Private tokens
Cloud credentials
Third-party service secrets
```

Before committing changes, check:

```bash
git status
git diff
```

Developers should also inspect new configuration files before staging them.

---

# 21. Troubleshooting Configuration

If the backend cannot connect to PostgreSQL:

1. Check PostgreSQL is running.
2. Verify the host.
3. Verify the port.
4. Verify the database name.
5. Verify the username.
6. Verify the password.
7. Check the active environment.

If the frontend cannot communicate with the backend:

1. Verify the backend is running.
2. Verify the API URL.
3. Check CORS configuration.
4. Check HTTP/HTTPS configuration.
5. Inspect the browser developer console.
6. Inspect backend logs.

---

# 22. Recommended Local Configuration

For local development, use:

```text
Environment: Development
Database: Local PostgreSQL
Frontend: Local Angular server
Backend: Local .NET server
API Documentation: Development only
External secrets: Environment variables or secure local configuration
```

---

# 23. Conclusion

Correct configuration ensures that AgriConnect operates consistently across development, testing, and production environments.

Developers should keep configuration environment-specific, protect all secrets, verify database and API connectivity, and avoid committing sensitive configuration to the repository.
