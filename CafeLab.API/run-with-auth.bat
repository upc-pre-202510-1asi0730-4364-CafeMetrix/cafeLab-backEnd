@echo off
echo ========================================
echo CafeLab - Sistema de Defectos y Calibraciones
echo ========================================
echo.
echo Iniciando sistema con autenticacion IAM y JWT...
echo Sistema enfocado en gestion de defectos y calibraciones del cafe.
echo.

REM Verificar si .NET 9.0 está instalado
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET 9.0 no está instalado o no está en el PATH
    echo Por favor instala .NET 9.0 desde: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

REM Verificar si Node.js está instalado
node --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: Node.js no está instalado o no está en el PATH
    echo Por favor instala Node.js desde: https://nodejs.org/
    pause
    exit /b 1
)

echo Verificando dependencias...
echo.

REM Navegar al directorio del backend
cd CafeLab.API

REM Restaurar dependencias del backend
echo [1/4] Restaurando dependencias del backend...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron restaurar las dependencias del backend
    pause
    exit /b 1
)

REM Aplicar migraciones de base de datos
echo [2/4] Aplicando migraciones de base de datos...
dotnet ef database update
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron aplicar las migraciones
    pause
    exit /b 1
)

REM Navegar al directorio del frontend
cd frontend-reference

REM Instalar dependencias del frontend
echo [3/4] Instalando dependencias del frontend...
npm install
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron instalar las dependencias del frontend
    pause
    exit /b 1
)

echo [4/4] Iniciando servicios...
echo.

REM Iniciar el backend en segundo plano
echo Iniciando backend en http://localhost:5000...
start "CafeLab Backend - Defectos y Calibraciones" cmd /k "cd /d %cd%\.. && dotnet run --urls=http://localhost:5000"

REM Esperar un momento para que el backend se inicie
timeout /t 5 /nobreak >nul

REM Iniciar el frontend en segundo plano
echo Iniciando frontend en http://localhost:5173...
start "CafeLab Frontend - Defectos y Calibraciones" cmd /k "npm run dev"

echo.
echo ========================================
echo Sistema iniciado exitosamente!
echo ========================================
echo.
echo Backend API: http://localhost:5000
echo Frontend: http://localhost:5173
echo Swagger: http://localhost:5000/swagger
echo.
echo ========================================
echo MODULOS DISPONIBLES
echo ========================================
echo.
echo 🔐 AUTENTICACION IAM:
echo   - Login/Registro de usuarios
echo   - JWT con refresh tokens automaticos
echo   - Roles: Admin, Technician, User
echo   - Bloqueo de cuentas por seguridad
echo   - Auditoria completa de sesiones
echo.
echo 📊 DEFECTOS DEL CAFE:
echo   - Biblioteca de defectos primarios y secundarios
echo   - Categorizacion por severidad (Bajo, Medio, Alto)
echo   - Soluciones y recomendaciones
echo   - Auditoria de creacion y modificacion
echo   - Busqueda y filtrado avanzado
echo.
echo ⚙️ CALIBRACIONES:
echo   - Calibracion de equipos de tostado
echo   - Calibracion de molinos
echo   - Calibracion de procesos
echo   - Resultados y notas tecnicas
echo   - Seguimiento de calibraciones
echo.
echo ========================================
echo CARACTERISTICAS TECNICAS
echo ========================================
echo.
echo 🏗️ ARQUITECTURA:
echo   - Backend: .NET 9.0 con Entity Framework Core
echo   - Frontend: Vue.js 3 con Composition API
echo   - Base de datos: MySQL con migraciones
echo   - Autenticacion: JWT con BCrypt
echo.
echo 🔒 SEGURIDAD:
echo   - Autenticacion IAM completa
echo   - Autorizacion basada en roles
echo   - Auditoria de todas las acciones
echo   - Bloqueo automatico de cuentas
echo   - Refresh tokens con expiracion
echo.
echo 🌍 INTERNACIONALIZACION:
echo   - Soporte completo para Español e Ingles
echo   - Traducciones dinamicas
echo   - Formateo de fechas por region
echo.
echo 📚 DOCUMENTACION:
echo   - Swagger UI completo
echo   - Comentarios XML detallados
echo   - Ejemplos de uso
echo   - Guias de instalacion
echo.
echo ========================================
echo USUARIOS POR DEFECTO
echo ========================================
echo.
echo 👤 ADMINISTRADOR:
echo   Username: admin
echo   Password: admin123
echo   Rol: Admin (acceso completo)
echo.
echo 👤 TECNICO:
echo   Username: technician
echo   Password: tech123
echo   Rol: Technician (especializado en calibraciones)
echo.
echo 👤 USUARIO:
echo   Username: user
echo   Password: user123
echo   Rol: User (acceso basico)
echo.
echo ========================================
echo COMANDOS UTILES
echo ========================================
echo.
echo Para detener el sistema:
echo   - Cierra las ventanas de comandos
echo   - O presiona Ctrl+C en cada una
echo.
echo Para ver logs del backend:
echo   - Revisa la ventana "CafeLab Backend"
echo.
echo Para ver logs del frontend:
echo   - Revisa la ventana "CafeLab Frontend"
echo.
echo Para acceder a la documentacion API:
echo   - Abre http://localhost:5000/swagger
echo.
echo ========================================
echo ¡Sistema listo para gestionar defectos y calibraciones!
echo ========================================
echo.
pause 