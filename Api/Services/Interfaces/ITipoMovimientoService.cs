using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface ITipoMovimientoService
{
    Task<int> CrearAsync(TipoMovimiento tipoMovimiento, CancellationToken ct = default);
    Task<bool> ActualizarAsync(TipoMovimiento tipoMovimiento, CancellationToken ct = default);
    Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default);
    Task<TipoMovimiento?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default);
    Task<TipoMovimiento?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<TipoMovimiento>> ObtenerTodosAsync(CancellationToken ct = default);
}
