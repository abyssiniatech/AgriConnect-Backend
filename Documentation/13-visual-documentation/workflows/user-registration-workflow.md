# AgriConnect Ethiopia — User Registration Workflow

## 1. Purpose

This document describes the workflow for registering a new user in the AgriConnect Ethiopia platform.

The registration process creates a user account and, where applicable, associates the account with an appropriate user role.

---

# 2. Registration Actors

The registration workflow may involve:

* Farmer.
* Buyer.
* Agricultural Expert.
* Logistics Provider.
* System.

---

# 3. High-Level Workflow

```text
┌──────────────┐
│    User      │
└──────┬───────┘
       │
       │ Open Registration
       ▼
┌──────────────────────┐
│ Registration Form    │
└──────────┬───────────┘
           │
           │ Enter Information
           ▼
┌──────────────────────┐
│ Client Validation    │
└──────────┬───────────┘
           │
           │ Valid
           ▼
┌──────────────────────┐
│ ASP.NET Core API     │
└──────────┬───────────┘
           │
           │ Validate Request
           ▼
┌──────────────────────┐
│ Server Validation    │
└──────────┬───────────┘
           │
           │ Valid
           ▼
┌──────────────────────┐
│ Create User Account  │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│ PostgreSQL Database  │
└──────────┬───────────┘
           │
           │ Success
           ▼
┌──────────────────────┐
│ Registration Success │
└──────────────────────┘
```

---

# 4. Registration Steps

## Step 1 — Open Registration

The user selects the registration option from the application.

Example:

```text
Login
  │
  └── Create Account
```

---

## Step 2 — Enter Information

The user provides the required registration information.

Typical information may include:

* Full name.
* Email.
* Password.
* Phone number.
* Location.
* Account role.

The exact fields should match the implemented registration form.

---

## Step 3 — Client-Side Validation

Angular validates the submitted information before sending it to the API.

Possible validation rules include:

* Required fields.
* Valid email format.
* Password requirements.
* Valid phone number.
* Valid role selection.

Example:

```text
Email:
✓ Valid format

Password:
✓ Meets requirements

Required fields:
✓ Complete
```

---

# 5. API Request

After successful client-side validation, the frontend sends the registration request to the backend.

Example conceptual request:

```http
POST /api/v1/auth/register
Content-Type: application/json
```

Example request body:

```json
{
  "name": "Test User",
  "email": "test@example.com",
  "password": "********",
  "role": "Farmer"
}
```

> The example password is intentionally masked. Real passwords must never be stored in documentation.

---

# 6. Server-Side Validation

The ASP.NET Core API validates the request again.

Server-side validation is required even when client-side validation exists because client-side validation can be bypassed.

The API verifies:

* Required fields.
* Email format.
* Password requirements.
* Role validity.
* Duplicate account rules.
* Business rules.

---

# 7. Duplicate Account Check

The system checks whether the email or other unique identifier is already registered.

```text
                Registration Request
                         │
                         ▼
                  Search Database
                         │
              ┌──────────┴──────────┐
              │                     │
           Exists                Not Found
              │                     │
              ▼                     ▼
        Return Conflict       Continue
```

If the account already exists, the system should return an appropriate error response.

Example:

```http
409 Conflict
```

---

# 8. Password Processing

Passwords must never be stored as plain text.

The backend should use a secure password-hashing mechanism before storing the credential representation.

```text
Plain Password
      │
      ▼
Password Hashing
      │
      ▼
Password Hash
      │
      ▼
Database
```

The original password should not be stored.

---

# 9. User Creation

After validation succeeds, the application creates the user account.

The account may include:

```text
User ID
Name
Email
Password Hash
Role
Created At
Status
```

The exact fields depend on the implemented database model.

---

# 10. Database Persistence

The user information is stored in PostgreSQL through the backend data-access layer.

```text
ASP.NET Core
      │
      ▼
Application Service
      │
      ▼
Entity Framework Core
      │
      ▼
PostgreSQL
```

---

# 11. Role Assignment

The user selects or receives an appropriate role according to the application's registration rules.

Possible roles include:

```text
Farmer
Buyer
Agricultural Expert
Logistics Provider
```

Administrative roles should normally be assigned through a controlled administrative process rather than unrestricted public registration.

---

# 12. Registration Success

When account creation succeeds, the API returns a successful response.

Example:

```http
201 Created
```

The frontend then displays a confirmation message.

Example:

```text
Account created successfully.

You can now sign in.
```

---

# 13. Registration Failure

Possible failures include:

| Condition               | Possible Response         |
| ----------------------- | ------------------------- |
| Missing required data   | 400 Bad Request           |
| Invalid email           | 400 Bad Request           |
| Invalid password        | 400 Bad Request           |
| Duplicate email         | 409 Conflict              |
| Unauthorized operation  | 403 Forbidden             |
| Unexpected server error | 500 Internal Server Error |

Actual status codes should follow the implemented API contract.

---

# 14. Complete Workflow

```text
User
 │
 │ 1. Open Registration
 ▼
Angular Registration Form
 │
 │ 2. Enter Data
 ▼
Client Validation
 │
 ├──────── Invalid ────────► Display Validation Error
 │
 │ Valid
 ▼
ASP.NET Core API
 │
 ▼
Server Validation
 │
 ├──────── Invalid ────────► Return 400
 │
 │ Valid
 ▼
Duplicate Account Check
 │
 ├──────── Exists ─────────► Return 409
 │
 │ Not Found
 ▼
Hash Password
 │
 ▼
Create User
 │
 ▼
PostgreSQL
 │
 ▼
201 Created
 │
 ▼
Registration Success
 │
 ▼
User Login
```

---

# 15. Security Considerations

The registration workflow must protect user information.

Important controls include:

* HTTPS.
* Server-side validation.
* Secure password hashing.
* Duplicate-account protection.
* Rate limiting where appropriate.
* Secure error messages.
* Input sanitization/validation.
* Protection against automated abuse.
* Secure session/token handling.

Sensitive credentials should never be written to logs.

---

# 16. Testing Workflow

Registration should be tested with:

### Valid Registration

```text
Valid data
   ↓
Account created
```

### Invalid Email

```text
Invalid email
   ↓
Validation error
```

### Weak Password

```text
Weak password
   ↓
Validation error
```

### Duplicate Account

```text
Existing email
   ↓
409 Conflict
```

### Missing Required Field

```text
Missing field
   ↓
400 Bad Request
```

---

# 17. Screenshot Evidence

Recommended screenshots:

```text
13-visual-documentation/screenshots/authentication/
```

Suggested filenames:

```text
registration-page.png
registration-validation-error.png
registration-success.png
duplicate-account-error.png
```

Screenshots must not contain real passwords, tokens, or other sensitive information.

---

# 18. Related Documentation

```text
05-api/
07-security/
08-testing/
09-user-guides/
10-developer-guide/
13-visual-documentation/screenshots/
```

---

# 19. Conclusion

The AgriConnect Ethiopia registration workflow validates user information at both the frontend and backend levels, securely processes credentials, creates the account in PostgreSQL, and provides an appropriate result to the user.

The exact workflow should remain synchronized with the implemented authentication API and database model.
