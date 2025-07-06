namespace CafeLab.API.Shared.Domain.Repositories;

/// <summary>
///     Base repository interface for all repositories
/// </summary>
/// <remarks>
///     This interface defines the basic CRUD operations for all repositories
/// </remarks>
/// <typeparam name="TEntity">The Entity Type</typeparam>
/// TODO ESTO SE USARÁ EN COMMAND SERVICES O QUERY SERVICES
public interface IBaseRepository<TEntity>
{
    /// <summary>
    ///     Add entity to the repository
    /// </summary>
    /// <param name="entity">Entity object to add</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    ///     Find entity by id
    /// </summary>
    /// <param name="id">The Entity ID to Find</param>
    /// <returns>Entity object if found</returns>
    Task<TEntity?> FindByIdAsync(int id);

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">The entity object to update</param>
    void Update(TEntity entity);

    /// <summary>
    ///     Remove and entity
    /// </summary>
    /// <param name="entity">The entity object to remove</param>
    void Remove(TEntity entity);

    /// <summary>
    ///     Get All entities
    /// </summary>
    /// <returns>An Enumerable containing all entity objects</returns>
    Task<IEnumerable<TEntity>> ListAsync();

    /// <summary>
    ///     Update entity async (por compatibilidad con servicios async)
    /// </summary>
    /// <param name="entity">The entity object to update</param>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity);
}