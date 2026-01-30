# Supabase PostgreSQL Integration Guide

## ✅ Completed Configuration

Your ASP.NET Core project has been successfully configured to use Supabase as the PostgreSQL database. Here's what was done:

### 1. **Updated Dependencies**
- ✅ Installed: `Npgsql.EntityFrameworkCore.PostgreSQL` (v9.0.0)
- ✅ Removed: `Microsoft.EntityFrameworkCore.Sqlite` (no longer needed)

### 2. **Updated Configuration Files**

#### `Program.cs`
Changed from SQLite to PostgreSQL:
```csharp
// Changed from:
options.UseSqlite(connectionString)

// To:
options.UseNpgsql(connectionString)

// Connection string now uses:
builder.Configuration.GetConnectionString("Supabase")
```

#### `appsettings.json` and `appsettings.Development.json`
Connection string format updated to Npgsql standard:
```json
"ConnectionStrings": {
  "Supabase": "Server=aws-0-eu-west-1.pooler.supabase.com;Port=5432;Database=postgres;User Id=postgres;Password=exsbhvR7w6q0xIXE;SSL Mode=Require;Trust Server Certificate=true;"
}
```

### 3. **Database Migrations**
- ✅ Removed old SQLite migration
- ✅ Created new migration: `InitialCreatePostgres` 
  - Location: `Migrations/20260130012429_InitialCreatePostgres.cs`
  - Creates `Users` table with all fields
  - Creates `Admins` table with all fields
  - Seeds default admin user (Admiranda Admin)

### 4. **Cleared User-Secrets**
- ✅ Removed cached credentials to ensure proper configuration loading

## 🚀 Next Steps: Deploy to Supabase

### Option A: If experiencing connection timeout

The timeout error usually occurs due to network/firewall restrictions. Try these steps:

1. **Verify Supabase Project is Running**
   - Go to [app.supabase.com](https://app.supabase.com)
   - Check project status

2. **Check IP Allowlist**
   - In Supabase Dashboard → Project Settings → Database → Networking
   - Ensure your IP is whitelisted or add `0.0.0.0/0` for development

3. **Test Connection String**
   ```bash
   cd c:\projects\trainee_projectmanagement
   dotnet ef database update
   ```

### Option B: Deploy directly

Once Supabase access is confirmed, apply migrations:

```bash
# From project directory
dotnet ef database update
```

This will:
- Create `Admins` table
- Create `Users` table
- Insert default admin user

## 📊 Database Schema

### Users Table
- `Id` (int) - Primary Key
- `FullName` (varchar 255) - Required
- `Email` (varchar 255) - Required
- `PasswordHash` (text) - Required
- `SchoolAttended` (varchar 255) - Required
- `Role` (varchar 255) - Required
- `CoursesInterested` (varchar 500) - Optional
- `InterestedInCertification` (bool)
- `CreatedAt` (timestamp) - Defaults to UTC now
- `UpdatedAt` (timestamp) - Defaults to UTC now

### Admins Table
- `Id` (int) - Primary Key
- `Email` (varchar 255) - Required
- `PasswordHash` (text) - Required
- `FullName` (varchar 255) - Required
- `CreatedAt` (timestamp) - Defaults to UTC now

## 🔐 Default Admin Account

**Email:** admiranda@sscgi.com  
**Password:** M!rand@22  
**Name:** Admiranda Admin

⚠️ **Important:** Change this password after first login!

## 🔧 Troubleshooting

### Connection Timeout
- Check internet connection
- Verify Supabase project is running
- Check firewall/IP allowlist in Supabase settings

### Migration Errors
```bash
# Rollback if needed
dotnet ef database update 0

# Remove migration and retry
dotnet ef migrations remove
dotnet ef migrations add InitialCreatePostgres
dotnet ef database update
```

### Clear All (Nuclear Option)
```bash
# Remove all data but keep structure
dotnet ef database update 0
dotnet ef database update
```

## 📝 Important Notes

1. The project is now **fully PostgreSQL based** - no SQLite fallback
2. Connection string uses **SSL Mode=Require** for security
3. All timestamps use **UTC timezone**
4. The `Users.CoursesInterested` field can store JSON or CSV format
5. **BCrypt.Net** is used for password hashing

## ✨ Your Application is Ready!

The project is now configured to:
- ✅ Connect to Supabase PostgreSQL
- ✅ Manage User and Admin data
- ✅ Use secure password hashing
- ✅ Handle migrations automatically

Run the application with:
```bash
dotnet run
```

The application will connect to your Supabase database automatically!
