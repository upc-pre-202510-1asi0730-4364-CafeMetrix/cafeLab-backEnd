using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Placeholder para aplicar convenciones globales en MySQL.
    /// Puedes implementar aquí lógica personalizada si lo necesitas.
    /// </summary>
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        // MySQL no soporta una convención snake_case automática como Npgsql.
        // Si necesitas convertir nombres a snake_case, puedes hacerlo aquí manualmente.
    }
} 