# NotesApi

Bu loyiha RESTful API bo'lib, foydalanuvchilarga eslatmalar (notes) va kategoriyalarni boshqarish imkonini beradi. Loyihada JWT orqali xavfsiz autentifikatsiya, foydalanuvchi ro'yxatdan o'tkazish va tizimga kirish, eslatmalar va kategoriyalar bilan to'liq CRUD amallari mavjud.

## Asosiy imkoniyatlar
- Foydalanuvchini ro'yxatdan o'tkazish va tizimga kirish (JWT autentifikatsiya)
- Eslatmalarni yaratish, o'chirish, tahrirlash va ko'rish
- Kategoriyalarni yaratish, o'chirish, tahrirlash va ko'rish
- Swagger orqali API hujjatlari

## Texnologiyalar
- .NET 9.0 (ASP.NET Core Web API)
- Entity Framework Core (SQL Server)
- JWT (Json Web Token) orqali autentifikatsiya
- Swagger (OpenAPI) hujjatlash
- BCrypt.Net-Core (parollarni xesh qilish)

## Ishga tushirish
1. `appsettings.json` faylida ma'lumotlar bazasi sozlamalarini to'ldiring.
2. Konsolda quyidagilarni bajaring:
   ```bash
   dotnet build
   dotnet run
   ```
3. API quyidagi manzilda ishlaydi: `https://localhost:5001` yoki `http://localhost:5000`
4. Swagger hujjatlari uchun: `https://localhost:5001/swagger`

## API endpointlar
- `POST /api/user/register` — Ro'yxatdan o'tish
- `POST /api/user/login` — Tizimga kirish (token olish)
- `GET /api/notes` — Eslatmalar ro'yxati
- `POST /api/notes` — Yangi eslatma yaratish
- `GET /api/notes/{id}` — Eslatmani ko'rish
- `PUT /api/notes/{id}` — Eslatmani tahrirlash
- `DELETE /api/notes/{id}` — Eslatmani o'chirish
- `GET /api/category` — Kategoriyalar ro'yxati
- `POST /api/category` — Yangi kategoriya yaratish
- `GET /api/category/{id}` — Kategoriyani ko'rish
- `PUT /api/category/{id}` — Kategoriyani tahrirlash
- `DELETE /api/category/{id}` — Kategoriyani o'chirish

## Muallif
- Husniddin