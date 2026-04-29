# Employee Management System (MVC)

A comprehensive Web Application built using **ASP.NET MVC** and **N-Tier Architecture** to manage organizational data efficiently.

## Key Features
* **Employee Photo Management:** Integrated a dedicated **Attachment Service** for secure image uploads, validation, and storage.
* **Security & Identity:** Implemented **Microsoft Identity Framework** for robust user management, featuring secure authentication and **Role-Based Access Control (RBAC)**.
* **Advanced Auth Flows:** Full implementation of **Forget Password** and **Reset Password** functionalities with secure token handling.
* **Email Notifications:** Implemented an **Email Sender Service** for automated communications and security alerts.
* **Advanced CRUD:** Full management for Employees and Departments with strict business logic validation.

## Technical Stack & Patterns
* **Framework:** .NET Core / ASP.NET MVC / **Microsoft Identity Framework**.
* **Architecture:** N-Tier Architecture (Presentation, Business Logic, Data Access layers).
* **Data Access:** Entity Framework Core with **Unit of Work** and **Generic Repository** patterns for clean, maintainable code.
* **Frontend:** Responsive UI built with **Bootstrap** and Razor Views.

## How to Run
1. Clone the repository.
2. Update the connection string in `appsettings.json`.
3. Run `dotnet ef database update` to apply migrations.
4. Press F5 to run the project.
