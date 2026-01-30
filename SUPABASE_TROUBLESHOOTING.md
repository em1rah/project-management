# Supabase Connection Troubleshooting

## Current Issue
**Error:** "An existing connection was forcibly closed by the remote host"

This means Supabase is actively rejecting your connection attempt. This is typically due to:
1. **IP not whitelisted** in Supabase firewall
2. **SSL/TLS handshake failure**
3. **Incorrect credentials**

---

## 🔧 Solution Steps

### Step 1: Check Your Public IP
Find your current IP address:
```
https://www.whatismyipaddress.com/
```
Write it down - you'll need it.

### Step 2: Whitelist Your IP in Supabase

1. Go to **[app.supabase.com](https://app.supabase.com)**
2. Select your project
3. Click **Settings** (bottom left)
4. Click **Database** (left sidebar)
5. Scroll down to **"Connection pooling"** or **"Networking"**
6. Look for **"IP Whitelist"** or **"Firewall Rules"**
7. Click **"Add IP"** or **"Edit"**
8. Add your IP address (from Step 1)
   - For development: Add `0.0.0.0/0` to allow all IPs
   - For production: Add your specific IP address

9. **Save changes**
10. Wait 30 seconds for changes to propagate

### Step 3: Test Connection Again
```bash
cd c:\projects\trainee_projectmanagement
dotnet ef database update
```

---

## 🌐 Alternative: Allow All IPs (Development Only)

If you're having trouble finding your IP or it keeps changing:

1. In Supabase Settings → Database → Networking
2. Set IP Whitelist to `0.0.0.0/0` (allows all)
3. ⚠️ **WARNING:** This is NOT secure for production!

---

## 🔐 Verify Your Credentials

Make sure your connection credentials are correct in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Supabase": "Server=aws-0-eu-west-1.pooler.supabase.com;Port=5432;Database=postgres;User Id=postgres;Password=YOUR_PASSWORD_HERE;SSL Mode=Require;Trust Server Certificate=true;Timeout=30;Command Timeout=30;"
  }
}
```

✓ Check in Supabase Settings → Database → Connection string
✓ Verify Username (usually `postgres`)
✓ Verify Password (from Supabase dashboard)
✓ Verify Database name (usually `postgres`)

---

## 📝 Current Configuration

**Server:** aws-0-eu-west-1.pooler.supabase.com
**Port:** 5432
**Database:** postgres
**User:** postgres
**SSL Mode:** Required ✓
**Timeout:** 30 seconds ✓
**Retry Policy:** 3 attempts with 1s delay ✓

---

## ✅ After Whitelisting Your IP

Once your IP is whitelisted, run:

```bash
# Create tables in Supabase
dotnet ef database update

# You should see:
# Done. This may have worked if migration didn't have errors.
```

Then run the application:
```bash
dotnet run
```

Your app will connect to Supabase PostgreSQL!

---

## 🆘 Still Not Working?

Try these advanced troubleshooting steps:

### Check Supabase Project Status
- Dashboard should show green checkmark
- Database should be "Healthy"
- Check if project is paused

### Try pgAdmin Connection
1. Install pgAdmin 4
2. Create new server with same credentials
3. If pgAdmin can't connect, it's a Supabase issue, not your app

### Check Supabase Logs
1. Go to Supabase Dashboard
2. Click **Database** → **Logs** tab
3. Look for connection rejection reasons

### Try Different Network
Test from mobile hotspot or different internet connection
- If it works elsewhere, it's a firewall issue on your network
- Your ISP or corporate firewall may be blocking PostgreSQL port 5432

### Last Resort: Create New Supabase Project
If all else fails:
1. Create new Supabase project
2. Update connection string
3. Run migrations again

---

## 📞 Support Resources

- **Supabase Docs:** https://supabase.com/docs
- **Supabase Networking:** https://supabase.com/docs/guides/database/firewall-rules
- **PostgreSQL Connection:** https://www.postgresql.org/docs/current/libpq-envars.html
