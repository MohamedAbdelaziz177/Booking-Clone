


# 🏨 Booking-Clone

A production-grade **hotel reservation system** built using **Clean Architecture**, **Vertical Slice Architecture (VSA)**, and **CQRS**. It offers secure and scalable hotel booking functionality with modern .NET practices and full infrastructure integration.

> 🚀 Designed with extensibility, scalability, and maintainability in mind.

---

## 🧱 Architecture Overview

- **Vertical Slice Architecture (VSA)**: Features are self-contained with commands, queries, and validations.
- **Clean Architecture**: Layers are clearly separated (API, Application, Domain, Infrastructure).
- **CQRS (Command Query Responsibility Segregation)**: Cleanly separates reads from writes using MediatR.
- **DDD (Domain-Driven Design)**: Rich domain entities and encapsulated business logic.
- **SOLID principles** and Dependency Injection across the stack.

---

## 🚀 Features

### 🔐 Authentication & Security

- 🔑 JWT-based **authentication** with **refresh token** support.
- 🔒 **Role-based authorization** with clean separation of concerns.
- 🛡️ **Global exception handling** middleware for standardized error responses.
- 🔁 **Refresh token flow** for session persistence with token rotation.
- 🔁 **Replay protection**:
  - Prevents **double payment** for the same reservation.
  - Prevents **double booking** of the same room using **serializable transactions** (Serializable Isolation)..
- 🧵 **Rate Limiting** (built-in protection for abuse/throttling):
  - **Token Bucket**: Used for authentication endpoints (e.g., login attempts).
  - **Sliding Window**: Applied to general endpoints with burst traffic tolerance (e.g., hotel/room search).

---

### 🏨 Hotel Booking System

- Full **CRUD** operations for Hotels, Rooms, Reservations, and Users.
- Automatic **background cancellation** of **expired or unconfirmed reservations**.
- **Transactional room booking** with validation on availability and duplication.
- Filters out unavailable rooms during user search or reservation.
- Ensures booking/payment integrity by preventing conflicting or duplicate actions.

---

### 📊 Filtering, Pagination & Sorting

- Advanced filtering on room/hotel listings:
  - `minPrice`, `maxPrice`
  - `availableBetween` (`startDate`, `endDate`)
- Integrated **pagination** and **sorting** support via query parameters.

---

### ⚙️ Background Jobs

- Uses **Hangfire** to manage background services and recurring jobs.
- Automatically cancels **pending** reservations after timeout windows.
- Automatically sends Email notification for users after Reservation to start **payment** process
- Automatically sends Email notification for users after Reservation Cancellation to start **Refund** process

---

### ✉️ Notifications & External Integrations

- 📧 **Email sending** via MailKit for confirmations, alerts, and notifications.
- 💳 **Stripe** integration for secure and real-time payments.
- ☁️ **Cloudinary** integration for handling file and image uploads.
- ⚡ **Redis** for caching sessions, refresh tokens, and rate limiting tokens.

---

## 🔧 Tech Stack

| Layer               | Technologies |
|--------------------|--------------|
| **API**            | ASP.NET Core, Swagger |
| **Application**    | MediatR, CQRS, FluentValidation, Mapster |
| **Domain**         | Rich Entities, Domain Events, Enums |
| **Infrastructure** | PostgreSQL, Redis, Hangfire, Stripe, Cloudinary, MailKit |
| **Auth**           | JWT + Refresh Tokens |
| **Validation**     | FluentValidation |
| **Mapping**        | Mapster |
| **Background Jobs**| Hangfire |
| **File Uploads**   | Cloudinary |
| **Email**          | MailKit |
| **Rate Limiting**  | SlidingWindow, TokenBucket (custom middleware / native support) |

---

## 📁 Project Structure

```

Booking-Clone/
│
├── BookingClone.Api/               → API layer (controllers, auth, middleware)
├── BookingClone.Application/      → Application logic (CQRS handlers, validators)
├── BookingClone.Domain/           → Domain models, Repositories contracts, Encapsuled logic
├── BookingClone.Infrastructure/   → External integrations (DB, Redis, Email, Stripe, etc.)
│
├── docker-compose.yaml            → Local containers for Redis, Postgres, etc.
├── BookingClone.sln               → Visual Studio Solution File

````

---

## 🧪 Developer Experience

- 🧼 **Global Exception Handler** for consistent error responses.
- 🧩 **IoC Registration** for automatic dependency wiring.
- 🐳 **Docker Compose** for easy local setup.
- 🧪 Fully testable domain and application layers (no infrastructure leakage).

---

## 🧭 Getting Started

```bash
# 1. Clone the repository
git clone https://github.com/MohamedAbdelaziz177/Booking-Clone.git
cd Booking-Clone

# 2. Start infrastructure (PostgreSQL, Redis, PgAdmin)
docker-compose up -d

# 3. Apply migrations
dotnet ef database update --project BookingClone.Infrastructure

# 4. Run the API
dotnet run --project BookingClone.Api
````

---

## 🛡️ Key Safeguards

* ✅ **Serializable Transactions** for concurrency-safe bookings.
* ✅ **Duplicate Payment & Reservation Protection** (idempotent handlers).
* ✅ **JWT + Refresh Tokens** with full session expiry handling.
* ✅ **FluentValidation** for all request inputs.
* ✅ **Rate Limiting**:

  * `TokenBucket`: Limits brute-force attacks on login.
  * `SlidingWindow`: Allows burst requests with a refill strategy for search endpoints.
* ✅ **Background Cancellation** of expired reservations ensures data hygiene and consistency.

---

## 📬 Contributions

For contributions, issues, or feature requests, feel free to fork the repo, open an issue, or submit a pull request.


