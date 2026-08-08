# AgriConnect Ethiopia — Database ER Diagram

## 1. Purpose

This document provides a visual representation of the main entities and relationships used by the AgriConnect Ethiopia platform.

The diagram should be updated whenever the database schema changes.

---

# 2. Entity Relationship Diagram

```text
┌─────────────────────┐
│       USERS         │
├─────────────────────┤
│ PK Id               │
│ Name                │
│ Email               │
│ PasswordHash        │
│ Role                │
│ CreatedAt           │
└─────────┬───────────┘
          │
          │ 1
          │
          ├──────────────────────┐
          │                      │
          │ 1                    │ 1
          ▼                      ▼
┌─────────────────────┐   ┌─────────────────────┐
│      FARMERS        │   │       BUYERS        │
├─────────────────────┤   ├─────────────────────┤
│ PK Id               │   │ PK Id               │
│ FK UserId           │   │ FK UserId           │
│ FarmName            │   │ OrganizationName    │
│ Location            │   │ Location            │
│ CreatedAt           │   │ CreatedAt           │
└─────────┬───────────┘   └─────────┬───────────┘
          │                         │
          │ 1                       │ 1
          │                         │
          ▼                         │
┌─────────────────────┐             │
│      PRODUCTS       │             │
├─────────────────────┤             │
│ PK Id               │
│ FK FarmerId         │
│ Name                │
│ Description         │
│ Category            │
│ Price               │
│ Quantity            │
│ Location            │
│ Status              │
│ CreatedAt           │
└─────────┬───────────┘
          │
          │ 1
          │
          │
          │ *
          ▼
┌─────────────────────┐
│    ORDER_ITEMS      │
├─────────────────────┤
│ PK Id               │
│ FK OrderId          │
│ FK ProductId        │
│ Quantity            │
│ UnitPrice           │
└─────────┬───────────┘
          │
          │ *
          │
          ▼
┌─────────────────────┐
│       ORDERS        │
├─────────────────────┤
│ PK Id               │
│ FK BuyerId          │
│ TotalAmount         │
│ Status              │
│ CreatedAt           │
└─────────────────────┘


┌─────────────────────┐
│ AGRICULTURAL_EXPERTS│
├─────────────────────┤
│ PK Id               │
│ FK UserId           │
│ Specialization      │
│ Experience          │
│ Location            │
│ Bio                 │
│ CreatedAt           │
└─────────┬───────────┘
          │
          │ 1
          │
          │ *
          ▼
┌─────────────────────┐
│    CONSULTATIONS    │
├─────────────────────┤
│ PK Id               │
│ FK ExpertId         │
│ FK FarmerId         │
│ Subject             │
│ Message             │
│ Status              │
│ CreatedAt           │
└─────────────────────┘


┌─────────────────────┐
│ LOGISTICS_PROVIDERS │
├─────────────────────┤
│ PK Id               │
│ FK UserId           │
│ CompanyName         │
│ Phone               │
│ Location            │
│ ServiceArea         │
│ CreatedAt           │
└─────────┬───────────┘
          │
          │ 1
          │
          │ *
          ▼
┌─────────────────────┐
│      DELIVERIES     │
├─────────────────────┤
│ PK Id               │
│ FK OrderId          │
│ FK ProviderId       │
│ PickupLocation      │
│ DeliveryLocation    │
│ Status              │
│ ScheduledAt         │
│ DeliveredAt         │
└─────────────────────┘
```

> **Important:** This is a conceptual ER diagram. Entity and field names should be changed to match the actual PostgreSQL/EF Core schema implemented in the project.

---

# 3. Core Relationships

## Users → Farmers

A user account may have a farmer profile.

```text
USERS 1 ───────── 1 FARMERS
```

The farmer profile references the user account.

---

## Users → Buyers

A buyer profile is associated with a user account.

```text
USERS 1 ───────── 1 BUYERS
```

---

## Farmers → Products

A farmer can create multiple agricultural product listings.

```text
FARMERS 1 ───────── * PRODUCTS
```

One farmer can therefore have many products.

---

## Buyers → Orders

A buyer can create multiple orders.

```text
BUYERS 1 ───────── * ORDERS
```

Each order belongs to a buyer.

---

## Orders → Order Items

An order can contain multiple order items.

```text
ORDERS 1 ───────── * ORDER_ITEMS
```

Each order item represents a product and quantity within an order.

---

## Products → Order Items

A product can appear in multiple order items.

```text
PRODUCTS 1 ───────── * ORDER_ITEMS
```

The `ORDER_ITEMS` entity resolves the many-to-many relationship between orders and products.

---

## Experts → Consultations

An agricultural expert can handle multiple consultations.

```text
AGRICULTURAL_EXPERTS 1 ───────── * CONSULTATIONS
```

---

## Farmers → Consultations

A farmer may create multiple consultation requests.

```text
FARMERS 1 ───────── * CONSULTATIONS
```

Therefore:

```text
FARMERS * ───────── * AGRICULTURAL_EXPERTS
             │
             │
      CONSULTATIONS
```

---

## Logistics Providers → Deliveries

A logistics provider can manage multiple deliveries.

```text
LOGISTICS_PROVIDERS 1 ───────── * DELIVERIES
```

---

## Orders → Deliveries

An order may have an associated delivery.

```text
ORDERS 1 ───────── 0..1 DELIVERIES
```

The exact relationship depends on the implemented logistics workflow.

---

# 4. Primary Keys

Each main entity should have a unique primary key.

Example:

```text
USERS
  PK Id

FARMERS
  PK Id

PRODUCTS
  PK Id

ORDERS
  PK Id
```

Primary keys provide unique identification for database records.

---

# 5. Foreign Keys

Foreign keys establish relationships between entities.

Examples:

```text
FARMERS.UserId
        ↓
USERS.Id
```

```text
PRODUCTS.FarmerId
        ↓
FARMERS.Id
```

```text
ORDERS.BuyerId
        ↓
BUYERS.Id
```

```text
ORDER_ITEMS.OrderId
        ↓
ORDERS.Id
```

```text
ORDER_ITEMS.ProductId
        ↓
PRODUCTS.Id
```

---

# 6. Data Integrity

The database should maintain integrity through:

* Primary keys.
* Foreign keys.
* Unique constraints.
* Required fields.
* Appropriate data types.
* Check constraints where required.
* Indexes.
* Referential actions.

---

# 7. Suggested Indexes

Indexes should be created according to actual query patterns.

Potential indexes include:

```text
Users.Email
Products.FarmerId
Products.Category
Products.Status
Products.Location
Orders.BuyerId
Orders.Status
OrderItems.OrderId
OrderItems.ProductId
```

Indexes should be validated against real application queries rather than added unnecessarily.

---

# 8. Database Flow

A typical marketplace transaction may follow:

```text
FARMER
  │
  │ Creates
  ▼
PRODUCT
  │
  │ Selected by
  ▼
BUYER
  │
  │ Creates
  ▼
ORDER
  │
  │ Contains
  ▼
ORDER_ITEM
  │
  │ References
  ▼
PRODUCT
  │
  │ May require
  ▼
DELIVERY
  │
  │ Managed by
  ▼
LOGISTICS_PROVIDER
```

---

# 9. Expert Consultation Flow

```text
FARMER
   │
   │ Requests consultation
   ▼
CONSULTATION
   │
   │ Assigned to
   ▼
AGRICULTURAL_EXPERT
   │
   │ Provides advice
   ▼
FARMER
```

---

# 10. Database Layer

The application communicates with the database through Entity Framework Core:

```text
┌──────────────────────┐
│ ASP.NET Core API     │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│ Entity Framework Core│
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│ PostgreSQL            │
└──────────────────────┘
```

---

# 11. Schema Change Management

Database changes should be implemented through controlled migrations.

Typical workflow:

```text
Modify Entity
     ↓
Create Migration
     ↓
Review Migration
     ↓
Run Tests
     ↓
Apply to Development
     ↓
Apply to Testing
     ↓
Backup Production
     ↓
Apply to Production
```

---

# 12. Database Documentation Rules

Whenever the schema changes:

* [ ] Update entity documentation.
* [ ] Update this ER diagram.
* [ ] Add or update migration.
* [ ] Update data dictionary.
* [ ] Update API documentation if affected.
* [ ] Update tests.
* [ ] Review foreign keys.
* [ ] Review indexes.
* [ ] Review security implications.

---

# 13. Related Documentation

Related files include:

```text
02-requirements/
04-system-design/
05-api/
07-security/
08-testing/
10-developer-guide/
11-deployment/
```

---

# 14. Conclusion

The ER diagram provides a conceptual view of how major AgriConnect Ethiopia entities relate to one another.

The diagram must remain synchronized with the actual EF Core models and PostgreSQL database schema. When the implemented schema differs from this conceptual model, the documentation should be updated to reflect the real system.
