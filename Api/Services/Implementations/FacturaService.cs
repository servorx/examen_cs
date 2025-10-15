using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class FacturaService : IFacturaService
{
    private readonly IUnitOfWork _unitOfWork;

    public FacturaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Factura?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Facturas.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Factura>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Facturas.GetAllAsync(ct);

    public async Task<int> AddAsync(Factura factura, CancellationToken ct = default)
    {
        var result = await _unitOfWork.Facturas.AddAsync(factura, ct);
        await _unitOfWork.SaveChanges(ct);
        return result;
    }

    public async Task<bool> UpdateAsync(Factura factura, CancellationToken ct = default)
    {
        var updated = await _unitOfWork.Facturas.UpdateAsync(factura, ct);
        if (updated)
            await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var deleted = await _unitOfWork.Facturas.DeleteAsync(id, ct);
        if (deleted)
            await _unitOfWork.SaveChanges(ct);

        return deleted;
    }

    public async Task<decimal> CalcularTotalAsync(IdVO facturaId, CancellationToken ct = default)
    {
        var factura = await _unitOfWork.Facturas.GetByIdAsync(facturaId, ct);
        if (factura == null)
            return 0;

        // Obtener detalles de orden relacionados
        var detalles = await _unitOfWork.DetalleOrden.GetByOrdenServicioIdAsync(factura.OrdenServicioId, ct);

        decimal totalRepuestos = detalles.Sum(d => d.Costo.Value * d.Cantidad.Value);
        decimal total = totalRepuestos + factura.ManoObra.Value;

        return total;
    }

    public async Task<IReadOnlyList<Factura>> GetByOrdenServicioIdAsync(IdVO ordenServicioId, CancellationToken ct = default)
        => await _unitOfWork.Facturas.GetByOrdenServicioIdAsync(ordenServicioId, ct);
}
