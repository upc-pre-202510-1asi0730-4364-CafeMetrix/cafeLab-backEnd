using CafeLab.API.Domain.Repositories;
using CafeLab.API.Infrastructure.Data;
using System.Threading.Tasks;

namespace CafeLab.API.Infrastructure.Persistence.EFC
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 