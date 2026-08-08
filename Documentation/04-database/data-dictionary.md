# AgriConnect Ethiopia — Data Dictionary

## 1. Document Information

| Item     | Details                                                                          |
| -------- | -------------------------------------------------------------------------------- |
| Project  | AgriConnect Ethiopia                                                             |
| Document | Database Data Dictionary                                                         |
| Version  | 1.0                                                                              |
| Status   | Draft / Development                                                              |
| Database | PostgreSQL                                                                       |
| Purpose  | Define database entities, attributes, data types, constraints, and relationships |

---

## 2. Purpose

The AgriConnect Ethiopia data dictionary provides a structured description of the data managed by the platform.

It defines:

* Database entities
* Attributes and their meanings
* Data types
* Primary keys
* Foreign keys
* Required and optional fields
* Validation rules
* Relationships between entities
* Business meaning of stored data

The data dictionary serves as a reference for developers, database administrators, testers, and project reviewers.

---

# 3. Database Design Principles

AgriConnect follows these database principles:

1. Use relational database design.
2. Use PostgreSQL as the primary database.
3. Use UUID or integer identifiers consistently.
4. Enforce referential integrity through foreign keys.
5. Avoid unnecessary duplication of data.
6. Store timestamps using UTC.
7. Use appropriate indexes for frequently queried fields.
8. Apply validation at both application and database levels.
9. Protect sensitive user information.
10. Maintain audit information for important operations.

---

# 4. Entity Overview

The major entities of AgriConnect include:

| Entity              | Description                                |
| ------------------- | ------------------------------------------ |
| Users               | Stores registered platform users           |
| Roles               | Defines user access roles                  |
| Farmers             | Stores farmer-specific information         |
| Buyers              | Stores buyer-specific information          |
| Experts             | Stores agricultural expert information     |
| LogisticsProviders  | Stores logistics provider information      |
| Farms               | Stores farmer farm information             |
| ProductCategories   | Defines agricultural product categories    |
| Products            | Stores products listed in the marketplace  |
| Orders              | Stores customer orders                     |
| OrderItems          | Stores individual products within orders   |
| Payments            | Stores payment transaction information     |
| Deliveries          | Stores logistics and delivery information  |
| ExpertConsultations | Stores farmer-expert consultation requests |
| Reviews             | Stores ratings and reviews                 |
| Notifications       | Stores platform notifications              |
| AIRecommendations   | Stores AI-generated recommendations        |
| AuditLogs           | Records important system activities        |

---

# 5. Users Entity

## Table: `Users`

Stores the core identity and account information of platform users.

| Field         | Type         | Required | Key    | Description                                |
| ------------- | ------------ | -------: | ------ | ------------------------------------------ |
| Id            | UUID         |      Yes | PK     | Unique user identifier                     |
| FirstName     | VARCHAR(100) |      Yes |        | User's first name                          |
| LastName      | VARCHAR(100) |      Yes |        | User's last name                           |
| Email         | VARCHAR(255) |      Yes | UNIQUE | User email address                         |
| PhoneNumber   | VARCHAR(20)  |      Yes | UNIQUE | User phone number                          |
| PasswordHash  | TEXT         |      Yes |        | Hashed password                            |
| IsActive      | BOOLEAN      |      Yes |        | Indicates whether account is active        |
| EmailVerified | BOOLEAN      |      Yes |        | Indicates whether email is verified        |
| PhoneVerified | BOOLEAN      |      Yes |        | Indicates whether phone number is verified |
| CreatedAt     | TIMESTAMPTZ  |      Yes |        | Account creation timestamp                 |
| UpdatedAt     | TIMESTAMPTZ  |      Yes |        | Last account update                        |
| LastLoginAt   | TIMESTAMPTZ  |       No |        | Last successful login                      |

### Constraints

* `Id` is the primary key.
* `Email` must be unique.
* `PhoneNumber` must be unique.
* `PasswordHash` must never store plain-text passwords.
* `CreatedAt` is required.

---

# 6. Roles Entity

## Table: `Roles`

Defines the roles available within the platform.

| Field       | Type         | Required | Key    | Description            |
| ----------- | ------------ | -------: | ------ | ---------------------- |
| Id          | UUID         |      Yes | PK     | Unique role identifier |
| Name        | VARCHAR(50)  |      Yes | UNIQUE | Role name              |
| Description | VARCHAR(500) |       No |        | Role description       |
| CreatedAt   | TIMESTAMPTZ  |      Yes |        | Creation timestamp     |

### Supported Roles

* Farmer
* Buyer
* Agricultural Expert
* Logistics Provider
* Administrator

---

# 7. UserRoles Entity

## Table: `UserRoles`

Associates users with their roles.

| Field  | Type | Required | Key   | Description      |
| ------ | ---- | -------: | ----- | ---------------- |
| UserId | UUID |      Yes | PK/FK | References Users |
| RoleId | UUID |      Yes | PK/FK | References Roles |

### Relationship

```text
Users 1 ──────── * UserRoles * ──────── 1 Roles
```

A user may have one or more roles depending on system configuration.

---

# 8. Farmers Entity

## Table: `Farmers`

Contains information specific to farmers.

| Field                  | Type        | Required | Key       | Description                       |
| ---------------------- | ----------- | -------: | --------- | --------------------------------- |
| Id                     | UUID        |      Yes | PK        | Farmer record identifier          |
| UserId                 | UUID        |      Yes | FK/UNIQUE | References Users                  |
| FarmerCode             | VARCHAR(50) |      Yes | UNIQUE    | Unique farmer identification code |
| FarmingExperienceYears | INTEGER     |       No |           | Years of farming experience       |
| FarmCount              | INTEGER     |      Yes |           | Number of registered farms        |
| VerificationStatus     | VARCHAR(30) |      Yes |           | Farmer verification status        |
| CreatedAt              | TIMESTAMPTZ |      Yes |           | Record creation timestamp         |
| UpdatedAt              | TIMESTAMPTZ |      Yes |           | Last update timestamp             |

### Verification Status

Possible values:

```text
Pending
Verified
Rejected
Suspended
```

---

# 9. Buyers Entity

## Table: `Buyers`

Stores buyer-specific information.

| Field                   | Type         | Required | Key       | Description                            |
| ----------------------- | ------------ | -------: | --------- | -------------------------------------- |
| Id                      | UUID         |      Yes | PK        | Buyer identifier                       |
| UserId                  | UUID         |      Yes | FK/UNIQUE | References Users                       |
| BuyerType               | VARCHAR(50)  |      Yes |           | Individual, retailer, wholesaler, etc. |
| BusinessName            | VARCHAR(200) |       No |           | Business name                          |
| TaxIdentificationNumber | VARCHAR(100) |       No |           | Business tax identifier                |
| VerificationStatus      | VARCHAR(30)  |      Yes |           | Verification state                     |
| CreatedAt               | TIMESTAMPTZ  |      Yes |           | Creation timestamp                     |
| UpdatedAt               | TIMESTAMPTZ  |      Yes |           | Last update                            |

---

# 10. Experts Entity

## Table: `Experts`

Stores agricultural expert profiles.

| Field              | Type          | Required | Key       | Description                 |
| ------------------ | ------------- | -------: | --------- | --------------------------- |
| Id                 | UUID          |      Yes | PK        | Expert identifier           |
| UserId             | UUID          |      Yes | FK/UNIQUE | References Users            |
| Specialization     | VARCHAR(150)  |      Yes |           | Agricultural specialization |
| Qualification      | VARCHAR(255)  |      Yes |           | Professional qualification  |
| YearsOfExperience  | INTEGER       |      Yes |           | Professional experience     |
| Bio                | TEXT          |       No |           | Expert biography            |
| ConsultationFee    | DECIMAL(12,2) |       No |           | Consultation fee            |
| IsAvailable        | BOOLEAN       |      Yes |           | Current availability        |
| VerificationStatus | VARCHAR(30)   |      Yes |           | Expert verification state   |
| CreatedAt          | TIMESTAMPTZ   |      Yes |           | Creation timestamp          |
| UpdatedAt          | TIMESTAMPTZ   |      Yes |           | Last update                 |

---

# 11. LogisticsProviders Entity

## Table: `LogisticsProviders`

Stores information about logistics and delivery providers.

| Field              | Type         | Required | Key       | Description                  |
| ------------------ | ------------ | -------: | --------- | ---------------------------- |
| Id                 | UUID         |      Yes | PK        | Provider identifier          |
| UserId             | UUID         |      Yes | FK/UNIQUE | References Users             |
| BusinessName       | VARCHAR(200) |      Yes |           | Logistics business name      |
| LicenseNumber      | VARCHAR(100) |       No | UNIQUE    | Business license number      |
| VehicleCount       | INTEGER      |      Yes |           | Number of available vehicles |
| ServiceArea        | VARCHAR(255) |      Yes |           | Geographic service area      |
| VerificationStatus | VARCHAR(30)  |      Yes |           | Verification status          |
| CreatedAt          | TIMESTAMPTZ  |      Yes |           | Creation timestamp           |
| UpdatedAt          | TIMESTAMPTZ  |      Yes |           | Last update                  |

---

# 12. Farms Entity

## Table: `Farms`

Stores farms registered by farmers.

| Field               | Type          | Required | Key | Description             |
| ------------------- | ------------- | -------: | --- | ----------------------- |
| Id                  | UUID          |      Yes | PK  | Farm identifier         |
| FarmerId            | UUID          |      Yes | FK  | References Farmers      |
| FarmName            | VARCHAR(150)  |      Yes |     | Name of farm            |
| Region              | VARCHAR(100)  |      Yes |     | Ethiopian region        |
| Zone                | VARCHAR(100)  |       No |     | Administrative zone     |
| Woreda              | VARCHAR(100)  |       No |     | Woreda                  |
| Kebele              | VARCHAR(100)  |       No |     | Kebele                  |
| Latitude            | DECIMAL(10,7) |       No |     | Geographic latitude     |
| Longitude           | DECIMAL(10,7) |       No |     | Geographic longitude    |
| FarmSizeHectares    | DECIMAL(10,2) |      Yes |     | Farm size               |
| SoilType            | VARCHAR(100)  |       No |     | Soil classification     |
| IrrigationAvailable | BOOLEAN       |      Yes |     | Irrigation availability |
| CreatedAt           | TIMESTAMPTZ   |      Yes |     | Creation timestamp      |
| UpdatedAt           | TIMESTAMPTZ   |      Yes |     | Last update             |

---

# 13. ProductCategories Entity

## Table: `ProductCategories`

Defines marketplace product categories.

| Field       | Type         | Required | Key    | Description           |
| ----------- | ------------ | -------: | ------ | --------------------- |
| Id          | UUID         |      Yes | PK     | Category identifier   |
| Name        | VARCHAR(100) |      Yes | UNIQUE | Category name         |
| Description | TEXT         |       No |        | Category description  |
| IsActive    | BOOLEAN      |      Yes |        | Category availability |
| CreatedAt   | TIMESTAMPTZ  |      Yes |        | Creation timestamp    |

### Example Categories

* Cereals
* Vegetables
* Fruits
* Pulses
* Oil Seeds
* Livestock Products
* Dairy Products
* Coffee
* Spices

---

# 14. Products Entity

## Table: `Products`

Stores agricultural products listed by farmers or approved sellers.

| Field                | Type          | Required | Key | Description                 |
| -------------------- | ------------- | -------: | --- | --------------------------- |
| Id                   | UUID          |      Yes | PK  | Product identifier          |
| FarmerId             | UUID          |      Yes | FK  | Product owner               |
| CategoryId           | UUID          |      Yes | FK  | Product category            |
| Name                 | VARCHAR(200)  |      Yes |     | Product name                |
| Description          | TEXT          |       No |     | Product description         |
| Quantity             | DECIMAL(12,2) |      Yes |     | Available quantity          |
| Unit                 | VARCHAR(30)   |      Yes |     | Measurement unit            |
| PricePerUnit         | DECIMAL(12,2) |      Yes |     | Product price               |
| MinimumOrderQuantity | DECIMAL(12,2) |       No |     | Minimum purchase quantity   |
| QualityGrade         | VARCHAR(50)   |       No |     | Product quality grade       |
| ProductionDate       | DATE          |       No |     | Production/harvest date     |
| ExpiryDate           | DATE          |       No |     | Expiry date when applicable |
| Region               | VARCHAR(100)  |      Yes |     | Product location            |
| Status               | VARCHAR(30)   |      Yes |     | Product listing status      |
| CreatedAt            | TIMESTAMPTZ   |      Yes |     | Creation timestamp          |
| UpdatedAt            | TIMESTAMPTZ   |      Yes |     | Last update                 |

### Product Status

```text
Draft
PendingApproval
Active
SoldOut
Inactive
Rejected
```

---

# 15. Orders Entity

## Table: `Orders`

Stores customer purchase orders.

| Field           | Type          | Required | Key    | Description                 |
| --------------- | ------------- | -------: | ------ | --------------------------- |
| Id              | UUID          |      Yes | PK     | Order identifier            |
| BuyerId         | UUID          |      Yes | FK     | Buyer who placed the order  |
| OrderNumber     | VARCHAR(50)   |      Yes | UNIQUE | Human-readable order number |
| OrderDate       | TIMESTAMPTZ   |      Yes |        | Date order was created      |
| SubTotal        | DECIMAL(14,2) |      Yes |        | Products subtotal           |
| DeliveryFee     | DECIMAL(12,2) |      Yes |        | Delivery cost               |
| TotalAmount     | DECIMAL(14,2) |      Yes |        | Final order amount          |
| Status          | VARCHAR(30)   |      Yes |        | Current order status        |
| DeliveryAddress | TEXT          |      Yes |        | Delivery destination        |
| CreatedAt       | TIMESTAMPTZ   |      Yes |        | Creation timestamp          |
| UpdatedAt       | TIMESTAMPTZ   |      Yes |        | Last update                 |

### Order Status

```text
Pending
Confirmed
Processing
Shipped
Delivered
Cancelled
Completed
```

---

# 16. OrderItems Entity

## Table: `OrderItems`

Stores individual products belonging to an order.

| Field      | Type          | Required | Key | Description               |
| ---------- | ------------- | -------: | --- | ------------------------- |
| Id         | UUID          |      Yes | PK  | Order item identifier     |
| OrderId    | UUID          |      Yes | FK  | References Orders         |
| ProductId  | UUID          |      Yes | FK  | References Products       |
| Quantity   | DECIMAL(12,2) |      Yes |     | Ordered quantity          |
| UnitPrice  | DECIMAL(12,2) |      Yes |     | Product price at purchase |
| TotalPrice | DECIMAL(14,2) |      Yes |     | Quantity × UnitPrice      |

The `UnitPrice` is stored at the time of purchase so that historical orders remain accurate even if the product price changes later.

---

# 17. Payments Entity

## Table: `Payments`

Stores payment transaction information.

| Field                | Type          | Required | Key    | Description                   |
| -------------------- | ------------- | -------: | ------ | ----------------------------- |
| Id                   | UUID          |      Yes | PK     | Payment identifier            |
| OrderId              | UUID          |      Yes | FK     | Related order                 |
| TransactionReference | VARCHAR(100)  |      Yes | UNIQUE | Payment transaction reference |
| Amount               | DECIMAL(14,2) |      Yes |        | Amount paid                   |
| PaymentMethod        | VARCHAR(50)   |      Yes |        | Payment method                |
| Status               | VARCHAR(30)   |      Yes |        | Payment status                |
| PaidAt               | TIMESTAMPTZ   |       No |        | Payment completion time       |
| CreatedAt            | TIMESTAMPTZ   |      Yes |        | Creation timestamp            |

### Payment Methods

Examples:

```text
Telebirr
CBE Birr
BankTransfer
Card
CashOnDelivery
```

### Payment Status

```text
Pending
Processing
Successful
Failed
Refunded
Cancelled
```

---

# 18. Deliveries Entity

## Table: `Deliveries`

Tracks product delivery and logistics operations.

| Field                 | Type         | Required | Key    | Description                 |
| --------------------- | ------------ | -------: | ------ | --------------------------- |
| Id                    | UUID         |      Yes | PK     | Delivery identifier         |
| OrderId               | UUID         |      Yes | FK     | Related order               |
| LogisticsProviderId   | UUID         |       No | FK     | Assigned logistics provider |
| TrackingNumber        | VARCHAR(100) |      Yes | UNIQUE | Delivery tracking number    |
| PickupLocation        | TEXT         |      Yes |        | Product pickup location     |
| DeliveryAddress       | TEXT         |      Yes |        | Destination                 |
| EstimatedDeliveryDate | DATE         |       No |        | Expected delivery date      |
| ActualDeliveryDate    | TIMESTAMPTZ  |       No |        | Actual delivery date        |
| Status                | VARCHAR(30)  |      Yes |        | Delivery status             |
| CreatedAt             | TIMESTAMPTZ  |      Yes |        | Creation timestamp          |
| UpdatedAt             | TIMESTAMPTZ  |      Yes |        | Last update                 |

### Delivery Status

```text
Pending
Assigned
PickedUp
InTransit
Delivered
Failed
Cancelled
```

---

# 19. ExpertConsultations Entity

## Table: `ExpertConsultations`

Stores requests for agricultural consultation.

| Field          | Type         | Required | Key | Description                 |
| -------------- | ------------ | -------: | --- | --------------------------- |
| Id             | UUID         |      Yes | PK  | Consultation identifier     |
| FarmerId       | UUID         |      Yes | FK  | Requesting farmer           |
| ExpertId       | UUID         |      Yes | FK  | Assigned expert             |
| Subject        | VARCHAR(255) |      Yes |     | Consultation topic          |
| Description    | TEXT         |      Yes |     | Farmer's problem/question   |
| ScheduledAt    | TIMESTAMPTZ  |       No |     | Scheduled consultation time |
| Status         | VARCHAR(30)  |      Yes |     | Consultation status         |
| ExpertResponse | TEXT         |       No |     | Expert response             |
| CreatedAt      | TIMESTAMPTZ  |      Yes |     | Request timestamp           |
| UpdatedAt      | TIMESTAMPTZ  |      Yes |     | Last update                 |

---

# 20. Reviews Entity

## Table: `Reviews`

Stores customer reviews and ratings.

| Field      | Type        | Required | Key | Description        |
| ---------- | ----------- | -------: | --- | ------------------ |
| Id         | UUID        |      Yes | PK  | Review identifier  |
| BuyerId    | UUID        |      Yes | FK  | Reviewer           |
| ProductId  | UUID        |      Yes | FK  | Reviewed product   |
| OrderId    | UUID        |       No | FK  | Related order      |
| Rating     | INTEGER     |      Yes |     | Rating from 1 to 5 |
| Comment    | TEXT        |       No |     | Review comment     |
| IsApproved | BOOLEAN     |      Yes |     | Moderation status  |
| CreatedAt  | TIMESTAMPTZ |      Yes |     | Review timestamp   |

### Validation

```text
Rating >= 1
Rating <= 5
```

---

# 21. Notifications Entity

## Table: `Notifications`

Stores notifications sent to users.

| Field     | Type         | Required | Key | Description                |
| --------- | ------------ | -------: | --- | -------------------------- |
| Id        | UUID         |      Yes | PK  | Notification identifier    |
| UserId    | UUID         |      Yes | FK  | Recipient                  |
| Title     | VARCHAR(200) |      Yes |     | Notification title         |
| Message   | TEXT         |      Yes |     | Notification content       |
| Type      | VARCHAR(50)  |      Yes |     | Notification type          |
| IsRead    | BOOLEAN      |      Yes |     | Read status                |
| ReadAt    | TIMESTAMPTZ  |       No |     | Time notification was read |
| CreatedAt | TIMESTAMPTZ  |      Yes |     | Creation timestamp         |

### Notification Types

```text
Order
Payment
Delivery
Consultation
Marketplace
System
Security
AI
```

---

# 22. AIRecommendations Entity

## Table: `AIRecommendations`

Stores AI-generated recommendations for users.

| Field              | Type         | Required | Key | Description                  |
| ------------------ | ------------ | -------: | --- | ---------------------------- |
| Id                 | UUID         |      Yes | PK  | Recommendation identifier    |
| FarmerId           | UUID         |      Yes | FK  | Target farmer                |
| RecommendationType | VARCHAR(50)  |      Yes |     | Type of recommendation       |
| InputData          | JSONB        |       No |     | Input information used by AI |
| Recommendation     | TEXT         |      Yes |     | Generated recommendation     |
| ConfidenceScore    | DECIMAL(5,4) |       No |     | AI confidence score          |
| CreatedAt          | TIMESTAMPTZ  |      Yes |     | Generation timestamp         |

### Recommendation Types

Examples:

```text
CropSelection
DiseaseDetection
MarketPrice
FarmingAdvice
WeatherAdvice
ProductRecommendation
```

---

# 23. AuditLogs Entity

## Table: `AuditLogs`

Records important system activities for security and accountability.

| Field      | Type         | Required | Key | Description                   |
| ---------- | ------------ | -------: | --- | ----------------------------- |
| Id         | UUID         |      Yes | PK  | Audit record identifier       |
| UserId     | UUID         |       No | FK  | User who performed the action |
| Action     | VARCHAR(100) |      Yes |     | Performed action              |
| EntityName | VARCHAR(100) |      Yes |     | Affected entity               |
| EntityId   | VARCHAR(100) |       No |     | Affected record               |
| OldValues  | JSONB        |       No |     | Previous values               |
| NewValues  | JSONB        |       No |     | Updated values                |
| IpAddress  | INET         |       No |     | Request IP address            |
| UserAgent  | TEXT         |       No |     | Client information            |
| CreatedAt  | TIMESTAMPTZ  |      Yes |     | Action timestamp              |

---

# 24. Common Data Types

| PostgreSQL Type | Usage                               |
| --------------- | ----------------------------------- |
| UUID            | Entity identifiers                  |
| VARCHAR(n)      | Short text                          |
| TEXT            | Long text                           |
| INTEGER         | Whole numbers                       |
| BIGINT          | Large whole numbers                 |
| DECIMAL(p,s)    | Monetary and precise numeric values |
| BOOLEAN         | True/false values                   |
| DATE            | Date without time                   |
| TIMESTAMPTZ     | Date and time with timezone         |
| JSONB           | Structured or flexible JSON data    |
| INET            | IP addresses                        |

---

# 25. Common Audit Fields

Where appropriate, entities should contain:

```text
CreatedAt
UpdatedAt
```

These fields provide traceability for when records were created and modified.

For critical entities, additional audit information may include:

```text
CreatedBy
UpdatedBy
DeletedAt
DeletedBy
```

Soft deletion should be considered where historical records must be preserved.

---

# 26. Primary Key Standards

Every major entity should have a unique primary key.

Recommended standard:

```text
Id UUID PRIMARY KEY
```

Example:

```text
Users.Id
Farmers.Id
Products.Id
Orders.Id
Payments.Id
Deliveries.Id
```

---

# 27. Foreign Key Standards

Foreign keys maintain relationships between entities.

Examples:

```text
Farmers.UserId
Farms.FarmerId
Products.FarmerId
Products.CategoryId
Orders.BuyerId
OrderItems.OrderId
OrderItems.ProductId
Payments.OrderId
Deliveries.OrderId
Deliveries.LogisticsProviderId
Reviews.ProductId
Notifications.UserId
```

Foreign key constraints should prevent orphaned records.

---

# 28. Indexing Strategy

Indexes should be created for frequently searched or joined fields.

Recommended indexes include:

```text
Users.Email
Users.PhoneNumber
Farmers.FarmerCode
Products.FarmerId
Products.CategoryId
Products.Status
Products.Region
Orders.BuyerId
Orders.OrderNumber
Orders.Status
Payments.TransactionReference
Deliveries.TrackingNumber
Notifications.UserId
```

Indexes should be added based on actual query patterns and measured performance.

---

# 29. Data Validation Rules

Important validation rules include:

### User

```text
Email must be valid.
Email must be unique.
Phone number must be valid.
Password must be securely hashed.
```

### Product

```text
Quantity > 0
PricePerUnit >= 0
MinimumOrderQuantity > 0
```

### Order

```text
TotalAmount >= 0
Order must contain at least one OrderItem.
```

### Review

```text
Rating must be between 1 and 5.
```

### Farm

```text
FarmSizeHectares > 0
Latitude must be between -90 and 90.
Longitude must be between -180 and 180.
```

---

# 30. Sensitive Data

The following information requires special protection:

* Password hashes
* Phone numbers
* Email addresses
* Payment information
* Business identification information
* User location information
* IP addresses
* Authentication and authorization information

Sensitive information should not be exposed through public API responses unless required.

---

# 31. Data Retention

AgriConnect should retain important business records such as:

* Completed orders
* Payment records
* Delivery records
* Reviews
* Consultation records
* Audit logs

Records should not be permanently deleted when doing so would compromise financial, legal, security, or business history.

Where appropriate, soft deletion or archival should be used.

---

# 32. Entity Relationship Summary

The major relationships are:

```text
Users
  │
  ├── Farmers
  │     └── Farms
  │           └── Products
  │
  ├── Buyers
  │     └── Orders
  │           └── OrderItems
  │                 └── Products
  │
  ├── Experts
  │     └── ExpertConsultations
  │
  └── LogisticsProviders
        └── Deliveries
```

Supporting relationships:

```text
Products ─────── ProductCategories

Orders ───────── Payments

Orders ───────── Deliveries

Products ─────── Reviews

Users ────────── Notifications

Farmers ──────── AIRecommendations

Users ────────── AuditLogs
```

---

# 33. Data Integrity Requirements

The database should maintain:

* Referential integrity
* Unique constraints
* Required-field constraints
* Appropriate numeric constraints
* Valid status values
* Valid date ranges
* Transaction consistency
* Proper foreign-key relationships

Critical operations such as order creation and payment processing should use database transactions where appropriate.

---

# 34. Security Requirements

Database security should include:

1. Strong database credentials.
2. No hard-coded production credentials.
3. Encrypted connections in production.
4. Least-privilege database access.
5. Regular backups.
6. Restricted database network access.
7. Secure storage of connection strings.
8. Protection of personally identifiable information.
9. Audit logging for sensitive operations.
10. Regular security reviews.

---

# 35. Future Database Extensions

The database can later be extended with:

* Crop production records
* Weather data
* Soil analysis
* IoT sensor data
* Market price history
* Agricultural cooperatives
* Warehouse management
* Inventory management
* Subscription plans
* Digital payments integration
* AI disease detection records
* AI crop yield prediction
* Real-time delivery tracking
* Multilingual content
* Ethiopian regional agricultural datasets

---

# 36. Document Status

| Version | Date       | Status | Description                         |
| ------- | ---------- | ------ | ----------------------------------- |
| 1.0     | 2026-08-08 | Draft  | Initial AgriConnect data dictionary |

---

## Conclusion

This data dictionary provides the foundational reference for the AgriConnect Ethiopia database.

It establishes a consistent understanding of the platform's major data entities, attributes, relationships, constraints, validation rules, and security considerations.

The dictionary should be updated whenever the database schema changes so that the documentation remains synchronized with the implemented system.
