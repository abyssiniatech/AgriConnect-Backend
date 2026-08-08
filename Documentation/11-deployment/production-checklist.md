# AgriConnect Ethiopia — Production Deployment Checklist

## 1. Purpose

This checklist is used before, during, and after deploying AgriConnect Ethiopia to a production environment.

The objective is to ensure that the application is:

* Functional.
* Secure.
* Configured correctly.
* Tested.
* Recoverable.
* Ready for real users.

---

# 2. Release Information

Complete the following before deployment:

| Item             | Value              |
| ---------------- | ------------------ |
| Release Version  | `________________` |
| Release Date     | `________________` |
| Deployment Owner | `________________` |
| Environment      | Production         |
| Backend Version  | `________________` |
| Frontend Version | `________________` |
| Database Version | `________________` |

---

# 3. Source Code Verification

* [ ] Correct production branch selected.
* [ ] Latest approved changes pulled.
* [ ] Working tree is clean.
* [ ] Code review completed.
* [ ] No unfinished development code remains.
* [ ] No debugging statements remain.
* [ ] No temporary files remain.
* [ ] No test credentials remain.
* [ ] No hard-coded secrets remain.

Check:

```bash
git status
```

Review recent commits:

```bash
git log --oneline -10
```

---

# 4. Backend Verification

* [ ] Backend dependencies restored.
* [ ] Backend builds successfully.
* [ ] Backend tests pass.
* [ ] Release configuration is enabled.
* [ ] Production configuration is loaded.
* [ ] Database connection is configured.
* [ ] Authentication is configured.
* [ ] Authorization is configured.
* [ ] CORS is configured.
* [ ] Logging is configured.
* [ ] Health checks are available where implemented.

Build:

```bash
dotnet build --configuration Release
```

Test:

```bash
dotnet test --configuration Release
```

---

# 5. Backend Publish Verification

Publish the backend:

```bash
dotnet publish --configuration Release --output ./publish
```

Verify that:

* [ ] Publish completed successfully.
* [ ] Required assemblies exist.
* [ ] Required configuration is available.
* [ ] Static resources are present where required.
* [ ] No development-only files are unnecessarily deployed.

---

# 6. Frontend Verification

* [ ] Frontend dependencies install successfully.
* [ ] Production build succeeds.
* [ ] API URL points to production.
* [ ] Development API URLs are removed.
* [ ] Development debugging is disabled.
* [ ] Authentication flow works.
* [ ] Routing works.
* [ ] Static assets load.
* [ ] Browser console has no critical errors.

Install:

```bash
npm ci
```

Build:

```bash
npm run build
```

Use the project's actual production build command if different.

---

# 7. Database Verification

* [ ] Production database exists.
* [ ] Database credentials are correct.
* [ ] Database is accessible from the backend.
* [ ] Required migrations have been reviewed.
* [ ] Pending migrations are identified.
* [ ] Database backup completed.
* [ ] Database indexes exist.
* [ ] Required constraints exist.
* [ ] Existing production data is protected.

Check migrations:

```bash
dotnet ef migrations list
```

---

# 8. Database Backup

Before applying database changes:

* [ ] Current database backup completed.
* [ ] Backup file verified.
* [ ] Backup stored securely.
* [ ] Backup restoration procedure is available.
* [ ] Recovery point is documented.

Example:

```bash
pg_dump -U postgres -d agric_connect -Fc -f agric_connect_pre_deployment.dump
```

Do not run destructive database commands against production without authorization.

---

# 9. Database Migration

Before migration:

* [ ] Migration reviewed.
* [ ] Migration is compatible with the deployed application.
* [ ] Backup verified.
* [ ] Maintenance requirements understood.

Apply the approved migration:

```bash
dotnet ef database update
```

After migration:

* [ ] Migration completed successfully.
* [ ] Tables verified.
* [ ] Indexes verified.
* [ ] Constraints verified.
* [ ] Application database connection verified.

---

# 10. Environment Configuration

Verify all required production environment variables.

Typical configuration includes:

```text
ASPNETCORE_ENVIRONMENT
DATABASE_CONNECTION_STRING
JWT_SECRET
JWT_ISSUER
JWT_AUDIENCE
API_BASE_URL
CORS_ALLOWED_ORIGINS
```

Optional services may require:

```text
AI_API_KEY
EMAIL_HOST
EMAIL_USERNAME
EMAIL_PASSWORD
STORAGE_ACCESS_KEY
STORAGE_SECRET_KEY
```

Only configure variables required by the actual application.

---

# 11. Secret Management

* [ ] Database password is stored securely.
* [ ] JWT secret is production-specific.
* [ ] External API keys are protected.
* [ ] Cloud credentials are protected.
* [ ] Secrets are not stored in Git.
* [ ] Secrets are not included in frontend bundles.
* [ ] Development credentials are not used.
* [ ] Compromised credentials have been rotated.

---

# 12. HTTPS Verification

* [ ] Production domain uses HTTPS.
* [ ] TLS certificate is valid.
* [ ] Certificate has not expired.
* [ ] HTTP redirects to HTTPS where appropriate.
* [ ] API uses HTTPS.
* [ ] Frontend uses HTTPS.
* [ ] Mixed-content errors do not occur.

Example:

```text
https://app.example.com
https://api.example.com
```

Use the actual production domains.

---

# 13. CORS Verification

* [ ] Only approved frontend origins are allowed.
* [ ] Production frontend origin is configured.
* [ ] Development origins are removed where unnecessary.
* [ ] Wildcard origins are not unnecessarily enabled.
* [ ] Authenticated requests work correctly.

Example:

```text
https://app.example.com
```

---

# 14. Authentication Verification

Test:

* [ ] User registration if implemented.
* [ ] User login.
* [ ] Invalid login.
* [ ] Token generation.
* [ ] Token expiration.
* [ ] Logout behavior.
* [ ] Protected endpoints.
* [ ] Unauthorized requests.
* [ ] Invalid tokens.

Never use real user passwords during testing unless explicitly authorized.

---

# 15. Authorization Verification

Verify each important role and permission.

Test:

* [ ] Authorized user can perform permitted action.
* [ ] Unauthorized user receives appropriate response.
* [ ] Users cannot access restricted resources.
* [ ] Role restrictions work.
* [ ] Resource ownership rules work where applicable.
* [ ] Administrative functions are protected.

---

# 16. API Verification

Test important API endpoints.

Verify:

* [ ] GET requests.
* [ ] POST requests.
* [ ] PUT/PATCH requests.
* [ ] DELETE requests where applicable.
* [ ] Validation.
* [ ] Authentication.
* [ ] Authorization.
* [ ] Error handling.
* [ ] Pagination.
* [ ] Filtering.
* [ ] API versioning where implemented.

Check expected status codes:

```text
200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
409 Conflict
500 Internal Server Error
```

---

# 17. Frontend Smoke Test

Open the production frontend and verify:

* [ ] Homepage loads.
* [ ] Navigation works.
* [ ] Login works.
* [ ] Dashboard loads.
* [ ] Data loads from API.
* [ ] Forms work.
* [ ] Validation works.
* [ ] Error messages appear correctly.
* [ ] Logout works.
* [ ] Protected routes are protected.

---

# 18. Core AgriConnect Workflows

Test the major platform workflows.

Depending on implemented features:

* [ ] Farmer registration.
* [ ] Farmer profile.
* [ ] Product creation.
* [ ] Product browsing.
* [ ] Buyer interaction.
* [ ] Order creation.
* [ ] Expert interaction.
* [ ] Logistics workflow.
* [ ] Notifications.
* [ ] AI-assisted functionality.
* [ ] File/image uploads.

Only mark a workflow complete if the corresponding feature is implemented.

---

# 19. Performance Verification

Check:

* [ ] API response times are acceptable.
* [ ] Database queries perform adequately.
* [ ] Frontend loads efficiently.
* [ ] Large requests are controlled.
* [ ] Pagination is enabled where necessary.
* [ ] No obvious memory leaks exist.
* [ ] Static assets are optimized.

Performance targets should be based on actual project requirements.

---

# 20. Security Verification

* [ ] HTTPS enabled.
* [ ] Authentication enabled.
* [ ] Authorization enabled.
* [ ] CORS restricted.
* [ ] Production secrets protected.
* [ ] Database access restricted.
* [ ] Sensitive logs removed.
* [ ] Error responses do not expose internal details.
* [ ] Debug mode disabled.
* [ ] Development tools are not unnecessarily exposed.
* [ ] Dependencies checked for known vulnerabilities.

---

# 21. Logging Verification

Verify that production logs contain useful operational information.

Check:

* [ ] Application startup logs.
* [ ] API errors.
* [ ] Database errors.
* [ ] Authentication failures.
* [ ] Important application events.

Ensure logs do not contain:

* Passwords.
* JWT tokens.
* API keys.
* Database passwords.
* Sensitive personal information.

---

# 22. Monitoring Verification

* [ ] Application monitoring enabled.
* [ ] Database monitoring enabled.
* [ ] Server monitoring enabled.
* [ ] Error monitoring configured.
* [ ] Health checks monitored where available.
* [ ] Alerts configured for critical failures.

---

# 23. Backup Verification

* [ ] Automated backups enabled.
* [ ] Latest backup completed successfully.
* [ ] Backup storage is secure.
* [ ] Backup retention configured.
* [ ] Restore procedure documented.
* [ ] Recovery test has been performed.

See:

```text
11-deployment/backup-and-recovery.md
```

for detailed procedures.

---

# 24. Rollback Preparation

Before deployment:

* [ ] Previous application version is available.
* [ ] Previous frontend build is available.
* [ ] Database backup is available.
* [ ] Configuration backup is available where appropriate.
* [ ] Rollback procedure is understood.
* [ ] Responsible team members know how to execute rollback.

---

# 25. Deployment

Perform deployment according to the approved deployment procedure.

Typical sequence:

```text
Backup
   ↓
Deploy Backend
   ↓
Apply Database Changes
   ↓
Deploy Frontend
   ↓
Restart Services
   ↓
Health Check
   ↓
Smoke Test
   ↓
Monitor
```

The exact order may vary depending on whether database changes are backward-compatible.

---

# 26. Post-Deployment Verification

Immediately after deployment:

* [ ] Backend is running.
* [ ] Frontend is accessible.
* [ ] Database is accessible.
* [ ] Health check succeeds.
* [ ] Authentication works.
* [ ] API requests succeed.
* [ ] Core workflows work.
* [ ] No critical errors appear in logs.
* [ ] Monitoring is receiving data.

---

# 27. Post-Deployment Monitoring

Monitor the application after release for:

* Increased HTTP 5xx errors.
* Authentication failures.
* Database errors.
* Slow API responses.
* Frontend failures.
* External service failures.
* Unexpected resource usage.

Compare production behavior with normal baseline behavior where available.

---

# 28. Deployment Failure Criteria

Consider rollback or immediate investigation if:

* Application cannot start.
* Database migration fails.
* Authentication is broken.
* Core API endpoints fail.
* Frontend cannot communicate with backend.
* Critical data is unavailable.
* Security configuration is incorrect.
* Severe performance degradation occurs.

---

# 29. Rollback Checklist

If rollback is required:

* [ ] Confirm rollback decision.
* [ ] Record the incident.
* [ ] Stop further deployment actions.
* [ ] Restore previous application version.
* [ ] Restore frontend if required.
* [ ] Assess database changes.
* [ ] Restore database only when necessary and authorized.
* [ ] Restart services.
* [ ] Run smoke tests.
* [ ] Verify authentication.
* [ ] Verify critical workflows.
* [ ] Monitor the application.

---

# 30. Final Approval

Before declaring the deployment complete:

| Area           | Status     |
| -------------- | ---------- |
| Source Code    | ☐ Approved |
| Backend        | ☐ Verified |
| Frontend       | ☐ Verified |
| Database       | ☐ Verified |
| Authentication | ☐ Verified |
| Authorization  | ☐ Verified |
| Security       | ☐ Verified |
| Backups        | ☐ Verified |
| Monitoring     | ☐ Verified |
| Smoke Tests    | ☐ Passed   |
| Rollback Plan  | ☐ Ready    |

---

# 31. Deployment Sign-Off

```text
Deployment Version: __________________________

Deployment Date: _____________________________

Deployed By: _________________________________

Reviewed By: __________________________________

Deployment Result: ____________________________

Issues Found: _________________________________

Rollback Required:  Yes / No

Notes:
________________________________________________
________________________________________________
________________________________________________
```

---

# 32. Final Production Checklist

Before announcing the release:

* [ ] Production build verified.
* [ ] Production configuration verified.
* [ ] Secrets protected.
* [ ] Database backup completed.
* [ ] Database migration verified.
* [ ] Backend deployed.
* [ ] Frontend deployed.
* [ ] HTTPS verified.
* [ ] CORS verified.
* [ ] Authentication verified.
* [ ] Authorization verified.
* [ ] API verified.
* [ ] Core workflows verified.
* [ ] Monitoring verified.
* [ ] Logs reviewed.
* [ ] Smoke tests passed.
* [ ] Rollback plan available.
* [ ] Deployment approved.

---

# 33. Conclusion

The AgriConnect production deployment should not be considered complete until the application, database, security configuration, backups, monitoring, and critical user workflows have all been verified.

This checklist should be reviewed for every production release and updated whenever the platform architecture or deployment process changes.
