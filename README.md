# 🏥 Clinic System

A backend system for managing clinics and their patients, built with **ASP.NET Core Web API** and **SQL Server**.

> 🚧 **Project Status: Under Development**
> This project is actively being built. Features, structure, and APIs may change as development continues.

---

## ✨ Features

- **Authentication** using JWT (JSON Web Tokens), with clinic ID and email embedded in the token claims
- **Secure password hashing** with BCrypt
- **Clinics & Patients** management (one clinic has many patients)
- **Entity Framework Core** with code-first migrations
- **Swagger UI** for exploring and testing the API
- **Unit tests** for core services (JWT and password hashing)

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 8) |
| Language | C# |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Authentication | JWT Bearer |
| Password Hashing | BCrypt.Net |
| API Docs | Swagger (Swashbuckle) |
| Testing | NUnit |
| Frontend | In progress |

---

## 📁 Project Structure

```
clinic system/
├── Controllers/          # API endpoints
├── DTOs/                 # Data transfer objects
├── Enums/                # Enumerations
├── Exceptions/           # Custom exceptions
├── Migrations/           # EF Core migrations
├── Models/               # Database entities
├── Services/             # Business logic (JWT, password hashing, ...)
├── frontend/             # Frontend app (in progress)
├── ClinicSystem.Tests/   # Unit tests
└── Program.cs
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/Rawan123123/Clinic_System.git
   cd Clinic_System
   ```

2. **Configure the app**

   Update the SQL Server connection string and JWT settings in `appsettings.json`
   (or use [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) for sensitive values like the JWT secret).

3. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the API**
   ```bash
   dotnet run
   ```

5. Open Swagger UI at `https://localhost:<port>/swagger`

---

## 🧪 Running Tests

```bash
dotnet test
```

---

## 🗺️ Roadmap

- [x] Project setup and database schema (Clinics & Patients)
- [x] JWT authentication and password hashing
- [x] Unit tests for core services
- [ ] Complete CRUD endpoints for clinics and patients
- [ ] Expand test coverage
- [ ] Frontend integration
- [ ] Deployment

---

## 👩‍💻 Author

**Rawan Adel Abdel Rahman**
Computer Systems Engineering graduate, Al-Azhar University

[GitHub](https://github.com/Rawan123123/) · [LinkedIn](https://www.linkedin.com/in/rawan-adel-abb8aa245/)

---

## 📄 License

This project is for learning and portfolio purposes.
