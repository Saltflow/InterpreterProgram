# InterpreterProgram

WHU course projects collected in one repository.

## Projects

### InterpreterProgram

An educational interpreter-construction project with a WinForms GUI. It includes
lexical analysis, parsing, parse-tree visualization, semantic processing, and
sample `.cmm` programs.

- Solution: `IntepreterProgram.sln`
- Project: `IntepreterProgram/IntepreterProgram.csproj`
- Examples: `TestProgram/`
- Diagrams: `Graphs/`

### DatabaseGUIManager

A WinForms database manager for PostgreSQL, with basic table and column CRUD
operations. The original project notes assume PostgreSQL 11 or later and
Visual Studio 2017.

- Solution: `DatabaseGUIManager/DatabaseUIManager.sln`
- Project: `DatabaseGUIManager/DatabaseUIManager/DatabaseUIManager.csproj`
- Project-specific notes: `DatabaseGUIManager/README.md`

## Directory

```text
.
├── IntepreterProgram.sln
├── IntepreterProgram/       # Interpreter source and WinForms UI
├── TestProgram/             # Interpreter sample programs
├── Graphs/                  # Design and analysis diagrams
└── DatabaseGUIManager/
    ├── DatabaseUIManager.sln
    └── DatabaseUIManager/   # PostgreSQL database manager source
```

## Build and run

1. Open the required `.sln` file in Visual Studio 2017 or a later compatible
   Visual Studio version.
2. Restore NuGet packages for `DatabaseGUIManager` (including `Npgsql 4.0.5`).
3. Build and run the selected project from Visual Studio.

Both projects target `.NET Framework 4.6.1`. `DatabaseGUIManager` also requires
a reachable PostgreSQL instance. Set the connection string through the
`DATABASE_GUI_MANAGER_CONNECTION_STRING` environment variable before running
database operations, for example:

```powershell
$env:DATABASE_GUI_MANAGER_CONNECTION_STRING = "Host=127.0.0.1;Username=postgres;Password=<your-password>"
```
