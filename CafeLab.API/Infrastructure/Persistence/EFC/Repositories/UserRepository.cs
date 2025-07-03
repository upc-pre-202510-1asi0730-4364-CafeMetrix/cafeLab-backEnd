using CafeLab.API.Domain.Model.Aggregates;
using CafeLab.API.Domain.Interfaces;
using CafeLab.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CafeLab.API.Infrastructure.Persistence.EFC.Repositories
{
    /**
     * Repositorio para la entidad User
     * 
     * Este repositorio implementa las operaciones de persistencia específicas
     * para la entidad User, extendiendo la funcionalidad del BaseRepository.
     */
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        /**
         * Obtiene un usuario por nombre de usuario
         * 
         * @param username Nombre de usuario a buscar
         * @returns Usuario encontrado o null si no existe
         */
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        /**
         * Obtiene un usuario por email
         * 
         * @param email Email del usuario a buscar
         * @returns Usuario encontrado o null si no existe
         */
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        /**
         * Verifica si existe un usuario con el nombre de usuario especificado
         * 
         * @param username Nombre de usuario a verificar
         * @returns True si existe, false en caso contrario
         */
        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _dbSet.AnyAsync(u => u.Username == username);
        }

        /**
         * Verifica si existe un usuario con el email especificado
         * 
         * @param email Email a verificar
         * @returns True si existe, false en caso contrario
         */
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        /**
         * Obtiene usuarios por rol
         * 
         * @param role Rol de los usuarios a buscar
         * @returns Lista de usuarios con el rol especificado
         */
        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        {
            return await _dbSet
                .Where(u => u.Role == role)
                .OrderBy(u => u.Username)
                .ToListAsync();
        }

        /**
         * Obtiene usuarios activos
         * 
         * @returns Lista de usuarios activos
         */
        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbSet
                .Where(u => u.IsActive)
                .OrderBy(u => u.Username)
                .ToListAsync();
        }

        /**
         * Obtiene usuarios bloqueados
         * 
         * @returns Lista de usuarios bloqueados
         */
        public async Task<IEnumerable<User>> GetLockedUsersAsync()
        {
            return await _dbSet
                .Where(u => u.IsLockedOut())
                .OrderBy(u => u.Username)
                .ToListAsync();
        }
    }
} 