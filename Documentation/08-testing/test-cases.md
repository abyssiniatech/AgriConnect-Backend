# Test Cases

## 1. Introduction

This document defines the test cases for the AgriConnect Ethiopia platform. The test cases verify that the system meets its functional requirements, business rules, security requirements, usability expectations, and technical specifications.

Testing covers the major platform users and modules, including:

* Farmers
* Buyers
* Agricultural Experts
* Logistics Providers
* Administrators
* Marketplace
* AI-based services
* Notifications
* Authentication and authorization
* Backend APIs
* Frontend application
* Database operations

---

## 2. Test Case Format

Each test case contains:

| Field           | Description                        |
| --------------- | ---------------------------------- |
| Test Case ID    | Unique identifier                  |
| Module          | System module being tested         |
| Test Scenario   | What is being tested               |
| Preconditions   | Conditions required before testing |
| Test Steps      | Actions performed                  |
| Expected Result | Expected system behavior           |
| Priority        | Test importance                    |
| Status          | Current execution status           |

---

# 3. Authentication Test Cases

## TC-AUTH-001: User Registration

**Module:** Authentication

**Scenario:** Verify that a new user can create an account.

**Preconditions:**

* User is not already registered.
* Registration page is accessible.

**Steps:**

1. Open the registration page.
2. Enter a valid name.
3. Enter a valid email address.
4. Enter a valid phone number.
5. Enter a valid password.
6. Select a valid user role.
7. Submit the registration form.

**Expected Result:**

* Account is created successfully.
* User receives confirmation.
* Password is securely stored.
* User can proceed to login.

**Priority:** High

**Status:** Not Executed

---

## TC-AUTH-002: User Login

**Module:** Authentication

**Scenario:** Verify login with valid credentials.

**Steps:**

1. Open the login page.
2. Enter a registered email.
3. Enter the correct password.
4. Submit the login form.

**Expected Result:**

* User is authenticated successfully.
* Access token/session is created.
* User is redirected to the appropriate dashboard.

**Priority:** Critical

**Status:** Not Executed

---

## TC-AUTH-003: Invalid Login

**Module:** Authentication

**Scenario:** Verify that invalid credentials are rejected.

**Steps:**

1. Open the login page.
2. Enter an incorrect email or password.
3. Submit the form.

**Expected Result:**

* Login is rejected.
* Appropriate error message is displayed.
* No authenticated session is created.

**Priority:** High

**Status:** Not Executed

---

## TC-AUTH-004: Logout

**Module:** Authentication

**Scenario:** Verify that an authenticated user can log out.

**Steps:**

1. Login successfully.
2. Select Logout.
3. Attempt to access a protected page.

**Expected Result:**

* User is logged out.
* Authentication credentials are cleared or invalidated.
* Protected resources cannot be accessed without logging in again.

**Priority:** High

**Status:** Not Executed

---

# 4. Farmer Test Cases

## TC-FARMER-001: Farmer Creates Product Listing

**Module:** Farmer Marketplace

**Scenario:** Verify that a farmer can create a product listing.

**Steps:**

1. Login as a farmer.
2. Open the product management page.
3. Select Add Product.
4. Enter product name.
5. Enter category.
6. Enter quantity.
7. Enter price.
8. Add location information.
9. Submit the listing.

**Expected Result:**

* Product is successfully created.
* Product appears in the farmer's listings.
* Valid buyers can view the product.

**Priority:** Critical

**Status:** Not Executed

---

## TC-FARMER-002: Farmer Updates Product

**Module:** Farmer Marketplace

**Scenario:** Verify that a farmer can update an existing product.

**Steps:**

1. Login as the product owner.
2. Open product listings.
3. Select an existing product.
4. Modify product information.
5. Save changes.

**Expected Result:**

* Product information is updated successfully.
* Updated information is displayed to authorized users.

**Priority:** High

**Status:** Not Executed

---

## TC-FARMER-003: Farmer Removes Product

**Module:** Farmer Marketplace

**Scenario:** Verify that a farmer can remove their own product.

**Steps:**

1. Login as the farmer.
2. Open product listings.
3. Select a product owned by the farmer.
4. Select Delete/Remove.
5. Confirm the operation.

**Expected Result:**

* Product is removed or marked inactive.
* Product is no longer available for new purchases.

**Priority:** High

**Status:** Not Executed

---

# 5. Buyer Test Cases

## TC-BUYER-001: Buyer Searches Products

**Module:** Marketplace

**Scenario:** Verify product search functionality.

**Steps:**

1. Login as a buyer.
2. Open marketplace.
3. Enter a product name in the search field.
4. Submit the search.

**Expected Result:**

* Matching products are displayed.
* Search results contain relevant information such as product name, price, quantity, and seller.

**Priority:** High

**Status:** Not Executed

---

## TC-BUYER-002: Buyer Places Order

**Module:** Marketplace

**Scenario:** Verify that a buyer can purchase an available product.

**Steps:**

1. Login as a buyer.
2. Search for an available product.
3. Open the product details.
4. Select quantity.
5. Place the order.
6. Confirm the order.

**Expected Result:**

* Order is created successfully.
* Product quantity is updated.
* Buyer receives order confirmation.
* Seller receives the appropriate notification.

**Priority:** Critical

**Status:** Not Executed

---

# 6. Agricultural Expert Test Cases

## TC-EXPERT-001: Expert Publishes Advice

**Module:** Agricultural Experts

**Scenario:** Verify that an authorized expert can publish agricultural advice.

**Steps:**

1. Login as an agricultural expert.
2. Open the expert dashboard.
3. Create an advice article.
4. Enter title and content.
5. Submit the article.

**Expected Result:**

* Advice is saved successfully.
* Published advice is visible to permitted users.

**Priority:** High

**Status:** Not Executed

---

## TC-EXPERT-002: Farmer Requests Expert Advice

**Module:** Expert Services

**Scenario:** Verify that a farmer can request agricultural assistance.

**Steps:**

1. Login as a farmer.
2. Open expert services.
3. Select an expert.
4. Describe the agricultural problem.
5. Submit the request.

**Expected Result:**

* Request is successfully created.
* Expert receives a notification.
* Request status is displayed to the farmer.

**Priority:** High

**Status:** Not Executed

---

# 7. Logistics Test Cases

## TC-LOGISTICS-001: Logistics Provider Views Delivery Request

**Module:** Logistics

**Scenario:** Verify that logistics providers can view available delivery requests.

**Steps:**

1. Login as a logistics provider.
2. Open delivery requests.
3. View available requests.

**Expected Result:**

* Authorized delivery requests are displayed.
* Relevant pickup, destination, product, and delivery information is available.

**Priority:** High

**Status:** Not Executed

---

## TC-LOGISTICS-002: Logistics Provider Accepts Delivery

**Module:** Logistics

**Scenario:** Verify that a logistics provider can accept a delivery request.

**Steps:**

1. Login as a logistics provider.
2. Open an available delivery request.
3. Select Accept.
4. Confirm the operation.

**Expected Result:**

* Delivery is assigned to the logistics provider.
* Delivery status is updated.
* Relevant users receive notifications.

**Priority:** High

**Status:** Not Executed

---

# 8. Marketplace Test Cases

## TC-MARKET-001: View Product Details

**Module:** Marketplace

**Scenario:** Verify product details are displayed correctly.

**Expected Result:**
The product page displays:

* Product name
* Description
* Category
* Price
* Available quantity
* Seller information
* Location
* Availability status

**Priority:** High

**Status:** Not Executed

---

## TC-MARKET-002: Product Availability

**Module:** Marketplace

**Scenario:** Verify that unavailable products cannot be ordered.

**Steps:**

1. Open a product with zero available quantity.
2. Attempt to place an order.

**Expected Result:**

* Ordering is prevented.
* User receives an appropriate availability message.

**Priority:** Critical

**Status:** Not Executed

---

# 9. AI Feature Test Cases

## TC-AI-001: AI Recommendation Request

**Module:** AI Services

**Scenario:** Verify that the AI service can process a valid recommendation request.

**Steps:**

1. Login as an authorized user.
2. Open the AI recommendation feature.
3. Provide valid agricultural information.
4. Submit the request.

**Expected Result:**

* Request is processed.
* A relevant recommendation is returned.
* Errors are handled gracefully if the AI service is unavailable.

**Priority:** Medium

**Status:** Not Executed

---

## TC-AI-002: Invalid AI Input

**Module:** AI Services

**Scenario:** Verify handling of incomplete AI requests.

**Expected Result:**

* Invalid input is rejected.
* User receives a clear validation message.
* No invalid request is processed.

**Priority:** Medium

**Status:** Not Executed

---

# 10. Notification Test Cases

## TC-NOTIFY-001: Order Notification

**Module:** Notifications

**Scenario:** Verify that users receive notifications after an order is created.

**Expected Result:**

* Buyer receives order confirmation.
* Seller receives order notification.
* Notification contains relevant order information.

**Priority:** High

**Status:** Not Executed

---

## TC-NOTIFY-002: Delivery Status Notification

**Module:** Notifications

**Scenario:** Verify delivery status notifications.

**Expected Result:**

* Relevant users are notified when delivery status changes.
* Notification reflects the current delivery status.

**Priority:** Medium

**Status:** Not Executed

---

# 11. Authorization Test Cases

## TC-SEC-001: Unauthorized Resource Access

**Module:** Security

**Scenario:** Verify that unauthenticated users cannot access protected resources.

**Steps:**

1. Do not authenticate.
2. Attempt to access a protected API endpoint.

**Expected Result:**

* Request is rejected.
* HTTP `401 Unauthorized` is returned where appropriate.

**Priority:** Critical

**Status:** Not Executed

---

## TC-SEC-002: Role-Based Access Control

**Module:** Security

**Scenario:** Verify that users cannot access functions outside their role.

**Steps:**

1. Login as a farmer.
2. Attempt to access an administrator-only function.

**Expected Result:**

* Access is denied.
* HTTP `403 Forbidden` is returned where appropriate.

**Priority:** Critical

**Status:** Not Executed

---

# 12. API Test Cases

## TC-API-001: Valid API Request

**Module:** Backend API

**Scenario:** Verify that a valid API request returns the expected response.

**Expected Result:**

* Correct HTTP status code is returned.
* Response follows the documented schema.
* Data is returned correctly.

**Priority:** Critical

**Status:** Not Executed

---

## TC-API-002: Invalid API Request

**Module:** Backend API

**Scenario:** Verify validation of invalid request data.

**Expected Result:**

* Request is rejected.
* Appropriate HTTP status code is returned.
* Validation errors are clearly communicated.

**Priority:** High

**Status:** Not Executed

---

## TC-API-003: API Error Handling

**Module:** Backend API

**Scenario:** Verify consistent API error responses.

**Expected Result:**

* Server errors are handled centrally.
* Sensitive internal information is not exposed.
* Client receives a consistent error response.

**Priority:** High

**Status:** Not Executed

---

# 13. Database Test Cases

## TC-DB-001: Data Persistence

**Module:** Database

**Scenario:** Verify that newly created records are persisted correctly.

**Expected Result:**

* Data is saved successfully.
* Data remains available after subsequent requests.

**Priority:** Critical

**Status:** Not Executed

---

## TC-DB-002: Referential Integrity

**Module:** Database

**Scenario:** Verify relationships between related entities.

**Expected Result:**

* Invalid foreign-key relationships are prevented.
* Related records remain consistent.

**Priority:** High

**Status:** Not Executed

---

# 14. Validation Test Cases

## TC-VAL-001: Required Fields

**Module:** Validation

**Scenario:** Verify required field validation.

**Expected Result:**

* Empty required fields are rejected.
* User receives meaningful validation messages.

**Priority:** High

**Status:** Not Executed

---

## TC-VAL-002: Invalid Data Format

**Module:** Validation

**Scenario:** Verify invalid email, phone number, quantity, and price formats.

**Expected Result:**

* Invalid values are rejected.
* Correct validation messages are displayed.

**Priority:** High

**Status:** Not Executed

---

# 15. Test Case Summary

| Category            | Test Cases |
| ------------------- | ---------: |
| Authentication      |          4 |
| Farmer              |          3 |
| Buyer               |          2 |
| Agricultural Expert |          2 |
| Logistics           |          2 |
| Marketplace         |          2 |
| AI                  |          2 |
| Notifications       |          2 |
| Security            |          2 |
| API                 |          3 |
| Database            |          2 |
| Validation          |          2 |
| **Total**           |     **28** |

---

# 16. Test Status

The initial test cases are defined for the AgriConnect platform.

The execution status will be updated during the implementation and testing phases:

* **Not Executed** — Test has not yet been performed.
* **Passed** — Expected result was achieved.
* **Failed** — Expected result was not achieved.
* **Blocked** — Test cannot currently be executed because of a dependency or environment problem.

Test evidence, screenshots, API responses, and discovered defects should be recorded in the appropriate testing documentation.
