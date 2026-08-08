# AgriConnect Ethiopia — Database Design

## 1. Overview

The AgriConnect Ethiopia database is designed to provide reliable, secure, and scalable storage for the platform's core business data.

The database supports the major functional areas of the system, including:

* User management
* Farmer management
* Buyer management
* Agricultural expert management
* Logistics management
* Marketplace management
* Product management
* Orders
* Payments
* Deliveries
* Reviews and ratings
* Notifications
* AI-assisted agricultural services
* Administrative management

The database is designed using a relational model and follows normalization, referential integrity, transactional consistency, and data security principles.

---

# 2. Database Management System

## 2.1 PostgreSQL

AgriConnect uses **PostgreSQL** as its primary relational database management system.

PostgreSQL was selected because it provides:

* Strong relational data modeling
* ACID-compliant transactions
* Referential integrity
* Foreign-key constraints
* Advanced indexing
* Reliable transaction processing
* Strong querying capabilities
* Good scalability
* Open-source licensing

---

# 3. Database Architecture

The application communicates with PostgreSQL through the backend API.

```text
┌─────────────────────────────┐
│      Angular Frontend       │
└──────────────┬──────────────┘
               │
               │ HTTPS / REST API
               ↓
┌─────────────────────────────┐
│      ASP.NET Core API       │
│                             │
│ Application / Domain Logic  │
└──────────────┬──────────────┘
               │
               │ Entity Framework Core
               ↓
┌─────────────────────────────┐
│         PostgreSQL          │
│                             │
│ Users                       │
│ Products                    │
│ Orders                      │
│ Payments                    │
│ Deliveries                  │
│ Notifications               │
│ Other business entities     │
└─────────────────────────────┘
```

The frontend does not communicate directly with PostgreSQL.

All database operations go through the backend API.

---

# 4. Database Design Principles

The database follows these principles:

### 4.1 Data Integrity

Foreign keys and constraints are used to ensure that relationships between records remain valid.

### 4.2 Normalization

Data is organized to minimize:

* Duplication
* Update anomalies
* Insert anomalies
* Delete anomalies

### 4.3 Referential Integrity

Relationships between entities are enforced using foreign keys.

### 4.4 Security

Database credentials and connection strings are not hard-coded into source code.

### 4.5 Scalability

Indexes, pagination, efficient queries, and appropriate relationships are used to support future growth.

### 4.6 Auditability

Important business operations should be traceable through timestamps and appropriate audit information.

---

# 5. Core Domain Entities

The major database entities are organized around the following business domains.

```text
Users
 │
 ├── Farmers
 ├── Buyers
 ├── Experts
 ├── Logistics Providers
 └── Administrators
```

Marketplace:

```text
Categories
     │
     ↓
Products
     │
     ↓
Orders
     │
     ├── Order Items
     ├── Payments
     └── Deliveries
```

Expert services:

```text
Farmers
   │
   ↓
Consultation Requests
   │
   ↓
Experts
```

Communication:

```text
Users
  │
  ├── Notifications
  ├── Messages
  └── Reviews
```

---

# 6. User Management

## 6.1 Users

The `Users` entity represents accounts registered on the platform.

Typical attributes include:

| Field        | Description                   |
| ------------ | ----------------------------- |
| Id           | Unique user identifier        |
| FirstName    | User first name               |
| LastName     | User last name                |
| Email        | Unique email address          |
| PhoneNumber  | Contact number                |
| PasswordHash | Securely stored password hash |
| Role         | User role                     |
| IsActive     | Account status                |
| CreatedAt    | Account creation date         |
| UpdatedAt    | Last modification date        |

Users may have different roles.

```text
User
 ├── Farmer
 ├── Buyer
 ├── Expert
 ├── Logistics Provider
 └── Administrator
```

---

# 7. Farmer Data

The Farmer domain stores information specific to agricultural producers.

Potential information includes:

* Farmer profile
* Farm location
* Farm size
* Crops
* Production information
* Marketplace products
* Expert consultations
* Orders
* Delivery information

Relationship:

```text
Farmer
  │
  ├── Farm
  ├── Products
  ├── Orders
  └── Consultations
```

---

# 8. Buyer Data

Buyers can browse agricultural products and purchase them through the marketplace.

Buyer-related information includes:

* Buyer profile
* Orders
* Order items
* Payments
* Delivery addresses
* Reviews

Relationship:

```text
Buyer
  │
  ├── Orders
  ├── Payments
  ├── Reviews
  └── Addresses
```

---

# 9. Expert Data

Agricultural experts provide professional agricultural guidance.

Expert information may include:

* Name
* Specialization
* Experience
* Qualification
* Availability
* Consultation history
* Ratings

Relationship:

```text
Expert
  │
  ├── Consultations
  ├── Availability
  └── Reviews
```

---

# 10. Logistics Data

The logistics domain manages the transportation and delivery of agricultural products.

Potential entities include:

```text
LogisticsProvider
Delivery
DeliveryStatus
DeliveryAddress
Vehicle
```

A logistics provider can manage multiple deliveries.

```text
Logistics Provider
       │
       ├── Delivery
       ├── Delivery
       └── Delivery
```

---

# 11. Marketplace Data

The marketplace is one of the central components of AgriConnect.

The main entities include:

```text
Category
Product
ProductImage
Inventory
Order
OrderItem
Payment
Review
```

---

# 12. Categories

Categories organize agricultural products.

Examples include:

```text
Cereals
Vegetables
Fruits
Pulses
Spices
Livestock Products
Dairy Products
Other Agricultural Products
```

Relationship:

```text
Category
    │
    └── Products
```

One category can contain multiple products.

---

# 13. Products

The `Products` entity stores agricultural products offered through the marketplace.

Typical attributes include:

| Field       | Description                 |
| ----------- | --------------------------- |
| Id          | Product identifier          |
| FarmerId    | Product owner               |
| CategoryId  | Product category            |
| Name        | Product name                |
| Description | Product description         |
| Price       | Unit price                  |
| Quantity    | Available quantity          |
| Unit        | Measurement unit            |
| Location    | Product location            |
| Status      | Product availability        |
| CreatedAt   | Creation timestamp          |
| UpdatedAt   | Last modification timestamp |

Relationship:

```text
Farmer
   │
   └── Product
          │
          └── Category
```

---

# 14. Inventory

Inventory information tracks product availability.

Important information may include:

* Product
* Available quantity
* Reserved quantity
* Minimum stock level
* Last updated timestamp

Inventory should be updated when:

* A product is created
* An order is placed
* An order is cancelled
* An order is completed

---

# 15. Orders

Orders represent purchases made through the marketplace.

Typical fields include:

| Field             | Description            |
| ----------------- | ---------------------- |
| Id                | Order identifier       |
| BuyerId           | Buyer                  |
| TotalAmount       | Total order value      |
| Status            | Current order status   |
| DeliveryAddressId | Delivery destination   |
| CreatedAt         | Order creation date    |
| UpdatedAt         | Last modification date |

Example order lifecycle:

```text
Pending
   ↓
Confirmed
   ↓
Processing
   ↓
Shipped
   ↓
Delivered
```

Possible cancellation path:

```text
Pending
   ↓
Cancelled
```

---

# 16. Order Items

An order may contain multiple products.

Example:

```text
Order #1001

 ├── Teff       × 2
 ├── Wheat      × 5
 └── Tomatoes   × 10
```

The `OrderItems` entity stores:

* Order
* Product
* Quantity
* Unit price
* Subtotal

The unit price should be preserved at the time of purchase so that historical orders remain accurate even if the product price changes later.

---

# 17. Payments

Payment records represent financial transactions associated with orders.

Potential attributes include:

| Field                | Description                    |
| -------------------- | ------------------------------ |
| Id                   | Payment identifier             |
| OrderId              | Related order                  |
| Amount               | Payment amount                 |
| PaymentMethod        | Payment method                 |
| TransactionReference | External transaction reference |
| Status               | Payment status                 |
| CreatedAt            | Payment timestamp              |

Possible payment statuses:

```text
Pending
Successful
Failed
Refunded
```

Payment credentials or sensitive financial information should not be stored unnecessarily in the database.

---

# 18. Deliveries

Deliveries connect orders with logistics providers.

Typical fields include:

| Field                 | Description                 |
| --------------------- | --------------------------- |
| Id                    | Delivery identifier         |
| OrderId               | Related order               |
| LogisticsProviderId   | Assigned logistics provider |
| AddressId             | Delivery address            |
| Status                | Delivery status             |
| PickupDate            | Pickup date                 |
| EstimatedDeliveryDate | Expected delivery           |
| DeliveredAt           | Actual delivery date        |

Example lifecycle:

```text
Pending
   ↓
Assigned
   ↓
Picked Up
   ↓
In Transit
   ↓
Delivered
```

---

# 19. Agricultural Consultations

The consultation domain connects farmers with agricultural experts.

A consultation may contain:

* Farmer
* Expert
* Subject
* Description
* Request date
* Response
* Status
* Completion date

Example:

```text
Farmer
   │
   │ Consultation Request
   ↓
Expert
   │
   │ Agricultural Advice
   ↓
Farmer
```

Possible statuses:

```text
Requested
Assigned
In Progress
Answered
Completed
Cancelled
```

---

# 20. Reviews and Ratings

Reviews allow users to provide feedback.

Reviews may apply to:

* Products
* Farmers
* Buyers
* Experts
* Logistics providers

Typical fields include:

```text
Id
UserId
Rating
Comment
CreatedAt
UpdatedAt
```

Rating values should be constrained to the supported range, such as:

```text
1 – 5
```

---

# 21. Notifications

Notifications inform users about important system events.

Examples:

* New order
* Order confirmation
* Payment confirmation
* Delivery update
* Expert response
* Account notification

Typical fields:

| Field     | Description             |
| --------- | ----------------------- |
| Id        | Notification identifier |
| UserId    | Recipient               |
| Title     | Notification title      |
| Message   | Notification content    |
| Type      | Notification type       |
| IsRead    | Read status             |
| CreatedAt | Creation timestamp      |

---

# 22. AI Interaction Data

If AI functionality stores interaction history, the database may contain records such as:

```text
AIConversation
AIMessage
AIRecommendation
AIInteraction
```

Possible information includes:

* User
* Question
* AI response
* Timestamp
* Recommendation type
* Feedback

Sensitive information should only be stored when required by the business functionality.

---

# 23. Entity Relationships

Major relationships include:

```text
User
 │
 ├──────── Farmer
 │            │
 │            ├── Farm
 │            ├── Product
 │            └── Consultation
 │
 ├──────── Buyer
 │            │
 │            └── Order
 │
 ├──────── Expert
 │            │
 │            └── Consultation
 │
 └──────── Logistics Provider
              │
              └── Delivery
```

Marketplace relationships:

```text
Category
   │
   └── Product
          │
          └── OrderItem
                   │
                   └── Order
                         │
                         ├── Payment
                         └── Delivery
```

---

# 24. Primary Keys

Every major entity should have a unique primary key.

Example:

```text
Users          → UserId
Products       → ProductId
Orders         → OrderId
OrderItems     → OrderItemId
Payments       → PaymentId
Deliveries     → DeliveryId
Categories     → CategoryId
Reviews        → ReviewId
Notifications  → NotificationId
```

Primary keys ensure that every record can be uniquely identified.

---

# 25. Foreign Keys

Foreign keys maintain relationships between tables.

Examples:

```text
Products.FarmerId
        ↓
Users.Id

Products.CategoryId
        ↓
Categories.Id

Orders.BuyerId
        ↓
Users.Id

OrderItems.OrderId
        ↓
Orders.Id

OrderItems.ProductId
        ↓
Products.Id

Payments.OrderId
        ↓
Orders.Id

Deliveries.OrderId
        ↓
Orders.Id
```

Foreign-key constraints prevent invalid references.

---

# 26. Indexing Strategy

Indexes are used to improve query performance.

Potential indexes include:

```text
Users.Email
Users.PhoneNumber

Products.FarmerId
Products.CategoryId
Products.Status

Orders.BuyerId
Orders.Status
Orders.CreatedAt

OrderItems.OrderId
OrderItems.ProductId

Deliveries.OrderId
Deliveries.Status

Notifications.UserId
Notifications.IsRead
```

Indexes should be added based on actual query patterns and performance requirements.

Excessive indexing should be avoided because indexes also increase storage requirements and write overhead.

---

# 27. Unique Constraints

Unique constraints should be used where duplicate values are not allowed.

Examples:

```text
Users.Email
Users.PhoneNumber
Product-specific identifiers
External transaction references
```

The exact uniqueness rules should follow the application's business requirements.

---

# 28. Transactions

Transactions are required for operations where multiple database changes must succeed or fail together.

Example order creation:

```text
1. Create Order
2. Create Order Items
3. Reserve Inventory
4. Create Payment Record
5. Commit Transaction
```

If an operation fails:

```text
Rollback
```

This prevents inconsistent database states.

---

# 29. Soft Delete

For business records that should be retained for historical or audit purposes, soft deletion may be used.

Example:

```text
IsDeleted = true
```

Instead of permanently removing the record.

This approach can be useful for:

* Users
* Products
* Orders
* Reviews

However, soft deletion should only be used where it provides a clear business or compliance benefit.

---

# 30. Audit Information

Important entities should include timestamps such as:

```text
CreatedAt
UpdatedAt
```

Where required, additional information may include:

```text
CreatedBy
UpdatedBy
DeletedAt
DeletedBy
```

Audit information improves traceability and troubleshooting.

---

# 31. Data Consistency Rules

The database should enforce important consistency rules.

Examples:

* Product prices cannot be negative.
* Product quantities cannot be negative.
* Ratings must be within the permitted range.
* Orders must reference valid buyers.
* Order items must reference valid products.
* Payments must reference valid orders.
* Deliveries must reference valid orders.
* Required user fields cannot be null.
* Email addresses must follow application validation rules.

Business rules should be enforced at both the application and database levels where appropriate.

---

# 32. Pagination

Large database collections should be queried using pagination.

Example:

```text
Page = 1
PageSize = 20
```

Conceptually:

```text
GET /api/v1/products?page=1&pageSize=20
```

Pagination prevents the application from loading thousands of records unnecessarily.

---

# 33. Database Migrations

Entity Framework Core migrations are used to manage database schema changes.

Typical workflow:

```text
Modify Entity
      ↓
Create Migration
      ↓
Review Migration
      ↓
Apply Migration
      ↓
Database Updated
```

Example commands:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Migration names should clearly describe the schema change.

Examples:

```text
InitialCreate
AddProductEntity
AddOrderEntities
AddDeliveryTracking
AddNotifications
```

---

# 34. Backup and Recovery

The production database should have a documented backup strategy.

Backups should consider:

* Backup frequency
* Retention period
* Storage location
* Encryption
* Restoration testing
* Disaster recovery

A backup is only useful if it can be successfully restored.

Therefore, restoration tests should be performed periodically.

---

# 35. Database Security

Database security measures include:

* Strong database credentials
* Restricted database access
* Encrypted connections where appropriate
* Least-privilege database users
* Secure connection strings
* Regular backups
* Monitoring
* Input validation
* Protection against SQL injection

Application code should use parameterized queries or ORM-generated queries rather than constructing SQL statements from untrusted input.

---

# 36. Performance Considerations

Database performance will be improved through:

* Proper indexing
* Pagination
* Efficient LINQ queries
* Avoiding unnecessary data loading
* Selecting only required columns
* Query optimization
* Connection pooling
* Caching where appropriate
* Database monitoring

Performance should be measured using real workloads before introducing unnecessary optimization complexity.

---

# 37. Scalability Considerations

The database architecture should support future growth.

Potential future improvements include:

* Read replicas
* Advanced caching
* Partitioning
* Database monitoring
* Query optimization
* Search indexing
* Geographic data support
* Archiving historical records

These techniques should be introduced when actual scale and performance requirements justify them.

---

# 38. Database Documentation

The following supporting database documents are maintained in this section:

```text
04-database/
│
├── database-design.md
├── entity-relationship.md
├── data-dictionary.md
│
└── diagrams/
    └── erd.png
```

`database-design.md` explains the overall database architecture and design principles.

`entity-relationship.md` documents relationships between entities.

`data-dictionary.md` provides detailed field-level definitions.

The `diagrams/` directory contains visual database diagrams.

---

# 39. Recommended Database Development Workflow

The recommended workflow is:

```text
Requirement
    ↓
Domain Entity
    ↓
Relationship Design
    ↓
Entity Framework Model
    ↓
Migration
    ↓
PostgreSQL
    ↓
Seed / Test Data
    ↓
API
    ↓
Frontend
```

This ensures that database changes are connected to actual business requirements.

---

# 40. Conclusion

The AgriConnect Ethiopia database is designed around PostgreSQL and Entity Framework Core, using a relational model that supports the platform's major business domains.

The design emphasizes:

* Data integrity
* Security
* Maintainability
* Scalability
* Transactional consistency
* Performance
* Clear relationships
* Future extensibility

The database serves as the reliable persistence layer for the AgriConnect ecosystem while remaining isolated from direct frontend access.

The next database documentation files will provide the detailed entity relationships and field-level definitions required to implement and maintain the database consistently.
