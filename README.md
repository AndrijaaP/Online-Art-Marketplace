Online Art Marketplace

A web application for managing and selling artworks, built with ASP.NET Core MVC (.NET 8) and MS SQL Server.
Supports three user roles: Administrator, Artist, and Buyer.

Features

Add, view, edit, and delete artworks

Search and filter by category, artist, price, and availability

User registration and login with role-based access

Shopping cart and complete checkout flow

Order history and order status updates (admin)

Sales reports by category, artist, and revenue analysis

Tech Stack

Backend: ASP.NET Core MVC (.NET 8), Entity Framework Core 9

Database: MS SQL Server

Frontend: Bootstrap 5, jQuery

Architecture: MVC

Authentication: Session-based (demo-friendly)

Database Schema

Users – user accounts with roles

Artworks – artwork details and pricing

Orders – order header / purchase data

OrderItems – line items for each order

A complete SQL script (tables + sample data) is included in the project under the /Database folder.

Getting Started
Prerequisites

.NET 8 SDK

SQL Server (local or remote)

Visual Studio 2022 or VS Code

Setup
1. Restore dependencies
dotnet restore

2. Configure the connection string

The connection string is located in:

Models/UmetnickaDelaContext.cs


Update the value of:

Server=YOUR_SQL_SERVER_INSTANCE;


Replace YOUR_SQL_SERVER_INSTANCE with your SQL Server instance name
(e.g. localhost, SQLEXPRESS, or a custom instance).

3. Create the database

You can either:

Use the provided SQL script in /Database/
or

Apply EF Core migrations (if you prefer generating the DB manually)

4. Run the application
dotnet run
