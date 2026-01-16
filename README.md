🎬 Movies API
A professional, clean, and robust ASP.NET Core Web API for managing movies and genres. This project follows industry best practices, focusing on performance, maintainability, and clean architecture.

🚀 Key Features
Clean Architecture: Separation of concerns between Controllers, Services, and Data layers.

Global Exception Handling: Centralized error management using IExceptionHandler (new in .NET 8/9/10), providing consistent ProblemDetails responses.

Optimized Data Fetching: Intelligent use of Eager Loading (Include) and Explicit Loading (LoadAsync) to minimize database roundtrips.

Image Management: Secure image uploading with validation for file extensions and sizes, stored efficiently as byte[].

Business Logic Layer: All validations and core logic are encapsulated within the Service Layer to keep controllers "thin".

AutoMapper: Seamless object-to-object mapping between Entities and DTOs.

Documentation: Interactive API documentation using Scalar/Swagger.

🛠️ Tech Stack
Framework: ASP.NET Core Web API

Language: C#

Database: SQL Server (using Entity Framework Core)

Mapping: AutoMapper

Validation: Custom Business Logic Validation

🏗️ Architecture & Patterns
Service Pattern: Decoupling business logic from controllers.

DTO Pattern: Ensuring data security and optimized transfer.

DRY Principle: Reusable private methods for image processing and validation.

Options Pattern: Managing configuration settings (like file limits) via appsettings.json.

📝 Configuration
The project is highly configurable. You can adjust file upload limits and allowed extensions in the appsettings.json:

JSON

"FileSettings": {
  "MaxAllowedSizeInBytes": 1048576,
  "AllowedExtensions": [ ".jpg", ".png" ]
}
🛤️ API Endpoints
🎬 Movies
GET /api/movies - Get all movies (supports filtering by GenreId).

GET /api/movies/{id} - Get detailed information about a specific movie.

POST /api/movies - Add a new movie (Form-data with Poster).

PUT /api/movies/{id} - Update movie details or its poster.

DELETE /api/movies/{id} - Remove a movie.

🎭 Genres
GET /api/genres - Get all available genres.

POST /api/genres - Create a new genre.

PUT /api/genres/{id} - Update genre name.

DELETE /api/genres/{id} - Remove a genre.

⚙️ How to Run
Clone the repository: git clone [Your-Repo-Link]

Update the connection string in appsettings.json.

Run Update-Database in the Package Manager Console.

Hit F5 and enjoy!
