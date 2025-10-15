using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IMecanicoService
{
    Task<Mecanico?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Mecanico>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Mecanico>> GetActiveAsync(CancellationToken ct = default); // solo activos

    Task<int> AddAsync(Mecanico mecanico, CancellationToken ct = default);
    Task<bool> UpdateAsync(Mecanico mecanico, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default);
}
