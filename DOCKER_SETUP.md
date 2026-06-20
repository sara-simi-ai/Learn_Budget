# LearnBudget Docker Setup

## Running the Application with Docker

### Prerequisites
- Docker Desktop installed and running
- Docker Compose installed

### Quick Start

1. **Build and start the containers:**
   ```bash
   docker-compose up --build
   ```

2. **Access the application:**
   - API: `http://localhost:5000`
   - Swagger UI: `http://localhost:5000/swagger`
   - SQL Server: `localhost,1433` (connection string already configured)

3. **Stop the containers:**
   ```bash
   docker-compose down
   ```

### Service Details

#### SQL Server Container
- **Service Name:** sqlserver
- **Container Name:** learnbudget-sql
- **Port:** 1433
- **Database:** LearnBudget
- **Username:** sa
- **Password:** YourSecurePassword123!
- **Storage:** learnbudget-sql-data (persistent volume)

#### ASP.NET Core API Container
- **Service Name:** server
- **Container Name:** learnbudget-api
- **Port:** 5000 (mapped to 8080 inside container)
- **Framework:** .NET 10.0
- **Environment:** Development

### Commands

**Build containers only:**
```bash
docker-compose build
```

**Start containers (without rebuild):**
```bash
docker-compose up
```

**Start containers in background:**
```bash
docker-compose up -d
```

**View logs:**
```bash
docker-compose logs -f server
docker-compose logs -f sqlserver
```

**Stop containers:**
```bash
docker-compose down
```

**Remove all containers and volumes (clean slate):**
```bash
docker-compose down -v
```

### Environment Variables

The application uses the following environment variables configured in docker-compose.yml:

- `ASPNETCORE_ENVIRONMENT`: Set to "Development"
- `ASPNETCORE_URLS`: Set to "http://+:8080"
- `ConnectionStrings__LearnBudgetConnection`: Database connection string

### Notes

- The SQL Server container has a health check that waits for the database to be ready before the API starts
- The API container depends on the SQL Server container being healthy
- All services are connected via a custom bridge network (`learnbudget-network`)
- Database changes (migrations) will persist due to the named volume `learnbudget-sql-data`

### Troubleshooting

**Port already in use:**
If port 5000 or 1433 is already in use, modify the port mappings in `docker-compose.yml`:
```yaml
ports:
  - "5001:8080"  # Change 5001 to your desired port
```

**Database connection issues:**
Ensure the SQL Server container is healthy:
```bash
docker-compose ps
```

**Rebuild from scratch:**
```bash
docker-compose down -v
docker-compose up --build
```
