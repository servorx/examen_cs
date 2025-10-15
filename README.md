# 🚗 AutoTallerManager backend

**AutoTallerManager** es un backend **RESTful** diseñado para cubrir de forma integral las operaciones de un **taller automotriz moderno**.  
Su propósito es **centralizar y automatizar procesos clave** como la gestión de clientes, vehículos, órdenes de servicio, repuestos y facturación, garantizando **trazabilidad, control financiero** y **eficiencia operativa** para mecánicos, recepcionistas y administradores.

---

## 🧩 Arquitectura General

El sistema está desarrollado sobre **ASP.NET Core** y sigue el patrón de **arquitectura hexagonal (Ports & Adapters)**, dividiéndose en **cuatro capas principales**:

### 1. 🧠 Capa de Dominio
Contiene las **entidades esenciales** y la **lógica de negocio principal**.

#### Entidades principales:
- **Cliente** → Propietario de vehículos, con datos de contacto.  
- **Vehículo** → Asociado a un cliente; incluye marca, modelo, año, VIN y kilometraje.  
- **OrdenServicio** → Representa una solicitud de trabajo (mantenimiento, reparación, diagnóstico).  
- **Repuesto** → Piezas o insumos con código, descripción, stock y precio unitario.  
- **DetalleOrden** → Relación entre orden y repuestos usados (cantidad, costo).  
- **Usuario** → Representa a los usuarios del sistema (Admin, Mecánico, Recepcionista) con credenciales y roles.  
- **Factura** → Documento generado al cerrar una orden, con resumen de servicios, repuestos y total.

#### Lógica clave:
- Validación de disponibilidad de vehículo y mecánico.
- Cálculo automático de fechas estimadas.
- Control de inventario (no permite usar repuestos sin stock).
- Generación de montos totales de factura.

---

### 2. ⚙️ Capa de Aplicación
Orquesta los **casos de uso del negocio** mediante servicios y **DTOs (Data Transfer Objects)**.

#### Casos de uso destacados:
- **RegistrarClienteConVehiculo** → Crea un cliente y uno o varios vehículos asociados.  
- **CrearOrdenServicio** → Genera una orden, asigna mecánico y reserva repuestos disponibles.  
- **ActualizarOrdenConTrabajoRealizado** → Registra avances, actualiza estado y descuenta stock.  
- **GenerarFactura** → Calcula mano de obra y repuestos, generando la factura final.

#### Características:
- Uso de **AutoMapper** para mapear entidades ↔ DTOs.
- Validaciones de negocio centralizadas.
- Exposición controlada de datos (oculta contraseñas, muestra campos calculados).

---

### 3. 🗃️ Capa de Infraestructura
Implementa la **persistencia de datos** y comunicación con la base de datos.

#### Tecnologías:
- **Entity Framework Core** con **Fluent API**.
- Base de datos **PostgreSQL**.

#### Características técnicas:
- Configuración completa en `AutoTallerDbContext`:
  - Relaciones (1:N, N:M).
  - Restricciones de borrado seguro.
- **Unit of Work (`IUnitOfWork`)** para manejar transacciones atómicas entre repositorios, estos son algunos ejemplos:
  - Clientes
  - Vehículos
  - Órdenes de servicio
  - Repuestos
  - Facturas
  - Usuarios

---

### 4. 🌐 Capa de API
Exposición de los servicios a través de **ASP.NET Core Controllers** bajo el estándar **RESTful**.

#### Controladores principales:
| Controlador | Descripción |
|--------------|--------------|
| **ClientesController** | CRUD de clientes, listado paginado |
| **VehiculosController** | CRUD de vehículos, filtrado por cliente o VIN |
| **OrdenesServicioController** | Creación, actualización, cierre y cancelación de órdenes |
| **RepuestosController** | Gestión de inventario, actualización de stock |
| **FacturasController** | Generación y consulta de facturas |
| **UsuariosController** | Gestión de credenciales y roles |

#### Características adicionales:
- **Respuestas JSON** con códigos HTTP estándar (200, 201, 204, 400, 404, 500).  
- **Rate Limiting** (con `AspNetCoreRateLimit`):
  - Ej: 60 solicitudes/min en `/api/ordenesservicio`, 30/min en `/api/repuestos`.
- **Autenticación JWT**:
  - Endpoint `/api/auth/token` → emite token JWT.
  - Claims: `sub`, `email`, `role`.
- **Autorización por roles**:
  - **Admin** → acceso total (usuarios, repuestos, informes).  
  - **Mecánico** → gestiona órdenes y genera facturas.  
- **Swagger / OpenAPI**:
  - Documentación interactiva con soporte para autenticación JWT.

---

## 🚀 Funcionalidades Clave

### 🔹 Gestión de Clientes y Vehículos
- Registro de clientes con datos completos.  
- Asociación de múltiples vehículos a un cliente.  
- Edición y eliminación controladas (restricciones si hay órdenes activas).

### 🔹 Órdenes de Servicio
- Creación de nuevas órdenes con validaciones de disponibilidad.  
- Actualización de estado (`pendiente`, `en proceso`, `completada`, `cancelada`).  
- Registro de trabajo realizado y avance de la orden.  
- Listados filtrables por fecha, cliente, estado o mecánico.

### 🔹 Control de Inventario
- CRUD completo de repuestos con stock en tiempo real.  
- Validación automática de stock antes de asignar repuestos.  
- Filtrado por descripción, categoría o nivel de stock.

### 🔹 Facturación
- Cálculo automático del total (mano de obra + repuestos).  
- Generación de facturas detalladas vinculadas a la orden.  
- Historial de facturas por cliente, fecha o número de orden.

### 🔹 Autenticación y Roles
- Login con emisión de token JWT.  
- Roles con permisos diferenciados:
  - **Admin:** gestión completa.  
  - **Mecánico:** actualiza órdenes y genera facturas.  
  - **Recepcionista:** crea órdenes, consulta información.

### 🔹 Paginación y Filtrado
- Parámetros `pageNumber`, `pageSize` y filtros dinámicos.  
- Encabezado `X-Total-Count` en cada respuesta de listado.

### 🔹 Rate Limiting
- Control de solicitudes por ruta.
- Respuesta `HTTP 429` (Too Many Requests) cuando se excede el límite.

### 🔹 Auditoría
- Registro de todas las acciones importantes en tabla `Auditorias`:  
  - Entidad afectada, tipo de acción, usuario, timestamp.  
- Consultas para trazabilidad de operaciones.

### 🔹 Migraciones y Sincronización
- Uso de **EF Core Migrations** (`InitialCreate`, `AddRepuestosTable`, etc.).
- Compatibilidad con entornos **MySQL** y **PostgreSQL**.

### 🔹 Documentación Swagger
- Documentación auto-generada y navegable.  
- Soporte para autenticación Bearer JWT desde la interfaz.

---

## 🧱 Tecnologías Principales

| Categoría | Tecnología |
|------------|-------------|
| Backend | ASP.NET Core 9 |
| ORM | Entity Framework Core |
| Base de Datos | PostgreSQL |
| Arquitectura | Hexagonal (Ports & Adapters) |
| Seguridad | JWT (JSON Web Token) |
| Documentación | Swagger / OpenAPI |
| Control de Flujo | AspNetCoreRateLimit |
| Mapeo de Objetos | AutoMapper |
| Patrón de Persistencia | Repository + Unit of Work |

---

## 🧰 Requisitos de Entorno

- .NET SDK 9.0+
- Postgres 16+
- Visual Studio / VS Code
- EF Core CLI (`dotnet ef`)
- Swagger UI para pruebas

---

## ⚙️ Instalación y Ejecución

### 1. clone el repositorio
```
git clone https://github.com/servorx/backend_cs
cd backend_cs
code .
```

### 2. Ejecutar docker-compose
```
cd docker
docker compose up -d
```
Despues de este paso se debe de abrir pgadmin con el localhost:8080 y crear la base de datos backend_cs

### 3. Crear migraciones 
```
cd .. 
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api -o Data/Migrations
dotnet ef database update --project Infrastructure --startup-project Api   --connection "Host=localhost;Port=5433;Database=backend_cs;Username=postgres;Password=postgres"
```

### 4. Ejecutar el proyecto con swagger
```
dotnet  watch run --project Api --startup-project Api
```

## Configuracion del proyecto 
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "PostgresDocker": "Host=db;Port=5432;Database=backend_cs;Username=postgres;Password=postgres",
    "PostgresLocal": "Host=localhost;Port=5433;Database=backend_cs;Username=postgres;Password=postgres"
  },
  "JWT": {
    "Key": "njMCY^UbEskeAFL6eDzHuqY!s^x6Qrwe",
    "Issuer": "MyStoreApi",
    "Audience": "MyStoreApiUser",
    "DurationInMinutes": 60
  }
}
```

el Key es la clave de encriptación de JWT, el Issuer es el nombre del emisor del JWT y el Audience es el nombre del receptor del JWT.

El Issuer es quien emite el token, en este caso la ejecucion del backend 
El Audience es quien recibe el token, en este caso el frontend 
La duración del token es de 60 minuto, esto es para que el token no expire y se vuelva a emitir.


### Instacion de paquetes

```bash

# Instalar paquetes de NuGet 
# En dominio no se necesitan paquetes externos 
# En Application
cd Application/
dotnet add Application/Application.csproj package AutoMapper --version 13.0.1
dotnet add Application/Application.csproj package FluentValidation --version 12.0.0
dotnet add Application/Application.csproj package MediatR --version 13.0.0
# En infrastructure
cd Infrastructure/
dotnet add Infrastructure/Infrastructure.csproj package Microsoft.EntityFrameworkCore --version 9.0.9
dotnet add Infrastructure/Infrastructure.csproj package Aspire.Npgsql.EntityFrameworkCore.PostgreSQL --version 9.4.2
# En api
cd Api/
dotnet add Api/Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.9
dotnet add Api/Api.csproj package System.IdentityModel.Tokens.Jwt --version 8.14.0
dotnet add Api/Api.csproj package AutoMapper --version 13.0.1
dotnet add Api/Api.csproj package FluentValidation.DependencyInjectionExtensions --version 12.0.0
dotnet add Api/Api.csproj package Microsoft.AspNetCore.OpenApi --version 9.0.9
dotnet add Api/Api.csproj package Microsoft.EntityFrameworkCore.Design --version 9.0.9
dotnet add Api/Api.csproj package Swashbuckle.AspNetCore --version 9.0.5
```