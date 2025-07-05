# NotesApi

This project is a RESTful API that allows users to manage notes and categories. It features secure authentication via JWT, user registration and login, and full CRUD operations for notes and categories.

## Features
- User registration and login (JWT authentication)
- Create, delete, edit, and view notes
- Create, delete, edit, and view categories
- API documentation via Swagger

## Technologies
- .NET 9.0 (ASP.NET Core Web API)
- Entity Framework Core (SQL Server)
- JWT (Json Web Token) authentication
- Swagger (OpenAPI) documentation
- BCrypt.Net-Core (password hashing)

## Getting Started
1. Fill in the database settings in the `appsettings.json` file.
2. Run the following commands in the console:
   ```bash
   dotnet build
   dotnet run
   ```
3. The API will be available at: `https://localhost:5001` or `http://localhost:5000`
4. Swagger documentation: `https://localhost:5001/swagger`

## API endpoints
- `POST /api/user/register` — Register
- `POST /api/user/login` — Login (get token)
- `GET /api/notes` — List notes
- `POST /api/notes` — Create note
- `GET /api/notes/{id}` — View note
- `PUT /api/notes/{id}` — Edit note
- `DELETE /api/notes/{id}` — Delete note
- `GET /api/category` — List categories
- `POST /api/category` — Create category
- `GET /api/category/{id}` — View category
- `PUT /api/category/{id}` — Edit category
- `DELETE /api/category/{id}` — Delete category

## Author
- Husniddin