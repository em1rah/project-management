# Quick Credentials Verification

## 🔍 How to Get Correct Supabase Database Credentials

### Step 1: Log In to Supabase
Go to **https://app.supabase.com** and select your project

### Step 2: Find Connection String
1. Click **Settings** (bottom left)
2. Click **Database** (left sidebar)
3. Find the **"Connection string"** section

### Step 3: Look for Connection Pooler
In the connection string dropdown, select:
- **"Transaction pooler"** (recommended for .NET apps)
OR
- **"Session pooler"** (alternative)

### Step 4: Copy & Verify

The connection string will look like ONE of these:

**Option A - Pooler (Port 6543):**
```
postgresql://postgres.[PROJECT_ID]:PASSWORD@aws-0-eu-west-1.pooler.supabase.com:6543/postgres
```

**Option B - Direct (Port 5432):**
```
postgresql://postgres:[PASSWORD]@db.aws-0-eu-west-1.supabase.co:5432/postgres
```

### Step 5: Extract Your Credentials

From your connection string, identify:
```
User Id: postgres (or postgres.PROJECT_ID for pooler)
Password: [The actual password]
Server: The hostname
Port: 6543 (pooler) or 5432 (direct)
Database: postgres
```

---

## ❓ What You Need to Tell Me

Please provide (you can hide password):
1. Your **Supabase Project ID**
2. Your **PostgreSQL password** (or I'll use the one you already set)
3. Whether you're using **Pooler** or **Direct** connection

OR

Just tell me the exact **connection string from Supabase** and I'll extract everything correctly.

---

## 🆘 If You Don't Know the Password

If you forgot your PostgreSQL password:
1. In Supabase Settings → Database
2. Scroll down to **"Reset database password"**
3. Click the button
4. Set a new password
5. Use that new password in connection string

---

## Current Error

Your error "**Tenant or user not found**" means:
- ❌ Username is wrong, OR
- ❌ Password is wrong, OR  
- ❌ Both are wrong

It's NOT a network/IP issue (we've confirmed network works).
It's NOT missing keys/secrets (those aren't needed for DB connection).

**It's purely a credentials mismatch.**

Let me know what your actual Supabase connection credentials are!
