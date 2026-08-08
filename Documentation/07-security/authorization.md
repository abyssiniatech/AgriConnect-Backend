# AgriConnect Ethiopia — Authorization

## 1. Overview

Authorization determines what an authenticated user is permitted to access or perform within the AgriConnect Ethiopia platform.

Authentication answers:

> **Who are you?**

Authorization answers:

> **What are you allowed to do?**

AgriConnect uses authorization to ensure that users can access only the resources and operations permitted for their assigned role and ownership.

## 2. Authorization Objectives

The authorization system is designed to:

* Enforce role-based permissions.
* Protect sensitive resources.
* Prevent unauthorized operations.
* Enforce ownership of user resources.
* Protect administrative functionality.
* Prevent privilege escalation.
* Enforce authorization on the backend.
* Provide consistent access-control decisions.

## 3. Authorization Model

AgriConnect follows a role-based access-control approach.

```text
User
 │
 ▼
Authenticated Identity
 │
 ▼
Assigned Role
 │
 ├── Farmer
 ├── Buyer
 ├── Expert
 ├── Logistics Provider
 └── Administrator
       │
       ▼
Permissions
       │
       ▼
Protected Resource
```

The backend determines whether the authenticated user has permission to perform the requested operation.

## 4. User Roles

The primary platform roles are:

| Role               | Description                                                                                        |
| ------------------ | -------------------------------------------------------------------------------------------------- |
| Farmer             | Manages agricultural activities, listings, requests, and permitted marketplace functions           |
| Buyer              | Searches products, manages purchasing activities, and accesses permitted marketplace functionality |
| Expert             | Provides agricultural expertise and manages authorized expert services                             |
| Logistics Provider | Manages transportation and delivery-related activities                                             |
| Administrator      | Performs authorized platform administration and management                                         |

## 5. Role-Based Permissions

Permissions should be granted according to the user's responsibilities.

Example:

| Operation                   | Farmer | Buyer | Expert | Logistics | Admin |
| --------------------------- | :----: | :---: | :----: | :-------: | :---: |
| View public products        |    ✓   |   ✓   |    ✓   |     ✓     |   ✓   |
| Manage own profile          |    ✓   |   ✓   |    ✓   |     ✓     |   ✓   |
| Create agricultural listing |    ✓   |   —   |    —   |     —     |   ✓   |
| Purchase products           |    —   |   ✓   |    —   |     —     |   ✓   |
| Manage expert services      |    —   |   —   |    ✓   |     —     |   ✓   |
| Manage delivery tasks       |    —   |   —   |    —   |     ✓     |   ✓   |
| Manage users                |    —   |   —   |    —   |     —     |   ✓   |
| Platform administration     |    —   |   —   |    —   |     —     |   ✓   |

Actual permissions should follow the implemented application behavior.

## 6. Resource Ownership

Authorization should not rely only on roles.

The system must also verify whether the authenticated user owns or is responsible for the requested resource.

For example:

```text
Farmer A
   │
   └── Product Listing 101

Farmer B
   │
   └── Product Listing 202
```

Farmer A should not be allowed to modify Farmer B's listing simply because both users have the `Farmer` role.

The backend should verify resource ownership before performing the operation.

## 7. Object-Level Authorization

Object-level authorization protects individual records.

For example:

```http
GET /api/v1/users/25/profile
```

The API must verify whether the authenticated user is allowed to access user `25`.

Similarly:

```http
PUT /api/v1/products/101
```

The API must verify that the authenticated user owns the product or has an administrative permission allowing modification.

## 8. Endpoint Authorization

Protected endpoints should define appropriate authorization requirements.

Example:

```text
Public Endpoint
      │
      └── No authentication required

Authenticated Endpoint
      │
      └── Valid user required

Role-Protected Endpoint
      │
      └── Specific role required

Permission-Protected Endpoint
      │
      └── Specific permission required
```

## 9. Administrative Authorization

Administrative operations require elevated privileges.

Examples include:

* Managing users
* Managing platform configuration
* Reviewing reports
* Managing roles
* Moderating marketplace content
* Reviewing security events

Administrator permissions must not be granted to ordinary users.

## 10. Privilege Escalation Prevention

The system must prevent users from increasing their own privileges.

For example, a farmer must not be able to modify a request and change:

```json
{
  "role": "Farmer"
}
```

to:

```json
{
  "role": "Administrator"
}
```

Role changes must be controlled by authorized backend operations.

## 11. Backend Enforcement

Authorization must always be enforced by the backend.

Frontend controls such as:

* Hidden buttons
* Disabled menu items
* Angular route guards
* UI role checks

improve user experience but do not provide sufficient security.

A malicious client can bypass frontend restrictions and send requests directly to the API.

Therefore:

```text
Frontend Authorization
        ↓
User Experience Protection

Backend Authorization
        ↓
Actual Security Boundary
```

## 12. HTTP Authorization Responses

The API should use appropriate HTTP status codes.

| Situation                                  |             Status |
| ------------------------------------------ | -----------------: |
| User is not authenticated                  | `401 Unauthorized` |
| User is authenticated but lacks permission |    `403 Forbidden` |
| Resource does not exist                    |    `404 Not Found` |
| User attempts unauthorized modification    |    `403 Forbidden` |

The exact response behavior should remain consistent across the API.

## 13. Authorization Flow

The authorization process is:

```text
HTTP Request
     │
     ▼
Authentication Check
     │
     ├── Failed ──► 401 Unauthorized
     │
     ▼
Authenticated User
     │
     ▼
Role / Permission Check
     │
     ├── Failed ──► 403 Forbidden
     │
     ▼
Resource Ownership Check
     │
     ├── Failed ──► Deny Access
     │
     ▼
Business Operation
     │
     ▼
Response
```

## 14. API Authorization Examples

### Farmer

A farmer may be allowed to:

```text
Create own product listing
Update own product listing
Delete own product listing
View permitted orders
Manage own profile
```

The farmer should not be allowed to:

```text
Manage system users
Change platform configuration
Access administrator functions
Modify another farmer's resources
```

### Buyer

A buyer may be allowed to:

```text
Browse products
Create orders
Manage own orders
Manage own profile
```

The buyer should not be allowed to:

```text
Modify another user's products
Manage users
Access administrator functions
```

### Expert

An expert may be allowed to:

```text
Manage expert profile
Manage authorized agricultural services
Respond to permitted consultations
```

### Logistics Provider

A logistics provider may be allowed to:

```text
View assigned delivery tasks
Update permitted delivery status
Manage logistics profile
```

### Administrator

An administrator may be allowed to perform platform-management operations according to the administrator permission model.

## 15. Authorization and APIs

Every protected API endpoint should define its authorization requirements clearly.

Documentation should specify:

* Required authentication
* Required role
* Required permission
* Resource ownership requirements
* Expected unauthorized response

This makes the API easier to maintain and audit.

## 16. Authorization Logging

Important authorization events should be logged when appropriate.

Examples:

* Repeated authorization failures
* Administrator actions
* Role changes
* Permission changes
* Attempts to access unauthorized resources

Logs should not expose sensitive credentials or authentication tokens.

## 17. Security Testing

Authorization must be tested using both valid and invalid scenarios.

Examples:

### Valid

```text
Farmer accesses own listing → Allowed
Buyer creates own order → Allowed
Admin manages authorized resource → Allowed
```

### Invalid

```text
Farmer modifies another farmer's listing → Denied
Buyer accesses admin endpoint → Denied
Unauthenticated user accesses protected endpoint → Denied
Farmer changes own role to Admin → Denied
```

## 18. Authorization Checklist

* [ ] All protected endpoints require authentication.
* [ ] Roles are validated server-side.
* [ ] Permissions are enforced server-side.
* [ ] Resource ownership is checked.
* [ ] Administrative endpoints are protected.
* [ ] Users cannot modify their own roles.
* [ ] Users cannot access other users' private resources.
* [ ] Frontend authorization is not treated as the security boundary.
* [ ] `401 Unauthorized` is used for authentication failures.
* [ ] `403 Forbidden` is used for authorization failures.
* [ ] Authorization failures are logged where appropriate.
* [ ] Authorization rules are covered by automated tests.

## 19. Conclusion

Authorization is a critical security layer within AgriConnect Ethiopia.

The platform combines role-based permissions with resource ownership checks to ensure that authenticated users can perform only the actions they are authorized to perform.

All authorization decisions must ultimately be enforced by the backend to prevent unauthorized access, privilege escalation, and manipulation of protected resources.
