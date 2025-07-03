# CafeLab - Sistema Completo con IAM

Sistema moderno de gestión de café con autenticación IAM (Identity and Access Management) y JWT, siguiendo las mejores prácticas del Learning Center Platform.

## 🚀 Características Principales

### 🔐 Autenticación IAM
- **JWT (JSON Web Tokens)** con refresh tokens automáticos
- **BCrypt** para hash de contraseñas
- **Protección de rutas** por roles (Admin, User)
- **Bloqueo de cuentas** después de intentos fallidos
- **Auditoría completa** de sesiones y actividades

### 🏗️ Arquitectura
- **Backend**: .NET 9.0 con Entity Framework Core
- **Frontend**: Vue.js 3 con Composition API
- **Base de datos**: MySQL con migraciones automáticas
- **API**: RESTful con documentación Swagger

### 📊 Módulos Principales
- **Defects**: Biblioteca de defectos del café
- **Calibrations**: Sistema de calibración de equipos
- **User Management**: Gestión de usuarios y roles

### 🌍 Internacionalización
- Soporte completo para **Español** e **Inglés**
- Traducciones dinámicas en tiempo real
- Formateo de fechas y números por región

## 🛠️ Tecnologías Utilizadas

### Backend (.NET 9.0)
- **ASP.NET Core Web API**
- **Entity Framework Core** con MySQL
- **JWT Authentication** con refresh tokens
- **BCrypt.Net-Next** para hash de contraseñas
- **AutoMapper** para mapeo de DTOs
- **FluentValidation** para validaciones
- **Serilog** para logging estructurado
- **Swagger/OpenAPI** para documentación

### Frontend (Vue.js 3)
- **Vue 3** con Composition API
- **Vue Router** con guards de navegación
- **Vue I18n** para internacionalización
- **Axios** con interceptors para JWT
- **Tailwind CSS** para estilos
- **Vite** como bundler

### Base de Datos
- **MySQL** como motor principal
- **Entity Framework Migrations**
- **Auditoría automática** (CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)

## 📋 Requisitos Previos

### Software Requerido
- **.NET 9.0 SDK** - [Descargar](https://dotnet.microsoft.com/download)
- **Node.js 18+** - [Descargar](https://nodejs.org/)
- **MySQL 8.0+** - [Descargar](https://dev.mysql.com/downloads/)
- **Git** - [Descargar](https://git-scm.com/)

### Configuración de Base de Datos
```sql
-- Crear base de datos
CREATE DATABASE cafelab CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Crear usuario (opcional)
CREATE USER 'cafelab_user'@'localhost' IDENTIFIED BY 'your_password';
GRANT ALL PRIVILEGES ON cafelab.* TO 'cafelab_user'@'localhost';
FLUSH PRIVILEGES;
```

## 🚀 Instalación y Configuración

### 1. Clonar el Repositorio
```bash
git clone <repository-url>
cd CafeLab
```

### 2. Configurar Base de Datos
Editar `CafeLab.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=cafelab;User=root;Password=your_password;"
  }
}
```

### 3. Ejecutar Script de Inicio
```bash
# Windows
run-with-auth.bat

# Linux/Mac
./run-with-auth.sh
```

### 4. Instalación Manual (Alternativa)

#### Backend
```bash
cd CafeLab.API
dotnet restore
dotnet ef database update
dotnet run --urls=http://localhost:5000
```

#### Frontend
```bash
cd CafeLab.API/frontend-reference
npm install
npm run dev
```

## 🔐 Configuración de Autenticación

### JWT Settings
```json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-256-bits-minimum",
    "Issuer": "CafeLab.API",
    "Audience": "CafeLab.Frontend",
    "ExpirationMinutes": 60,
    "RefreshExpirationDays": 7
  }
}
```

### Roles del Sistema
- **Admin**: Acceso completo a todos los módulos
- **User**: Acceso a módulos básicos (Defects, Calibrations)

### Endpoints de Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar usuario
- `POST /api/auth/refresh` - Renovar token
- `POST /api/auth/logout` - Cerrar sesión
- `GET /api/auth/profile` - Obtener perfil
- `GET /api/auth/users` - Listar usuarios (Admin)

## 📊 Estructura del Proyecto

```
CafeLab.API/
├── CafeLab.API/
│   ├── Application/
│   │   ├── DTOs/           # Data Transfer Objects
│   │   ├── Interfaces/     # Interfaces de servicios
│   │   └── Services/       # Servicios de aplicación
│   ├── Controllers/        # Controladores API
│   ├── Domain/
│   │   ├── Entities/       # Entidades de dominio
│   │   └── Interfaces/     # Interfaces de repositorio
│   ├── Infrastructure/
│   │   ├── Data/          # Contexto de base de datos
│   │   ├── Persistence/   # Repositorios EF
│   │   └── Security/      # Servicios de seguridad
│   └── Resources/         # Archivos de recursos
├── frontend-reference/
│   ├── src/
│   │   ├── auth/          # Componentes de autenticación
│   │   ├── roasting/      # Módulos de tostado
│   │   ├── shared/        # Componentes compartidos
│   │   └── locales/       # Traducciones
│   └── public/            # Archivos estáticos
└── README-IAM-SYSTEM.md
```

## 🔧 Configuración de Desarrollo

### Variables de Entorno
```bash
# Backend
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://localhost:5000

# Frontend
VITE_API_BASE_URL=http://localhost:5000/api
```

### Logging
El sistema utiliza Serilog para logging estructurado:
- **Console**: Para desarrollo
- **File**: Para producción
- **Database**: Para auditoría

### Migraciones
```bash
# Crear migración
dotnet ef migrations add MigrationName

# Aplicar migraciones
dotnet ef database update

# Revertir migración
dotnet ef database update PreviousMigrationName
```

## 🧪 Testing

### Backend Tests
```bash
dotnet test
```

### Frontend Tests
```bash
npm run test
```

### API Testing
- **Swagger UI**: http://localhost:5000/swagger
- **Postman Collection**: Disponible en `/docs`

## 📈 Auditoría y Seguridad

### Características de Auditoría
- **Trail de auditoría** completo en todas las entidades
- **Logging de autenticación** con IP y timestamp
- **Bloqueo automático** después de 5 intentos fallidos
- **Refresh tokens** con expiración configurable
- **Validación de roles** en cada endpoint

### Seguridad Implementada
- **BCrypt** para hash de contraseñas
- **JWT** con firma digital
- **CORS** configurado para frontend
- **Validación de entrada** en todos los endpoints
- **Sanitización** de datos de entrada

## 🌍 Internacionalización

### Idiomas Soportados
- **Español (es)** - Idioma por defecto
- **Inglés (en)** - Idioma alternativo

### Cambiar Idioma
```javascript
// En el frontend
this.$i18n.locale = 'en' // Cambiar a inglés
this.$i18n.locale = 'es' // Cambiar a español
```

## 🚀 Despliegue

### Backend (Producción)
```bash
dotnet publish -c Release -o ./publish
dotnet ./publish/CafeLab.API.dll
```

### Frontend (Producción)
```bash
npm run build
# Servir archivos de /dist
```

### Docker (Opcional)
```dockerfile
# Dockerfile para backend
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY ./publish /app
WORKDIR /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "CafeLab.API.dll"]
```

## 📚 Documentación API

### Swagger UI
- **URL**: http://localhost:5000/swagger
- **Autenticación**: Bearer Token
- **Ejemplos**: Incluidos para cada endpoint

### Endpoints Principales

#### Autenticación
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "password123"
}
```

#### Defects
```http
GET /api/defects
Authorization: Bearer <token>

POST /api/defects
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Defecto de Tueste",
  "description": "Descripción del defecto",
  "category": "Primary",
  "severity": "Medium"
}
```

#### Calibrations
```http
GET /api/calibrations
Authorization: Bearer <token>

POST /api/calibrations
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Calibración Molino",
  "description": "Calibración del molino principal",
  "calibrationDate": "2024-01-15",
  "type": "Equipment"
}
```

## 🤝 Contribución

### Guías de Contribución
1. **Fork** el repositorio
2. **Crear** una rama para tu feature
3. **Commit** tus cambios con mensajes descriptivos
4. **Push** a la rama
5. **Crear** un Pull Request

### Estándares de Código
- **Backend**: C# coding conventions
- **Frontend**: Vue.js style guide
- **Commits**: Conventional commits
- **Documentación**: XML comments en C#

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 🆘 Soporte

### Problemas Comunes

#### Error de Conexión a Base de Datos
```bash
# Verificar que MySQL esté ejecutándose
sudo systemctl status mysql

# Verificar credenciales en appsettings.json
```

#### Error de CORS
```bash
# Verificar configuración en Program.cs
# Asegurar que el frontend esté en puerto 5173
```

#### Token Expirado
```bash
# El sistema maneja automáticamente refresh tokens
# Verificar configuración de JWT en appsettings.json
```

### Contacto
- **Issues**: Crear issue en GitHub
- **Documentación**: Ver `/docs` folder
- **Email**: soporte@cafelab.com

## 🔄 Changelog

### v1.0.0 (2024-01-15)
- ✅ Sistema IAM completo con JWT
- ✅ Módulos Defects y Calibrations
- ✅ Internacionalización ES/EN
- ✅ Auditoría completa
- ✅ Documentación Swagger
- ✅ Scripts de automatización

---

**CafeLab** - Sistema moderno de gestión de café con las mejores prácticas de seguridad y desarrollo. 