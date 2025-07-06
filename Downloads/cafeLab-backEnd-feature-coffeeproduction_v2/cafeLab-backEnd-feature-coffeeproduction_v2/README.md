# CafeLab Backend API

Backend API para CafeLab siguiendo Domain-Driven Design (DDD) con bounded contexts.

## 📋 Descripción General

Este proyecto implementa una API REST completa para la gestión de una cafetería, siguiendo las mejores prácticas de Domain-Driven Design (DDD) y arquitectura limpia. La aplicación está organizada en bounded contexts que encapsulan diferentes áreas de negocio.

### 🎯 Objetivos del Proyecto

- **Gestión de usuarios**: Sistema completo de autenticación y autorización
- **Control de calidad**: Gestión de defectos del café y calibración de equipos
- **Producción de café**: Administración de lotes, proveedores y perfiles de tueste
- **Escalabilidad**: Arquitectura modular que permite crecimiento futuro
- **Mantenibilidad**: Código bien documentado y siguiendo principios SOLID

## Estructura del Proyecto

El proyecto está organizado en bounded contexts siguiendo las mejores prácticas de DDD:

### Bounded Contexts

1. **Profiles** - Gestión de perfiles de usuarios
2. **CoffeeProduction** - Gestión de producción de café (lotes, proveedores, perfiles de tueste)
3. **IAM** - Identity and Access Management (autenticación y autorización)
4. **Defects** - Gestión de defectos del café
5. **Calibration** - Gestión de calibración de equipos

### Arquitectura por Bounded Context

Cada bounded context sigue la estructura:

```
BoundedContext/
├── Application/
│   ├── Internal/
│   │   ├── CommandServices/
│   │   └── QueryServices/
│   └── ACL/ (Anti-Corruption Layer)
├── Domain/
│   ├── Model/
│   │   ├── Aggregates/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   └── ValueObjects/
│   ├── Repositories/
│   └── Services/
├── Infrastructure/
│   └── Persistence/
│       └── EFC/
│           └── Repositories/
└── Interfaces/
    ├── REST/
    │   ├── Resources/
    │   ├── Transform/
    │   └── Controllers/
    └── ACL/
```

## Características Principales

### IAM Context
- **Autenticación JWT**: Sistema completo de autenticación con tokens JWT
- **Gestión de usuarios**: Registro, login, y gestión de usuarios
- **Autorización**: Middleware de autorización basado en JWT
- **Roles**: Sistema de roles (barista, owner, admin)

### Defects Context
- **Biblioteca de defectos**: Registro de problemas comunes del café
- **Causas probables**: Documentación de causas de defectos
- **Soluciones recomendadas**: Guías de solución para cada defecto
- **Categorización**: Organización por categorías de defectos

### Calibration Context
- **Gestión de equipos**: Registro y seguimiento de equipos
- **Calibración**: Proceso de calibración con valores objetivo y medidos
- **Tolerancias**: Control de tolerancias para cada calibración
- **Programación**: Fechas de calibración y próximas calibraciones

### CoffeeProduction Context
- **Gestión de lotes**: Administración de lotes de café
- **Proveedores**: Información de proveedores de café
- **Perfiles de tueste**: Configuraciones de tueste para diferentes lotes

### Profiles Context
- **Perfiles de usuario**: Información de usuarios del sistema
- **Planes**: Gestión de planes de suscripción
- **Roles**: Diferenciación entre baristas y dueños

## Tecnologías Utilizadas

- **.NET 9.0**: Framework principal
- **Entity Framework Core**: ORM para acceso a datos
- **MySQL**: Base de datos
- **JWT**: Autenticación y autorización
- **BCrypt**: Hashing de contraseñas
- **Swagger**: Documentación de API

## Configuración

### Base de Datos
La aplicación utiliza MySQL. Configura la cadena de conexión en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=your-server;user=your-user;password=your-password;database=cafe-lab"
  }
}
```

### JWT Settings
Configura los parámetros de JWT en `appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-here-make-it-long-enough-for-security",
    "Issuer": "CafeLab",
    "Audience": "CafeLabUsers",
    "ExpirationHours": 24
  }
}
```

## Endpoints Principales

### IAM
- `POST /api/v1/users/sign-up` - Registro de usuarios
- `POST /api/v1/users/sign-in` - Autenticación
- `GET /api/v1/users/{id}` - Obtener usuario por ID
- `GET /api/v1/users` - Listar todos los usuarios

### Defects
- `GET /api/v1/defects` - Listar defectos del usuario
- `POST /api/v1/defects` - Crear nuevo defecto
- `PUT /api/v1/defects/{id}` - Actualizar defecto
- `DELETE /api/v1/defects/{id}` - Eliminar defecto
- `GET /api/v1/defects/search?name={name}` - Buscar defectos por nombre

### Calibration
- `GET /api/v1/calibrations` - Listar calibraciones del usuario
- `POST /api/v1/calibrations` - Crear nueva calibración
- `PUT /api/v1/calibrations/{id}` - Actualizar calibración
- `DELETE /api/v1/calibrations/{id}` - Eliminar calibración
- `GET /api/v1/calibrations/search?equipmentName={name}` - Buscar por nombre de equipo

### CoffeeProduction
- `GET /api/v1/coffee-lots` - Listar lotes de café
- `GET /api/v1/suppliers` - Listar proveedores
- `GET /api/v1/roast-profiles` - Listar perfiles de tueste

## Autenticación

La API utiliza JWT para autenticación. Para acceder a endpoints protegidos:

1. Registra un usuario con `POST /api/v1/users/sign-up`
2. Autentícate con `POST /api/v1/users/sign-in`
3. Incluye el token JWT en el header `Authorization: Bearer {token}`

## Desarrollo

### Ejecutar el Proyecto
```bash
cd CafeLab.API
dotnet run
```

### Acceder a Swagger
Una vez ejecutado, accede a `https://localhost:7077/swagger` para ver la documentación de la API.

### Migraciones
La aplicación utiliza `EnsureCreated()` para crear la base de datos automáticamente en desarrollo.

## Patrones de Diseño

- **Domain-Driven Design (DDD)**: Organización por bounded contexts
- **Command Query Responsibility Segregation (CQRS)**: Separación de comandos y queries
- **Repository Pattern**: Abstracción del acceso a datos
- **Unit of Work**: Gestión de transacciones
- **Anti-Corruption Layer (ACL)**: Comunicación entre bounded contexts
