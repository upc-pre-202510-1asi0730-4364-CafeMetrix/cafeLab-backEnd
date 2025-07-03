@echo off
echo ========================================
echo CafeLab - Sistema de Defectos y Calibraciones
echo ========================================
echo.
echo Iniciando aplicacion para desarrollo local...
echo.

REM Verificar si .NET 9.0 esta instalado
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET 9.0 no esta instalado.
    echo Por favor instala .NET 9.0 desde: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo Verificando dependencias...
cd CafeLab.API

REM Restaurar paquetes NuGet
echo Restaurando paquetes NuGet...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron restaurar los paquetes NuGet.
    pause
    exit /b 1
)

REM Verificar si la base de datos existe y crear si es necesario
echo Verificando base de datos...
dotnet ef database update --no-build
if %errorlevel% neq 0 (
    echo ADVERTENCIA: No se pudo actualizar la base de datos.
    echo Esto puede ser normal si es la primera vez que ejecutas la aplicacion.
    echo.
)

REM Compilar el proyecto
echo Compilando proyecto...
dotnet build --no-restore
if %errorlevel% neq 0 (
    echo ERROR: Error al compilar el proyecto.
    pause
    exit /b 1
)

echo.
echo ========================================
echo Iniciando servidor de desarrollo...
echo ========================================
echo.
echo La aplicacion estara disponible en:
echo - API: https://localhost:5001
echo - Swagger: https://localhost:5001/swagger
echo.
echo Para detener el servidor, presiona Ctrl+C
echo.

REM Ejecutar la aplicacion
dotnet run --urls "https://localhost:5001;http://localhost:5000"

pause 