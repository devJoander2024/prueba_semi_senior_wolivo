# Proyecto API - Gestión de Usuarios, Proyectos y Actividades

Este proyecto consiste en una API RESTful desarrollada en **ASP.NET Core** que permite la gestión de usuarios, proyectos y actividades. Está conectada a una base de datos **SQL Server**, y expone endpoints para realizar operaciones CRUD y generar reportes.

---

## 🧱 Entidades del Sistema

### Usuario
| Campo        | Tipo     | Descripción                  |
|--------------|----------|------------------------------|
| UsuarioId    | GUID     | Identificador único          |
| Nombres      | string   | Nombres del usuario          |
| Apellidos    | string   | Apellidos del usuario        |
| Correo       | string   | Correo electrónico           |
| Telefono     | string   | Teléfono                     |
| Rol          | enum     | Admin / Editor / Viewer      |
| Estado       | bool     | Activo o inactivo            |

### Proyecto
| Campo        | Tipo     | Descripción                     |
|--------------|----------|---------------------------------|
| ProyectoId   | GUID     | Identificador único             |
| Nombre       | string   | Nombre del proyecto             |
| Descripcion  | string   | Descripción del proyecto        |
| FechaInicio  | DateTime | Fecha de inicio                 |
| FechaFin     | DateTime | Fecha de finalización           |
| Estado       | enum     | Activo / Inactivo               |
| UsuarioId    | GUID     | Usuario responsable (FK)        |

### Actividad
| Campo          | Tipo     | Descripción                   |
|----------------|----------|-------------------------------|
| ActividadId    | GUID     | Identificador único           |
| Titulo         | string   | Título de la actividad        |
| Descripcion    | string   | Descripción                   |
| Fecha          | DateTime | Fecha de la actividad         |
| HorasEstimadas | int      | Estimación de horas           |
| HorasReales    | int      | Horas reales                  |
| ProyectoId     | GUID     | Proyecto asociado (FK)        |

---

## 📦 Endpoints REST

### Usuarios
- `GET /api/usuarios` — Obtener todos
- `GET /api/usuarios/{id}` — Obtener uno
- `POST /api/usuarios` — Crear
- `PUT /api/usuarios/{id}` — Actualizar
- `DELETE /api/usuarios/{id}` — Eliminar

### Proyectos
- `GET /api/proyectos`
- `GET /api/proyectos/{id}`
- `POST /api/proyectos`
- `PUT /api/proyectos/{id}`
- `DELETE /api/proyectos/{id}`

### Actividades
- `GET /api/actividades`
- `GET /api/actividades/{id}`
- `POST /api/actividades`
- `PUT /api/actividades/{id}`
- `DELETE /api/actividades/{id}`

### Reportes
- `GET /api/reportes/actividades?usuarioId={id}&fechaDesde=yyyy-MM-dd&fechaHasta=yyyy-MM-dd`

---

## ⚙️ Requisitos Previos

- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop) (Opcional)
- [Postman](https://www.postman.com/downloads/)

---

## 🐳 Ejecutar SQL Server en Docker

Si no tienes SQL Server instalado, puedes usar Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Pass12345?." \
   -p 1435:1433 --name sqlserver \
   -d mcr.microsoft.com/mssql/server:latest
```

## 🛠️ Migraciones y Base de Datos

Si ya tienes una instancia de SQL Server corriendo, asegúrate de que coincidan las credenciales definidas en el archivo appsettings.json:

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\master,1435; Database=prueba; User Id=sa; Password=Pass12345?.; TrustServerCertificate=True;"
}
```

## 🛠️ Ejecutar Migraciones

Abre Visual Studio.

Ve a: Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes

Ejecuta el siguiente comando para aplicar las migraciones y crear la base de datos:

```bash
Update-Database
```

Después de aplicar las migraciones, ejecuta la aplicación desde Visual Studio o desde la terminal con:

```bash
dotnet run
```

Puedes probar los endpoints de dos formas:

- A través de Swagger UI (https://localhost:7282/swagger/index.html)
- Importando la colección de Postman incluida en el proyecto (Collection.json)

