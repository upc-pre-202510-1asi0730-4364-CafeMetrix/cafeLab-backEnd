using CafeLab.API.Domain.Entities;

namespace CafeLab.API.Domain.Interfaces
{
    /**
     * Interfaz para el repositorio de calibraciones
     * 
     * Define las operaciones específicas para la gestión de calibraciones
     * extendiendo la funcionalidad básica del IRepository genérico.
     */
    public interface ICalibrationRepository : IRepository<Calibration>
    {
        /**
         * Obtiene calibraciones por estado
         * 
         * @param status Estado de la calibración a filtrar
         * @returns Lista de calibraciones con el estado especificado
         */
        Task<IEnumerable<Calibration>> GetByStatusAsync(string status);

        /**
         * Obtiene calibraciones por tipo
         * 
         * @param equipmentType Tipo de calibración a filtrar
         * @returns Lista de calibraciones del tipo especificado
         */
        Task<IEnumerable<Calibration>> GetByEquipmentTypeAsync(string equipmentType);

        /**
         * Obtiene calibraciones activas
         * 
         * @param technicianId ID del técnico (no usado en esta implementación)
         * @returns Lista de calibraciones activas
         */
        Task<IEnumerable<Calibration>> GetByTechnicianAsync(int technicianId);

        /**
         * Obtiene calibraciones próximas a vencer (basado en fecha de calibración)
         * 
         * @param daysThreshold Días de anticipación para considerar próximas a vencer
         * @returns Lista de calibraciones que vencen en los próximos días
         */
        Task<IEnumerable<Calibration>> GetUpcomingExpirationsAsync(int daysThreshold = 30);

        /**
         * Obtiene calibraciones vencidas (basado en fecha de calibración)
         * 
         * @returns Lista de calibraciones que ya han vencido
         */
        Task<IEnumerable<Calibration>> GetExpiredCalibrationsAsync();

        /**
         * Obtiene calibraciones por rango de fechas
         * 
         * @param startDate Fecha de inicio
         * @param endDate Fecha de fin
         * @returns Lista de calibraciones en el rango de fechas especificado
         */
        Task<IEnumerable<Calibration>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

        /**
         * Obtiene estadísticas de calibraciones
         * 
         * @returns Estadísticas agregadas de calibraciones
         */
        Task<object> GetCalibrationStatisticsAsync();

        /**
         * Obtiene calibraciones por nombre
         * 
         * @param serialNumber Nombre de la calibración (renombrado para mantener compatibilidad)
         * @returns Lista de calibraciones con el nombre especificado
         */
        Task<IEnumerable<Calibration>> GetBySerialNumberAsync(string serialNumber);
    }
} 