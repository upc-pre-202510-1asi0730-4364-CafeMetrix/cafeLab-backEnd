using CafeLab.API.Domain.Interfaces;
using CafeLab.API.Infrastructure.Data;
using System.Threading.Tasks;

namespace CafeLab.API.Infrastructure.Persistence.EFC
{
    /**
     * Implementación del patrón Unit of Work
     * 
     * Gestiona las transacciones de base de datos y asegura
     * la consistencia de los datos en operaciones complejas.
     */
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Completa la transacción actual
         * 
         * @returns Task que representa la operación asíncrona
         */
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 