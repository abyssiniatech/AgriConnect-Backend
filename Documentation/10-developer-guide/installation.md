# AgriConnect Ethiopia — Installation Guide

## 1. Introduction

This guide explains how to install the AgriConnect Ethiopia development environment and prepare the project for local development.

The installation process includes:

1. Installing required tools.
2. Cloning the repository.
3. Installing backend dependencies.
4. Installing frontend dependencies.
5. Configuring the database.
6. Configuring environment variables.
7. Running the backend.
8. Running the frontend.
9. Verifying the installation.

---

# 2. Required Software

Before installing AgriConnect, make sure the following software is available:

| Software            | Purpose                      |
| ------------------- | ---------------------------- |
| Git                 | Source-code management       |
| .NET SDK            | Backend development          |
| Node.js             | Frontend runtime and tooling |
| npm                 | Frontend package management  |
| Angular CLI         | Angular development          |
| PostgreSQL          | Database                     |
| Visual Studio Code  | Development environment      |
| Scalar/Postman/curl | API testing                  |

The exact versions should match the versions specified by the project's configuration files.

---

# 3. Verify Installed Tools

Open Git Bash and run:

```bash
git --version
dotnet --version
node --version
npm --version
psql --version
```

If Angular CLI is installed globally:

```bash
ng version
```

If any command is not recognized, install the corresponding software and restart the terminal.

---

# 4. Clone the Repository

Navigate to the directory where the project should be stored:

```bash
cd /c/Projects
```

Clone the repository:

```bash
git clone <repository-url>
```

Enter the project directory:

```bash
cd AgriConnect
```

> Replace `<repository-url>` with the actual repository URL.

---

# 5. Inspect the Repository

After cloning, inspect the project:

```bash
ls
```

Check the Git status:

```bash
git status
```

Review the available branches:

```bash
git branch -a
```

---

# 6. Backend Installation

Navigate to the backend project directory.

Example:

```bash
cd backend
```

If the repository uses a solution file, locate it with:

```bash
find . -name "*.sln" -o -name "*.slnx"
```

Restore .NET dependencies:

```bash
dotnet restore
```

Build the backend:

```bash
dotnet build
```

The build should complete without errors.

---

# 7. Frontend Installation

Navigate to the frontend directory.

Example:

```bash
cd frontend
```

Install npm dependencies:

```bash
npm install
```

Verify the frontend installation:

```bash
npm list --depth=0
```

If Angular CLI is installed globally:

```bash
ng version
```

Otherwise, use the project-local Angular CLI through npm scripts or:

```bash
npx ng version
```

---

# 8. PostgreSQL Installation

Install PostgreSQL using the official PostgreSQL distribution for your operating system.

After installation, verify:

```bash
psql --version
```

Make sure the PostgreSQL server is running.

Connect to PostgreSQL:

```bash
psql -U postgres
```

If authentication is successful, PostgreSQL is ready for database setup.

---

# 9. Create the Development Database

Create a database for local development.

Example:

```sql
CREATE DATABASE agric_connect;
```

Verify the database:

```sql
\l
```

Connect to it:

```sql
\c agric_connect
```

The actual database name should match the application's configured connection string.

---

# 10. Configure the Backend

Locate the backend configuration files.

Common examples include:

```text
appsettings.json
appsettings.Development.json
```

Configure the database connection string according to the project.

Example structure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=agric_connect;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

Do not commit real passwords or other secrets to Git.

---

# 11. Configure Environment Variables

Configure required environment variables according to the project documentation.

Possible variables include:

```text
DATABASE_CONNECTION_STRING
JWT_SECRET
API_BASE_URL
AI_API_KEY
```

Only variables actually required by the implementation should be configured.

Never commit secret values to the repository.

---

# 12. Database Migrations

If the project uses Entity Framework Core migrations, verify available migrations:

```bash
dotnet ef migrations list
```

Apply migrations:

```bash
dotnet ef database update
```

If `dotnet ef` is not available, install the Entity Framework CLI tool:

```bash
dotnet tool install --global dotnet-ef
```

Then verify:

```bash
dotnet ef --version
```

---

# 13. Seed Development Data

If the application contains a database seeder, run the application according to the project's documented startup process.

Verify that required development data exists after initialization.

Examples may include:

* Users.
* Farmers.
* Buyers.
* Experts.
* Logistics providers.
* Products.
* Categories.
* Orders.

The actual seed data depends on the project implementation.

---

# 14. Start the Backend

From the backend project directory:

```bash
dotnet run
```

The terminal should display the application URL.

Example:

```text
http://localhost:5000
```

or:

```text
https://localhost:7000
```

Use the actual URL displayed by the application.

---

# 15. Verify the API

Open the configured API documentation endpoint.

For example:

```text
http://localhost:<port>/scalar/v1
```

or the OpenAPI endpoint configured by the project.

Verify that:

* API documentation loads.
* Endpoints are visible.
* Health/status endpoints work if implemented.
* API requests can be executed.

---

# 16. Start the Frontend

Open a second Git Bash terminal.

Navigate to the frontend directory:

```bash
cd /path/to/frontend
```

Start the development server:

```bash
npm start
```

If the project uses Angular CLI directly:

```bash
ng serve
```

The frontend will normally be available at an address similar to:

```text
http://localhost:4200
```

Use the actual URL displayed by the terminal.

---

# 17. Verify Frontend and Backend Communication

Open the frontend application in a browser.

Verify:

1. Homepage loads.
2. Login page loads.
3. API requests are sent successfully.
4. Data is displayed.
5. Authentication works.
6. Protected pages behave correctly.
7. No unexpected browser console errors occur.

---

# 18. Common Installation Problems

## .NET SDK Not Found

Run:

```bash
dotnet --version
```

If unavailable, install the required .NET SDK.

---

## Node.js Not Found

Run:

```bash
node --version
```

If unavailable, install Node.js and restart Git Bash.

---

## npm Install Fails

Try:

```bash
npm cache verify
npm install
```

Review the error message before deleting dependency files.

---

## PostgreSQL Connection Failure

Check:

* PostgreSQL is running.
* Host is correct.
* Port is correct.
* Database exists.
* Username is correct.
* Password is correct.

The default PostgreSQL port is commonly:

```text
5432
```

---

## Database Migration Failure

Check:

```bash
dotnet ef migrations list
```

Then verify:

* Connection string.
* PostgreSQL availability.
* Migration files.
* Database permissions.

Do not delete migrations or production data without understanding the consequences.

---

## Frontend Cannot Reach API

Check:

* Backend is running.
* API URL is correct.
* Frontend environment configuration is correct.
* HTTP/HTTPS configuration matches.
* CORS configuration permits the frontend origin.

---

# 19. Installation Verification Checklist

After installation, verify:

* [ ] Git works.
* [ ] .NET SDK works.
* [ ] Node.js works.
* [ ] npm works.
* [ ] Angular CLI works if required.
* [ ] PostgreSQL works.
* [ ] Repository is cloned.
* [ ] Backend dependencies restored.
* [ ] Backend builds successfully.
* [ ] Frontend dependencies installed.
* [ ] Database created.
* [ ] Database migrations applied.
* [ ] Development data initialized if applicable.
* [ ] Backend starts successfully.
* [ ] API documentation loads.
* [ ] Frontend starts successfully.
* [ ] Frontend communicates with the backend.

---

# 20. Recommended Development Startup

For daily development, use two terminals.

### Terminal 1 — Backend

```bash
cd /path/to/backend
dotnet run
```

### Terminal 2 — Frontend

```bash
cd /path/to/frontend
npm start
```

Then open the frontend URL in the browser.

---

# 21. Conclusion

After completing this installation process, the developer should have a working local AgriConnect development environment.

If any installation step fails, consult:

```text
10-developer-guide/troubleshooting.md
```

and review the relevant project documentation before modifying the environment.
