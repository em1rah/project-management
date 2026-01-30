# Deploying to Vercel (Docker)

Quick steps to deploy this ASP.NET Core app to Vercel using the Dockerfile included at the repository root.

Prerequisites
- Install Docker (for local build/testing).
- Install the Vercel CLI: `npm i -g vercel`.

Deploy (recommended)
1. Log into Vercel:

```bash
vercel login
```

2. From the repo root run:

```bash
vercel --prod
```

Vercel will detect `vercel.json` and build via the Dockerfile using `@vercel/docker`.

Local test (optional)

```bash
docker build -t trainee_projectmanagement:local .
docker run -e PORT=8080 -p 8080:8080 trainee_projectmanagement:local
# then open http://localhost:8080
```

Notes
- The Dockerfile sets a default `PORT=8080` and `ASPNETCORE_URLS` to bind to the runtime `PORT` environment variable Vercel provides.
- If you want automatic deployments, connect this repository to Vercel via the Vercel dashboard.
