# Clinic System

- **Backend:** ASP.NET Core Web API (.NET) + EF Core + SQL Server + JWT
- **Frontend:** React (Vite)

Both must run at the same time.

## Requirements

- .NET SDK
- SQL Server (default instance `.` with Windows auth, or edit the connection string)
- Node.js 18+
- EF Core CLI: `dotnet tool install --global dotnet-ef`

## Backend

```bash
cd <backend-folder>
```

1. In `appsettings.json`:
   - `ConnectionStrings:MyConnection` -> point to your SQL Server.
   - `JWT:SecretKey` -> replace the placeholder with any string of 32+ characters.
2. Create the database:
   ```bash
   dotnet ef database update
   ```
3. Trust the HTTPS dev certificate (once):
   ```bash
   dotnet dev-certs https --trust
   ```
4. Run:
   ```bash
   dotnet run --launch-profile https
   ```

API must be on **https://localhost:7279** (Swagger: `/swagger`).

## Frontend

```bash
cd <frontend-folder>
npm install
npm run dev
```

Open **http://localhost:5173**.

## Notes

- CORS only allows `http://localhost:5173`. If Vite uses another port, update the `AllowReact` policy in `Program.cs`.
- The API URL (`https://localhost:7279`) is hard-coded in `Login.jsx` and `Register.jsx`. If the backend port changes, update both files.

## Endpoints

| Method | URL | Body |
|--------|-----|------|
| POST | `/api/Auth/Register` | `{ name, email, password, phoneNumber, address }` |
| POST | `/api/Auth/Login` | `{ email, password }` -> returns `token` |
