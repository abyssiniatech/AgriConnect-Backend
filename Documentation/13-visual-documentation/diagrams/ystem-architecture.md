# AgriConnect Ethiopia — System Architecture Diagram

## 1. Overview

This document describes the high-level architecture of the AgriConnect Ethiopia platform.

The system follows a layered web-application architecture consisting of:

* Angular frontend.
* ASP.NET Core backend API.
* Application and business logic.
* Entity Framework Core.
* PostgreSQL database.
* External services where required.

---

## 2. High-Level Architecture

```text
┌─────────────────────────────────────────────────────────────┐
│                         USERS                               │
│                                                             │
│  Farmers │ Buyers │ Agricultural Experts │ Logistics       │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           │ HTTPS
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                    ANGULAR FRONTEND                         │
│                                                             │
│  Components │ Routing │ Forms │ Services │ State           │
│  Management │ Authentication │ User Interface               │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           │ REST API / JSON
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                  ASP.NET CORE API                           │
│                                                             │
│  Controllers                                                │
│      ↓                                                      │
│  Application Services / CQRS                                │
│      ↓                                                      │
│  Business Logic                                             │
│      ↓                                                      │
│  Validation / Authorization                                 │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           │ Entity Framework Core
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                     POSTGRESQL                              │
│                                                             │
│  Users │ Products │ Orders │ Expert Data │ Logistics Data  │
└─────────────────────────────────────────────────────────────┘

                           │
                           │ Optional integrations
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                   EXTERNAL SERVICES                         │
│                                                             │
│  AI Services │ Email │ Notifications │ File Storage         │
└─────────────────────────────────────────────────────────────┘
```

---

## 3. Frontend Layer

The Angular frontend provides the user interface.

Major responsibilities include:

* User registration.
* Login.
* Dashboards.
* Product browsing.
* Product management.
* Forms.
* Search.
* Filtering.
* API communication.
* Notifications.
* User interaction.

The frontend communicates with the backend through HTTPS requests.

---

## 4. API Layer

The ASP.NET Core API provides the primary communication interface between the frontend and backend services.

Responsibilities include:

* HTTP request handling.
* Authentication.
* Authorization.
* Request validation.
* Business operations.
* API responses.
* Error handling.
* API versioning where implemented.

Example:

```text
Angular
   │
   │ POST /api/v1/products
   ▼
ASP.NET Core Controller
   │
   ▼
Application Service
   │
   ▼
Database
```

---

## 5. Application Layer

The application layer coordinates business operations.

Responsibilities include:

* Application use cases.
* Commands.
* Queries.
* Validation.
* Business workflows.
* Service coordination.

Where MediatR/CQRS is used, requests can follow:

```text
HTTP Request
     ↓
Controller
     ↓
Command / Query
     ↓
Handler
     ↓
Business Operation
     ↓
Database
```

---

## 6. Data Access Layer

Entity Framework Core provides database access between the application and PostgreSQL.

```text
Application
     ↓
Entity Framework Core
     ↓
PostgreSQL
```

Responsibilities include:

* Entity mapping.
* Queries.
* Inserts.
* Updates.
* Deletes.
* Transactions.
* Database migrations.

---

## 7. Database Layer

PostgreSQL stores persistent application data.

Possible major data areas include:

```text
Users
Farmers
Buyers
Experts
Logistics Providers
Products
Orders
Notifications
Reviews
Transactions
```

The exact entities depend on the implemented application features.

---

## 8. Authentication Flow

A simplified authentication flow is:

```text
User
  │
  │ Login
  ▼
Angular Frontend
  │
  │ Credentials
  ▼
ASP.NET Core API
  │
  │ Validate credentials
  ▼
Authentication Service
  │
  │ Generate token
  ▼
Angular Frontend
  │
  │ Store/use access token
  ▼
Protected API Requests
```

---

## 9. Marketplace Flow

```text
Farmer
  │
  │ Create Product
  ▼
Angular Frontend
  │
  │ API Request
  ▼
ASP.NET Core API
  │
  │ Validate
  ▼
PostgreSQL
  │
  │ Product Stored
  ▼
Marketplace
  │
  │ Search / Browse
  ▼
Buyer
```

---

## 10. External Service Integration

External services may be integrated through the backend.

```text
                    ┌──────────────┐
                    │ Angular      │
                    │ Frontend     │
                    └──────┬───────┘
                           │
                           ▼
                    ┌──────────────┐
                    │ ASP.NET Core │
                    │ API          │
                    └──────┬───────┘
                           │
             ┌─────────────┼─────────────┐
             ▼             ▼             ▼
        ┌─────────┐   ┌──────────┐  ┌────────────┐
        │PostgreSQL│   │ AI       │  │Notification│
        │Database │   │ Service  │  │ Service    │
        └─────────┘   └──────────┘  └────────────┘
```

The backend should control access to sensitive external services rather than exposing secret credentials to the frontend.

---

## 11. Security Boundary

The major security boundary is between the public client and protected backend services.

```text
PUBLIC
────────────────────────────────────

Angular Browser Client
        │
        │ HTTPS
        ▼

PROTECTED
────────────────────────────────────

ASP.NET Core API
        │
        ├── Authentication
        ├── Authorization
        ├── Validation
        └── Business Logic
                │
                ▼

        PostgreSQL Database
```

Sensitive credentials should never be embedded in the Angular frontend.

---

## 12. Deployment Architecture

A simplified production architecture is:

```text
                         INTERNET
                            │
                            │ HTTPS
                            ▼
                 ┌────────────────────┐
                 │ Production Frontend│
                 │ Angular Application│
                 └──────────┬─────────┘
                            │
                            │ HTTPS / REST
                            ▼
                 ┌────────────────────┐
                 │ Production API     │
                 │ ASP.NET Core       │
                 └──────────┬─────────┘
                            │
                            │ Database Connection
                            ▼
                 ┌────────────────────┐
                 │ PostgreSQL         │
                 │ Production DB      │
                 └────────────────────┘
```

---

## 13. Architecture Principles

The architecture should follow these principles:

* Separation of concerns.
* Secure communication.
* Clear API boundaries.
* Centralized business logic.
* Controlled database access.
* Input validation.
* Authentication and authorization.
* Maintainability.
* Scalability.
* Testability.

---

## 14. Related Documentation

Related documents include:

```text
04-system-design/
05-api/
07-security/
08-testing/
10-developer-guide/
11-deployment/
```

---

## 15. Conclusion

The AgriConnect Ethiopia architecture separates the user interface, API, application logic, data access, database, and external integrations.

This separation makes the platform easier to develop, test, secure, deploy, and maintain while allowing individual components to evolve as the project grows.
