# Online Art Marketplace

A web application for managing and selling artworks, built with ASP.NET Core MVC (.NET 8) and MS SQL Server.  
Supports three user roles: Administrator, Artist, and Buyer.

## Features
- Add, view, edit, and delete artworks
- Search and filter by category, artist, price, and availability
- User registration and login with role selection
- Shopping cart and purchase flow
- View order history and update order status (admin)
- Sales reports by category and artist, revenue analysis

## Tech Stack
- **Backend:** ASP.NET Core MVC (.NET 8), Entity Framework Core 9
- **Database:** MS SQL Server
- **Frontend:** Bootstrap 5, jQuery
- **Architecture:** MVC pattern
- **Authentication:** Session-based (for demo purposes)

## Database Schema
- **Users** – user accounts with roles
- **Artworks** – details and pricing for artworks
- **Orders** – purchase records
- **OrderItems** – links orders to artworks

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Setup
1. Update connection string in `Models/UmetnickaDelaContext.cs` – replace Server=XXXX with your SQL Server instance.
2. Create the database via EF migrations or the provided SQL script
3. Run the application:

```bash
dotnet restore
dotnet run
