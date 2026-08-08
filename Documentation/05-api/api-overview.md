# AgriConnect Ethiopia — API Overview

## 1. Document Information

| Item              | Details              |
| ----------------- | -------------------- |
| Project           | AgriConnect Ethiopia |
| Document          | API Overview         |
| Version           | 1.0                  |
| API Style         | RESTful API          |
| Data Format       | JSON                 |
| Primary Backend   | ASP.NET Core         |
| Database          | PostgreSQL           |
| API Documentation | OpenAPI              |
| Status            | Development          |

---

# 2. Introduction

The AgriConnect Ethiopia API provides a secure and structured communication layer between the platform's frontend applications and backend services.

The API enables farmers, buyers, agricultural experts, logistics providers, and administrators to access platform functionality through standardized HTTP endpoints.

The API follows RESTful principles and uses JSON for request and response payloads.

---

# 3. API Architecture

The general communication flow is:

```text
┌─────────────────────────┐
│      Client Layer       │
│                         │
│ Angular Web Application │
│ Mobile Application      │
│ Admin Dashboard         │
└────────────┬────────────┘
             │
             │ HTTPS / JSON
             ▼
┌─────────────────────────┐
│       API Layer         │
│                         │
│ ASP.NET Core Web API    │
│ Controllers             │
│ Authentication          │
│ Authorization            │
│ Validation              │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│   Application Layer     │
│                         │
│ Business Logic          │
│ Services                │
│ CQRS / Commands         │
│ Queries                 │
│ Validation              │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│    Infrastructure       │
│                         │
│ Entity Framework Core   │
│ PostgreSQL              │
│ External Services       │
│ AI Services             │
└─────────────────────────┘
```

---

# 4. API Base URL

During local development, the API is hosted on the local development server.

Example:

```text
http://localhost:5071
```

The exact port may vary depending on the local ASP.NET Core configuration.

For production, the API should be served through HTTPS.

Example:

```text
https://api.agriconnect.example
```

The production domain should be configured through deployment infrastructure rather than hard-coded into the application.

---

# 5. API Versioning

AgriConnect uses API versioning to allow the backend to evolve without unnecessarily breaking existing clients.

Example versioned endpoints:

```text
/api/v1/...
/api/v2/...
```

For example:

```text
GET /api/v1/products
GET /api/v2/products
```

Versioning allows future API changes while maintaining compatibility with existing applications.

---

# 6. HTTP Methods

The API uses standard HTTP methods.

| Method | Purpose                             |
| ------ | ----------------------------------- |
| GET    | Retrieve resources                  |
| POST   | Create resources or execute actions |
| PUT    | Replace an existing resource        |
| PATCH  | Partially update a resource         |
| DELETE | Remove or deactivate a resource     |

Examples:

```text
GET    /api/v1/products
GET    /api/v1/products/{id}

POST   /api/v1/products

PUT    /api/v1/products/{id}

PATCH  /api/v1/products/{id}

DELETE /api/v1/products/{id}
```

---

# 7. Response Format

Successful API responses generally use JSON.

Example:

```json
{
  "id": "7f7e9b1e-7c2f-4e9f-9e42-5f0a8f3f6b10",
  "name": "Coffee",
  "category": "Coffee",
  "quantity": 500,
  "unit": "kg",
  "pricePerUnit": 450.00,
  "region": "Oromia",
  "status": "Active"
}
```

---

# 8. Standard HTTP Status Codes

The API uses standard HTTP status codes.

| Status | Meaning                                  |
| -----: | ---------------------------------------- |
|    200 | Request successful                       |
|    201 | Resource successfully created            |
|    202 | Request accepted for processing          |
|    204 | Successful request with no response body |
|    400 | Invalid request                          |
|    401 | Authentication required                  |
|    403 | Access denied                            |
|    404 | Resource not found                       |
|    409 | Resource conflict                        |
|    422 | Validation failure                       |
|    429 | Too many requests                        |
|    500 | Internal server error                    |
|    503 | Service unavailable                      |

---

# 9. Authentication

Protected API endpoints require authentication.

The expected authentication mechanism is token-based authentication.

Example:

```http
Authorization: Bearer <access-token>
```

Authentication responsibilities include:

* User identity verification
* Access-token validation
* Token expiration handling
* Secure credential handling
* Refresh-token management where implemented

Detailed authentication information is documented in:

```text
05-api/authentication.md
```

---

# 10. Authorization

Authentication determines **who the user is**.

Authorization determines **what the user is allowed to do**.

AgriConnect uses role-based access control.

Primary roles include:

```text
Farmer
Buyer
AgriculturalExpert
LogisticsProvider
Administrator
```

Example:

```text
Farmer
  ├── Manage own farms
  ├── Create products
  ├── Manage own listings
  └── Request expert consultations

Buyer
  ├── Browse products
  ├── Create orders
  ├── Make payments
  └── Submit reviews

Agricultural Expert
  ├── Manage expert profile
  ├── View consultations
  └── Provide agricultural advice

Logistics Provider
  ├── View assigned deliveries
  ├── Update delivery status
  └── Manage delivery information

Administrator
  ├── Manage users
  ├── Manage products
  ├── Manage categories
  ├── Monitor transactions
  └── Manage platform configuration
```

---

# 11. Core API Resource Groups

The API is organized around major business resources.

## 11.1 Authentication

```text
/api/v1/auth
```

Typical operations:

```text
POST /api/v1/auth/register
POST /api/v1/auth/login
POST /api/v1/auth/refresh
POST /api/v1/auth/logout
```

---

## 11.2 Users

```text
/api/v1/users
```

Example operations:

```text
GET    /api/v1/users/me
PUT    /api/v1/users/me
PATCH  /api/v1/users/me
```

---

## 11.3 Farmers

```text
/api/v1/farmers
```

Example operations:

```text
GET  /api/v1/farmers/me
PUT  /api/v1/farmers/me
GET  /api/v1/farmers/{id}
```

---

## 11.4 Farms

```text
/api/v1/farms
```

Example operations:

```text
GET    /api/v1/farms
POST   /api/v1/farms
GET    /api/v1/farms/{id}
PUT    /api/v1/farms/{id}
DELETE /api/v1/farms/{id}
```

---

## 11.5 Products

```text
/api/v1/products
```

Example operations:

```text
GET    /api/v1/products
GET    /api/v1/products/{id}
POST   /api/v1/products
PUT    /api/v1/products/{id}
PATCH  /api/v1/products/{id}
DELETE /api/v1/products/{id}
```

Products can be filtered by:

* Category
* Region
* Price
* Availability
* Farmer
* Quality grade

Example:

```text
GET /api/v1/products?category=coffee&region=Oromia
```

---

# 12. Product Categories

```text
/api/v1/categories
```

Example:

```text
GET  /api/v1/categories
GET  /api/v1/categories/{id}
POST /api/v1/categories
PUT  /api/v1/categories/{id}
```

Category management is primarily restricted to administrators.

---

# 13. Orders

```text
/api/v1/orders
```

Example operations:

```text
GET  /api/v1/orders
POST /api/v1/orders
GET  /api/v1/orders/{id}
PATCH /api/v1/orders/{id}/status
POST /api/v1/orders/{id}/cancel
```

Only authorized users should access their relevant orders.

---

# 14. Payments

```text
/api/v1/payments
```

Example:

```text
POST /api/v1/payments
GET  /api/v1/payments/{id}
GET  /api/v1/orders/{orderId}/payment
```

Payment processing may integrate with external Ethiopian payment providers.

Sensitive payment information must never be unnecessarily exposed through API responses.

---

# 15. Deliveries

```text
/api/v1/deliveries
```

Example operations:

```text
GET   /api/v1/deliveries
GET   /api/v1/deliveries/{id}
PATCH /api/v1/deliveries/{id}/status
```

Logistics providers can update delivery information according to their authorization level.

---

# 16. Expert Consultations

```text
/api/v1/consultations
```

Example:

```text
GET  /api/v1/consultations
POST /api/v1/consultations
GET  /api/v1/consultations/{id}
POST /api/v1/consultations/{id}/response
PATCH /api/v1/consultations/{id}/status
```

Farmers can submit agricultural questions and experts can provide responses.

---

# 17. Reviews

```text
/api/v1/reviews
```

Example:

```text
GET  /api/v1/products/{productId}/reviews
POST /api/v1/products/{productId}/reviews
PUT  /api/v1/reviews/{id}
DELETE /api/v1/reviews/{id}
```

Reviews should only be submitted by eligible buyers according to business rules.

---

# 18. Notifications

```text
/api/v1/notifications
```

Example:

```text
GET   /api/v1/notifications
PATCH /api/v1/notifications/{id}/read
PATCH /api/v1/notifications/read-all
```

Notifications may be generated for:

* New orders
* Payment updates
* Delivery updates
* Consultation responses
* Product approvals
* Security events
* System announcements

---

# 19. AI Services

The AI layer may provide agricultural decision-support services.

Possible endpoints include:

```text
/api/v1/ai
```

Examples:

```text
POST /api/v1/ai/crop-recommendation
POST /api/v1/ai/disease-detection
POST /api/v1/ai/farming-advice
GET  /api/v1/ai/recommendations
```

AI-generated information should be treated as decision support rather than an absolute replacement for professional agricultural advice.

---

# 20. Marketplace Search

The marketplace API should support efficient product discovery.

Example:

```text
GET /api/v1/products/search?q=coffee
```

Possible filters:

```text
category
region
minPrice
maxPrice
qualityGrade
availability
farmerId
```

Example:

```text
GET /api/v1/products?category=coffee&region=Oromia&minPrice=300&maxPrice=600
```

---

# 21. Pagination

Large collections should support pagination.

Example:

```text
GET /api/v1/products?page=1&pageSize=20
```

Example response:

```json
{
  "items": [],
  "page": 1,
  "pageSize": 20,
  "totalItems": 120,
  "totalPages": 6
}
```

Pagination prevents unnecessarily large API responses.

---

# 22. Sorting

Collection endpoints may support sorting.

Example:

```text
GET /api/v1/products?sortBy=price&sortDirection=asc
```

Possible sorting fields:

```text
price
createdAt
name
rating
quantity
```

Only approved sorting fields should be accepted by the API.

---

# 23. Filtering

Filtering allows clients to retrieve specific resources.

Example:

```text
GET /api/v1/products?region=Oromia
```

Multiple filters can be combined:

```text
GET /api/v1/products?category=coffee&region=Oromia&status=Active
```

---

# 24. Validation

API requests should be validated before business operations are executed.

Validation should cover:

* Required fields
* Data types
* String lengths
* Numeric ranges
* Email formats
* Phone numbers
* IDs
* Dates
* Product quantities
* Prices
* Order quantities

Invalid requests should return an appropriate validation response.

Example:

```json
{
  "title": "Validation failed",
  "status": 422,
  "errors": {
    "quantity": [
      "Quantity must be greater than zero."
    ]
  }
}
```

---

# 25. Idempotency

Critical operations such as payments and order creation should support idempotency where necessary.

Example:

```http
Idempotency-Key: 9d6c3d55-9b38-4f1a-bf36-3b7c1c2e4a11
```

If the same request is accidentally submitted multiple times, the server should avoid creating duplicate business transactions.

---

# 26. Rate Limiting

The API should apply rate limiting to protect against excessive requests and abuse.

Rate limiting may be applied to:

* Authentication endpoints
* Public search endpoints
* AI endpoints
* Payment endpoints
* Administrative endpoints

Example response when the limit is exceeded:

```http
HTTP/1.1 429 Too Many Requests
```

---

# 27. Error Handling

The API should return consistent error responses.

Example:

```json
{
  "title": "Resource not found",
  "status": 404,
  "detail": "The requested product was not found.",
  "instance": "/api/v1/products/123"
}
```

Detailed error-handling standards are documented in:

```text
05-api/error-handling.md
```

---

# 28. OpenAPI Documentation

AgriConnect should expose an OpenAPI specification for API discovery and testing.

The API documentation should allow developers to:

* View available endpoints
* View request parameters
* View request schemas
* View response schemas
* Test endpoints
* Understand authentication requirements
* Inspect API versions

During development, the project may provide an interactive API documentation interface such as Scalar or another OpenAPI-compatible tool.

---

# 29. API Security

The API should implement:

* HTTPS in production
* Authentication
* Role-based authorization
* Input validation
* Rate limiting
* Secure headers
* CORS configuration
* Secure token handling
* Protection against injection attacks
* Audit logging
* Secure error handling

Detailed security information is documented in:

```text
07-security/
```

---

# 30. CORS

Cross-Origin Resource Sharing should be configured to allow only approved frontend applications.

Development may allow:

```text
http://localhost:4200
```

Production should use the official frontend domain.

Wildcard CORS configuration should not be used for authenticated production APIs unless there is a specific security justification.

---

# 31. API Performance

The API should be designed for efficient operation.

Performance considerations include:

* Database indexing
* Pagination
* Efficient queries
* Async database operations
* Caching where appropriate
* Response compression
* Rate limiting
* Avoiding unnecessary database calls
* Appropriate connection pooling

---

# 32. API Observability

The backend should provide sufficient observability to identify problems.

Recommended logging information includes:

* Request method
* Endpoint
* Response status
* Execution duration
* Correlation ID
* User ID where appropriate
* Error information
* Important business events

Sensitive information such as passwords and payment credentials must not be logged.

---

# 33. API Versioning Strategy

When breaking changes are introduced, a new API version should be created.

Example:

```text
v1
v2
```

Non-breaking changes may include:

* Adding optional response fields
* Adding new endpoints
* Adding new optional query parameters

Breaking changes may require:

* New endpoint version
* New request structure
* Removed fields
* Changed response semantics

---

# 34. API Development Standards

Developers should follow these standards:

1. Use clear resource names.
2. Use plural nouns for collections.
3. Use HTTP methods correctly.
4. Validate all external input.
5. Return appropriate status codes.
6. Keep response structures consistent.
7. Protect sensitive information.
8. Document every public endpoint.
9. Use asynchronous operations where appropriate.
10. Write automated tests for critical endpoints.

---

# 35. Example API Workflow

A typical marketplace purchase workflow is:

```text
1. Buyer logs in
       ↓
2. API authenticates buyer
       ↓
3. Buyer searches products
       ↓
4. API returns available products
       ↓
5. Buyer selects product
       ↓
6. Buyer creates order
       ↓
7. API validates order
       ↓
8. Payment is initiated
       ↓
9. Payment is confirmed
       ↓
10. Delivery is created
       ↓
11. Logistics provider receives assignment
       ↓
12. Delivery status is updated
       ↓
13. Buyer receives notification
       ↓
14. Order is completed
```

---

# 36. API Documentation Structure

The API documentation is organized as follows:

```text
05-api/
├── api-overview.md
├── authentication.md
├── endpoints.md
├── error-handling.md
└── examples/
```

Each document has a specific purpose:

| Document          | Purpose                                |
| ----------------- | -------------------------------------- |
| api-overview.md   | General API architecture and standards |
| authentication.md | Authentication implementation          |
| endpoints.md      | Detailed endpoint reference            |
| error-handling.md | API error standards                    |
| examples/         | Request and response examples          |

---

# 37. Future API Improvements

Future versions may include:

* Real-time notifications
* WebSocket/SignalR integration
* Advanced marketplace search
* AI-powered agricultural services
* Real-time delivery tracking
* Payment-provider webhooks
* Advanced analytics APIs
* Agricultural market-price APIs
* Weather-data APIs
* IoT sensor APIs
* Mobile-specific API optimizations
* GraphQL for selected use cases

---

# 38. Document Status

| Version | Date       | Status | Description          |
| ------- | ---------- | ------ | -------------------- |
| 1.0     | 2026-08-08 | Draft  | Initial API overview |

---

## Conclusion

The AgriConnect Ethiopia API provides the communication foundation for the platform's web applications, mobile applications, administrative tools, AI services, and external integrations.

The API is designed around RESTful principles, secure authentication, role-based authorization, consistent validation, standardized errors, versioning, pagination, and scalable backend architecture.

As development progresses, this document should be updated to reflect the actual implemented endpoints and production infrastructure.
