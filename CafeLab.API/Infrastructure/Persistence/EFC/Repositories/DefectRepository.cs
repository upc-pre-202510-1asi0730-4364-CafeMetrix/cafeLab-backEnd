using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using CafeLab.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Infrastructure.Persistence.EFC.Repositories
{
    /**
     * Repositorio para la entidad Defect
     * 
     * Este repositorio implementa las operaciones de persistencia específicas
     * para la entidad Defect, extendiendo la funcionalidad del BaseRepository.
     * 
     * Características implementadas:
     * - Operaciones CRUD básicas heredadas del BaseRepository
     * - Consultas específicas para defectos
     * - Filtrado por categoría y severidad
     * - Paginación y ordenamiento
     */
    public class DefectRepository : BaseRepository<Defect>, IDefectRepository
    {
        public DefectRepository(ApplicationDbContext context) : base(context)
        {
        }

        /**
         * Obtiene defectos por categoría
         * 
         * @param category Categoría del defecto a filtrar
         * @returns Lista de defectos de la categoría especificada
         */
        public async Task<IEnumerable<Defect>> GetByStatusAsync(string category)
        {
            return await _context.Defects
                .Where(d => d.Category == category)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene defectos por severidad
         * 
         * @param severity Severidad del defecto a filtrar
         * @returns Lista de defectos con la severidad especificada
         */
        public async Task<IEnumerable<Defect>> GetByTypeAsync(string severity)
        {
            return await _context.Defects
                .Where(d => d.Severity == severity)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene defectos activos
         * 
         * @returns Lista de defectos activos
         */
        public async Task<IEnumerable<Defect>> GetByReportedByAsync(int userId)
        {
            return await _context.Defects
                .Where(d => d.IsActive)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene defectos por rango de fechas
         * 
         * @param startDate Fecha de inicio
         * @param endDate Fecha de fin
         * @returns Lista de defectos en el rango de fechas especificado
         */
        public async Task<IEnumerable<Defect>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Defects
                .Where(d => d.CreatedAt >= startDate && d.CreatedAt <= endDate)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene defectos de alta severidad
         * 
         * @returns Lista de defectos con severidad alta
         */
        public async Task<IEnumerable<Defect>> GetCriticalDefectsAsync()
        {
            return await _context.Defects
                .Where(d => d.Severity == "Alto" || d.Severity == "Crítico")
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        /**
         * Obtiene estadísticas de defectos
         * 
         * @returns Estadísticas agregadas de defectos
         */
        public async Task<object> GetDefectStatisticsAsync()
        {
            var stats = await _context.Defects
                .GroupBy(d => d.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var totalDefects = await _context.Defects.CountAsync();
            var activeDefects = await _context.Defects
                .Where(d => d.IsActive)
                .CountAsync();
            var criticalDefects = await _context.Defects
                .Where(d => d.Severity == "Alto" || d.Severity == "Crítico")
                .CountAsync();

            return new
            {
                TotalDefects = totalDefects,
                ActiveDefects = activeDefects,
                CriticalDefects = criticalDefects,
                CategoryBreakdown = stats
            };
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