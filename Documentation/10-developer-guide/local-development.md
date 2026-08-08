# AgriConnect Ethiopia — Local Development Guide

## 1. Introduction

This guide explains how developers can run, test, and work on the AgriConnect Ethiopia platform in a local development environment.

The local environment typically consists of:

```text
Angular Frontend
       ↓
ASP.NET Core API
       ↓
PostgreSQL Database
```

---

# 2. Development Environment

Before starting development, verify that the required tools are installed:

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

---

# 3. Clone the Repository

Clone the project repository:

```bash
git clone <repository-url>
```

Enter the project directory:

```bash
cd AgriConnect
```

Check the repository status:

```bash
git status
```

---

# 4. Create a Development Branch

Do not normally develop directly on the main branch.

Create a feature branch:

```bash
git checkout -b feature/feature-name
```

Verify the current branch:

```bash
git branch
```

---

# 5. Backend Setup

Navigate to the backend project:

```bash
cd backend
```

Restore dependencies:

```bash
dotnet restore
```

Build the application:

```bash
dotnet build
```

Resolve all build errors before continuing.

---

# 6. Database Setup

Make sure PostgreSQL is running.

Verify the database:

```bash
psql -U postgres -l
```

If the development database does not exist, create it according to:

```text
10-developer-guide/database-setup.md
```

Apply migrations:

```bash
dotnet ef database update
```

---

# 7. Configure the Backend

Verify the development configuration.

Typical configuration files include:

```text
appsettings.json
appsettings.Development.json
```

Check:

* Database connection.
* Authentication settings.
* CORS configuration.
* Logging.
* External services.
* API configuration.

Never commit passwords or API keys.

---

# 8. Start the Backend

From the backend project directory:

```bash
dotnet run
```

The terminal will display the listening URL.

For example:

```text
http://localhost:5071
```

Use the actual URL displayed by the application.

---

# 9. Verify the Backend

Open the configured API documentation endpoint.

For example:

```text
http://localhost:<port>/scalar/v1
```

Verify that:

* API documentation loads.
* Expected endpoints are visible.
* Requests can be executed.
* The backend connects to PostgreSQL.

---

# 10. Start the Frontend

Open a second Git Bash terminal.

Navigate to the frontend:

```bash
cd frontend
```

Install dependencies if necessary:

```bash
npm install
```

Start the development server:

```bash
npm start
```

If the project uses Angular CLI directly:

```bash
ng serve
```

The frontend is normally available at:

```text
http://localhost:4200
```

Use the URL printed by the terminal.

---

# 11. Run Frontend and Backend Together

For normal development, use two terminals.

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

Keep both processes running while developing features that require API communication.

---

# 12. Verify Frontend-to-API Communication

Open the frontend in a browser.

Check:

1. Application loads.
2. API requests succeed.
3. Data is displayed.
4. Authentication works.
5. Protected routes work.
6. Forms submit successfully.
7. Error messages appear correctly.
8. No unexpected browser console errors occur.

Use the browser developer tools to inspect network requests.

---

# 13. Development Workflow

A typical workflow is:

```text
Create Branch
     ↓
Pull Latest Changes
     ↓
Run Application
     ↓
Implement Feature
     ↓
Run Tests
     ↓
Review Changes
     ↓
Fix Problems
     ↓
Commit
     ↓
Push Branch
```

Before starting work:

```bash
git pull
```

Check your changes:

```bash
git status
```

Review the diff:

```bash
git diff
```

---

# 14. Backend Development

When changing backend code:

1. Update the appropriate layer.
2. Keep business logic separated from controllers.
3. Follow existing project architecture.
4. Update DTOs when required.
5. Update validation rules when required.
6. Update database models carefully.
7. Create migrations for schema changes.
8. Run backend tests.
9. Build the project.

Example:

```bash
dotnet build
```

---

# 15. Database Changes

When changing an Entity Framework Core entity:

1. Update the entity.
2. Update configuration if necessary.
3. Create a migration.
4. Review the migration.
5. Apply the migration locally.
6. Test the affected functionality.

Example:

```bash
dotnet ef migrations add AddNewFeature
```

Apply it:

```bash
dotnet ef database update
```

Do not create migrations blindly. Review generated migration operations before applying them.

---

# 16. Frontend Development

When changing Angular code:

1. Update the appropriate component or service.
2. Follow the existing component architecture.
3. Update models and interfaces when necessary.
4. Update routes if required.
5. Update forms and validation.
6. Test API integration.
7. Run frontend tests.
8. Check browser console errors.

Run the development server:

```bash
npm start
```

---

# 17. API Development

When adding or changing an API endpoint:

1. Define the required request model.
2. Define the response model.
3. Implement validation.
4. Implement business logic.
5. Add authorization where required.
6. Add the endpoint.
7. Update API documentation.
8. Test successful requests.
9. Test validation failures.
10. Test authorization failures.
11. Test error handling.

---

# 18. API Testing

API requests can be tested with Scalar, curl, Postman, or another API testing tool.

Example:

```bash
curl -i http://localhost:<port>/api/health
```

Use the actual endpoint provided by the application.

Test:

* Successful requests.
* Invalid requests.
* Unauthorized requests.
* Forbidden requests.
* Missing resources.
* Duplicate operations.
* Server errors.

---

# 19. Frontend Testing

Run frontend tests using the project's configured command.

Common example:

```bash
npm test
```

For Angular projects, the exact test command depends on the configured test runner.

Tests should cover:

* Components.
* Services.
* Forms.
* Validation.
* Routing.
* API interactions.
* Important user workflows.

---

# 20. Backend Testing

Run backend tests from the solution or test project directory:

```bash
dotnet test
```

Before committing a feature, verify that the relevant test suite passes.

---

# 21. Code Formatting and Quality

Before committing changes:

```bash
git status
git diff
```

Check for:

* Unused imports.
* Debugging statements.
* Temporary files.
* Hard-coded credentials.
* Unnecessary comments.
* Broken formatting.
* Unhandled errors.

Use the formatting and linting tools configured by the project.

---

# 22. Git Commit Workflow

Stage the intended changes:

```bash
git add .
```

Review staged changes:

```bash
git diff --cached
```

Commit:

```bash
git commit -m "Implement feature"
```

Push the branch:

```bash
git push -u origin feature/feature-name
```

Use clear and meaningful commit messages.

---

# 23. Pulling Updates

Before starting new work:

```bash
git checkout main
git pull
```

Return to your feature branch:

```bash
git checkout feature/feature-name
```

Update the branch according to the team's preferred Git workflow.

---

# 24. Handling Merge Conflicts

If Git reports a conflict:

```bash
git status
```

Open the affected files and resolve the conflict markers:

```text
<<<<<<<
Your changes
=======
Other changes
>>>>>>>
```

After resolving:

```bash
git add .
```

Continue the appropriate Git operation.

Verify the final result before committing.

---

# 25. Environment Variables

Local development may require environment variables for:

* Database credentials.
* JWT secrets.
* API keys.
* External services.
* Notification services.

Do not store sensitive values directly in source code.

Use the project's approved local configuration mechanism.

---

# 26. Local Development Security

Developers should:

* Use development credentials.
* Never use production credentials locally unless explicitly authorized.
* Never commit secrets.
* Protect local database backups.
* Avoid exposing development services publicly.
* Keep dependencies updated.
* Use HTTPS where required.
* Test authorization rules.

---

# 27. Useful Development Commands

### Git

```bash
git status
git branch
git pull
git diff
git add .
git commit -m "message"
git push
```

### .NET

```bash
dotnet restore
dotnet build
dotnet run
dotnet test
```

### Entity Framework Core

```bash
dotnet ef migrations list
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Angular / npm

```bash
npm install
npm start
npm test
ng serve
```

### PostgreSQL

```bash
psql --version
psql -U postgres
```

---

# 28. Daily Development Checklist

Before starting:

* [ ] PostgreSQL is running.
* [ ] Backend dependencies are available.
* [ ] Frontend dependencies are installed.
* [ ] Development configuration is correct.
* [ ] Git branch is correct.
* [ ] Latest changes have been pulled.

During development:

* [ ] Follow the project architecture.
* [ ] Validate inputs.
* [ ] Handle errors.
* [ ] Protect authorization boundaries.
* [ ] Write or update tests.
* [ ] Avoid committing secrets.

Before committing:

* [ ] Backend builds.
* [ ] Frontend builds.
* [ ] Tests pass.
* [ ] Changes have been reviewed.
* [ ] No secrets are included.
* [ ] No temporary files are included.
* [ ] Git diff is correct.

---

# 29. Troubleshooting

If the application does not start, check:

```text
10-developer-guide/troubleshooting.md
```

Also inspect:

* Terminal errors.
* Browser console.
* Network requests.
* Backend logs.
* Database connection.
* Environment variables.

Always use the complete error message when diagnosing a problem.

---

# 30. Conclusion

A consistent local development workflow helps developers build AgriConnect safely and efficiently.

Developers should keep frontend, backend, and database environments synchronized, test changes before committing them, protect sensitive configuration, and follow the project's Git and architectural conventions.
