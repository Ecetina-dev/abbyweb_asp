# Abby — Gestión de Estudiantes

CRUD completo con ASP.NET Core + EF Core + SQL Server. Listo para correr en 3 pasos.

## Quick Path

```bash
# 1. Configurar base de datos (appsettings.json)
# 2. Aplicar migraciones
dotnet ef database update --project AbbyWeb

# 3. Correr
dotnet run --project AbbyWeb
```

## Stack

| Tecnología | Versión |
|---|---|
| .NET | 10.0 |
| ASP.NET Core | 10.0 (Razor Pages) |
| EF Core | 10.0.9 |
| SQL Server | — |
| Bootstrap | 5.x |

## Estructura

```
Abby/
├── Abby.slnx
└── AbbyWeb/
    ├── Program.cs                  # Entry point
    ├── Data/
    │   └── ApplicationDbContext.cs # EF Core DbContext
    ├── Models/
    │   └── Estudiante.cs           # Entidad: Id, Nombres, Apellidos, Direccion...
    ├── Pages/
    │   ├── Index.cshtml            # Home
    │   └── Estudiantes/            # CRUD: Create, Edit, Delete, Index
    ├── Migrations/
    └── wwwroot/
```

## Modelo: Estudiante

| Campo | Tipo | Requerido | Validación |
|---|---|---|---|
| Id_Estudiante | int (PK, auto) | ✅ | — |
| Nombres | string(100) | ✅ | Solo letras |
| Apellidos | string(100) | ✅ | Solo letras |
| Direccion | string(200) | ❌ | — |
| Universidad | string(150) | ❌ | — |
| Telefono | string(10) | ❌ | Solo dígitos |
| Correo | string(150) | ❌ | Email válido |
| Semestre | int (1-12) | ❌ | Rango 1-12 |
| Foto | byte[] | ❌ | — |

## Setup

### Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (local o remoto)

### Connection String

En `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AbbyDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Migraciones

```bash
# Aplicar
dotnet ef database update --project AbbyWeb

# Agregar nueva
dotnet ef migrations Add_Nombre --project AbbyWeb
```

## Comandos

```bash
dotnet run --project AbbyWeb          # Ejecutar
dotnet build --project AbbyWeb        # Compilar
dotnet ef migrations list --project AbbyWeb  # Listar migraciones
```

## Skills del Proyecto

| Skill | Para qué |
|---|---|
| `dotnet-efcore-queries` | Optimizar consultas EF Core |
| `dotnet-webapi` | Patrones ASP.NET Core |
| `dotnet-mstest-tests` | Tests unitarios |
| `work-unit-commits` | Commits lógicos |
| `comment-writer` | Código comentado profesional |
| `cognitive-doc-design` | Documentación clara |
