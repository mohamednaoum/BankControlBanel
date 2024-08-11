# BankControlPanelAPI

## Overview

This project It uses SQL Server as the database and Entity Framework Core for ORM. This guide provides instructions for setting up the database on a Mac using Docker and DBeaver and managing migrations with Entity Framework Core.

## Prerequisites

Ensure you have the following installed on your system:

- **Docker Desktop**: [Download and Install Docker Desktop](https://www.docker.com/products/docker-desktop) for macOS.
- **DBeaver**: [Download and Install DBeaver](https://dbeaver.io/download/) for macOS.
- **.NET SDK**: [Download and Install .NET SDK](https://dotnet.microsoft.com/download) (.NET8).

## Setup Instructions

### 1. Run SQL Server in Docker

1. **Open Terminal**: Use Spotlight (Cmd + Space) to open the Terminal app.

2. **Pull the SQL Server Docker Image**:

   ```bash
   docker pull --platform=linux/amd64 mcr.microsoft.com/mssql/server
   ```

3. **Run SQL Server Container**:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" --platform=linux/amd64 -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server
   ```

    - Replace `Password123` with your strong password if needed.

4. **Verify Container is Running**:

   ```bash
   docker ps
   ```

   Ensure the container named `sqlserver` is listed and running.

### 2. Connect to SQL Server using DBeaver

1. **Launch DBeaver**: Open DBeaver from your Applications folder or using Spotlight.

2. **Create a New Database Connection**:

    - Click on **"New Database Connection"** (or navigate to **Database > New Connection**).
    - Select **"SQL Server"** from the list of databases.

3. **Configure the Connection**:

    - **Host**: `localhost`
    - **Port**: `1433`
    - **Database**: `master` (or any database you want to connect to)
    - **Username**: `SA`
    - **Password**: `Password123`

4. **Test the Connection**: Click **"Test Connection"** to ensure DBeaver can connect to the SQL Server instance.

5. **Connect**: If the test is successful, click **"Finish"** to establish the connection.

### 3. Manage Database Migrations with Entity Framework Core

1. **Add a Migration**: Create a new migration using the following command:

   ```bash
   dotnet ef migrations add InitialCreate --project BankingControlPanel.Infrastructure --startup-project YourStartupProject
   ```

    - Replace `InitialCreate` with the name you want for your migration.
    - Ensure `--startup-project` points to the correct project if it's different from your main project.

2. **Apply Migrations**: Update the database to the latest migration:

   ```bash
   dotnet ef database update --project BankingControlPanel.Infrastructure --startup-project YourStartupProject
   ```

3. **Verify Database Changes**: Use DBeaver to connect to the database and verify that the tables and schema are created as expected.

## Accessing and Using Swagger UI

### Accessing Swagger UI

1. **Open Browser**: Launch your web browser.

2. **Navigate to Swagger UI**: Enter the following URL in the address bar:

   ```
   http://localhost:5147/swagger/index.html
   ```

   This URL will display the Swagger UI, where you can interact with your API endpoints.

### Trying API Requests

1. **Explore Endpoints**: Swagger UI provides an interactive interface to view and test all available API endpoints in your application.

2. **Test API Endpoints**:

   - Click on an endpoint to expand its details.
   - Fill in any required parameters or request bodies.
   - Click **"Try it out"** to execute the request and view the response directly in the browser.

