# AgriConnect Ethiopia API Endpoints

## 1. Overview

The AgriConnect Ethiopia API provides RESTful endpoints that allow the web application and other authorized clients to communicate with the backend system.

The API supports the major platform capabilities, including:

* User authentication and account management
* Farmer management
* Buyer management
* Agricultural expert services
* Logistics management
* Product and marketplace management
* Orders and transactions
* AI-powered agricultural assistance
* Notifications
* Administrative operations

The API follows REST principles and uses JSON for request and response payloads.

---

## 2. Base URL

### Development

```text
http://localhost:5000/api/v1
```

> The actual development URL depends on the configured backend port.

### Production

```text
https://api.agriconnect.et/api/v1
```

> The production URL will be updated when the production deployment is finalized.

---

## 3. API Versioning

AgriConnect uses API versioning to maintain backward compatibility.

The current API version is:

```text
/api/v1
```

Future versions may use:

```text
/api/v2
```

Versioning prevents breaking changes from affecting existing clients.

---

# 4. Authentication Endpoints

## 4.1 Register User

```http
POST /api/v1/auth/register
```

Creates a new AgriConnect user account.

### Request

```json
{
  "firstName": "Abebe",
  "lastName": "Kebede",
  "email": "abebe@example.com",
  "password": "SecurePassword123!",
  "role": "Farmer"
}
```

### Response

```http
201 Created
```

```json
{
  "message": "User registered successfully."
}
```

---

## 4.2 Login

```http
POST /api/v1/auth/login
```

Authenticates a user and returns an access token.

### Request

```json
{
  "email": "abebe@example.com",
  "password": "SecurePassword123!"
}
```

### Response

```http
200 OK
```

```json
{
  "accessToken": "JWT_TOKEN",
  "expiresIn": 3600,
  "user": {
    "id": 1,
    "name": "Abebe Kebede",
    "role": "Farmer"
  }
}
```

---

# 5. Farmer Endpoints

## 5.1 Get Farmer Profile

```http
GET /api/v1/farmers/{id}
```

Returns information about a farmer.

### Authorization

```text
Bearer Token
```

### Example

```http
GET /api/v1/farmers/1
```

---

## 5.2 Update Farmer Profile

```http
PUT /api/v1/farmers/{id}
```

Updates farmer profile information.

### Example Request

```json
{
  "firstName": "Abebe",
  "lastName": "Kebede",
  "phoneNumber": "+251900000000",
  "location": "Oromia"
}
```

---

## 5.3 Get Farmer Products

```http
GET /api/v1/farmers/{id}/products
```

Returns products listed by a farmer.

---

# 6. Buyer Endpoints

## 6.1 Get Buyer Profile

```http
GET /api/v1/buyers/{id}
```

Returns buyer profile information.

---

## 6.2 Search Products

```http
GET /api/v1/products
```

### Supported Query Parameters

```text
category
location
minPrice
maxPrice
search
page
pageSize
```

### Example

```http
GET /api/v1/products?category=Vegetables&location=Addis%20Ababa&page=1&pageSize=20
```

---

## 6.3 Get Product Details

```http
GET /api/v1/products/{id}
```

Returns detailed information about a marketplace product.

---

# 7. Marketplace Endpoints

## 7.1 Create Product Listing

```http
POST /api/v1/products
```

Allows an authorized farmer or seller to create a marketplace listing.

### Request

```json
{
  "name": "Tomato",
  "category": "Vegetables",
  "quantity": 500,
  "unit": "kg",
  "price": 85,
  "location": "Bishoftu",
  "description": "Fresh locally produced tomatoes."
}
```

---

## 7.2 Update Product

```http
PUT /api/v1/products/{id}
```

Updates an existing product listing.

---

## 7.3 Delete Product

```http
DELETE /api/v1/products/{id}
```

Removes a product listing.

---

# 8. Order Endpoints

## 8.1 Create Order

```http
POST /api/v1/orders
```

Creates an order for one or more marketplace products.

### Request

```json
{
  "productId": 10,
  "quantity": 50,
  "deliveryAddress": "Addis Ababa"
}
```

---

## 8.2 Get Orders

```http
GET /api/v1/orders
```

Returns orders belonging to the authenticated user.

---

## 8.3 Get Order Details

```http
GET /api/v1/orders/{id}
```

Returns detailed information about an order.

---

## 8.4 Update Order Status

```http
PATCH /api/v1/orders/{id}/status
```

Updates the status of an order.

Possible statuses include:

```text
Pending
Confirmed
Processing
Shipped
Delivered
Cancelled
```

---

# 9. Agricultural Expert Endpoints

## 9.1 Get Experts

```http
GET /api/v1/experts
```

Returns available agricultural experts.

---

## 9.2 Get Expert Profile

```http
GET /api/v1/experts/{id}
```

Returns an expert's profile and specialization.

---

## 9.3 Request Expert Consultation

```http
POST /api/v1/consultations
```

Creates a consultation request.

### Request

```json
{
  "expertId": 5,
  "subject": "Tomato disease",
  "description": "Tomato leaves are developing brown spots."
}
```

---

## 9.4 Get Consultations

```http
GET /api/v1/consultations
```

Returns consultation requests associated with the authenticated user.

---

# 10. Logistics Endpoints

## 10.1 Get Logistics Providers

```http
GET /api/v1/logistics/providers
```

Returns available logistics providers.

---

## 10.2 Request Delivery

```http
POST /api/v1/logistics/deliveries
```

Creates a delivery request.

### Request

```json
{
  "orderId": 100,
  "pickupLocation": "Bishoftu",
  "deliveryLocation": "Addis Ababa",
  "requestedDate": "2026-08-10"
}
```

---

## 10.3 Track Delivery

```http
GET /api/v1/logistics/deliveries/{id}
```

Returns the current delivery status.

---

# 11. AI Endpoints

## 11.1 Agricultural AI Assistant

```http
POST /api/v1/ai/advice
```

Allows farmers to request AI-powered agricultural recommendations.

### Request

```json
{
  "question": "What can cause yellow leaves on tomato plants?"
}
```

### Response

```json
{
  "answer": "Yellow tomato leaves can be caused by nutrient deficiencies, overwatering, diseases, or insufficient sunlight.",
  "recommendations": [
    "Check soil moisture",
    "Inspect the leaves for disease symptoms",
    "Review soil nutrient levels"
  ]
}
```

---

## 11.2 Crop Disease Analysis

```http
POST /api/v1/ai/disease-analysis
```

Analyzes crop information or an uploaded image to assist with disease identification.

---

# 12. Notification Endpoints

## 12.1 Get Notifications

```http
GET /api/v1/notifications
```

Returns notifications for the authenticated user.

---

## 12.2 Mark Notification as Read

```http
PATCH /api/v1/notifications/{id}/read
```

Marks a notification as read.

---

# 13. Administrative Endpoints

Administrative endpoints require an administrator role.

## 13.1 Get Users

```http
GET /api/v1/admin/users
```

Returns registered platform users.

---

## 13.2 Update User Status

```http
PATCH /api/v1/admin/users/{id}/status
```

Activates or deactivates a user account.

---

## 13.3 Get Platform Statistics

```http
GET /api/v1/admin/statistics
```

Returns platform-level statistics.

Example:

```json
{
  "totalUsers": 1250,
  "totalFarmers": 720,
  "totalBuyers": 410,
  "totalExperts": 65,
  "totalProducts": 1840,
  "totalOrders": 950
}
```

---

# 14. HTTP Status Codes

AgriConnect uses standard HTTP status codes.

| Status | Meaning                                  |
| ------ | ---------------------------------------- |
| `200`  | Request completed successfully           |
| `201`  | Resource created successfully            |
| `204`  | Request successful with no response body |
| `400`  | Invalid request                          |
| `401`  | Authentication required                  |
| `403`  | Access denied                            |
| `404`  | Resource not found                       |
| `409`  | Resource conflict                        |
| `422`  | Validation error                         |
| `429`  | Too many requests                        |
| `500`  | Internal server error                    |

---

# 15. Pagination

Endpoints returning collections should support pagination.

Example:

```http
GET /api/v1/products?page=1&pageSize=20
```

Example response:

```json
{
  "items": [],
  "page": 1,
  "pageSize": 20,
  "totalItems": 150,
  "totalPages": 8
}
```

---

# 16. Filtering and Searching

Collection endpoints may support filtering and searching.

Example:

```http
GET /api/v1/products?search=tomato&category=Vegetables
```

Filtering helps users quickly find relevant agricultural products and services.

---

# 17. API Security

Protected endpoints require an access token.

Example:

```http
Authorization: Bearer JWT_TOKEN
```

Role-based authorization is applied to sensitive operations.

Examples:

```text
Farmer       → Manage own products
Buyer        → Create and manage own orders
Expert       → Manage consultations
Logistics    → Manage assigned deliveries
Admin        → Manage platform resources
```

---

# 18. API Design Principles

The AgriConnect API follows these principles:

1. RESTful resource-oriented endpoints
2. JSON request and response formats
3. API versioning
4. Authentication and authorization
5. Input validation
6. Consistent error responses
7. Pagination for large collections
8. Secure data handling
9. Appropriate HTTP status codes
10. Separation of concerns

---

# 19. API Documentation Tools

During development, the API can be tested and documented using API documentation tools such as:

* Swagger/OpenAPI
* Scalar
* Postman
* curl

The API documentation should be kept synchronized with the actual backend implementation.

---

## 20. Implementation Status

| API Area          | Status                   |
| ----------------- | ------------------------ |
| Authentication    | Planned / In Development |
| Farmer Management | Planned / In Development |
| Buyer Management  | Planned / In Development |
| Expert Services   | Planned / In Development |
| Logistics         | Planned / In Development |
| Marketplace       | Planned / In Development |
| Orders            | Planned / In Development |
| AI Services       | Planned / In Development |
| Notifications     | Planned / In Development |
| Administration    | Planned / In Development |

> The implementation status must be updated as the AgriConnect backend is completed.

---

## 21. Conclusion

The AgriConnect API provides the communication layer between the frontend application, backend services, database, AI services, and external integrations.

A consistent RESTful API design allows the platform to remain maintainable, scalable, secure, and easy to integrate with future applications such as mobile clients and third-party agricultural services.
