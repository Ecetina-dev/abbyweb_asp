# Abby

Sistema web para gestión de estudiantes. CRUD completo con ASP.NET Core y Entity Framework Core.

## Stack Tecnológico

| Tecnología | Versión |
|---|---|
| .NET | 10.0 |
| ASP.NET Core | 10.0 (Razor Pages) |
| Entity Framework Core | 10.0.9 |
| SQL Server | — |
| Bootstrap | 5.x (incluido) |

## Estructura del Proyecto

```
Abby/
├── Abby.slnx                    # Solución .NET 10
├── README.md
└── AbbyWeb/                     # Proyecto web
    ├── Program.cs               # Punto de entrada y configuración
    ├── AbbyWeb.csproj           # Dependencias NuGet
    ├── appsettings.json         # Configuración (connection strings)
    ├── Data/
    │   └── ApplicationDbContext.cs  # DbContext de EF Core
    ├── Models/
    │   └── Estudiante.cs        # Entidad Estudiante
    ├── Pages/
    │   ├── Index.cshtml         # Página principal
    │   ├── Error.cshtml         # Página de error
    │   ├── Estudiantes/
    │   │   ├── Index.cshtml     # Listar estudiantes
    │   │   ├── Create.cshtml    # Crear estudiante
    │   │   ├── Edit.cshtml      # Editar estudiante
    │   │   └── Delete.cshtml    # Eliminar estudiante
    │   └── Shared/              # Layouts y partials
    ├── Migrations/              # Migraciones de EF Core
    └── wwwroot/                 # Archivos estáticos (CSS, JS, imágenes)
```

## Modelo de Datos

### Estudiante

| Campo | Tipo | Requerido |
|---|---|---|
| Id_Estudiante | int (PK, autoincremental) | ✅ |
| Nombres | string (100) | ✅ |
| Apellidos | string (100) | ✅ |
| Direccion | string (200) | ❌ |
| Universidad | string (150) | ❌ |
| Telefono | string (10) | ❌ |
| Correo | string (150) | ❌ |
| Semestre | int (1-12) | ❌ |
| Foto | byte[] | ❌ |

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (local o remoto)

## Configuración

1. Clonar el repositorio
2. Configurar la connection string en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AbbyDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Aplicar migraciones:

```bash
dotnet ef database update --project AbbyWeb
```

4. Ejecutar:

```bash
dotnet run --project AbbyWeb
```

## Comandos Útiles

```bash
# Ejecutar la aplicación
dotnet run --project AbbyWeb

# Agregar una migración
dotnet ef migrations add NombreMigracion --project AbbyWeb

# Aplicar migraciones a la BD
dotnet ef database update --project AbbyWeb

# Ver migraciones existentes
dotnet ef migrations list --project AbbyWeb

# Compilar sin ejecutar
dotnet build --project AbbyWeb
```

## Skills de Desarrollo

Este proyecto está configurado con skills oficiales de Microsoft para .NET:

- `dotnet-efcore-queries` — optimización de consultas EF Core
- `dotnet-webapi` — patrones ASP.NET Core
- `dotnet-mstest-tests` — testing unitario
- `work-unit-commits` — commits lógicos y revisables
- `comment-writer` — comentarios de código profesionales
- `cognitive-doc-design` — documentación clara y efectiva
