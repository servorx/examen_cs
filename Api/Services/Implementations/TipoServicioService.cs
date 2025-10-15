using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class TipoServicioService : ITipoServicioService
{
    private readonly IUnitOfWork _unitOfWork;

    public TipoServicioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TipoServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.TipoServicio.GetByIdAsync(id, ct);
    public async Task<TipoServicio?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        => await _unitOfWork.TipoServicio.GetByNombreAsync(nombre, ct);
    public async Task<IReadOnlyList<TipoServicio>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.TipoServicio.GetAllAsync(ct);

    public async Task<int> AddAsync(TipoServicio tipoServicio, CancellationToken ct = default)
    {
        // Validación de negocio
        if (tipoServicio.PrecioBase.Value < 0)
            throw new Exception("El precio base no puede ser negativo.");

        var id = await _unitOfWork.TipoServicio.AddAsync(tipoServicio, ct);
        await _unitOfWork.SaveChanges(ct);

        return id;
    }

    public async Task<bool> UpdateAsync(TipoServicio tipoServicio, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.TipoServicio.GetByIdAsync(tipoServicio.Id, ct);
        if (existing == null)
            return false;

        if (tipoServicio.PrecioBase.Value < 0)
            throw new Exception("El precio base no puede ser negativo.");

        var updated = await _unitOfWork.TipoServicio.UpdateAsync(tipoServicio, ct);
        await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.TipoServicio.GetByIdAsync(id, ct);
        if (existing == null)
            return false;

        var deleted = await _unitOfWork.TipoServicio.DeleteAsync(id, ct);
        await _unitOfWork.SaveChanges(ct);

        return deleted;
    }
}
