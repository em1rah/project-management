# Fix: "Tenant or user not found" Error

## 🔍 What This Means

Your connection is reaching Supabase, but **the username or password is WRONG**.

---

## ✅ Step-by-Step Fix

### Step 1: Get Correct Credentials from Supabase

1. Go to **[app.supabase.com](https://app.supabase.com)**
2. Select your project
3. Click **Settings** (bottom left)
4. Click **Database** (left sidebar)
5. Look for **"Connection string"** section
6. You'll see options:
   - **Transaction pooler** ← Use this one
   - Session pooler
   - Direct connection

**Copy the Transaction pooler connection string** - it looks like:
```
postgresql://postgres.[PROJECT-ID]:PASSWORD@[REGION].pooler.supabase.com:6543/postgres
```

⚠️ **Note:** Port is usually `6543` for pooler, not `5432`!

---

### Step 2: Extract Credentials

From the connection string, find:
- **Username:** `postgres.[PROJECT-ID]` (or sometimes just `postgres`)
- **Password:** The part after the colon
- **Server:** The hostname
- **Port:** Usually `6543` for pooler or `5432` for direct
- **Database:** `postgres`

**Example:**
```
postgresql://postgres.abc123:mySecurePassword@aws-0-eu-west-1.pooler.supabase.com:6543/postgres
```

- Username: `postgres.abc123`
- Password: `mySecurePassword`
- Server: `aws-0-eu-west-1.pooler.supabase.com`
- Port: `6543`
- Database: `postgres`

---

### Step 3: Update Your Connection String

Update `appsettings.Development.json` with **correct** format:

**Using Pooler (Recommended):**
```json
{
  "ConnectionStrings": {
    "Supabase": "Server=aws-0-eu-west-1.pooler.supabase.com;Port=6543;Database=postgres;User Id=postgres.YOUR_PROJECT_ID;Password=YOUR_PASSWORD_HERE;SSL Mode=Require;Trust Server Certificate=true;Timeout=30;Command Timeout=30;"
  }
}
```

**Or as URI (alternative):**
```json
{
  "ConnectionStrings": {
    "Supabase": "postgresql://postgres.YOUR_PROJECT_ID:YOUR_PASSWORD_HERE@aws-0-eu-west-1.pooler.supabase.com:6543/postgres?sslmode=require"
  }
}
```

---

### Step 4: Apply Your Credentials

Edit `appsettings.Development.json`:

1. Replace `YOUR_PROJECT_ID` with your actual project ID
   - Found in Supabase URL: `https://app.supabase.com/project/[YOUR_PROJECT_ID]/`

2. Replace `YOUR_PASSWORD_HERE` with your actual password
   - From Supabase Settings → Database → Password reset (if needed)

3. Save the file

---

### Step 5: Test Connection

```bash
cd c:\projects\trainee_projectmanagement
dotnet ef database update
```

You should see:
```
Done. This may have worked if migration didn't have errors.
```

---

## 🔑 How to Find Your Supabase Project Credentials

**Easy Method:**
1. Go to Supabase Dashboard
2. Click **Settings** → **Database**
3. Scroll to **"Connection string"**
4. Select **Transaction pooler**
5. Click **"Copy"**
6. Paste it somewhere to extract credentials

The connection string format from Supabase is the source of truth!

---

## ⚙️ Connection String Formats

### Format 1: Server-based (Recommended for .NET)
```
Server=HOST;Port=PORT;Database=DATABASE;User Id=USERNAME;Password=PASSWORD;SSL Mode=Require;Trust Server Certificate=true;
```

### Format 2: URI-based
```
postgresql://USERNAME:PASSWORD@HOST:PORT/DATABASE?sslmode=require
```

Both work, but Format 1 is clearer for debugging.

---

## 🔐 Common Supabase Credentials Issues

| Issue | Solution |
|-------|----------|
| Port is 5432 | Change to 6543 (pooler) or use direct connection |
| Username is just `postgres` | Try `postgres.PROJECT_ID` |
| Authentication failed | Reset password in Supabase Settings |
| Wrong database name | Should be `postgres` (not your project name) |
| Password has special chars | Make sure it's properly escaped in connection string |

---

## 🆘 Still Getting "Tenant or user not found"?

Try these steps:

1. **Reset Password** (if unsure)
   - Supabase Settings → Database → Reset password
   - Update connection string with new password

2. **Try Direct Connection** (instead of pooler)
   - Use port `5432` instead of `6543`
   - Use `postgresql://postgres:PASSWORD@HOST:5432/postgres`

3. **Verify Project is Running**
   - Supabase Dashboard → check project status
   - Should show green checkmark

4. **Check Supabase Logs**
   - Dashboard → Logs → check for authentication errors

---

## ✅ Once Working

After successful `dotnet ef database update`, run:
```bash
dotnet run
```

Your app will be connected to Supabase PostgreSQL!
