# AgriConnect Ethiopia — Backup and Recovery Guide

## 1. Introduction

This document defines the backup and recovery procedures for AgriConnect Ethiopia.

The purpose of the backup strategy is to protect:

* Application data.
* PostgreSQL databases.
* Uploaded files.
* Configuration.
* Deployment artifacts.
* Critical operational information.

A reliable backup strategy helps reduce the risk of permanent data loss caused by hardware failure, accidental deletion, software errors, security incidents, or deployment problems.

---

# 2. Backup Objectives

The backup strategy should provide:

* Regular backups.
* Secure backup storage.
* Verified backups.
* Appropriate retention.
* Recovery procedures.
* Documented responsibilities.
* Protection against accidental deletion.

---

# 3. What Should Be Backed Up

Depending on the deployed architecture, backups should include:

```text
PostgreSQL Database
Uploaded Files
Application Configuration
Deployment Configuration
Critical Infrastructure Configuration
```

Source code should normally be maintained through the project's Git repository rather than treated as the primary backup mechanism.

---

# 4. Database Backup

AgriConnect uses PostgreSQL.

A logical database backup can be created using:

```bash
pg_dump -U postgres -d agric_connect > agric_connect_backup.sql
```

Replace:

* `postgres` with the appropriate database user.
* `agric_connect` with the actual database name.

---

# 5. Compressed Database Backup

A compressed PostgreSQL backup can be created using:

```bash
pg_dump -U postgres -d agric_connect | gzip > agric_connect_backup.sql.gz
```

Compression can reduce storage requirements for larger databases.

---

# 6. Custom PostgreSQL Backup Format

PostgreSQL also supports its custom backup format:

```bash
pg_dump -U postgres -d agric_connect -Fc -f agric_connect_backup.dump
```

This format can be restored using PostgreSQL's `pg_restore`.

---

# 7. Database Restore

For a plain SQL backup:

```bash
psql -U postgres -d agric_connect < agric_connect_backup.sql
```

For a custom-format backup:

```bash
pg_restore -U postgres -d agric_connect agric_connect_backup.dump
```

The target database must be prepared according to the restore procedure before restoring data.

---

# 8. Restore to a New Database

When testing a backup, it is safer to restore it into a separate database.

Create a test database:

```sql
CREATE DATABASE agric_connect_restore_test;
```

Restore the backup:

```bash
psql -U postgres -d agric_connect_restore_test < agric_connect_backup.sql
```

Then verify the restored data.

---

# 9. Backup Verification

Creating a backup is not enough.

Backups should be periodically tested by restoring them to a controlled environment.

Verify:

* Database opens successfully.
* Tables exist.
* Expected records exist.
* Important relationships remain intact.
* Application can connect.
* Critical features work.

---

# 10. Backup Schedule

The exact schedule should be based on business requirements.

A typical strategy may include:

| Backup                   | Example Frequency           |
| ------------------------ | --------------------------- |
| Database backup          | Daily                       |
| Critical database backup | More frequently if required |
| File backup              | Daily                       |
| Configuration backup     | After important changes     |
| Full recovery test       | Periodically                |

The final schedule should be approved according to the project's operational requirements.

---

# 11. Backup Retention

Backups should not be kept indefinitely without a retention policy.

Example policy:

```text
Daily backups → Short-term retention
Weekly backups → Medium-term retention
Monthly backups → Long-term retention
```

The actual retention period should be determined by:

* Business requirements.
* Storage capacity.
* Data importance.
* Compliance requirements.
* Recovery objectives.

---

# 12. Backup Storage

Backups should be stored separately from the primary application server where possible.

Suitable storage may include:

* Dedicated backup server.
* Secure cloud object storage.
* Managed database backups.
* Organization-approved storage.

Avoid keeping the only copy of a backup on the same server as the production database.

---

# 13. Backup Security

Backup files may contain sensitive information.

Therefore:

* Restrict access.
* Encrypt backups where appropriate.
* Protect backup credentials.
* Do not expose backups publicly.
* Do not commit backups to Git.
* Monitor access to backup storage.

---

# 14. Uploaded File Backups

If AgriConnect stores:

* Farmer images.
* Product images.
* Documents.
* Reports.
* Other uploaded files.

those files must be included in the backup strategy.

Database backups alone may not protect files stored outside PostgreSQL.

---

# 15. Configuration Backups

Production configuration should be reproducible without exposing secrets.

Back up the configuration structure and deployment configuration where appropriate.

Do not create unsecured copies of:

```text
Database passwords
JWT secrets
API keys
Private keys
Cloud credentials
```

Use a secure secret-management system for sensitive values.

---

# 16. Recovery Objectives

Two important recovery concepts are:

### Recovery Point Objective — RPO

RPO defines how much data loss is acceptable.

Example:

```text
RPO = 24 hours
```

means the organization may accept losing up to approximately one day's worth of data.

### Recovery Time Objective — RTO

RTO defines how quickly the system should be restored.

Example:

```text
RTO = 4 hours
```

means the target is to restore service within four hours.

Actual RPO and RTO values must be defined by project requirements.

---

# 17. Recovery Scenarios

Recovery procedures should cover:

### Scenario 1 — Accidental Data Deletion

Restore the affected data from a verified backup.

### Scenario 2 — Database Corruption

Restore the database from the most recent valid backup.

### Scenario 3 — Server Failure

Provision a replacement server and restore:

* Application.
* Database.
* Configuration.
* Uploaded files.

### Scenario 4 — Deployment Failure

Roll back to the previous application version and restore database state only if necessary.

### Scenario 5 — Security Incident

Isolate the affected system, preserve evidence, rotate credentials, and restore from a known-good state when appropriate.

---

# 18. Database Recovery Procedure

A controlled database recovery should follow:

```text
Identify Incident
       ↓
Stop Further Damage
       ↓
Determine Recovery Point
       ↓
Verify Backup
       ↓
Prepare Recovery Environment
       ↓
Restore Database
       ↓
Verify Data
       ↓
Start Application
       ↓
Run Smoke Tests
       ↓
Monitor System
```

---

# 19. Production Recovery

Before restoring a production database:

1. Notify responsible stakeholders.
2. Confirm the correct backup.
3. Determine the recovery point.
4. Stop or isolate application writes if required.
5. Preserve the current database when possible.
6. Restore the selected backup.
7. Verify database integrity.
8. Start application services.
9. Run smoke tests.
10. Monitor the system.

Avoid destructive recovery operations without authorization.

---

# 20. Backup Naming Convention

Use a consistent naming convention.

Example:

```text
agric_connect_2026-08-08.sql
```

Compressed:

```text
agric_connect_2026-08-08.sql.gz
```

Custom format:

```text
agric_connect_2026-08-08.dump
```

For automated backups, include timestamps where appropriate.

---

# 21. Backup Integrity Checks

After creating a backup:

1. Verify the file exists.
2. Verify the file size is reasonable.
3. Verify the backup command completed successfully.
4. Store it securely.
5. Periodically perform a test restore.

Example:

```bash
ls -lh agric_connect_backup.sql
```

---

# 22. Test Restore Procedure

A backup recovery test should be performed in a non-production environment.

Example:

```bash
createdb -U postgres agric_connect_restore_test
```

Restore:

```bash
psql -U postgres -d agric_connect_restore_test < agric_connect_backup.sql
```

Inspect:

```bash
psql -U postgres -d agric_connect_restore_test
```

Then:

```sql
\dt
```

Verify important records and relationships.

---

# 23. Application Verification After Recovery

After restoring the database, verify:

* Application starts.
* Database connection succeeds.
* Login works.
* User records are available.
* Core agricultural workflows work.
* API requests succeed.
* Frontend displays expected data.
* Uploaded files are accessible.
* No unexpected database errors appear.

---

# 24. Disaster Recovery Checklist

### Before Incident

* [ ] Backups are automated.
* [ ] Backup storage is separate.
* [ ] Backups are protected.
* [ ] Restore procedure is documented.
* [ ] Recovery responsibilities are assigned.
* [ ] Recovery tests are performed.

### During Incident

* [ ] Incident identified.
* [ ] System protected from further damage.
* [ ] Recovery point selected.
* [ ] Backup verified.
* [ ] Stakeholders informed.
* [ ] Recovery procedure started.

### After Recovery

* [ ] Database verified.
* [ ] Application verified.
* [ ] Frontend verified.
* [ ] API verified.
* [ ] Authentication verified.
* [ ] Critical workflows tested.
* [ ] Monitoring enabled.
* [ ] Incident documented.

---

# 25. Backup Failure

If a scheduled backup fails:

1. Identify the failure.
2. Check database connectivity.
3. Check storage availability.
4. Check permissions.
5. Check backup logs.
6. Retry the backup.
7. Verify the new backup.
8. Investigate recurring failures.

A failed backup should not be ignored.

---

# 26. Recovery Testing

Recovery tests should be performed periodically.

A recovery test should verify that:

* Backups can actually be restored.
* Database schema is usable.
* Data is present.
* Application can connect.
* Critical workflows function.

Document:

```text
Test Date
Backup Used
Restore Duration
Result
Problems Found
Corrective Actions
```

---

# 27. Security Incident Recovery

If a backup or production system is involved in a security incident:

1. Isolate affected systems.
2. Preserve relevant logs.
3. Identify compromised credentials.
4. Rotate secrets.
5. Determine a known-good recovery point.
6. Restore only trusted data and software.
7. Patch the underlying vulnerability.
8. Verify system security.
9. Monitor for suspicious activity.

Do not blindly restore a compromised system without investigating the cause.

---

# 28. Responsibilities

The deployment team should define responsibility for:

* Backup monitoring.
* Backup storage.
* Restore testing.
* Database recovery.
* Application recovery.
* Security incident response.
* Recovery documentation.

Responsibilities should be assigned before an incident occurs.

---

# 29. Backup and Recovery Checklist

* [ ] Database backups configured.
* [ ] Uploaded file backups configured.
* [ ] Backup storage secured.
* [ ] Backup retention defined.
* [ ] Backup integrity verified.
* [ ] Restore procedure documented.
* [ ] Restore test completed.
* [ ] RPO defined.
* [ ] RTO defined.
* [ ] Recovery responsibilities assigned.
* [ ] Security recovery procedure documented.

---

# 30. Conclusion

A backup is useful only when it can be successfully restored.

AgriConnect should maintain regular, secure, and verified backups of critical application data and supporting files. Recovery procedures should be documented, tested periodically, and updated whenever the system architecture changes.
