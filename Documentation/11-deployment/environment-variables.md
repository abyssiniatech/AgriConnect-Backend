# AgriConnect Ethiopia — Environment Variables

## 1. Introduction

Environment variables provide configuration values that can change between development, testing, staging, and production environments.

They are especially useful for sensitive configuration such as:

* Database credentials.
* Authentication secrets.
* API keys.
* External service credentials.
* Application URLs.

Sensitive values should never be committed to the Git repository.

---

# 2. Configuration Principles

AgriConnect should follow these principles:

1. Keep environment-specific configuration outside source code where practical.
2. Never commit production secrets.
3. Use different credentials for different environments.
4. Rotate credentials when they are exposed.
5. Grant external services only the permissions they require.
6. Validate required configuration during application startup.

---

# 3. Environment Names

The application may use the following environments:

```text
Development
Testing
Staging
Production
```

Each environment should have its own configuration.

Example:

```text
Development → Local PostgreSQL
Testing     → Test database
Staging     → Staging infrastructure
Production  → Production infrastructure
```

---

# 4. ASP.NET Core Environment

The ASP.NET Core environment can be selected using:

### Windows PowerShell

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
```

### Windows Command Prompt

```cmd
set ASPNETCORE_ENVIRONMENT=Development
```

### Linux/macOS

```bash
export ASPNETCORE_ENVIRONMENT=Development
```

Verify the active environment using the appropriate operating-system command.

---

# 5. Database Configuration

The application requires database connectivity.

A recommended variable is:

```text
DATABASE_CONNECTION_STRING
```

Example development value:

```text
Host=localhost;Port=5432;Database=agric_connect;Username=postgres;Password=YOUR_PASSWORD
```

Do not use the example password in a real environment.

---

# 6. Database Components

If the application builds its connection string from individual variables, the following may be used:

```text
DATABASE_HOST
DATABASE_PORT
DATABASE_NAME
DATABASE_USER
DATABASE_PASSWORD
```

Example:

```text
DATABASE_HOST=localhost
DATABASE_PORT=5432
DATABASE_NAME=agric_connect
DATABASE_USER=postgres
DATABASE_PASSWORD=YOUR_PASSWORD
```

Use either the project's complete connection string or individual variables according to the implemented configuration strategy.

---

# 7. Authentication Configuration

Authentication may require:

```text
JWT_SECRET
JWT_ISSUER
JWT_AUDIENCE
JWT_EXPIRATION_MINUTES
```

Example:

```text
JWT_ISSUER=AgriConnect
JWT_AUDIENCE=AgriConnectClient
JWT_EXPIRATION_MINUTES=60
```

The JWT signing secret must be a strong, unpredictable value.

Example placeholder:

```text
JWT_SECRET=REPLACE_WITH_SECURE_SECRET
```

Never use this placeholder as a production secret.

---

# 8. Frontend API URL

The frontend needs to know the backend API address.

Example:

```text
API_BASE_URL=https://api.example.com
```

For local development:

```text
API_BASE_URL=http://localhost:5071
```

Use the actual API URL configured by the project.

---

# 9. CORS Configuration

The backend should know which frontend origins are allowed.

Example:

```text
CORS_ALLOWED_ORIGINS=https://app.example.com
```

For local development:

```text
CORS_ALLOWED_ORIGINS=http://localhost:4200
```

If multiple origins are supported, use the format expected by the application's configuration implementation.

Do not allow unrestricted origins for production authenticated applications.

---

# 10. AI Service Configuration

If AgriConnect uses an external AI service, the application may require:

```text
AI_API_KEY
AI_API_URL
AI_MODEL
```

Example:

```text
AI_API_KEY=REPLACE_WITH_SECURE_KEY
AI_API_URL=https://api.example.com
AI_MODEL=REPLACE_WITH_MODEL
```

Only configure these variables if AI functionality is enabled by the application.

---

# 11. Notification Configuration

If notification services are implemented, environment variables may include:

```text
NOTIFICATION_API_KEY
NOTIFICATION_API_URL
```

For email services, additional variables may be required, such as:

```text
EMAIL_HOST
EMAIL_PORT
EMAIL_USERNAME
EMAIL_PASSWORD
EMAIL_FROM
```

Only variables required by the selected provider should be configured.

---

# 12. File Storage Configuration

If the application supports file or image storage, configuration may include:

```text
STORAGE_PROVIDER
STORAGE_ENDPOINT
STORAGE_BUCKET
STORAGE_ACCESS_KEY
STORAGE_SECRET_KEY
```

The exact names depend on the storage implementation.

Storage credentials must be treated as secrets.

---

# 13. Logging Configuration

Logging configuration may include:

```text
LOG_LEVEL
```

Example:

```text
LOG_LEVEL=Information
```

Production logging should not expose:

* Passwords.
* JWT tokens.
* API keys.
* Database credentials.
* Sensitive personal information.

---

# 14. Application URL

The backend may require an application URL:

```text
APP_BASE_URL=https://api.example.com
```

The frontend may use:

```text
FRONTEND_URL=https://app.example.com
```

These values should match the actual deployed domains.

---

# 15. Development Example

A local development environment may conceptually contain:

```text
ASPNETCORE_ENVIRONMENT=Development

DATABASE_CONNECTION_STRING=Host=localhost;Port=5432;Database=agric_connect;Username=postgres;Password=YOUR_PASSWORD

API_BASE_URL=http://localhost:5071

CORS_ALLOWED_ORIGINS=http://localhost:4200

JWT_ISSUER=AgriConnect
JWT_AUDIENCE=AgriConnectClient
JWT_EXPIRATION_MINUTES=60
```

Do not copy these values directly into production.

---

# 16. Production Example

A production environment may contain:

```text
ASPNETCORE_ENVIRONMENT=Production

DATABASE_CONNECTION_STRING=REPLACE_WITH_SECURE_DATABASE_CONNECTION

API_BASE_URL=https://api.example.com

CORS_ALLOWED_ORIGINS=https://app.example.com

JWT_SECRET=REPLACE_WITH_SECURE_SECRET
JWT_ISSUER=AgriConnect
JWT_AUDIENCE=AgriConnectClient
JWT_EXPIRATION_MINUTES=60
```

The example values are placeholders and must be replaced with secure production configuration.

---

# 17. Secret Management

Production secrets should preferably be stored using a dedicated secret-management solution.

Examples include:

* Cloud secret managers.
* Container secret stores.
* CI/CD secret variables.
* Server-managed environment variables.
* Enterprise password/secret management systems.

Avoid storing secrets in:

```text
Git repositories
Public configuration files
Frontend source code
Screenshots
Issue trackers
Chat messages
Documentation
```

---

# 18. Frontend Secret Protection

Frontend environment variables are not automatically secret.

Any value included in an Angular production bundle may be visible to users.

Therefore:

**Never put private API keys, database passwords, JWT signing secrets, or other server-side secrets in frontend configuration.**

The frontend should only contain values that are safe to expose publicly.

---

# 19. Local Environment Files

If the project uses local environment files, they should be excluded from Git when they contain secrets.

Example:

```text
.env
.env.local
.env.development.local
```

Check `.gitignore`:

```bash
cat .gitignore
```

Make sure secret-bearing local configuration files are ignored where appropriate.

---

# 20. Checking for Accidentally Tracked Secrets

Before committing:

```bash
git status
```

Review changes:

```bash
git diff
```

Also inspect staged files:

```bash
git diff --cached
```

Look for:

```text
Passwords
API keys
Tokens
Private keys
Database credentials
Cloud credentials
```

---

# 21. Secret Exposure Response

If a secret is accidentally committed:

1. Stop using the exposed secret.
2. Rotate or revoke it immediately.
3. Create a replacement secret.
4. Update the affected environment.
5. Check repository history if necessary.
6. Review logs for unauthorized use.
7. Document the incident.

Simply deleting the file in a later commit does not necessarily remove the secret from Git history.

---

# 22. CI/CD Variables

When automated deployment is used, sensitive configuration should be stored as protected CI/CD variables or secrets.

Typical categories include:

```text
Database credentials
JWT secrets
API keys
Cloud credentials
Deployment credentials
```

The CI/CD system should inject these values during deployment rather than storing them in source code.

---

# 23. Configuration Validation

The application should fail clearly when required configuration is missing.

For example, if the database connection string is required, startup should report a meaningful configuration error rather than failing later with an unclear database exception.

Required configuration should be validated before production traffic is accepted.

---

# 24. Environment Variable Checklist

### Development

* [ ] Development environment selected.
* [ ] Database connection configured.
* [ ] API URL configured.
* [ ] CORS origin configured.
* [ ] Authentication configuration available.
* [ ] Optional external services configured.

### Production

* [ ] Production environment selected.
* [ ] Production database configured.
* [ ] Strong JWT secret configured.
* [ ] Production API URL configured.
* [ ] Production CORS configured.
* [ ] External service credentials configured securely.
* [ ] Secrets stored outside source control.
* [ ] Configuration validated.
* [ ] No development secrets are being used.

---

# 25. Security Rules

The following rules are mandatory:

* Never commit secrets.
* Never place database credentials in frontend code.
* Never use production credentials for ordinary local development.
* Never expose JWT signing keys.
* Never share API keys publicly.
* Rotate compromised credentials.
* Use least-privilege credentials.
* Review configuration before deployment.

---

# 26. Example Variable Reference

| Variable                     | Purpose                  | Secret?   |
| ---------------------------- | ------------------------ | --------- |
| `ASPNETCORE_ENVIRONMENT`     | Application environment  | No        |
| `DATABASE_CONNECTION_STRING` | Database connection      | Yes       |
| `DATABASE_HOST`              | Database host            | No        |
| `DATABASE_PORT`              | Database port            | No        |
| `DATABASE_NAME`              | Database name            | No        |
| `DATABASE_USER`              | Database username        | Sensitive |
| `DATABASE_PASSWORD`          | Database password        | Yes       |
| `JWT_SECRET`                 | Token signing secret     | Yes       |
| `JWT_ISSUER`                 | JWT issuer               | No        |
| `JWT_AUDIENCE`               | JWT audience             | No        |
| `JWT_EXPIRATION_MINUTES`     | Token lifetime           | No        |
| `API_BASE_URL`               | API address              | No        |
| `CORS_ALLOWED_ORIGINS`       | Allowed frontend origins | No        |
| `AI_API_KEY`                 | AI provider credential   | Yes       |
| `EMAIL_PASSWORD`             | Email service credential | Yes       |
| `STORAGE_SECRET_KEY`         | Storage credential       | Yes       |

The actual variables used by the application must be confirmed against its implementation.

---

# 27. Conclusion

Environment variables provide a secure and flexible way to configure AgriConnect across different environments.

Developers should separate configuration from source code, protect all secrets, keep frontend configuration free of private credentials, validate required settings, and use secure secret-management practices for production.
