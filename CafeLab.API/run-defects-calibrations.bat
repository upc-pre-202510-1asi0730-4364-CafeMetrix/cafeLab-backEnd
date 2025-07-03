@echo off
echo ========================================
echo    CafeLab - Defects y Calibrations
echo    Sistema Simplificado
echo ========================================
echo.

echo Iniciando sistema de Defects y Calibrations...
echo.

REM Verificar si .NET está instalado
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET no está instalado o no está en el PATH
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
echo [1/3] Restaurando dependencias del backend...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron restaurar las dependencias del backend
    pause
    exit /b 1
)

REM Aplicar migraciones de base de datos
echo [2/3] Aplicando migraciones de base de datos...
dotnet ef database update
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron aplicar las migraciones
    pause
    exit /b 1
)

REM Navegar al directorio del frontend
cd frontend-reference

REM Instalar dependencias del frontend
echo [3/3] Instalando dependencias del frontend...
npm install
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron instalar las dependencias del frontend
    pause
    exit /b 1
)

echo Iniciando servicios...
echo.

REM Iniciar el backend en segundo plano
echo Iniciando Backend (.NET API) en http://localhost:5000...
start "CafeLab Backend" cmd /k "cd /d %CD%\.. && dotnet run --project CafeLab.API"

REM Esperar un momento para que el backend se inicie
timeout /t 5 /nobreak >nul

REM Iniciar el frontend en segundo plano
echo Iniciando Frontend (Vue.js) en http://localhost:5173...
start "CafeLab Frontend" cmd /k "cd /d %CD% && npm run dev"

echo.
echo ========================================
echo    Sistema iniciado exitosamente!
echo ========================================
echo.
echo Backend API:  http://localhost:5000
echo Frontend:     http://localhost:5173
echo Swagger:      http://localhost:5000/swagger
echo.
echo Módulos disponibles:
echo - Biblioteca de Defectos (CRUD completo)
echo - Sistema de Calibraciones (CRUD completo)
echo.
echo Endpoints principales:
echo - GET    /api/v1/defects
echo - POST   /api/v1/defects
echo - GET    /api/v1/calibrations
echo - POST   /api/v1/calibrations
echo.
echo Para detener el sistema, cierra las ventanas de comandos
echo o presiona Ctrl+C en cada una.
echo.
pause 