
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IMecanicoRepository
{
    Task<Mecanico?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Mecanico>> GetAllAsync(CancellationToken ct = default);

    // devolver false o tru
    Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<int> AddAsync(Mecanico mecanico, CancellationToken ct = default);
    Task<bool> UpdateAsync(Mecanico mecanico, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
