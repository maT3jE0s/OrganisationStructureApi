# REST API – Company Organizational Structure
REST API for managing employees and a 4-level hierarchical organizational structure
(Company → Division → Project → Department).

## Requirements
* [.NET SDK 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
* **Microsoft SQL Server** (Express or any other edition)

## Getting Started

### 1. Database Setup
1. Open your database management tool (e.g., SQL Server Management Studio)
2. Execute the `SetupDb.sql` script located in the root directory.
3. The script will automatically create the database and the necessary tables (`Employees` and `OrgNodes`).

### 2. Run the API
1. Update the connection string in `appsettings.json` to point to your SQL Server instance.
2. Run the following command to start the API:
   ```bash
   dotnet run
   ```

### Testing the API (Scalar)
Once the API is running, open your browser and navigate to `http://localhost:5080/scalar`.
Here you can explore the endpoints and interact with the API using the integrated Scalar UI.
