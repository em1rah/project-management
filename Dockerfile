FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY ["trainee_projectmanagement.csproj", "./"]
RUN dotnet restore "./trainee_projectmanagement.csproj"

# copy everything else and publish
COPY . .
RUN dotnet publish "trainee_projectmanagement.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# default port if PORT not supplied by hosting environment
ENV PORT 8080
ENV ASPNETCORE_URLS=http://+:${PORT}

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "trainee_projectmanagement.dll"]
