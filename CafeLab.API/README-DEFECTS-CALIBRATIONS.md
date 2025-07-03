# CafeLab - Sistema de Defectos y Calibraciones

Sistema simplificado para la gestión de defectos de café y calibraciones de equipos en laboratorios de café.

## 🚀 Características

### 📚 Biblioteca de Defectos
- **CRUD completo** de defectos de café
- **Categorización** (Primario/Secundario)
- **Niveles de severidad** (Bajo/Medio/Alto)
- **Soluciones recomendadas** para cada defecto
- **Activación/Desactivación** de defectos

### 🔧 Sistema de Calibraciones
- **CRUD completo** de calibraciones de equipos
- **Estados de calibración** (Pendiente/En Proceso/Completada/Fallida)
- **Tipos de calibración** (Equipo/Proceso/Método)
- **Notas y comentarios** adicionales
- **Gestión de estados** de calibración

## 🛠️ Instalación Rápida

### Prerrequisitos
- **.NET 9.0 SDK**
- **Node.js 18+**

### Ejecución
```bash
# Windows
run-defects-calibrations.bat

# El sistema iniciará automáticamente:
# - Backend en http://localhost:5000
# - Frontend en http://localhost:5173
```

## 📡 Endpoints API

### Defectos
```
GET    /api/v1/defects              # Listar defectos
GET    /api/v1/defects/{id}         # Obtener defecto
POST   /api/v1/defects              # Crear defecto
PUT    /api/v1/defects/{id}         # Actualizar defecto
DELETE /api/v1/defects/{id}         # Eliminar defecto
PATCH  /api/v1/defects/{id}/activate # Activar defecto
```

### Calibraciones
```
GET    /api/v1/calibrations              # Listar calibraciones
GET    /api/v1/calibrations/{id}         # Obtener calibración
POST   /api/v1/calibrations              # Crear calibración
PUT    /api/v1/calibrations/{id}         # Actualizar calibración
DELETE /api/v1/calibrations/{id}         # Eliminar calibración
PATCH  /api/v1/calibrations/{id}/status  # Actualizar estado
PATCH  /api/v1/calibrations/{id}/activate # Activar calibración
```

## 📊 Modelos de Datos

### Defect
```json
{
  "id": 1,
  "name": "Sabor a tierra",
  "description": "Sabor desagradable a tierra o moho",
  "category": "Primario",
  "severity": "Alto",
  "solution": "Revisar proceso de secado y almacenamiento",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": null,
  "isActive": true
}
```

### Calibration
```json
{
  "id": 1,
  "name": "Calibración molinillo",
  "description": "Calibración del molinillo para espresso",
  "calibrationDate": "2024-01-15T10:30:00Z",
  "result": "Ajuste realizado correctamente",
  "status": "Completada",
  "type": "Equipo",
  "notes": "Molinillo ajustado a 18g en 25 segundos",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": null,
  "isActive": true
}
```

## 🎯 Ejemplos de Uso

### Crear un Defecto
```bash
curl -X POST http://localhost:5000/api/v1/defects \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Sabor a quemado",
    "description": "Sabor desagradable a café quemado",
    "category": "Primario",
    "severity": "Alto",
    "solution": "Revisar temperatura de tostado"
  }'
```

### Crear una Calibración
```bash
curl -X POST http://localhost:5000/api/v1/calibrations \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Calibración termómetro",
    "description": "Verificación de precisión del termómetro",
    "calibrationDate": "2024-01-15T10:30:00Z",
    "result": "Termómetro calibrado correctamente",
    "type": "Equipo",
    "notes": "Ajuste de +2°C realizado"
  }'
```

## 🌐 Frontend

El frontend incluye:
- **Vue.js 3** con Composition API
- **Componentes** para gestión de defectos y calibraciones
- **Servicios API** para comunicación con el backend
- **Internacionalización** (Español/Inglés)

### Navegación
- `/defects` - Lista de defectos
- `/defects/create` - Crear defecto
- `/defects/:id` - Detalles del defecto
- `/calibrations` - Lista de calibraciones
- `/calibrations/create` - Crear calibración
- `/calibrations/:id` - Detalles de calibración

## 🚀 Despliegue

### Desarrollo
```bash
# Backend
cd CafeLab.API
dotnet run

# Frontend
cd frontend-reference
npm run dev
```

### Producción
```bash
# Backend
dotnet publish -c Release
dotnet CafeLab.API.dll

# Frontend
npm run build
```

## 📝 Logging

El sistema incluye logging estructurado para:
- Operaciones CRUD
- Errores y excepciones
- Auditoría de cambios

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature
3. Commit tus cambios
4. Push a la rama
5. Abre un Pull Request

---

**CafeLab** - Gestión simplificada de defectos y calibraciones ☕ 