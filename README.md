# Event-Driven Microservices with CQRS (.NET 10)

This repository demonstrates a production-style microservices architecture using **CQRS**, **Clean Architecture**, and **event-driven communication** with **Kafka**.

The project was built with a practical mindset: to learn how real distributed systems are designed, structured, and executed using modern .NET.

---

## 🧱 Architecture Overview

The system is composed of three independent microservices:

```text
Order Service (Write / Command)
   └─ Creates orders
   └─ Persists in MongoDB (write model)
   └─ Publishes OrderCreatedEvent to Kafka

Order Read Service (Read / Query)
   └─ Consumes OrderCreatedEvent
   └─ Projects data into a read model (MongoDB)
   └─ Uses Redis for caching
   └─ Invalidates cache on new events

Notification Service (Worker)
   └─ Consumes OrderCreatedEvent
   └─ Simulates sending notifications (log output)
```

All communication between services is asynchronous and event-driven.

Each microservice is isolated and follows **Clean Architecture**:

```text
Domain
Application
Infrastructure
Host (API or Worker)
```

Dependency direction:

```text
Host → Infrastructure → Application → Domain
```

The core layers (`Domain`, `Application`) are completely independent of frameworks and technologies.

---

## 🔁 Event Flow

```text
POST /orders (Order Service)
   ↓
MongoDB (Write)
   ↓
Kafka (OrderCreatedEvent)
   ↓
Order Read Service
   ├─ MongoDB (Read)
   └─ Redis (Cache)
   ↓
GET /orders

Kafka (OrderCreatedEvent)
   ↓
Notification Service
   └─ "Send" notification (console log)
```

Multiple services react to the same event using **different consumer groups**, ensuring fan-out behavior.

---

## 🛠 Technologies Used

* **.NET 10**
* **ASP.NET Core**
* **Worker Services**
* **Kafka (Confluent.Kafka)**
* **MongoDB**
* **Redis**
* **Docker / Docker Compose**
* **CQRS**
* **Clean Architecture**

---

## 🚀 Running the Project

1. Start the infrastructure:

```bash
docker compose up -d
```

2. Run the services:

* `OrderService.API`
* `OrderReadService.API`
* `NotificationService.Worker`

3. Create an order:

```http
POST /orders
{
  "customerName": "Lucas",
  "totalAmount": 199.90
}
```

4. Observe:

* The order appears in `GET /orders`
* Redis cache is created and invalidated on new events
* Notification Service logs:

```text
📩 Notification sent to Lucas | Order: ... | Total: $199.90
```

---

## 🎯 Goals of This Project

* Practice real-world microservice architecture
* Understand CQRS in a distributed environment
* Learn event-driven communication with Kafka
* Apply Clean Architecture correctly in .NET
* Build a portfolio-ready system similar to production setups

This project reflects how modern, scalable backends are built in real companies.
