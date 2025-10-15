using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IEstadoCitaRepository
{
    Task<EstadoCita?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<EstadoCita?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<EstadoCita>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(EstadoCita estado, CancellationToken ct = default);
    Task<bool> UpdateAsync(EstadoCita estado, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
