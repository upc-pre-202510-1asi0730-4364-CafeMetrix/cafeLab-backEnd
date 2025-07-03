@echo off
echo ========================================
echo CafeLab - Sistema de Autenticacion IAM
echo ========================================
echo.

echo Iniciando el backend (.NET API)...
cd CafeLab.API
start "CafeLab Backend" cmd /k "dotnet run --urls=http://localhost:5000"

echo.
echo Esperando 5 segundos para que el backend inicie...
timeout /t 5 /nobreak > nul

echo.
echo Iniciando el frontend (Vue.js)...
cd ..\frontend-reference
start "CafeLab Frontend" cmd /k "npm run dev"

echo.
echo ========================================
echo Proyecto iniciado correctamente!
echo ========================================
echo Backend: http://localhost:5000
echo Frontend: http://localhost:5173
echo Swagger: http://localhost:5000/swagger
echo ========================================
echo.
echo Presiona cualquier tecla para cerrar esta ventana...
pause > nul 