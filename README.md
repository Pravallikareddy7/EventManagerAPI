# Event Manager API

## Overview

The Event Management API is a RESTful API developed using ASP.NET Core and Entity Framework Core. It provides endpoints for managing events and user authentication with role-based access control. 

## Features

- **User Management**: Register, login, and logout users with role-based access.
- **Event Management**: Create, read, update, delete, and search for events.

## Technology Stack

- **ASP.NET Core** (latest stable version)
- **Entity Framework Core** (latest stable version)
- **Database**: SQL Server, LocalDB, PostgreSQL, or MySQL
- **JWT-based Authentication** for secure access
- **xUnit** for unit testing
- **Moq** for mocking dependencies

## Prerequisites

1. **.NET SDK**: Install the [.NET SDK](https://dotnet.microsoft.com/download) (version compatible with your project, e.g., .NET 6.0 or .NET 7.0).
2. **Database Server**: Ensure you have a database server (SQL Server, PostgreSQL, MySQL) set up. You can use SQL Server LocalDB for development.
3. **IDE**: Install an Integrated Development Environment (IDE) like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/yourusername/event-management-api.git
cd event-manager-api
