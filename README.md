# Modular Monolith Application using ABP Framework (.NET)

This repository contains a real-world implementation of a Modular Monolith application demonstrating how simple and powerful development can be using the ABP Framework.

What You'll Find Here:
* A fully modularized application featuring two distinct modules:
  * Shipments Module: Manages shipments, their statuses, and shipment items (built using the DDD Module template).
  * Stocks Module: Manages stock levels and integrates with the Shipments Module (built using the Standard Module template).
* Integration via method interface calls and event-driven architecture.
* Asynchronous module communication using ABP's distributed event bus.
* MVC Razor pages for user interface.
* SQL Server database integration using Entity Framework Core.

🛠️ Technologies Used:
* .NET 9
* ABP Framework (free templates)
* ABP Studio (free)
* Entity Framework Core
* SQL Server
* MVC Razor Pages

🎯 Why ABP Framework?

ABP significantly accelerates modular monolith application development by:
* Removing boilerplate code
* Simplifying module integration and communication
* Simplifying database migrations and API generation
* Providing built-in templates and UI components
* Simplifying combining UI of each module into a single user interface

✅ Getting Started

Prerequisites:
* .NET 9 SDK
* ABP Studio
* IDE: Visual Studio, VS Code or JetBrains Rider
* SQL Server (can run via Docker)

Steps:
1. Clone the repository:
`git clone [repository-url]`

2. Configure the database connection string in appsettings.json

3. Update the database:
`dotnet ef database update`

4. Run the application:
`dotnet run`

Open the application in your browser:
`https://localhost:44311`

Check out [ABP Framework](https://abp.io/docs/latest/tutorials/modular-crm?utm_source=github&utm_medium=social&utm_campaign=antonmartynyuk_github), and start building applications skipping the boilerplate code.
