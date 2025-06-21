using System.Threading.Tasks;

namespace CafeLab.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
} 