# DatabaseGUIManager

## About

This is a simple database manager with a WinForms GUI and PostgreSQL CRUD
operations. The project uses Npgsql and targets .NET Framework 4.6.1.

## Getting started

1. Open `DatabaseUIManager.sln` in Visual Studio 2017 or a compatible version.
2. Restore the NuGet packages, including Npgsql 4.0.5.
3. Start PostgreSQL 11 or later.
4. Set the connection string before running the application:

   ```powershell
   $env:DATABASE_GUI_MANAGER_CONNECTION_STRING = "Host=127.0.0.1;Username=postgres;Password=<your-password>"
   ```

5. Build and run the project from Visual Studio.