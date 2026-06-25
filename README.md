# Tracking Web App

## Description

Tracking Web App is an ASP.NET Core MVC application designed to collect, store, and analyze user interactions on a website.

The application provides a complete tracking solution consisting of a public website, a REST API, and an administrative dashboard. User interactions are captured, stored in a SQLite database, and presented through an intuitive interface for analysis.

The project demonstrates the implementation of a custom analytics platform without relying on third-party tracking services.

---

## Features

- Track website user interactions
- REST API for receiving tracking events
- Store tracking data in a SQLite database
- Authentication-protected admin dashboard
- View and analyze recorded events
- Entity Framework Core integration
- Cookie Authentication
- MVC architecture

---

## Technologies

### Backend
- C#
- .NET 8
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core

### Database
- SQLite

### Frontend
- Razor Views
- HTML
- CSS
- Bootstrap
- JavaScript

### Authentication
- Cookie Authentication

---

## Project Structure

```text
TrackingWebApp
│
├── Controllers
├── Data
├── DTOs
├── Entities
├── Services
├── ViewModels
├── Views
├── wwwroot
└── Program.cs
```

---

## How It Works

1. A visitor interacts with the website.
2. JavaScript captures the interaction.
3. The event is sent to the REST API.
4. The API validates the incoming data.
5. Entity Framework Core stores the event in a SQLite database.
6. Administrators can review the collected data through the admin dashboard.

---

## Installation

Clone the repository

```bash
git clone https://github.com/Dasekan/TrackingWebApp.git
```

Navigate to the project

```bash
cd TrackingWebApp
```

Restore NuGet packages

```bash
dotnet restore
```

Run the application

```bash
dotnet run
```

---

## Skills Demonstrated

This project demonstrates experience with:

- ASP.NET Core MVC
- REST API development
- Entity Framework Core
- SQLite
- Cookie Authentication
- CRUD operations
- Dependency Injection
- MVC architecture
- Database design
- Object-Oriented Programming

---

## Future Improvements

- Interactive analytics dashboard
- Charts and data visualization
- Export tracking data to CSV or Excel
- Docker support
- Unit and integration testing
- Real-time tracking using SignalR
- User and session analytics

---

## Developed by

**Dasekan Allan Karim**
