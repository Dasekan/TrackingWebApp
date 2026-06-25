Tracking Web App
Beskrivelse

Tracking Web App er en ASP.NET Core MVC-applikation udviklet i C#, som registrerer og analyserer brugeraktivitet på en hjemmeside.

Applikationen indeholder både en offentlig hjemmeside og et administrationspanel, hvor registrerede tracking-events kan vises og analyseres. Brugerinteraktioner sendes til et REST API og gemmes i en SQLite-database via Entity Framework Core.

Projektet demonstrerer, hvordan webanalyse kan implementeres i en ASP.NET Core-applikation uden brug af eksterne analyseværktøjer.

Funktioner
Registrering af brugeraktivitet
REST API til modtagelse af tracking-events
Gemmer events i SQLite-database
Administrationspanel
Cookie Authentication
Statistik over brugeraktivitet
Razor Views
Entity Framework Core
MVC-arkitektur
Teknologier
C#
.NET 8
ASP.NET Core MVC
ASP.NET Core Web API
Entity Framework Core
SQLite
Cookie Authentication
Bootstrap
Razor Views
Projektstruktur
TrackingWebApp
│
├── Controllers
│   ├── SiteController
│   ├── TrackingController
│   └── AdminController
│
├── Data
│   └── AppDbContext
│
├── Entities
│
├── Dtos
│
├── Services
│
├── ViewModels
│
├── Views
│
├── wwwroot
│
└── Program.cs
Hvordan fungerer systemet?
Brugeren besøger hjemmesiden.
JavaScript registrerer brugerinteraktioner.
Tracking-events sendes til:
POST /api/tracking/event
API'et validerer data.
Eventet gemmes i SQLite via Entity Framework Core.
Administrator kan efterfølgende se data og statistik via administrationspanelet.
API
Test API
GET /api/tracking/ping

Returnerer

{
    "message": "Tracking API virker!"
}
Registrer tracking-event
POST /api/tracking/event

Eksempel

{
    "eventType": "click",
    "path": "/services",
    "elementId": "btnContact",
    "sessionId": "abc123"
}
Hent tracking-events
GET /api/tracking/events

Returnerer de seneste registrerede events.

Installation

Klon projektet

git clone https://github.com/Dasekan/TrackingWebApp.git

Gå til projektmappen

cd TrackingWebApp

Installer dependencies

dotnet restore

Kør projektet

dotnet run

Applikationen opretter automatisk SQLite-databasen ved første migration.

Kompetencer demonstreret

Projektet demonstrerer erfaring med:

ASP.NET Core MVC
REST API
Entity Framework Core
SQLite
Authentication
MVC-arkitektur
Dependency Injection
DTO-design
CRUD-operationer
Databasehåndtering
Razor Views
Mulige forbedringer
Dashboard med grafer
Filtrering af events
Eksport til CSV eller Excel
Docker-support
Unit Tests
SignalR til live tracking
Azure deployment
Udviklet af

Dasekan
