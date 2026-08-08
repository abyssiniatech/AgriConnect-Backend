# AgriConnect Ethiopia — Developer Prerequisites

## 1. Introduction

This document describes the software, tools, and environment required to develop and run the AgriConnect Ethiopia platform locally.

Developers should install and configure the required tools before starting development.

---

# 2. System Requirements

Recommended development environment:

| Requirement      | Recommendation                                          |
| ---------------- | ------------------------------------------------------- |
| Operating System | Windows 10/11, Linux, or macOS                          |
| RAM              | 8 GB minimum, 16 GB recommended                         |
| Storage          | At least 10 GB free                                     |
| Internet         | Required for package installation and external services |
| Git              | Latest stable version                                   |
| Code Editor      | Visual Studio Code or equivalent                        |

---

# 3. Version Control

AgriConnect uses Git for source-code management.

Install Git and verify the installation:

```bash
git --version
```

Configure Git with your identity:

```bash
git config --global user.name "Your Name"
git config --global user.email "your-email@example.com"
```

The project repository should be cloned before development begins.

Example:

```bash
git clone <repository-url>
```

---

# 4. Code Editor

Visual Studio Code is recommended for development.

Useful extensions may include:

* C# / C# Dev Kit
* Angular Language Service
* ESLint
* Prettier
* GitLens
* PostgreSQL tools
* REST Client
* Markdown support

Only extensions required by the project should be installed.

---

# 5. Backend Requirements

The AgriConnect backend is implemented using the .NET platform.

Developers should install the appropriate .NET SDK version used by the project.

Verify the installation:

```bash
dotnet --version
```

Also verify available SDKs:

```bash
dotnet --list-sdks
```

The installed SDK should match the version specified by the project configuration.

---

# 6. Frontend Requirements

The frontend uses Angular and TypeScript.

Install Node.js and verify:

```bash
node --version
```

Verify npm:

```bash
npm --version
```

If Angular CLI is required globally:

```bash
ng version
```

The Angular and Node.js versions should match the versions specified by the project.

---

# 7. Database Requirements

AgriConnect uses PostgreSQL for persistent data storage.

Install PostgreSQL and verify the client:

```bash
psql --version
```

The PostgreSQL server should be running before database-dependent application features are tested.

---

# 8. Database Management Tools

Developers may use one of the following tools:

* PostgreSQL `psql`
* pgAdmin
* Visual Studio Code PostgreSQL extensions
* Other PostgreSQL-compatible database tools

The selected tool should support:

* Database creation.
* Table inspection.
* Query execution.
* Migration verification.
* Data inspection.

---

# 9. API Testing Tools

Developers should have access to at least one API testing tool.

Recommended options include:

* Scalar
* curl
* Postman

For command-line testing:

```bash
curl --version
```

The project's API documentation should be available through the configured OpenAPI/Scalar interface when the backend is running.

---

# 10. Package Management

Backend dependencies are managed through the .NET project files.

Typical command:

```bash
dotnet restore
```

Frontend dependencies are managed through npm.

Typical command:

```bash
npm install
```

Developers should avoid manually modifying generated dependency files unless necessary.

---

# 11. Environment Configuration

AgriConnect may require environment-specific configuration such as:

* Database connection strings.
* API URLs.
* Authentication configuration.
* External service keys.
* AI service configuration.
* Notification configuration.

Sensitive values must not be committed to source control.

Use environment-specific configuration files or environment variables according to the project's configuration strategy.

---

# 12. Repository Structure

The project may contain separate frontend, backend, and documentation directories.

A typical structure is:

```text
AgriConnect/
├── backend/
├── frontend/
├── AgriConnect-Documentation/
└── README.md
```

The actual repository structure should be verified against the current implementation.

---

# 13. Documentation Requirements

Developers should review the project documentation before making significant changes.

Important documentation includes:

```text
01-project-overview/
02-requirements/
03-system-architecture/
04-database/
05-api/
06-features/
07-security/
08-testing/
09-user-guides/
10-developer-guide/
11-deployment/
12-project-management/
14-final-report/
15-presentation/
```

---

# 14. Recommended Development Workflow

A typical development workflow is:

```text
Clone Repository
      ↓
Install Dependencies
      ↓
Configure Environment
      ↓
Setup Database
      ↓
Run Backend
      ↓
Run Frontend
      ↓
Test Application
      ↓
Develop Feature
      ↓
Run Tests
      ↓
Review Changes
      ↓
Commit Changes
```

---

# 15. Git Workflow

Before starting work:

```bash
git pull
```

Create a feature branch:

```bash
git checkout -b feature/feature-name
```

After making changes:

```bash
git status
```

Review changes:

```bash
git diff
```

Commit changes:

```bash
git add .
git commit -m "Implement feature"
```

Push the branch:

```bash
git push -u origin feature/feature-name
```

---

# 16. Security Requirements

Developers must:

* Never commit passwords.
* Never commit API keys.
* Never commit database credentials.
* Never expose authentication tokens.
* Validate user input.
* Follow authorization rules.
* Protect sensitive data.
* Review dependencies for known vulnerabilities.

Sensitive configuration should be managed outside source control.

---

# 17. Before Starting Development

Confirm that:

* [ ] Git is installed.
* [ ] .NET SDK is installed.
* [ ] Node.js is installed.
* [ ] npm is available.
* [ ] Angular CLI is available if required.
* [ ] PostgreSQL is installed.
* [ ] Database service is running.
* [ ] Repository is cloned.
* [ ] Backend dependencies can be restored.
* [ ] Frontend dependencies can be installed.
* [ ] Required environment variables are configured.
* [ ] API documentation can be accessed.
* [ ] Tests can be executed.

---

# 18. Conclusion

A correctly configured development environment helps developers build, test, and maintain AgriConnect consistently.

Before contributing to the project, developers should verify all required tools, dependencies, database services, configuration, and repository access.
