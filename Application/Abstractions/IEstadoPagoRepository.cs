
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IEstadoPagoRepository
{
    Task<EstadoPago?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<EstadoPago?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<EstadoPago>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(EstadoPago estado, CancellationToken ct = default);
    Task<bool> UpdateAsync(EstadoPago estado, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}