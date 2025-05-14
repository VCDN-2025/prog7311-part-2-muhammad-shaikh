# Agri-Energy Connect Web Application

Agri-Energy Connect is a prototype web platform designed to link South African farmers with green energy technology providers. The system supports sustainable farming practices by enabling collaboration, product management, and knowledge sharing between agricultural and renewable energy sectors.

---

## Project Overview

This ASP.NET Core MVC application demonstrates core functionalities of the envisioned Agri-Energy Connect platform, including role-based access, product tracking, and a green energy resource hub.

---

## 1. Development Environment Setup

To run this project locally, follow these steps:

### Prerequisites

- Visual Studio 2022 (recommended)
- .NET 8 SDK or compatible installed
- SQL Server LocalDB (installed with Visual Studio)
- Git (optional, for version control)

### Steps

1. Clone the repository or download the ZIP file
2. Open the `.sln` file in Visual Studio 2022
3. Restore NuGet packages if prompted
4. Ensure the database connection string in `appsettings.json` matches your local SQL Server setup
5. Open Package Manager Console and run:

Update-Database
This will create the database schema from migrations.

Press F5 or click Start to launch the application.

**2. How to Build and Run**
Build
Open the solution in Visual Studio

Ensure the build configuration is set to Debug

Click Build > Build Solution or press Ctrl + Shift + B

Run
Press F5 or click the green play button to start the application

The app will launch in your browser

**3. System Functionality**
Key Features
Authentication & Role Management:
Supports Farmer and Employee roles with session-based access.

Product Management:
Farmers can add products with category and date. Employees can view and filter all farmer products.

Farmer Administration:
Employees can create new farmer profiles directly from their dashboard.

Data Filtering:
Employees can filter products by date and category per farmer.

User Interface:
Clean Bootstrap 5-based design with background styling, responsive layout, and structured forms.

4. User Roles
Farmer
Register or be added by an Employee

Log in and access a personalized dashboard

Add and view products (with name, category, and production date)

Employee
Register as Employee or log in with existing credentials

Access dashboard to:

View all products

Filter by category and date

View products by specific farmers

Add new farmer profiles

License
This is a prototype developed for academic demonstration purposes. No license is applied.

