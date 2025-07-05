# NotesApi

Этот проект представляет собой RESTful API, позволяющий пользователям управлять заметками и категориями. В проекте реализована безопасная аутентификация через JWT, регистрация и вход пользователя, а также полный набор CRUD-операций для заметок и категорий.

## Основные возможности
- Регистрация и вход пользователя (JWT-аутентификация)
- Создание, удаление, редактирование и просмотр заметок
- Создание, удаление, редактирование и просмотр категорий
- Документация API через Swagger

## Технологии
- .NET 9.0 (ASP.NET Core Web API)
- Entity Framework Core (SQL Server)
- JWT (Json Web Token) для аутентификации
- Swagger (OpenAPI) для документации
- BCrypt.Net-Core (хеширование паролей)

## Запуск
1. Укажите настройки базы данных в файле `appsettings.json`.
2. Выполните в консоли:
   ```bash
   dotnet build
   dotnet run
   ```
3. API будет доступен по адресу: `https://localhost:5001` или `http://localhost:5000`
4. Документация Swagger: `https://localhost:5001/swagger`

## API endpoints
- `POST /api/user/register` — Регистрация
- `POST /api/user/login` — Вход (получение токена)
- `GET /api/notes` — Список заметок
- `POST /api/notes` — Создать заметку
- `GET /api/notes/{id}` — Просмотр заметки
- `PUT /api/notes/{id}` — Редактировать заметку
- `DELETE /api/notes/{id}` — Удалить заметку
- `GET /api/category` — Список категорий
- `POST /api/category` — Создать категорию
- `GET /api/category/{id}` — Просмотр категории
- `PUT /api/category/{id}` — Редактировать категорию
- `DELETE /api/category/{id}` — Удалить категорию

## Автор
- Husniddin