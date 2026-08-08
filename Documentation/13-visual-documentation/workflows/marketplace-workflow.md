# AgriConnect Ethiopia — Marketplace Workflow

## 1. Purpose

This document describes the workflow for discovering, listing, viewing, and purchasing agricultural products through the AgriConnect Ethiopia marketplace.

The marketplace connects farmers who supply agricultural products with buyers who need those products.

---

# 2. Marketplace Actors

The primary actors are:

* Farmer.
* Buyer.
* System.
* Administrator.

---

# 3. High-Level Marketplace Workflow

```text id="j1q6f3"
┌──────────────┐
│    Farmer    │
└──────┬───────┘
       │
       │ Create Product
       ▼
┌──────────────────┐
│ Product Listing  │
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│ ASP.NET Core API │
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│    PostgreSQL    │
└────────┬─────────┘
         │
         │ Product Published
         ▼
┌──────────────────┐
│    Marketplace   │
└────────┬─────────┘
         │
         │ Search / Browse
         ▼
┌──────────────────┐
│      Buyer       │
└────────┬─────────┘
         │
         │ Select Product
         ▼
┌──────────────────┐
│ Product Details  │
└────────┬─────────┘
         │
         │ Purchase
         ▼
┌──────────────────┐
│      Order       │
└──────────────────┘
```

---

# 4. Farmer Product Listing Workflow

## Step 1 — Farmer Login

The farmer authenticates with the platform.

```text id="g0k5j1"
Farmer
  ↓
Login
  ↓
Authentication
  ↓
Farmer Dashboard
```

---

## Step 2 — Create Product

The farmer selects:

```text id="q1gtki"
Add Product
```

The product form may contain:

* Product name.
* Category.
* Description.
* Price.
* Quantity.
* Location.
* Images.
* Availability.

The exact fields should match the implemented product model.

---

# 5. Product Validation

The frontend performs basic validation.

Examples:

```text id="xg70h6"
Product Name
✓ Required

Price
✓ Valid number

Quantity
✓ Greater than zero

Category
✓ Selected
```

The backend performs validation again before saving the product.

---

# 6. Product API Request

Conceptually:

```http id="5a8ksm"
POST /api/v1/products
Content-Type: application/json
Authorization: Bearer <access-token>
```

Example:

```json id="c0v9xa"
{
  "name": "Teff",
  "category": "Cereal",
  "description": "Quality agricultural product",
  "price": 2500,
  "quantity": 100,
  "location": "Addis Ababa"
}
```

The token shown above is only a placeholder and must never be committed as a real token.

---

# 7. Product Persistence

The backend processes the request:

```text id="f0g8xj"
Angular
   ↓
Products API
   ↓
Validation
   ↓
Business Logic
   ↓
Entity Framework Core
   ↓
PostgreSQL
```

After successful persistence, the product becomes available according to its status and marketplace rules.

---

# 8. Product Discovery

A buyer can browse the marketplace.

```text id="z2r7hc"
Buyer
  ↓
Marketplace
  ↓
Search / Filter
  ↓
Product List
```

The buyer may search by:

* Product name.
* Category.
* Location.
* Price.
* Availability.

---

# 9. Product Search Workflow

```text id="1zj7s9"
Buyer enters search
        ↓
Angular sends query
        ↓
Products API
        ↓
Database Query
        ↓
Matching Products
        ↓
API Response
        ↓
Angular Product List
```

---

# 10. Product Details

When a buyer selects a product:

```text id="5avk6p"
Product List
      ↓
Product ID
      ↓
Product Details API
      ↓
Product Details
      ↓
Product Details Page
```

The details page may display:

* Product name.
* Description.
* Price.
* Quantity available.
* Farmer information.
* Location.
* Availability.
* Images.

---

# 11. Purchase Workflow

The buyer selects an available product and starts the purchase process.

```text id="dx0ndm"
Product Details
      ↓
Add to Cart / Purchase
      ↓
Review Order
      ↓
Confirm Order
      ↓
Create Order
```

The exact cart and checkout behavior depends on the implemented application.

---

# 12. Order Creation

The backend validates:

* Buyer authentication.
* Product availability.
* Requested quantity.
* Product status.
* Pricing information.
* Business rules.

Then an order is created.

```text id="7h2e6g"
Buyer
  │
  ▼
Order Request
  │
  ▼
Validation
  │
  ├── Invalid ──► Error
  │
  ▼
Create Order
  │
  ▼
PostgreSQL
  │
  ▼
Order Created
```

---

# 13. Inventory Consideration

When a buyer orders a product, the available quantity should be updated according to the application's inventory rules.

Example:

```text id="5st5ca"
Available Quantity: 100
Order Quantity:      20
                    ───
Remaining:           80
```

The application should prevent orders that exceed available stock.

---

# 14. Order Status

Possible order statuses include:

```text id="z9n5p4"
Pending
Confirmed
Processing
Ready for Delivery
Delivered
Cancelled
```

The actual statuses should match the implemented order model.

---

# 15. Logistics Integration

If delivery is required:

```text id="t3h84d"
Order
  ↓
Delivery Request
  ↓
Logistics Provider
  ↓
Pickup
  ↓
Transportation
  ↓
Delivery
  ↓
Order Completed
```

The logistics workflow should remain synchronized with the order status.

---

# 16. Marketplace Workflow — Complete

```text id="d8lyi2"
                         FARMER
                           │
                           │ Create Product
                           ▼
                    ┌───────────────┐
                    │ Product Form  │
                    └───────┬───────┘
                            │
                            ▼
                    ┌───────────────┐
                    │ Validation    │
                    └───────┬───────┘
                            │
                            ▼
                    ┌───────────────┐
                    │ Products API  │
                    └───────┬───────┘
                            │
                            ▼
                    ┌───────────────┐
                    │  PostgreSQL   │
                    └───────┬───────┘
                            │
                            ▼
                     MARKETPLACE
                            │
                            │ Search
                            ▼
                          BUYER
                            │
                            │ Select
                            ▼
                    Product Details
                            │
                            │ Purchase
                            ▼
                          ORDER
                            │
                            ▼
                        DELIVERY
                            │
                            ▼
                       COMPLETED
```

---

# 17. Product Availability

A product should only be displayed as purchasable when it satisfies the marketplace rules.

Possible conditions:

```text id="uhz3vs"
Product exists
     AND
Product is active
     AND
Quantity > 0
     AND
Farmer account is valid
```

---

# 18. Authorization

Marketplace operations should enforce role-based access.

Example:

| Operation        |  Farmer |      Buyer | Expert | Logistics | Admin |
| ---------------- | ------: | ---------: | -----: | --------: | ----: |
| Create Product   |       ✓ |          — |      — |         — |     ✓ |
| Edit Own Product |       ✓ |          — |      — |         — |     ✓ |
| Browse Products  |       ✓ |          ✓ |      ✓ |         ✓ |     ✓ |
| Purchase Product |       — |          ✓ |      — |         — |     ✓ |
| Manage Orders    | Limited | Own Orders |      — |  Delivery |     ✓ |
| Manage Users     |       — |          — |      — |         — |     ✓ |

Actual permissions should follow the implemented authorization policies.

---

# 19. Error Scenarios

### Product Not Found

```text id="q0h7se"
Buyer
 ↓
Product ID
 ↓
API
 ↓
404 Not Found
```

### Insufficient Quantity

```text id="8y2d3q"
Requested: 100
Available: 20

        ↓

Order rejected
```

### Unauthorized Product Update

```text id="n9txzk"
User attempts to edit another farmer's product
        ↓
Authorization check
        ↓
403 Forbidden
```

### Unauthenticated Request

```text id="e0op2g"
Protected API Request
        ↓
No valid authentication
        ↓
401 Unauthorized
```

---

# 20. Security Considerations

Marketplace operations should include:

* Authentication for protected actions.
* Authorization for ownership.
* Server-side validation.
* Input validation.
* Price validation.
* Quantity validation.
* Protection against unauthorized product modification.
* Secure API communication.
* Audit logging where appropriate.

---

# 21. Testing Workflow

The marketplace should be tested using scenarios such as:

### Product Creation

```text id="2ijqmi"
Valid product
   ↓
Product created
```

### Product Search

```text id="7g7owf"
Search term
   ↓
Matching products returned
```

### Product Details

```text id="6thv8n"
Valid Product ID
   ↓
Product details returned
```

### Invalid Product

```text id="w0p6m0"
Unknown Product ID
   ↓
404 Not Found
```

### Unauthorized Update

```text id="wz6zuw"
User edits another user's product
   ↓
403 Forbidden
```

### Insufficient Stock

```text id="5jz9z0"
Requested quantity > available quantity
   ↓
Order rejected
```

---

# 22. Screenshot Evidence

Recommended screenshots:

```text id="r55n4d"
13-visual-documentation/screenshots/marketplace/
```

Suggested screenshots:

```text id="q2cqmx"
marketplace-home.png
product-list.png
product-search.png
product-details.png
product-create-form.png
order-confirmation.png
order-details.png
```

Do not include sensitive credentials or private information.

---

# 23. Related Documentation

```text id="8x1l4v"
05-api/
06-features/
07-security/
08-testing/
09-user-guides/
10-developer-guide/
13-visual-documentation/screenshots/
13-visual-documentation/workflows/
```

---

# 24. Conclusion

The marketplace workflow connects farmers and buyers through a controlled digital marketplace.

Farmers can publish agricultural products, buyers can discover available products, and buyers can initiate orders for available products. The backend validates requests, enforces authorization, manages data through PostgreSQL, and coordinates order and logistics processes where implemented.

The workflow should be updated whenever marketplace functionality or business rules change.
