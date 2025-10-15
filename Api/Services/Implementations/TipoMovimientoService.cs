using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class TipoMovimientoService : ITipoMovimientoService
{
    private readonly IUnitOfWork _unitOfWork;

    public TipoMovimientoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CrearAsync(TipoMovimiento tipoMovimiento, CancellationToken ct = default)
    {
        return await _unitOfWork.TipoMovimiento.AddAsync(tipoMovimiento, ct);
    }

    public async Task<bool> ActualizarAsync(TipoMovimiento tipoMovimiento, CancellationToken ct = default)
    {
        return await _unitOfWork.TipoMovimiento.UpdateAsync(tipoMovimiento, ct);
    }

    public async Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default)
    {
        return await _unitOfWork.TipoMovimiento.DeleteAsync(id, ct);
    }

    public async Task<TipoMovimiento?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _unitOfWork.TipoMovimiento.GetByIdAsync(id, ct);
    }

    public async Task<TipoMovimiento?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default)
    {
        return await _unitOfWork.TipoMovimiento.GetByNombreAsync(nombre, ct);
    }

    public async Task<IReadOnlyList<TipoMovimiento>> ObtenerTodosAsync(CancellationToken ct = default)
    {
        return await _unitOfWork.TipoMovimiento.GetAllAsync(ct);
    }
}
