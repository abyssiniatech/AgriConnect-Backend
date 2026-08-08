# AgriConnect Ethiopia — Order Workflow

## 1. Purpose

This document describes the complete order lifecycle in the AgriConnect Ethiopia marketplace, from product selection through order completion or cancellation.

---

## 2. Order Actors

The main actors are:

* Buyer
* Farmer
* Logistics Provider
* System
* Administrator

---

## 3. High-Level Order Workflow

```text
Buyer
  │
  │ Select Product
  ▼
Product Details
  │
  │ Purchase
  ▼
Create Order
  │
  ▼
Validate Order
  │
  ├── Invalid ──────► Reject Order
  │
  ▼
Save Order
  │
  ▼
Pending
  │
  ▼
Confirmed
  │
  ▼
Processing
  │
  ▼
Ready for Delivery
  │
  ▼
Delivery
  │
  ▼
Delivered
  │
  ▼
Completed
```

---

## 4. Step 1 — Buyer Selects Product

The buyer browses the marketplace and selects a product.

```text
Marketplace
     ↓
Search / Filter
     ↓
Product
     ↓
Product Details
```

The buyer reviews:

* Product name.
* Description.
* Price.
* Available quantity.
* Farmer.
* Location.
* Availability.

---

## 5. Step 2 — Specify Quantity

The buyer selects the required quantity.

Example:

```text
Available: 100 units
Requested: 20 units
```

The system must prevent the buyer from requesting more than the available quantity.

```text
Requested Quantity <= Available Quantity
```

---

## 6. Step 3 — Create Order

The buyer confirms the purchase.

Conceptually:

```http
POST /api/v1/orders
Content-Type: application/json
Authorization: Bearer <access-token>
```

Example request:

```json
{
  "productId": 101,
  "quantity": 20
}
```

The exact endpoint and request structure must match the implemented API.

---

## 7. Step 4 — Validate Order

The backend validates:

* Buyer authentication.
* Buyer authorization.
* Product existence.
* Product availability.
* Requested quantity.
* Product status.
* Current price.
* Business rules.

```text
Order Request
     │
     ▼
Authentication
     │
     ▼
Authorization
     │
     ▼
Product Validation
     │
     ▼
Quantity Validation
     │
     ▼
Business Validation
```

---

## 8. Step 5 — Create Order

If validation succeeds, the system creates the order.

```text
Buyer
  │
  ▼
Order Request
  │
  ▼
Validation
  │
  ▼
Order Entity
  │
  ▼
PostgreSQL
```

An order should have a unique identifier.

Example:

```text
Order ID: ORD-2026-000001
```

The actual identifier format depends on the implementation.

---

## 9. Order Items

An order may contain one or more order items.

```text
ORDER
 │
 ├── ORDER ITEM 1
 │      └── Product A
 │
 ├── ORDER ITEM 2
 │      └── Product B
 │
 └── ORDER ITEM 3
        └── Product C
```

Each order item should preserve the relevant purchase information, including quantity and applicable price.

---

## 10. Order Total

The system calculates the order total.

Conceptually:

```text
Item Total = Quantity × Unit Price

Order Total = Sum of Item Totals + Applicable Delivery Charges
```

Example:

```text
Quantity:     20
Unit Price:   2,500 ETB

20 × 2,500 = 50,000 ETB
```

The backend should perform the authoritative calculation rather than trusting a total supplied by the client.

---

## 11. Order Status Lifecycle

The order may move through the following states:

```text
Pending
   ↓
Confirmed
   ↓
Processing
   ↓
Ready for Delivery
   ↓
Delivered
   ↓
Completed
```

Possible alternative paths:

```text
Pending ─────────────► Cancelled

Confirmed ───────────► Cancelled

Processing ──────────► Failed
```

The exact statuses should match the implemented order model.

---

## 12. Pending State

Immediately after successful creation:

```text
Order
  ↓
Pending
```

The system records:

* Order ID.
* Buyer.
* Order items.
* Quantity.
* Price.
* Total.
* Creation time.
* Current status.

---

## 13. Confirmation

The farmer or authorized system process confirms the order.

```text
Pending
   ↓
Confirmation
   ↓
Confirmed
```

Confirmation indicates that the order can proceed to fulfillment.

---

## 14. Processing

After confirmation:

```text
Confirmed
    ↓
Processing
```

The farmer prepares the requested products.

Possible activities:

* Confirm stock.
* Prepare products.
* Package products.
* Prepare pickup.

---

## 15. Delivery Preparation

Once the order is ready:

```text
Processing
     ↓
Ready for Delivery
```

If logistics integration is implemented, a delivery request can be created.

```text
Order
  ↓
Delivery Request
  ↓
Logistics Provider
```

---

## 16. Delivery Workflow

```text
Ready for Delivery
       ↓
Pickup
       ↓
Transportation
       ↓
Delivery
       ↓
Delivered
```

The logistics provider updates delivery progress according to the implemented workflow.

---

## 17. Order Completion

After successful delivery:

```text
Delivered
    ↓
Completed
```

The system records the completion time.

The buyer may then be allowed to:

* View order history.
* Submit a review.
* Provide feedback.
* Report an issue.

These capabilities depend on implemented features.

---

## 18. Cancellation Workflow

An eligible order may be cancelled.

```text
Order
  │
  ▼
Check Cancellation Rules
  │
  ├── Not Allowed ──► Reject Request
  │
  ▼
Cancel Order
  │
  ▼
Cancelled
```

Cancellation rules should be enforced by the backend.

---

## 19. Inventory Update

When an order is successfully processed, inventory must be handled consistently.

Example:

```text
Initial Quantity:     100
Ordered Quantity:      20
Remaining Quantity:    80
```

The system should prevent negative inventory.

```text
Remaining Quantity >= 0
```

Where concurrency is possible, inventory updates should use appropriate transaction or concurrency controls.

---

## 20. Complete Order Flow

```text
                         BUYER
                           │
                           ▼
                   Select Product
                           │
                           ▼
                    Select Quantity
                           │
                           ▼
                    Create Order
                           │
                           ▼
                   Authentication
                           │
                           ▼
                    Authorization
                           │
                           ▼
                   Validate Product
                           │
                           ▼
                  Validate Quantity
                           │
                           ├──── Invalid
                           │        │
                           │        ▼
                           │     Rejected
                           │
                           ▼
                    Create Order
                           │
                           ▼
                        Pending
                           │
                           ▼
                       Confirmed
                           │
                           ▼
                      Processing
                           │
                           ▼
                 Ready for Delivery
                           │
                           ▼
                       Delivery
                           │
                           ▼
                       Delivered
                           │
                           ▼
                       Completed
```

---

## 21. Error Scenarios

### Product Does Not Exist

```text
Product ID
   ↓
Database Lookup
   ↓
Not Found
   ↓
404 Not Found
```

### Insufficient Stock

```text
Requested: 50
Available: 10
     ↓
Order Rejected
```

### Unauthenticated Buyer

```text
Order Request
     ↓
No Valid Authentication
     ↓
401 Unauthorized
```

### Unauthorized Operation

```text
User
  ↓
Attempts Restricted Action
  ↓
Authorization Check
  ↓
403 Forbidden
```

### Invalid Order Data

```text
Invalid Request
     ↓
Validation
     ↓
400 Bad Request
```

---

## 22. Security Controls

The order workflow should implement:

* Authentication.
* Authorization.
* Server-side validation.
* Ownership checks.
* Secure price calculation.
* Quantity validation.
* Protection against duplicate submissions.
* Transactional database operations where necessary.
* Audit logging for important state changes.
* HTTPS.

Sensitive information must not be exposed through API responses or logs.

---

## 23. Idempotency

Order creation should consider duplicate requests.

For example:

```text
Buyer clicks "Place Order"
        │
        ▼
Request sent
        │
        ▼
Network retry
        │
        ▼
Second request
```

The backend should use appropriate safeguards to prevent accidental duplicate orders where required.

If an idempotency-key mechanism is implemented, the client should provide a unique key for the operation.

---

## 24. Testing Scenarios

### Successful Order

```text
Valid buyer
   +
Available product
   +
Valid quantity
   ↓
Order Created
```

### Product Not Found

```text
Invalid product ID
   ↓
404 Not Found
```

### Quantity Too Large

```text
Requested > Available
   ↓
Order Rejected
```

### Unauthorized Order

```text
No valid authentication
   ↓
401 Unauthorized
```

### Duplicate Submission

```text
Same order request submitted twice
   ↓
Duplicate protection
   ↓
Single valid order
```

### Cancellation

```text
Eligible order
   ↓
Cancel
   ↓
Cancelled
```

---

## 25. Screenshot Evidence

Recommended screenshots:

```text
13-visual-documentation/screenshots/orders/
```

Suggested filenames:

```text
cart.png
checkout.png
order-confirmation.png
order-details.png
order-history.png
order-cancelled.png
delivery-status.png
```

Screenshots must not expose passwords, access tokens, API keys, or other sensitive information.

---

## 26. Related Documentation

```text
05-api/
06-features/
07-security/
08-testing/
09-user-guides/
10-developer-guide/
11-deployment/
13-visual-documentation/screenshots/
13-visual-documentation/workflows/marketplace-workflow.md
```

---

## 27. Conclusion

The order workflow provides a controlled lifecycle from product selection to order completion.

The backend remains responsible for authentication, authorization, validation, pricing, inventory, order creation, and status transitions. This ensures that important business rules cannot be bypassed by the frontend.

The workflow should be updated whenever order, inventory, payment, or delivery functionality changes.
