# API Testing

## 1. Introduction

This document describes the API testing strategy for the AgriConnect Ethiopia platform.

The purpose of API testing is to verify that the backend services:

* Accept valid requests.
* Reject invalid requests.
* Return correct HTTP status codes.
* Return the expected response structure.
* Validate input correctly.
* Enforce authentication and authorization.
* Persist and retrieve data correctly.
* Handle errors consistently.
* Protect sensitive information.

API testing covers the major backend resources and services used by farmers, buyers, agricultural experts, logistics providers, administrators, marketplace services, AI services, and notifications.

---

## 2. API Testing Tools

The following tools may be used for API testing:

| Tool                   | Purpose                                          |
| ---------------------- | ------------------------------------------------ |
| Scalar                 | Interactive API documentation and manual testing |
| curl                   | Command-line API testing                         |
| Postman                | Request collections and API testing              |
| Swagger/OpenAPI        | API contract documentation                       |
| .NET Integration Tests | Automated backend testing                        |
| PostgreSQL             | Database verification                            |

The primary development API can be tested through the local backend environment.

Example:

```text
http://localhost:5071
```

---

# 3. API Testing Levels

## 3.1 Endpoint Testing

Individual API endpoints are tested to verify:

* Request validation.
* Response data.
* HTTP status codes.
* Authentication requirements.
* Authorization rules.

## 3.2 Integration Testing

Integration tests verify communication between:

* API controllers.
* Application services.
* Database.
* Authentication services.
* Notification services.
* AI services.
* External integrations.

## 3.3 Error Testing

The API is tested using invalid requests, missing data, unauthorized requests, and unexpected conditions.

## 3.4 Security Testing

Security tests verify that protected endpoints cannot be accessed by unauthorized users.

---

# 4. HTTP Status Codes

The API should use appropriate HTTP status codes.

| Status Code | Meaning               | Example                                           |
| ----------- | --------------------- | ------------------------------------------------- |
| 200         | OK                    | Successful GET request                            |
| 201         | Created               | New resource created                              |
| 204         | No Content            | Successful deletion/update without response body  |
| 400         | Bad Request           | Invalid request data                              |
| 401         | Unauthorized          | Authentication required                           |
| 403         | Forbidden             | Insufficient permissions                          |
| 404         | Not Found             | Resource does not exist                           |
| 409         | Conflict              | Duplicate or conflicting resource                 |
| 422         | Unprocessable Entity  | Validation/business-rule failure where applicable |
| 429         | Too Many Requests     | Rate limit exceeded                               |
| 500         | Internal Server Error | Unexpected server error                           |

---

# 5. Authentication API Testing

## 5.1 Valid Login

**Test:** Send valid user credentials.

**Expected:**

* Authentication succeeds.
* Appropriate response is returned.
* Access token/session information is provided when applicable.

---

## 5.2 Invalid Credentials

**Test:** Send an incorrect password.

**Expected:**

* Authentication fails.
* Appropriate error response is returned.
* Sensitive authentication information is not exposed.

---

## 5.3 Missing Authentication

**Test:** Call a protected endpoint without authentication.

**Expected:**

```text
401 Unauthorized
```

---

# 6. Authorization API Testing

## 6.1 Farmer Access

Verify that farmer-specific endpoints are accessible to authenticated farmers.

## 6.2 Buyer Access

Verify that buyer-specific endpoints are accessible to authenticated buyers.

## 6.3 Expert Access

Verify that expert-only functionality is restricted to authorized experts.

## 6.4 Logistics Access

Verify that logistics functionality is restricted to authorized logistics providers.

## 6.5 Administrator Access

Verify that administrative endpoints are restricted to administrators.

## 6.6 Role Violation

A user must not access functionality belonging to another restricted role.

Expected response:

```text
403 Forbidden
```

---

# 7. Request Validation Testing

API endpoints should reject invalid request data.

Test the following:

* Missing required fields.
* Invalid data types.
* Invalid email addresses.
* Invalid phone numbers.
* Negative quantities.
* Invalid prices.
* Invalid identifiers.
* Invalid dates.
* Empty strings.
* Excessively long input.

Example:

```json
{
  "name": "",
  "quantity": -10,
  "price": -100
}
```

**Expected Result:**

The API rejects the request and returns meaningful validation errors.

---

# 8. Marketplace API Testing

## 8.1 Get Products

**Method:**

```text
GET
```

**Purpose:**

Retrieve available agricultural products.

**Expected:**

* Successful response.
* Correct product collection.
* Correct pagination information where supported.

---

## 8.2 Get Product by ID

**Method:**

```text
GET
```

**Purpose:**

Retrieve details for a specific product.

**Expected:**

* Existing product returns successfully.
* Non-existing product returns `404 Not Found`.

---

## 8.3 Create Product

**Method:**

```text
POST
```

**Purpose:**

Create a new marketplace product.

**Expected:**

* Valid request returns `201 Created`.
* Product is persisted.
* Response contains the created resource or appropriate identifier.

---

## 8.4 Update Product

**Method:**

```text
PUT/PATCH
```

**Purpose:**

Update an existing product.

**Expected:**

* Authorized owner can update the product.
* Unauthorized users cannot modify it.
* Updated information is persisted.

---

## 8.5 Delete Product

**Method:**

```text
DELETE
```

**Purpose:**

Remove or deactivate a product.

**Expected:**

* Authorized user can remove their own product.
* Unauthorized users are rejected.

---

# 9. Order API Testing

Order APIs should be tested for:

* Creating orders.
* Retrieving orders.
* Updating order status.
* Cancelling orders.
* Invalid product IDs.
* Invalid quantities.
* Insufficient product quantity.
* Unauthorized order access.

Example invalid order:

```json
{
  "productId": 999999,
  "quantity": 100000
}
```

**Expected Result:**

The API rejects the request and returns an appropriate error.

---

# 10. Expert Service API Testing

Test the expert service endpoints for:

* Creating consultation requests.
* Viewing consultation requests.
* Assigning experts.
* Providing expert advice.
* Updating consultation status.

Verify that farmers can access their own requests and experts can access requests assigned to them.

---

# 11. Logistics API Testing

Test logistics endpoints for:

* Creating delivery requests.
* Viewing delivery requests.
* Accepting deliveries.
* Updating delivery status.
* Completing deliveries.

Verify that users cannot modify delivery records they are not authorized to manage.

---

# 12. AI API Testing

AI-related endpoints should be tested using:

### Valid Request

Provide complete and valid agricultural information.

**Expected:**

* Request is accepted.
* AI service returns a valid response.

### Invalid Request

Send incomplete or invalid information.

**Expected:**

* Validation error is returned.
* Invalid request is not processed.

### AI Service Failure

Simulate an unavailable AI service.

**Expected:**

* API handles the failure gracefully.
* Internal service details are not exposed.
* User receives an appropriate error response.

---

# 13. Notification API Testing

Test notification functionality for:

* Creating notifications.
* Retrieving notifications.
* Marking notifications as read.
* Delivery status updates.
* Order notifications.
* Expert consultation notifications.

Verify that users can only access their own notifications.

---

# 14. Pagination Testing

For endpoints supporting pagination, test:

```text
?page=1&pageSize=10
```

Verify:

* Correct page is returned.
* Correct number of records is returned.
* Invalid page numbers are handled.
* Excessive page sizes are limited.
* Pagination metadata is correct.

---

# 15. Filtering and Searching

Test supported filtering and searching parameters.

Examples:

```text
?search=maize
```

```text
?category=vegetables
```

```text
?location=Addis%20Ababa
```

Verify that results match the requested filters.

---

# 16. Rate Limiting

Rate-limited endpoints should be tested by sending multiple requests within a short period.

**Expected:**

After the configured limit is reached:

```text
429 Too Many Requests
```

The API should recover after the applicable rate-limit period.

---

# 17. Error Response Testing

API errors should have a consistent structure.

Example:

```json
{
  "status": 400,
  "message": "Invalid request.",
  "errors": []
}
```

The exact structure should follow the implemented AgriConnect API error contract.

Error responses must not expose:

* Database connection strings.
* Passwords.
* Access tokens.
* Internal stack traces.
* Private configuration.
* Sensitive server information.

---

# 18. Database Integration Testing

After an API creates or modifies a resource, verify that the database reflects the operation.

Example flow:

```text
API Request
    ↓
Controller
    ↓
Application Service
    ↓
Database
    ↓
API Response
```

Verify:

1. Request is received.
2. Business rules are applied.
3. Database operation succeeds.
4. Correct response is returned.
5. Persisted data can be retrieved.

---

# 19. API Test Matrix

| Area           | Test                   | Expected Result  |
| -------------- | ---------------------- | ---------------- |
| Authentication | Valid login            | Success          |
| Authentication | Invalid login          | Rejected         |
| Authorization  | Unauthorized access    | 401              |
| Authorization  | Wrong role             | 403              |
| Products       | Get products           | 200              |
| Products       | Create product         | 201              |
| Products       | Missing product        | 404              |
| Orders         | Valid order            | Success          |
| Orders         | Invalid quantity       | Rejected         |
| Expert         | Consultation request   | Success          |
| Logistics      | Delivery request       | Success          |
| AI             | Valid request          | Success          |
| AI             | Invalid request        | Validation error |
| Notifications  | Retrieve notifications | Success          |
| Rate Limiting  | Excessive requests     | 429              |
| Server Errors  | Unexpected failure     | 500              |

---

# 20. Manual API Testing with Scalar

The API can be manually tested using the Scalar documentation interface.

Start the backend application and open the configured Scalar URL.

Example:

```text
http://localhost:5071/scalar
```

If the application exposes a versioned OpenAPI document, use the corresponding configured Scalar endpoint.

For each endpoint:

1. Open the endpoint in Scalar.
2. Review the request parameters.
3. Enter valid test data.
4. Execute the request.
5. Verify the HTTP status code.
6. Verify the response body.
7. Verify database changes where applicable.
8. Record the test result.

---

# 21. curl Testing

Example GET request:

```bash
curl -i http://localhost:5071/api/v1/products
```

Example POST request:

```bash
curl -i -X POST http://localhost:5071/api/v1/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Maize",
    "quantity": 100,
    "price": 50
  }'
```

The actual endpoint paths and request properties must match the implemented AgriConnect API.

---

# 22. API Testing Checklist

* [ ] All documented endpoints are tested.
* [ ] Valid requests are successful.
* [ ] Invalid requests are rejected.
* [ ] Required fields are validated.
* [ ] Authentication is tested.
* [ ] Authorization is tested.
* [ ] HTTP status codes are correct.
* [ ] Response schemas are correct.
* [ ] Error responses are consistent.
* [ ] Database persistence is verified.
* [ ] Pagination is tested.
* [ ] Search and filtering are tested.
* [ ] Rate limiting is tested.
* [ ] Sensitive information is protected.
* [ ] API documentation matches implementation.
* [ ] Failed tests are documented.

---

# 23. Test Evidence

API test evidence should be stored in:

```text
13-visual-documentation/screenshots/
```

Recommended evidence includes:

* Successful API requests.
* Failed validation requests.
* Authentication responses.
* Authorization failures.
* Product creation.
* Order creation.
* API error responses.
* Scalar API testing.
* Database verification.

Test results should be recorded in:

```text
08-testing/test-results.md
```

---

# 24. Conclusion

API testing ensures that the AgriConnect backend is reliable, secure, consistent, and compliant with the defined API contract.

Testing should be performed throughout development rather than only before final submission. Critical failures should be resolved before the corresponding feature is considered complete.
