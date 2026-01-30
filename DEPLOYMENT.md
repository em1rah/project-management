# Deploying to Vercel (Docker)
# Deploying this ASP.NET Core app

This repository includes a multi-stage `Dockerfile` for producing a runnable Docker image. Below are two deployment options: Vercel (attempted earlier) and Render (recommended for Docker builds).

Prerequisites
- Docker (for local testing)
- Vercel CLI (optional): `npm i -g vercel`

Local test (optional)

```bash
docker build -t trainee_projectmanagement:local .
docker run -e PORT=8080 -p 8080:8080 trainee_projectmanagement:local
# open http://localhost:8080
```

Vercel notes
- Vercel does not currently support the `@vercel/docker` builder from the npm registry; the earlier deploy attempt failed.
- If you still want Vercel, convert the app to a supported serverless or Edge deployment (significant changes), or use a different host.

Render (recommended for Docker)

This repo contains `render.yaml` so Render can create the web service automatically from the repository using the repository Dockerfile.

Automatic via Render Dashboard
1. Push your branch (already done) and go to https://dashboard.render.com.
2. Click "New +" → "Web Service" → "Connect a repository" and pick this repository.
3. Render will detect `render.yaml` and create the `project-management-deploy` web service using the `Dockerfile`.
4. Add any required Environment > Environment Variables (DB connection strings, secrets) in the Render service settings.

Manual setup in Render

```bash
# Connect your GitHub repo in the Render dashboard
# Create a new Web Service and choose "Docker" as the environment
# Set the Dockerfile path to: Dockerfile
# Set the branch to: master
# Add required env vars (DATABASE_URL, any app secrets)
```

Notes about secrets and DB
- Use Render's Environment Variables for `DATABASE_URL` and other secrets; do not commit credentials.
- Confirm `ASPNETCORE_ENVIRONMENT` and other appsettings are set in Render if needed.

If you'd like, I can:
- provide the Render dashboard steps in a checklist and follow along, or
- attempt to create the service using the Render API/CLI if you provide a Render API key.
