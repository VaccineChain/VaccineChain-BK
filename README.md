# 💉 VaccineChain Backend

<p align="center">
  <strong>Backend API for Blockchain-Based Vaccine Cold Chain Monitoring</strong>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/31e4f2c4-579a-476a-a8f7-ac64888c5e89" width="443" alt="VaccineChain Backend preview" />
</p>

<p align="center">
  ASP.NET Core · SQL Server · Hyperledger Fabric · JWT Authentication
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-7.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 7" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core" />
  <img src="https://img.shields.io/badge/Database-SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/Blockchain-Hyperledger%20Fabric-2F3134?style=for-the-badge&logo=hyperledger&logoColor=white" alt="Hyperledger Fabric" />
</p>

---

## 📖 Overview

**VaccineChain Backend** is the backend API component of the VaccineChain ecosystem, a blockchain-based system for vaccine traceability and cold-chain monitoring.

Built with ASP.NET Core Web API, the backend provides application services for the frontend and communicates with a Node.js API gateway to retrieve vaccine sensor records from a Hyperledger Fabric blockchain network.

The system combines traditional backend services and blockchain-based data retrieval to help users inspect vaccine-related records, monitor sensor measurements, and analyze recorded environmental values.

## ✨ Key Features

- **Vaccine Data Retrieval** — Retrieve sensor records associated with a specific Vaccine ID.
- **Blockchain Integration** — Query vaccine records through the Node.js API connected to Hyperledger Fabric.
- **JWT Authentication** — Use bearer tokens to authorize protected API requests.
- **Vaccine Information Management** — Provide backend services for vaccine-related application data.
- **Sensor Data Analysis** — Support the frontend in calculating average, highest, and lowest recorded values.
- **API Integration** — Expose HTTP endpoints for communication with the VaccineChain frontend.

## 🏗️ Architecture

```text
┌──────────────────────────────┐
│      Angular Frontend        │
│   Search Vaccine / Results   │
└──────────────┬───────────────┘
               │ HTTP / Bearer Token
               ▼
┌──────────────────────────────┐
│      ASP.NET Core API        │
│                              │
│ Controllers / Middleware     │
│ Services / Repositories      │
└──────────────┬───────────────┘
               │ HTTP Request
               ▼
┌──────────────────────────────┐
│       Node.js API            │
│   Hyperledger Fabric API     │
└──────────────┬───────────────┘
               │ Chaincode Query
               ▼
┌──────────────────────────────┐
│   Hyperledger Fabric         │
│   fabvaccine Chaincode       │
│   Vaccine Sensor Records     │
└──────────────────────────────┘

┌──────────────────────────────┐
│         SQL Server           │
│ Application Relational Data  │
└──────────────────────────────┘
```

The architecture separates application-level API handling from blockchain interaction. SQL Server supports the backend's relational data requirements, while vaccine sensor records are retrieved through the blockchain integration layer.

## 🔍 Search Vaccine by ID

The vaccine search workflow retrieves sensor records associated with a Vaccine ID and presents the results for further analysis.

1. **Submit Search** — The Angular frontend validates the Vaccine ID entered by the user.
2. **Call Backend API** — The frontend sends an HTTP GET request to the ASP.NET Core endpoint.
3. **Authorize Request** — The backend obtains the bearer token from the authenticated request context.
4. **Forward Blockchain Query** — The backend's `HttpClientService` sends a request to the Node.js API.
5. **Query Ledger Records** — The Node.js API invokes `queryVaccineDataByVaccineID` in the `fabvaccine` chaincode.
6. **Return Sensor Data** — The retrieved records are passed back through the API layers to the frontend.
7. **Calculate Statistics** — The frontend processes the measurements and displays summary statistics and timestamps.

### Example API Request

```http
GET /api/Sensor/get/{vaccineId}
Authorization: Bearer <access_token>
```

Example request path:

```http
GET /api/Sensor/get/VAC001
```

The response contains a success message and the retrieved data:

```json
{
  "message": "Vaccine data retrieved successfully.",
  "data": [
    {
      "vaccine_id": "VAC001",
      "device_id": "DEV002",
      "value": -4,
      "created_date": 1720000000
    }
  ]
}
```

*The JSON above is illustrative; the actual response fields and values depend on the chaincode data and API serialization.*

### Sensor Data Statistics

The frontend derives the following information from the returned sensor records:

| Metric | Description |
|---|---|
| Average Value | Mean of the retrieved measurements |
| Highest Value | Maximum recorded measurement |
| Lowest Value | Minimum recorded measurement |
| Measurement Timestamps | Dates and times associated with the highest and lowest values |
| Date Range | Earliest and latest record timestamps |
| Device IDs | Unique devices represented in the result set |
| Number of Records | Total number of retrieved sensor records |

## 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| C# / ASP.NET Core Web API | Backend HTTP endpoints |
| .NET 7 | Application runtime and target framework |
| Entity Framework Core | Relational data access |
| Microsoft SQL Server | Relational database |
| JWT Bearer Authentication | API request authorization |
| AutoMapper | Object mapping |
| Swagger / Swashbuckle | API documentation and testing |
| Docker Compose | SQL Server development environment |
| Node.js API | Integration with the Fabric network |
| Hyperledger Fabric | Permissioned blockchain ledger |
| Go Chaincode | Vaccine record queries |

## 📁 Project Structure

```text
VaccineChain-BK/
├── Constraints/       # Request and model constraints
├── Controllers/       # HTTP API endpoints
├── DTO/               # Data transfer objects
├── Exceptions/        # Application exception handling
├── Hubs/              # Real-time communication hubs
├── Mapper/            # Object mapping configuration
├── Middlewares/       # HTTP request pipeline middleware
├── Migrations/        # Entity Framework Core migrations
├── Models/             # Application data models
├── Repositories/       # Data access layer
├── Services/           # Application and integration services
├── Properties/
├── Dockerfile
├── docker-compose.yml
├── Program.cs
├── appsettings.json
└── vaccine-chain-bk.csproj
```

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Git
- Access to the Node.js API and Hyperledger Fabric network for blockchain queries

### 1. Clone the Repository

```bash
git clone https://github.com/VaccineChain/VaccineChain-BK.git
cd VaccineChain-BK
```

### 2. Configure the Environment

Review `appsettings.json` and `appsettings.Development.json` to identify the required application settings.

Configure the database connection string and any authentication or integration settings required by the application.

The blockchain integration service uses the `NODE_VACCINE_URL` environment variable to locate the Node.js API.

Set the variable to the appropriate endpoint for your environment. Do not commit access tokens, database passwords, private keys, or other secrets to source control.

### 3. Start SQL Server

Start the database service defined in `docker-compose.yml`:

```bash
docker compose up -d sql-server
```

Verify that the SQL Server container is running before starting the application.

**Security note:** Use a strong, environment-specific database password. Do not reuse credentials committed to a repository or publish them in documentation.

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Apply Database Migrations

If the application uses Entity Framework Core migrations for database initialization, run:

```bash
dotnet ef database update
```

If the Entity Framework CLI is not installed, install the tool using the official [.NET EF Core tools documentation](https://learn.microsoft.com/en-us/ef/core/cli/dotnet).

Ensure that the database connection string is configured correctly before applying migrations.

### 6. Run the API

```bash
dotnet run
```

Use the local URL reported by the application in the terminal. If Swagger is enabled for the current environment, open the configured Swagger endpoint to inspect and test the available API routes.

## 🔐 Security Considerations

- Require valid authentication for protected API endpoints.
- Keep bearer tokens and signing credentials out of source control.
- Store secrets using environment variables or an appropriate secret-management mechanism.
- Validate request parameters and handle upstream API failures safely.
- Avoid returning internal exception details to clients in production.
- Restrict network access to database and blockchain infrastructure.

## 🔗 Related Repositories

- [VaccineChain Frontend](https://github.com/VaccineChain/VaccineChain-FE)
- [VaccineChain Blockchain](https://github.com/VaccineChain/VaccineChain-Blockchain)
- [VaccineChain Explorer](https://github.com/VaccineChain/VaccineChain-Explorer)
- [VaccineChain IoT](https://github.com/VaccineChain/VaccineChain-IoT)

## 📚 References

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [Docker Compose Documentation](https://docs.docker.com/compose/)
- [Hyperledger Fabric Documentation](https://hyperledger-fabric.readthedocs.io/)

---

<p align="center">
  Built with ❤️ for vaccine traceability and cold-chain monitoring.
</p>
