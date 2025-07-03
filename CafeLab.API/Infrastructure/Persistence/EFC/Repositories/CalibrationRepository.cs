using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using CafeLab.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Infrastructure.Persistence.EFC.Repositories
{
    /**
     * Repositorio para la entidad Calibration
     * 
     * Este repositorio implementa las operaciones de persistencia específicas
     * para la entidad Calibration, extendiendo la funcionalidad del BaseRepository.
     * 
     * Características implementadas:
     * - Operaciones CRUD básicas heredadas del BaseRepository
     * - Consultas específicas para calibraciones
     * - Filtrado por estado y tipo
     * - Paginación y ordenamiento
     * - Consultas de calibraciones por fecha
     */
    public class CalibrationRepository : BaseRepository<Calibration>, ICalibrationRepository
    {
        public CalibrationRepository(ApplicationDbContext context) : base(context)
        {
        }

        /**
         * Obtiene calibraciones por estado
         * 
         * @param status Estado de la calibración a filtrar
         * @returns Lista de calibraciones con el estado especificado
         */
        public async Task<IEnumerable<Calibration>> GetByStatusAsync(string status)
        {
            return await _context.Calibrations
                .Where(c => c.Status == status)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene calibraciones por tipo
         * 
         * @param equipmentType Tipo de calibración a filtrar
         * @returns Lista de calibraciones del tipo especificado
         */
        public async Task<IEnumerable<Calibration>> GetByEquipmentTypeAsync(string equipmentType)
        {
            return await _context.Calibrations
                .Where(c => c.Type == equipmentType)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene calibraciones activas
         * 
         * @param technicianId ID del técnico (no usado en esta implementación)
         * @returns Lista de calibraciones activas
         */
        public async Task<IEnumerable<Calibration>> GetByTechnicianAsync(int technicianId)
        {
            return await _context.Calibrations
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene calibraciones próximas a vencer (basado en fecha de calibración)
         * 
         * @param daysThreshold Días de anticipación para considerar próximas a vencer
         * @returns Lista de calibraciones que vencen en los próximos días
         */
        public async Task<IEnumerable<Calibration>> GetUpcomingExpirationsAsync(int daysThreshold = 30)
        {
            var expirationDate = DateTime.UtcNow.AddDays(daysThreshold);
            
            return await _context.Calibrations
                .Where(c => c.CalibrationDate <= expirationDate && c.Status == "Completada")
                .OrderBy(c => c.CalibrationDate)
                .ToListAsync();
        }

        /**
         * Obtiene calibraciones vencidas (basado en fecha de calibración)
         * 
         * @returns Lista de calibraciones que ya han vencido
         */
        public async Task<IEnumerable<Calibration>> GetExpiredCalibrationsAsync()
        {
            return await _context.Calibrations
                .Where(c => c.CalibrationDate < DateTime.UtcNow && c.Status == "Completada")
                .OrderBy(c => c.CalibrationDate)
                .ToListAsync();
        }

        /**
         * Obtiene calibraciones por rango de fechas
         * 
         * @param startDate Fecha de inicio
         * @param endDate Fecha de fin
         * @returns Lista de calibraciones en el rango de fechas especificado
         */
        public async Task<IEnumerable<Calibration>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Calibrations
                .Where(c => c.CalibrationDate >= startDate && c.CalibrationDate <= endDate)
                .OrderByDescending(c => c.CalibrationDate)
                .ToListAsync();
        }

        /**
         * Obtiene estadísticas de calibraciones
         * 
         * @returns Estadísticas agregadas de calibraciones
         */
        public async Task<object> GetCalibrationStatisticsAsync()
        {
            var stats = await _context.Calibrations
                .GroupBy(c => c.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var totalCalibrations = await _context.Calibrations.CountAsync();
            var activeCalibrations = await _context.Calibrations
                .Where(c => c.IsActive)
                .CountAsync();
            var completedCalibrations = await _context.Calibrations
                .Where(c => c.Status == "Completada")
                .CountAsync();

            return new
            {
                TotalCalibrations = totalCalibrations,
                ActiveCalibrations = activeCalibrations,
                CompletedCalibrations = completedCalibrations,
                StatusBreakdown = stats
            };
        }

        /**
         * Obtiene calibraciones por nombre
         * 
         * @param serialNumber Nombre de la calibración (renombrado para mantener compatibilidad)
         * @returns Lista de calibraciones con el nombre especificado
         */
        public async Task<IEnumerable<Calibration>> GetBySerialNumberAsync(string serialNumber)
        {
            return await _context.Calibrations
                .Where(c => c.Name.Contains(serialNumber))
                .OrderByDescending(c => c.CalibrationDate)
                .ToListAsync();
        }

        /**
         * Guarda los cambios en la base de datos
         * 
         * @returns Número de entidades afectadas
         */
        public new async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 