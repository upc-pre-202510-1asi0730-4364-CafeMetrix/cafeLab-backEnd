# Documentación de la API CafeLab

## Descripción General
CafeLab API es un sistema RESTful diseñado para la gestión integral de procesos de café, incluyendo evaluación sensorial, control de defectos y gestión de perfiles de tostado.

## Endpoints Principales

### Gestión de Defectos
```http
GET /api/defects
GET /api/defects/{id}
POST /api/defects
PUT /api/defects/{id}
DELETE /api/defects/{id}
```

#### Modelo de Defecto
```json
{
  "id": "integer",
  "name": "string",
  "description": "string",
  "category": "string",
  "imageUrl": "string",
  "severity": "decimal",
  "detectionMethod": "string",
  "preventionMethod": "string",
  "isActive": "boolean",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

### Patrones de Cata
```http
GET /api/tasting-patterns
GET /api/tasting-patterns/{id}
POST /api/tasting-patterns
PUT /api/tasting-patterns/{id}
DELETE /api/tasting-patterns/{id}
```

#### Modelo de Patrón de Cata
```json
{
  "id": "integer",
  "name": "string",
  "description": "string",
  "aroma": "decimal",
  "flavor": "decimal",
  "aftertaste": "decimal",
  "acidity": "decimal",
  "body": "decimal",
  "uniformity": "decimal",
  "balance": "decimal",
  "cleanCup": "decimal",
  "sweetness": "decimal",
  "overall": "decimal",
  "notes": "string",
  "coffeeId": "integer",
  "createdBy": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

## Autenticación
La API utiliza autenticación JWT. Para obtener un token:

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "string",
  "password": "string"
}
```

## Códigos de Estado
- 200: Éxito
- 201: Creado
- 400: Solicitud incorrecta
- 401: No autorizado
- 403: Prohibido
- 404: No encontrado
- 500: Error interno del servidor

## Ejemplos de Uso

### Crear un Nuevo Defecto
```http
POST /api/defects
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Defecto de Tostado",
  "description": "Café con sabor a quemado",
  "category": "Tostado",
  "severity": 3.5,
  "detectionMethod": "Evaluación sensorial",
  "preventionMethod": "Ajustar perfil de tostado"
}
```

### Crear un Patrón de Cata
```http
POST /api/tasting-patterns
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Perfil Clásico",
  "description": "Perfil balanceado con notas cítricas",
  "aroma": 8.5,
  "flavor": 8.0,
  "aftertaste": 7.5,
  "acidity": 8.0,
  "body": 7.5,
  "uniformity": 8.0,
  "balance": 8.5,
  "cleanCup": 8.0,
  "sweetness": 7.5,
  "overall": 8.0,
  "notes": "Notas de limón y caramelo"
}
```

## Mejores Prácticas
1. Siempre incluir el token JWT en el header Authorization
2. Validar los datos antes de enviarlos
3. Manejar los errores apropiadamente
4. Usar los códigos de estado HTTP correctamente

## Límites y Restricciones
- Tamaño máximo de payload: 10MB
- Límite de peticiones: 100 por minuto
- Tiempo máximo de sesión: 24 horas 