# CafeLab - Sistema de Autenticación y Gestión Completo

Sistema completo de autenticación con IAM (Identity and Access Management) y JWT, incluyendo gestión de defectos y calibraciones para laboratorios de café.

## 🚀 Características Principales

### 🔐 Autenticación y Autorización
- **JWT (JSON Web Tokens)** para autenticación segura
- **IAM (Identity and Access Management)** con roles y permisos
- **Bloqueo de cuentas** tras 5 intentos fallidos
- **Auditoría completa** de acciones de usuario
- **Refresh tokens** para sesiones prolongadas

### 📚 Biblioteca de Defectos
- **CRUD completo** de defectos de café
- **Categorización** (Primario/Secundario)
- **Niveles de severidad** (Bajo/Medio/Alto)
- **Soluciones recomendadas** para cada defecto
- **Auditoría** de creación y modificación
- **Activación/Desactivación** de defectos

### 🔧 Sistema de Calibraciones
- **CRUD completo** de calibraciones de equipos
- **Estados de calibración** (Pendiente/En Proceso/Completada/Fallida)
- **Tipos de calibración** (Equipo/Proceso/Método)
- **Asignación de usuarios** responsables
- **Notas y comentarios** adicionales
- **Auditoría** completa de cambios

### 🌐 Frontend Moderno
- **Vue.js 3** con Composition API
- **Tailwind CSS** para diseño responsive
- **Internacionalización** (Español/Inglés)
- **Componentes reutilizables**
- **Validaciones en tiempo real**
- **Manejo de errores** robusto

## 🏗️ Arquitectura

### Backend (.NET 9.0)
```
CafeLab.API/
├── Application/
│   ├── DTOs/           # Data Transfer Objects
│   ├── Services/       # Lógica de negocio
│   └── Interfaces/     # Contratos de servicios
├── Domain/
│   ├── Entities/       # Entidades del dominio
│   └── Repositories/   # Interfaces de repositorios
├── Infrastructure/
│   ├── Data/          # Contexto de base de datos
│   ├── Persistence/   # Implementación de repositorios
│   └── Security/      # Servicios de seguridad
└── Controllers/       # Controladores API
```

### Frontend (Vue.js 3)
```
frontend-reference/
├── src/
│   ├── auth/          # Autenticación
│   ├── roasting/      # Módulos de tostado
│   │   ├── api/       # Servicios API
│   │   └── components/ # Componentes Vue
│   ├── shared/        # Componentes compartidos
│   └── locales/       # Traducciones
```

## 🛠️ Instalación y Configuración

### Prerrequisitos
- **.NET 9.0 SDK**
- **Node.js 18+**
- **SQL Server** (LocalDB o Express)

### Instalación Rápida

1. **Clonar el repositorio**
```bash
git clone <repository-url>
cd CafeLab
```

2. **Ejecutar el sistema integrado**
```bash
# Windows
run-integrated.bat

# Linux/Mac
./run-integrated.sh
```

### Instalación Manual

#### Backend
```bash
cd CafeLab.API
dotnet restore
dotnet ef database update
dotnet run
```

#### Frontend
```bash
cd frontend-reference
npm install
npm run dev
```

## 📡 Endpoints API

### Autenticación
```
POST   /api/v1/auth/login
POST   /api/v1/auth/register
POST   /api/v1/auth/refresh
GET    /api/v1/auth/profile
POST   /api/v1/auth/logout
```

### Defectos
```
GET    /api/v1/defects              # Listar defectos
GET    /api/v1/defects/{id}         # Obtener defecto
POST   /api/v1/defects              # Crear defecto (Admin)
PUT    /api/v1/defects/{id}         # Actualizar defecto (Admin)
DELETE /api/v1/defects/{id}         # Eliminar defecto (Admin)
PATCH  /api/v1/defects/{id}/activate # Activar defecto (Admin)
```

### Calibraciones
```
GET    /api/v1/calibrations              # Listar calibraciones
GET    /api/v1/calibrations/{id}         # Obtener calibración
POST   /api/v1/calibrations              # Crear calibración (Admin)
PUT    /api/v1/calibrations/{id}         # Actualizar calibración (Admin)
DELETE /api/v1/calibrations/{id}         # Eliminar calibración (Admin)
PATCH  /api/v1/calibrations/{id}/status  # Actualizar estado (Admin)
PATCH  /api/v1/calibrations/{id}/activate # Activar calibración (Admin)
```

## 🔒 Seguridad

### Características de Seguridad
- **JWT con expiración** configurable
- **Refresh tokens** para renovación automática
- **Bloqueo de cuentas** por intentos fallidos
- **Validación de roles** en endpoints
- **Auditoría completa** de acciones
- **CORS configurado** para frontend
- **Validaciones** robustas en entidades

### Configuración de JWT
```json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-here",
    "Issuer": "CafeLab",
    "Audience": "CafeLabUsers",
    "ExpirationMinutes": 60,
    "RefreshExpirationDays": 7
  }
}
```

## 📊 Base de Datos

### Entidades Principales

#### User
- `Id`, `Username`, `Email`, `FullName`
- `PasswordHash`, `Role`, `IsActive`
- `FailedLoginAttempts`, `LockoutEnd`
- `CreatedAt`, `UpdatedAt`

#### Defect
- `Id`, `Name`, `Description`, `Category`
- `Severity`, `Solution`, `IsActive`
- `CreatedBy`, `UpdatedBy`
- `CreatedAt`, `UpdatedAt`

#### Calibration
- `Id`, `Name`, `Description`, `CalibrationDate`
- `Result`, `Status`, `Type`, `Notes`
- `UserId`, `IsActive`
- `CreatedBy`, `UpdatedBy`
- `CreatedAt`, `UpdatedAt`

## 🌍 Internacionalización

El sistema soporta múltiples idiomas:

### Español (es)
- Interfaz completa en español
- Mensajes de error localizados
- Formateo de fechas y números

### Inglés (en)
- Complete interface in English
- Localized error messages
- Date and number formatting

## 🧪 Testing

### Backend Tests
```bash
cd CafeLab.API
dotnet test
```

### Frontend Tests
```bash
cd frontend-reference
npm run test
```

## 📝 Logging

### Configuración de Logging
- **Serilog** para logging estructurado
- **Niveles**: Debug, Information, Warning, Error
- **Destinos**: Console, File, Database
- **Auditoría** de acciones de usuario

### Ejemplo de Log
```json
{
  "Timestamp": "2024-01-15T10:30:00.000Z",
  "Level": "Information",
  "Message": "Defect created successfully",
  "Properties": {
    "DefectId": 123,
    "CreatedBy": "admin@cafelab.com",
    "Action": "CreateDefect"
  }
}
```

## 🚀 Despliegue

### Producción
```bash
# Backend
dotnet publish -c Release
dotnet CafeLab.API.dll

# Frontend
npm run build
```

### Docker
```bash
docker-compose up -d
```

## 📚 Documentación Adicional

- [API Documentation](http://localhost:5000/swagger)
- [Frontend Components](./frontend-reference/README.md)
- [Database Schema](./docs/database-schema.md)

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.

## 🆘 Soporte

Para soporte técnico:
- 📧 Email: support@cafelab.com
- 📖 Documentación: [docs.cafelab.com](https://docs.cafelab.com)
- 🐛 Issues: [GitHub Issues](https://github.com/cafelab/issues)

---

**CafeLab** - Transformando la gestión de laboratorios de café ☕ 