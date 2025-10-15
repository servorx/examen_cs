using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class RepuestoService : IRepuestoService
{
    private readonly IUnitOfWork _unitOfWork;

    public RepuestoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Repuesto?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Repuestos.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Repuesto>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Repuestos.GetAllAsync(ct);

    public async Task<int> AddAsync(Repuesto repuesto, CancellationToken ct = default)
    {
        // Validar código único
        if (await _unitOfWork.Repuestos.ExistsByCodigoAsync(repuesto.Codigo, ct))
            throw new Exception("Ya existe un repuesto con ese código.");

        var result = await _unitOfWork.Repuestos.AddAsync(repuesto, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> UpdateAsync(Repuesto repuesto, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Repuestos.GetByIdAsync(repuesto.Id, ct);
        if (existing == null) return false;

        var result = await _unitOfWork.Repuestos.UpdateAsync(repuesto, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Repuestos.GetByIdAsync(id, ct);
        if (existing == null) return false;

        var result = await _unitOfWork.Repuestos.DeleteAsync(id, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> ExistsByCodigoAsync(CodigoRepuestoVO codigo, CancellationToken ct = default)
        => await _unitOfWork.Repuestos.ExistsByCodigoAsync(codigo, ct);

    public async Task<bool> UpdateStockAsync(IdVO id, int cantidad, CancellationToken ct = default)
    {
        var repuesto = await _unitOfWork.Repuestos.GetByIdAsync(id, ct);
        if (repuesto == null)
            return false;

        var nuevaCantidad = repuesto.CantidadStock.Value + cantidad;
        if (nuevaCantidad < 0)
            throw new Exception("Stock insuficiente.");

        repuesto.CantidadStock = new CantidadVO(nuevaCantidad);

        var result = await _unitOfWork.Repuestos.UpdateAsync(repuesto, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }
}
