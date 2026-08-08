# AgriConnect Ethiopia — Screenshots

## 1. Purpose

This directory contains screenshots that visually document the AgriConnect Ethiopia application.

Screenshots provide evidence of implemented features, user interfaces, workflows, testing results, and deployment status.

---

# 2. Screenshot Organization

Screenshots should be organized according to the area of the application they document.

Recommended structure:

```text
screenshots/
├── authentication/
├── dashboard/
├── marketplace/
├── products/
├── orders/
├── experts/
├── logistics/
├── admin/
├── testing/
└── deployment/
```

Create directories only when screenshots are available for the corresponding feature.

---

# 3. Naming Convention

Use clear and consistent filenames.

Recommended format:

```text
<feature>-<screen>-<description>.png
```

Examples:

```text
login-page.png
farmer-dashboard.png
product-list.png
product-create-form.png
marketplace-search.png
order-details.png
expert-directory.png
admin-dashboard.png
api-test-success.png
deployment-success.png
```

Avoid filenames such as:

```text
image1.png
screenshot.png
final.png
test.png
new.png
```

---

# 4. Authentication Screenshots

Authentication screenshots may include:

```text
login-page.png
registration-page.png
forgot-password.png
password-reset.png
logout-success.png
```

These screenshots demonstrate authentication-related user interfaces.

### Security Requirement

Never capture or store screenshots containing:

* Passwords.
* JWT tokens.
* API keys.
* Database credentials.
* Secret configuration values.
* Private authentication information.

Sensitive information must be removed or hidden before committing screenshots.

---

# 5. Dashboard Screenshots

Dashboard screenshots may include:

```text
farmer-dashboard.png
buyer-dashboard.png
expert-dashboard.png
logistics-dashboard.png
admin-dashboard.png
```

Each screenshot should demonstrate the relevant dashboard functionality.

---

# 6. Marketplace Screenshots

Recommended marketplace screenshots:

```text
marketplace-home.png
product-list.png
product-details.png
product-search.png
product-filter.png
product-category.png
```

These screenshots should demonstrate how users discover and inspect agricultural products.

---

# 7. Product Management Screenshots

For farmer product management:

```text
product-create-form.png
product-edit-form.png
product-management.png
product-status.png
```

Where possible, screenshots should demonstrate:

* Product information.
* Price.
* Quantity.
* Category.
* Location.
* Product status.

---

# 8. Order Screenshots

Recommended order screenshots:

```text
cart.png
checkout.png
order-confirmation.png
order-details.png
order-history.png
```

Screenshots should demonstrate the buyer's ordering workflow.

---

# 9. Agricultural Expert Screenshots

Recommended screenshots:

```text
expert-directory.png
expert-profile.png
consultation-request.png
consultation-history.png
```

These demonstrate how farmers can find and communicate with agricultural experts.

---

# 10. Logistics Screenshots

Recommended screenshots:

```text
delivery-request.png
delivery-details.png
delivery-status.png
logistics-dashboard.png
```

These should demonstrate the logistics workflow where implemented.

---

# 11. Administration Screenshots

Administrative screenshots may include:

```text
admin-dashboard.png
user-management.png
product-management-admin.png
order-management-admin.png
system-settings.png
```

Administrative screenshots should not expose sensitive information.

---

# 12. Testing Screenshots

Testing evidence may include:

```text
api-test-success.png
frontend-test-success.png
integration-test-success.png
security-test-success.png
database-test-success.png
```

Testing screenshots should clearly show:

* Test being executed.
* Test result.
* Relevant endpoint or feature.
* Success or failure status.

---

# 13. Deployment Screenshots

Deployment evidence may include:

```text
production-frontend.png
production-api.png
health-check.png
deployment-success.png
```

Do not expose production credentials or sensitive infrastructure information.

---

# 14. Screenshot Metadata

For important screenshots, record:

| Field       | Description                        |
| ----------- | ---------------------------------- |
| Filename    | Screenshot filename                |
| Feature     | Related application feature        |
| Environment | Development / Testing / Production |
| Date        | Date captured                      |
| Description | What the screenshot demonstrates   |
| Version     | Application version if available   |

Example:

| Field       | Value                          |
| ----------- | ------------------------------ |
| Filename    | product-list.png               |
| Feature     | Marketplace                    |
| Environment | Development                    |
| Date        | 2026-08-08                     |
| Description | Displays agricultural products |
| Version     | Development                    |

---

# 15. Screenshot Quality

Screenshots should:

* Clearly show the relevant feature.
* Be readable.
* Avoid unnecessary browser clutter.
* Use reasonable resolution.
* Hide sensitive information.
* Use realistic test data where possible.
* Represent the current application version.

---

# 16. Recommended Screenshot Workflow

```text
Implement Feature
      ↓
Run Application
      ↓
Verify Feature
      ↓
Prepare Test Data
      ↓
Capture Screenshot
      ↓
Remove Sensitive Information
      ↓
Rename File
      ↓
Store in Correct Directory
      ↓
Update Documentation
      ↓
Commit to Git
```

---

# 17. Before Adding a Screenshot

Check:

* [ ] Feature works correctly.
* [ ] Screenshot shows the intended functionality.
* [ ] No passwords are visible.
* [ ] No tokens are visible.
* [ ] No API keys are visible.
* [ ] No database credentials are visible.
* [ ] Personal sensitive information is hidden.
* [ ] Filename follows the naming convention.
* [ ] Screenshot is stored in the correct directory.

---

# 18. Git Management

Screenshots can increase repository size.

Before committing large image files:

* Compress unnecessarily large images.
* Avoid duplicate screenshots.
* Remove outdated screenshots.
* Keep only useful evidence.

Check the repository before committing:

```bash
git status
```

Then:

```bash
git add 13-visual-documentation/screenshots/
git commit -m "docs: add application screenshots"
```

---

# 19. Screenshot Index

Maintain an index of important screenshots.

Example:

| Screenshot               | Feature        | Purpose             |
| ------------------------ | -------------- | ------------------- |
| `login-page.png`         | Authentication | Login interface     |
| `farmer-dashboard.png`   | Dashboard      | Farmer overview     |
| `product-list.png`       | Marketplace    | Product listings    |
| `product-details.png`    | Marketplace    | Product information |
| `order-details.png`      | Orders         | Order information   |
| `expert-directory.png`   | Experts        | Expert discovery    |
| `delivery-status.png`    | Logistics      | Delivery tracking   |
| `admin-dashboard.png`    | Administration | Admin overview      |
| `api-test-success.png`   | Testing        | API test evidence   |
| `deployment-success.png` | Deployment     | Deployment evidence |

Update this table when new screenshots are added.

---

# 20. Documentation Relationship

Screenshots should support the written documentation.

Relevant sections include:

```text
06-features/
07-security/
08-testing/
09-user-guides/
10-developer-guide/
11-deployment/
13-visual-documentation/
```

A screenshot should complement documentation rather than replace important written instructions.

---

# 21. Privacy and Security

Screenshots are documentation artifacts and may be stored in Git repositories.

Before committing a screenshot, verify that it does not contain:

* Authentication credentials.
* Personal passwords.
* Access tokens.
* Private keys.
* API secrets.
* Production database information.
* Sensitive personal data.

If sensitive information appears accidentally, remove it before committing.

---

# 22. Current Screenshot Status

At the beginning of visual documentation, the screenshot collection may be empty.

Use the following status:

```text
Status: Screenshots to be captured during implementation and testing.
```

As features are completed, replace this status with the actual screenshot inventory.

---

# 23. Conclusion

Screenshots provide visual evidence of the AgriConnect Ethiopia application's functionality and progress.

They should be captured from verified application features, named consistently, organized by feature, reviewed for sensitive information, and maintained together with the project's written documentation.
