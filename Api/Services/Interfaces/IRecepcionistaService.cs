
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IRecepcionistaService
{
    Task<Recepcionista?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Recepcionista>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Recepcionista>> GetActiveAsync(CancellationToken ct = default);

    Task<int> AddAsync(Recepcionista recepcionista, CancellationToken ct = default);
    Task<bool> UpdateAsync(Recepcionista recepcionista, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default);
}
