# AgriConnect Ethiopia — Entity Relationship Design

## 1. Overview

The Entity Relationship Design defines the relationships between the major entities in the AgriConnect Ethiopia platform.

The database follows a relational model in which entities are connected through primary keys and foreign keys.

The main database domains include:

* Users and identity
* Farmers
* Buyers
* Agricultural experts
* Logistics providers
* Farms
* Agricultural products
* Product categories
* Orders
* Order items
* Consultations
* Deliveries
* Notifications
* AI requests

---

## 2. Core Entities

The main entities represented in the AgriConnect database are:

```text
User
Farmer
Buyer
Expert
LogisticsProvider
Farm
Product
ProductCategory
Order
OrderItem
Consultation
Delivery
Notification
AIRequest
```

---

## 3. Entity Relationship Overview

The high-level relationship structure is:

```text
                              ┌──────────────┐
                              │     User     │
                              └──────┬───────┘
                                     │
                  ┌──────────────────┼──────────────────┐
                  │                  │                  │
                  ▼                  ▼                  ▼
             ┌─────────┐        ┌─────────┐        ┌─────────┐
             │ Farmer  │        │  Buyer  │        │ Expert  │
             └────┬────┘        └────┬────┘        └────┬────┘
                  │                  │                  │
                  ▼                  │                  ▼
             ┌─────────┐             │           ┌─────────────┐
             │  Farm   │             │           │Consultation │
             └─────────┘             │           └─────────────┘
                  │                  │
                  ▼                  ▼
             ┌─────────┐        ┌─────────┐
             │ Product │◄───────┤  Order  │
             └────┬────┘        └────┬────┘
                  │                  │
                  │                  ▼
                  │             ┌────────────┐
                  │             │ OrderItem  │
                  │             └────────────┘
                  │
                  ▼
          ┌─────────────────┐
          │ ProductCategory │
          └─────────────────┘

                  Order
                    │
                    ▼
              ┌──────────┐
              │ Delivery │
              └────┬─────┘
                   │
                   ▼
          ┌───────────────────┐
          │LogisticsProvider  │
          └───────────────────┘

             User
              │
       ┌──────┴────────┐
       ▼               ▼
┌──────────────┐ ┌───────────┐
│Notification  │ │ AIRequest │
└──────────────┘ └───────────┘
```

---

## 4. User Relationships

### User and Farmer

A user can have a farmer profile.

Relationship:

```text
User 1 ───────── 0..1 Farmer
```

The `Farmer.UserId` foreign key references `User.UserId`.

---

### User and Buyer

A user can have a buyer profile.

```text
User 1 ───────── 0..1 Buyer
```

The `Buyer.UserId` foreign key references `User.UserId`.

---

### User and Expert

A user can have an expert profile.

```text
User 1 ───────── 0..1 Expert
```

The `Expert.UserId` foreign key references `User.UserId`.

---

### User and Logistics Provider

A user may manage a logistics provider profile.

```text
User 1 ───────── 0..1 LogisticsProvider
```

---

## 5. Farmer Relationships

### Farmer and Farm

A farmer can manage one or more farms.

```text
Farmer 1 ───────── * Farm
```

Foreign key:

```text
Farm.FarmerId → Farmer.FarmerId
```

---

### Farmer and Product

A farmer can publish multiple agricultural products.

```text
Farmer 1 ───────── * Product
```

Foreign key:

```text
Product.FarmerId → Farmer.FarmerId
```

---

## 6. Product Relationships

### Product Category and Product

A product belongs to a product category.

```text
ProductCategory 1 ───────── * Product
```

Foreign key:

```text
Product.CategoryId → ProductCategory.CategoryId
```

Example categories include:

* Cereals
* Vegetables
* Fruits
* Pulses
* Dairy
* Livestock
* Seeds
* Other agricultural products

---

### Product and Order Item

A product can appear in multiple order items.

```text
Product 1 ───────── * OrderItem
```

Foreign key:

```text
OrderItem.ProductId → Product.ProductId
```

---

## 7. Buyer and Order Relationships

A buyer can create multiple orders.

```text
Buyer 1 ───────── * Order
```

Foreign key:

```text
Order.BuyerId → Buyer.BuyerId
```

An order belongs to one buyer.

---

## 8. Order and Order Item Relationships

An order contains one or more order items.

```text
Order 1 ───────── * OrderItem
```

Foreign key:

```text
OrderItem.OrderId → Order.OrderId
```

An order item identifies:

* Product
* Quantity
* Unit price
* Subtotal

This structure allows one order to contain multiple agricultural products.

---

## 9. Order and Product Relationship

The relationship between orders and products is many-to-many conceptually.

```text
Order * ───────── * Product
```

This relationship is resolved using the `OrderItem` entity.

```text
Order
  │
  │ 1
  ▼
OrderItem
  ▲
  │ *
  │
Product
```

Therefore:

```text
Order 1 ───────── * OrderItem
Product 1 ─────── * OrderItem
```

---

## 10. Order and Delivery

An order may have an associated delivery.

```text
Order 1 ───────── 0..1 Delivery
```

Foreign key:

```text
Delivery.OrderId → Order.OrderId
```

The delivery contains information such as:

* Pickup location
* Delivery location
* Delivery status
* Assigned logistics provider
* Estimated delivery date
* Actual delivery date

---

## 11. Logistics Provider and Delivery

A logistics provider can manage multiple deliveries.

```text
LogisticsProvider 1 ───────── * Delivery
```

Foreign key:

```text
Delivery.LogisticsProviderId
        ↓
LogisticsProvider.LogisticsProviderId
```

---

## 12. Expert and Consultation

An agricultural expert can handle multiple consultations.

```text
Expert 1 ───────── * Consultation
```

Foreign key:

```text
Consultation.ExpertId → Expert.ExpertId
```

A consultation can contain:

* Subject
* Farmer/user question
* Expert response
* Status
* Created date
* Completion date

---

## 13. User and Consultation

A user can create multiple consultation requests.

```text
User 1 ───────── * Consultation
```

Foreign key:

```text
Consultation.UserId → User.UserId
```

This allows the platform to maintain consultation history.

---

## 14. User and Notification

A user can receive multiple notifications.

```text
User 1 ───────── * Notification
```

Foreign key:

```text
Notification.UserId → User.UserId
```

Notifications may include:

* New order
* Order status update
* Delivery update
* Expert response
* Marketplace activity
* Administrative messages
* AI service updates

---

## 15. User and AI Request

A user can make multiple AI requests.

```text
User 1 ───────── * AIRequest
```

Foreign key:

```text
AIRequest.UserId → User.UserId
```

An AI request may contain:

* User question
* Request type
* AI response
* Created timestamp

Example AI services include:

* Crop recommendations
* Agricultural questions
* Pest and disease guidance
* Farming recommendations
* Market-related assistance

---

## 16. Cardinality Summary

| Relationship                 | Cardinality |
| ---------------------------- | ----------- |
| User → Farmer                | 1 : 0..1    |
| User → Buyer                 | 1 : 0..1    |
| User → Expert                | 1 : 0..1    |
| User → LogisticsProvider     | 1 : 0..1    |
| Farmer → Farm                | 1 : *       |
| Farmer → Product             | 1 : *       |
| ProductCategory → Product    | 1 : *       |
| Buyer → Order                | 1 : *       |
| Order → OrderItem            | 1 : *       |
| Product → OrderItem          | 1 : *       |
| Order → Delivery             | 1 : 0..1    |
| LogisticsProvider → Delivery | 1 : *       |
| User → Consultation          | 1 : *       |
| Expert → Consultation        | 1 : *       |
| User → Notification          | 1 : *       |
| User → AIRequest             | 1 : *       |

---

## 17. Referential Integrity

Foreign keys are used to maintain referential integrity.

For example:

```text
Product.FarmerId
        ↓
Farmer.FarmerId
```

A product cannot reference a farmer that does not exist.

Similarly:

```text
Order.BuyerId
        ↓
Buyer.BuyerId
```

An order must belong to a valid buyer.

---

## 18. Delete Behavior

Delete behavior should be carefully controlled.

Recommended behavior:

### User

Deleting a user should not automatically delete critical historical transaction data.

### Farmer

A farmer with existing marketplace records should preferably be deactivated rather than permanently deleted.

### Product

Products may be marked inactive instead of permanently removed.

### Order

Orders should normally be preserved for historical and reporting purposes.

### OrderItem

Order items should remain associated with their historical orders.

### Notification

Old notifications may be archived or periodically cleaned according to retention policies.

---

## 19. Example Data Flow

A typical marketplace transaction follows this relationship:

```text
Farmer
   │
   ▼
Product
   │
   ▼
Buyer
   │
   ▼
Order
   │
   ▼
OrderItem
   │
   ▼
Delivery
   │
   ▼
LogisticsProvider
```

At the same time, the system can generate:

```text
Order
   │
   ▼
Notification
   │
   ▼
Buyer
```

---

## 20. Expert Service Flow

A typical expert consultation follows:

```text
Farmer/User
     │
     ▼
Consultation Request
     │
     ▼
Agricultural Expert
     │
     ▼
Expert Response
     │
     ▼
Notification
```

This allows farmers to receive agricultural guidance through the platform.

---

## 21. AI Service Flow

The AI service relationship follows:

```text
User
 │
 ▼
AIRequest
 │
 ▼
AI Processing
 │
 ▼
AI Response
 │
 ▼
User
```

AI requests may later be extended to reference crops, farms, locations, weather information, or marketplace information.

---

## 22. ERD Diagram

The visual Entity Relationship Diagram will be stored at:

```text
04-database/diagrams/erd.png
```

The diagram should visually represent:

* Primary keys
* Foreign keys
* Entities
* Relationships
* Cardinalities

The diagram should remain synchronized with the implemented database schema.

---

## 23. Database Design Principles

The ER design follows these principles:

1. Each entity represents a clearly defined business concept.
2. Each entity has a unique primary key.
3. Relationships are represented using foreign keys.
4. Many-to-many relationships use junction entities.
5. Historical transaction records are preserved.
6. Data duplication is minimized.
7. Referential integrity is enforced.
8. Sensitive information is protected.
9. Database structure supports future expansion.
10. The ERD should reflect the actual implemented database.

---

## 24. Future Extensions

Future versions may add relationships for:

* Payments
* Reviews and ratings
* Crop records
* Farm activities
* Weather data
* Market prices
* IoT devices
* Agricultural disease records
* AI recommendations
* Geographical locations
* Government agricultural programs

These entities should be introduced only when the corresponding features are implemented.

---

## 25. Conclusion

The AgriConnect Ethiopia Entity Relationship Design provides a structured representation of the platform's major business entities and their relationships.

The design supports the complete agricultural ecosystem:

```text
Farmers
   ↓
Products
   ↓
Marketplace
   ↓
Buyers
   ↓
Orders
   ↓
Logistics
   ↓
Delivery
```

while also supporting:

```text
Farmers → Agricultural Experts → Consultations
Users → AI Services
Users → Notifications
Administrators → Platform Management
```

The ERD provides the foundation for implementing and maintaining a consistent relational database for AgriConnect Ethiopia.
