using CafeLab.API.Domain.Entities;

namespace CafeLab.API.Domain.Interfaces
{
    /**
     * Interfaz para el repositorio de defectos
     * 
     * Define las operaciones específicas para la gestión de defectos
     * extendiendo la funcionalidad básica del IRepository genérico.
     */
    public interface IDefectRepository : IRepository<Defect>
    {
        /**
         * Obtiene defectos por categoría
         * 
         * @param category Categoría del defecto a filtrar
         * @returns Lista de defectos de la categoría especificada
         */
        Task<IEnumerable<Defect>> GetByStatusAsync(string category);

        /**
         * Obtiene defectos por severidad
         * 
         * @param severity Severidad del defecto a filtrar
         * @returns Lista de defectos con la severidad especificada
         */
        Task<IEnumerable<Defect>> GetByTypeAsync(string severity);

        /**
         * Obtiene defectos activos
         * 
         * @param userId ID del usuario (no usado en esta implementación)
         * @returns Lista de defectos activos
         */
        Task<IEnumerable<Defect>> GetByReportedByAsync(int userId);

        /**
         * Obtiene defectos por rango de fechas
         * 
         * @param startDate Fecha de inicio
         * @param endDate Fecha de fin
         * @returns Lista de defectos en el rango de fechas especificado
         */
        Task<IEnumerable<Defect>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

        /**
         * Obtiene defectos de alta severidad
         * 
         * @returns Lista de defectos con severidad alta
         */
        Task<IEnumerable<Defect>> GetCriticalDefectsAsync();

        /**
         * Obtiene estadísticas de defectos
         * 
         * @returns Estadísticas agregadas de defectos
         */
        Task<object> GetDefectStatisticsAsync();
    }
} 