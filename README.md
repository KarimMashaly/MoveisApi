# 🎬 Movies API

A professional, clean, and robust **ASP.NET Core Web API** for managing movies and genres. This project follows industry best practices, focusing on performance, maintainability, and clean architecture.

---

## 🚀 Key Features

* **Clean Architecture**: Separation of concerns between Controllers, Services, and Data layers.
* **Global Exception Handling**: Centralized error management using `IExceptionHandler` (new in .NET 8/9/10), providing consistent `ProblemDetails` responses.
* **Optimized Data Fetching**: Intelligent use of Eager Loading (`Include`) and Explicit Loading (`LoadAsync`) to minimize database roundtrips.
* **Image Management**: Secure image uploading with validation for file extensions and sizes, stored efficiently as `byte[]`.
* **Business Logic Layer**: All validations and core logic are encapsulated within the Service Layer to keep controllers "thin".
* **AutoMapper**: Seamless object-to-object mapping between Entities and DTOs.
* **Documentation**: Interactive API documentation using **Scalar/Swagger**.

---

## 🛠️ Tech Stack

* **Framework**: ASP.NET Core Web API
* **Language**: C#
* **Database**: SQL Server (using Entity Framework Core)
* **Mapping**: AutoMapper
* **Validation**: Custom Business Logic Validation

---

## 🏗️ Architecture & Patterns

1.  **Service Pattern**: Decoupling business logic from controllers.
2.  **DTO Pattern**: Ensuring data security and optimized transfer.
3.  **DRY Principle**: Reusable private methods for image processing and validation.
* **Options Pattern**: Managing configuration settings (like file limits) via `appsettings.json`.

