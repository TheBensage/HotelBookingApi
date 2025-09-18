# HotelBooking API

A simple hotel room booking API built with ASP.NET Core and Entity Framework Core (SQLite), following RESTful principles.

It allows searching for hotels, checking room availability, creating bookings, and retrieving booking details. Designed for testability with seed and reset functionality.

Features

- Manage hotels with 3 room types: Single, Double, Deluxe
- Each hotel has 6 rooms with capacities based on type
- Prevents double bookings or over-capacity bookings
- Unique booking references
- Find hotels by name
- Check available rooms between two dates for a number of guests
- Create bookings without requiring room changes mid-stay
- Retrieve booking details by reference
- Swagger/OpenAPI documentation included
- Seed and reset database functionality for testing

## Project Structure

- **HotelBooking/**  
  - **HotelBooking.Api/** – ASP.NET Core API  
    - **Controllers/** – Single controller for hotel & booking endpoints  
  - **HotelBooking.Application/** – Application layer  
    - **DTOs/** – Data Transfer Objects  
    - **Queries/Commands/Handlers/** – CQRS pattern for queries and commands  
    - **Services/Interfaces/** – Service interfaces for hotel & booking logic  
    - **Responses/** – Standardized Response & Pagination objects  
  - **HotelBooking.Domain/** – Domain models and enums  
  - **HotelBooking.Infrastructure/** – Infrastructure layer  
    - **Persistence/** – EF Core DbContext  
    - **Services/** – Implementations of services for DB operations  
    - **DependencyInjection.cs** – Registers services and DbContext


### Why this structure:
- Separation of concerns: Controllers are thin, logic lives in Application Services
- Domain models are isolated in the Domain layer
- Infrastructure handles EF Core, seeding, and persistence
- CQRS (Commands/Queries/Handlers) makes adding features like booking or search straightforward

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQLite (database included as hotel.db)

### Running Locally

1. Clone the repository:  

`git clone https://github.com/TheBensage/HotelBookingApi.git`
`cd HotelBooking`


2. Run the API:

`dotnet run --project HotelBooking.Api`

3. Open Swagger UI to explore endpoints:

`https://localhost:7149/swagger/index.html`
