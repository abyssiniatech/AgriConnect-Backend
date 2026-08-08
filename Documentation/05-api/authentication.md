# API Authentication

## 1. Overview

AgriConnect Ethiopia uses secure API authentication to protect user accounts, agricultural data, marketplace transactions, expert services, logistics operations, and administrative functionality.

Authentication verifies the identity of a user before the system allows access to protected API resources.

The authentication system is designed to support multiple AgriConnect user roles, including:

* Farmer
* Buyer
* Agricultural Expert
* Logistics Provider
* Administrator

Protected API endpoints require a valid authentication credential.

---

## 2. Authentication Flow

The general authentication flow is:

```text
User
  │
  │ 1. Enter credentials
  ▼
AgriConnect Frontend
  │
  │ 2. POST /api/auth/login
  ▼
Authentication API
  │
  │ 3. Validate credentials
  ▼
User Database
  │
  │ 4. User verified
  ▼
Authentication Service
  │
  │ 5. Generate access token
  ▼
Frontend
  │
  │ 6. Store token securely
  ▼
Protected API
  │
  │ 7. Authorization header
  ▼
AgriConnect Backend
```

---

## 3. Registration

New users can create an AgriConnect account by providing the required registration information.

Typical registration information includes:

* Full name
* Email address
* Phone number
* Password
* User role
* Location
* Additional role-specific information

Example request:

```http
POST /api/auth/register
Content-Type: application/json
```

Example:

```json
{
  "fullName": "Abebe Kebede",
  "email": "abebe@example.com",
  "phoneNumber": "+251911234567",
  "password": "SecurePassword123!",
  "role": "Farmer",
  "location": "Oromia"
}
```

The server validates the submitted information before creating the account.

---

## 4. Login

Registered users authenticate using their email/phone number and password.

Example:

```http
POST /api/auth/login
Content-Type: application/json
```

Example request:

```json
{
  "email": "abebe@example.com",
  "password": "SecurePassword123!"
}
```

A successful login returns an authentication token and basic user information.

Example response:

```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "userId": "USR-1001",
    "fullName": "Abebe Kebede",
    "role": "Farmer",
    "accessToken": "<access-token>",
    "expiresIn": 3600
  }
}
```

---

## 5. Access Token

AgriConnect uses an access token to identify an authenticated user when accessing protected resources.

The token should be included in the HTTP `Authorization` header.

Example:

```http
Authorization: Bearer <access-token>
```

Example API request:

```http
GET /api/farmers/profile
Authorization: Bearer <access-token>
```

The backend validates the token before processing the request.

---

## 6. Protected Endpoints

Protected endpoints require authentication.

Examples include:

```text
GET    /api/farmers/profile
PUT    /api/farmers/profile
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}

POST   /api/orders
GET    /api/orders/{id}

POST   /api/expert/consultations
GET    /api/notifications
```

Unauthenticated requests to protected resources should be rejected.

Example:

```http
401 Unauthorized
```

---

## 7. Role-Based Authentication

Authentication establishes **who the user is**.

Authorization determines **what the authenticated user is allowed to do**.

AgriConnect supports role-based access control.

| Role               | Main Responsibilities                                          |
| ------------------ | -------------------------------------------------------------- |
| Farmer             | Manage farm information, products, and agricultural activities |
| Buyer              | Search products, place orders, and manage purchases            |
| Expert             | Provide agricultural advice and consultations                  |
| Logistics Provider | Manage deliveries and transportation                           |
| Administrator      | Manage users, content, transactions, and system operations     |

For example, an authenticated farmer should not be allowed to access administrator-only endpoints.

---

## 8. Password Security

User passwords must never be stored as plain text.

Passwords should be securely hashed before being stored in the database.

```text
Plain Password
      │
      ▼
Password Hashing Algorithm
      │
      ▼
Secure Password Hash
      │
      ▼
Database
```

During login, the submitted password is compared with the stored password hash.

The original password should never be retrievable from the database.

---

## 9. Token Expiration

Access tokens should have a limited lifetime.

Example:

```text
Token issued
     │
     ▼
Valid for limited period
     │
     ▼
Token expires
     │
     ▼
User authenticates again
```

Short-lived tokens reduce the security impact of a stolen credential.

The exact expiration period should be configured through the application's environment/configuration settings.

---

## 10. Logout

When a user logs out, the frontend should remove the active authentication credential.

Example:

```text
User clicks Logout
        │
        ▼
Frontend clears authentication state
        │
        ▼
Token is no longer used
        │
        ▼
User returns to Login page
```

If refresh tokens are implemented, they should also be invalidated or revoked during logout.

---

## 11. Authentication Errors

The API should return appropriate HTTP status codes.

### Invalid Credentials

```http
401 Unauthorized
```

Example:

```json
{
  "success": false,
  "message": "Invalid email or password"
}
```

### Missing Authentication

```http
401 Unauthorized
```

Example:

```json
{
  "success": false,
  "message": "Authentication is required"
}
```

### Expired Token

```http
401 Unauthorized
```

Example:

```json
{
  "success": false,
  "message": "Authentication token has expired"
}
```

### Insufficient Permissions

```http
403 Forbidden
```

Example:

```json
{
  "success": false,
  "message": "You do not have permission to access this resource"
}
```

---

## 12. Security Requirements

AgriConnect authentication should follow these security principles:

* Passwords must never be stored as plain text.
* Authentication endpoints must use HTTPS in production.
* Access tokens must have an expiration time.
* Sensitive authentication information must not be written to application logs.
* Failed login attempts should be monitored.
* Protected endpoints must validate authentication.
* Role-based authorization must be enforced on the server.
* Tokens should not be exposed in URLs.
* Authentication secrets must be stored in secure configuration.
* Production credentials must not be committed to Git.

---

## 13. HTTPS

All authentication traffic must use HTTPS in production.

```text
HTTP
  │
  ▼
Not recommended for authentication
```

Instead:

```text
HTTPS
  │
  ▼
Encrypted communication
  │
  ▼
Authentication API
```

HTTPS protects credentials and authentication tokens while they are transmitted between the client and server.

---

## 14. Frontend Authentication

The AgriConnect frontend is responsible for:

1. Collecting login credentials.
2. Sending credentials to the authentication API.
3. Receiving the authentication response.
4. Maintaining the authenticated user state.
5. Sending authentication credentials with protected requests.
6. Handling authentication failures.
7. Redirecting unauthenticated users to the login page.
8. Clearing authentication state during logout.

Example request header:

```http
Authorization: Bearer <access-token>
```

---

## 15. Backend Authentication

The backend is responsible for:

1. Validating credentials.
2. Verifying the user's account.
3. Generating authentication tokens.
4. Validating tokens on protected requests.
5. Checking token expiration.
6. Identifying the authenticated user.
7. Enforcing role-based permissions.
8. Rejecting invalid or unauthorized requests.

---

## 16. Authentication Sequence

```text
┌──────────────┐
│     User     │
└──────┬───────┘
       │
       │ Login credentials
       ▼
┌──────────────────┐
│ AgriConnect UI   │
└──────┬───────────┘
       │
       │ POST /api/auth/login
       ▼
┌──────────────────┐
│ Authentication   │
│ API              │
└──────┬───────────┘
       │
       │ Validate credentials
       ▼
┌──────────────────┐
│ User Database    │
└──────┬───────────┘
       │
       │ User verified
       ▼
┌──────────────────┐
│ Token Service    │
└──────┬───────────┘
       │
       │ Access token
       ▼
┌──────────────────┐
│ AgriConnect UI   │
└──────┬───────────┘
       │
       │ Bearer token
       ▼
┌──────────────────┐
│ Protected API    │
└──────────────────┘
```

---

## 17. Recommended Authentication Endpoints

| Method | Endpoint                    | Purpose                      | Authentication    |
| ------ | --------------------------- | ---------------------------- | ----------------- |
| POST   | `/api/auth/register`        | Register a new user          | No                |
| POST   | `/api/auth/login`           | Authenticate user            | No                |
| POST   | `/api/auth/logout`          | End authenticated session    | Yes               |
| POST   | `/api/auth/refresh`         | Refresh authentication token | Yes/Refresh Token |
| GET    | `/api/auth/me`              | Get current user             | Yes               |
| POST   | `/api/auth/forgot-password` | Request password reset       | No                |
| POST   | `/api/auth/reset-password`  | Reset password               | Reset Token       |

> These endpoint names represent the documented API design. They should be updated if the implemented AgriConnect backend uses different routes.

---

## 18. Authentication Best Practices

AgriConnect follows these recommended practices:

### Do

* Use HTTPS.
* Hash passwords securely.
* Use short-lived access tokens.
* Validate tokens on every protected request.
* Apply role-based authorization.
* Validate login input.
* Rate-limit authentication endpoints.
* Monitor suspicious login activity.
* Keep secrets outside source control.

### Do Not

* Store plain-text passwords.
* Put passwords in URLs.
* Put access tokens in URLs.
* Commit secrets to Git.
* Log passwords or tokens.
* Trust role information supplied directly by the client.
* Disable authentication for production APIs.

---

## 19. Summary

AgriConnect authentication provides a secure mechanism for identifying users and protecting API resources.

The authentication architecture separates:

```text
Authentication
      ↓
Who is the user?

Authorization
      ↓
What can the user access?
```

This separation allows AgriConnect to securely support farmers, buyers, agricultural experts, logistics providers, and administrators while protecting sensitive agricultural, marketplace, and user information.

Authentication is a foundational security layer of the AgriConnect Ethiopia platform and should be integrated with the authorization, data protection, API security, and overall application security architecture.
