# CafeLab - Configuración Local para Desarrollo

## 🚀 Configuración Rápida

### Prerrequisitos

- **.NET 9.0 SDK** - [Descargar aquí](https://dotnet.microsoft.com/download)
- **MySQL 8.0+** o **SQL Server** (local o remoto)
- **Visual Studio 2022** o **VS Code** con extensiones de C#

### 📋 Pasos de Configuración

#### 1. Clonar el Repositorio

```bash
git clone https://github.com/upc-pre-202510-1asi0730-4364-CafeMetrix/cafeLab-backEnd.git
cd cafeLab-backEnd
git checkout develop
```

#### 2. Configurar Base de Datos

**Opción A: MySQL (Recomendado para desarrollo)**

1. Instalar MySQL Server 8.0+
2. Crear una base de datos llamada `cafelab_db`
3. Actualizar la cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=cafelab_db;User=root;Password=tu_password;"
}
```

**Opción B: SQL Server LocalDB**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CafeLabDB;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
}
```

#### 3. Ejecutar Migraciones

```bash
cd CafeLab.API
dotnet ef database update
```

#### 4. Ejecutar la Aplicación

**Opción A: Script Automático**
```bash
run-local.bat
```

**Opción B: Comando Manual**
```bash
dotnet run --urls "https://localhost:5001;http://localhost:5000"
```

## 🔗 Endpoints Disponibles

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar usuario
- `POST /api/auth/refresh` - Renovar token
- `POST /api/auth/logout` - Cerrar sesión
- `GET /api/auth/profile` - Obtener perfil
- `GET /api/auth/users` - Listar usuarios (solo Admin)

### Defectos
- `GET /api/defects` - Listar defectos
- `GET /api/defects/{id}` - Obtener defecto específico
- `POST /api/defects` - Crear defecto
- `PUT /api/defects/{id}` - Actualizar defecto
- `DELETE /api/defects/{id}` - Eliminar defecto

### Calibraciones
- `GET /api/calibrations` - Listar calibraciones
- `GET /api/calibrations/{id}` - Obtener calibración específica
- `POST /api/calibrations` - Crear calibración
- `PUT /api/calibrations/{id}` - Actualizar calibración
- `DELETE /api/calibrations/{id}` - Eliminar calibración

## 📚 Documentación API

Una vez que la aplicación esté ejecutándose, puedes acceder a la documentación Swagger en:

- **Swagger UI**: https://localhost:5001/swagger
- **OpenAPI JSON**: https://localhost:5001/swagger/v1/swagger.json

## 🔐 Configuración de Autenticación

### JWT Settings (appsettings.json)

```json
"JwtSettings": {
  "SecretKey": "CafeLab_Super_Secure_Secret_Key_For_JWT_Tokens_2024_Minimum_32_Characters_Long",
  "Issuer": "CafeLab.API",
  "Audience": "CafeLab.Client",
  "AccessTokenExpirationMinutes": 15,
  "RefreshTokenExpirationDays": 7
}
```

### Roles Disponibles

- **Admin**: Acceso completo a todos los módulos
- **Technician**: Acceso a calibraciones y defectos
- **User**: Acceso solo a defectos

## 🧪 Datos de Prueba

### Usuario Administrador
```json
{
  "username": "admin",
  "password": "Admin123!",
  "email": "admin@cafelab.com",
  "role": "Admin"
}
```

### Usuario Técnico
```json
{
  "username": "technician",
  "password": "Tech123!",
  "email": "tech@cafelab.com",
  "role": "Technician"
}
```

### Usuario Regular
```json
{
  "username": "user",
  "password": "User123!",
  "email": "user@cafelab.com",
  "role": "User"
}
```

## 🔧 Solución de Problemas

### Error de Conexión a Base de Datos

1. Verificar que el servidor de base de datos esté ejecutándose
2. Verificar la cadena de conexión en `appsettings.json`
3. Verificar credenciales de acceso

### Error de Migraciones

```bash
# Eliminar migraciones existentes
dotnet ef migrations remove

# Crear nueva migración inicial
dotnet ef migrations add InitialCreate

# Aplicar migraciones
dotnet ef database update
```

### Error de Certificados HTTPS

```bash
# Generar certificado de desarrollo
dotnet dev-certs https --trust
```

### Error de Paquetes NuGet

```bash
# Limpiar caché de NuGet
dotnet nuget locals all --clear

# Restaurar paquetes
dotnet restore
```

## 📁 Estructura del Proyecto

```
CafeLab.API/
├── Application/          # Lógica de negocio
│   ├── DTOs/            # Objetos de transferencia
│   ├── Services/        # Servicios de aplicación
│   └── Interfaces/      # Contratos de servicios
├── Domain/              # Entidades del dominio
│   ├── Entities/        # Entidades principales
│   └── Interfaces/      # Contratos del dominio
├── Infrastructure/      # Implementaciones técnicas
│   ├── Data/           # Contexto de base de datos
│   ├── Security/       # Servicios de seguridad
│   └── Persistence/    # Repositorios y UoW
└── Controllers/         # Endpoints de la API
```

## 🚀 Despliegue

### Variables de Entorno para Producción

```bash
# Base de datos
DATABASE_CONNECTION_STRING="Server=prod-server;Database=cafelab_prod;..."

# JWT
JWT_SECRET_KEY="tu_clave_secreta_super_segura_de_produccion"
JWT_ISSUER="CafeLab.API"
JWT_AUDIENCE="CafeLab.Client"

# CORS
ALLOWED_ORIGINS="https://tu-dominio.com"
```

## 📞 Soporte

Si tienes problemas con la configuración local:

1. Verificar que todos los prerrequisitos estén instalados
2. Revisar los logs de la aplicación
3. Consultar la documentación de Swagger
4. Crear un issue en el repositorio con detalles del problema

---

**¡Listo para desarrollar! 🎉** 