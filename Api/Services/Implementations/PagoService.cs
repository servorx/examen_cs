    using Api.DTOs.Pagos;
using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class PagoService : IPagoService
{
    private readonly IUnitOfWork _unitOfWork;

    public PagoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Pago?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Pagos.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Pago>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Pagos.GetAllAsync(ct);

    public async Task<int> AddAsync(Pago pago, CancellationToken ct = default)
    {
        // Validaciones
        if (pago.Monto.Value <= 0)
            throw new ArgumentException("El monto del pago debe ser mayor a cero.");

        var factura = await _unitOfWork.Facturas.GetByIdAsync(pago.FacturaId, ct);
        if (factura == null)
            throw new InvalidOperationException("No se puede registrar el pago porque la factura no existe.");

        var totalPagado = await GetTotalPagadoPorFacturaAsync(pago.FacturaId, ct);
        if (totalPagado + pago.Monto.Value > factura.Total.Value)
            throw new InvalidOperationException("El pago excede el total pendiente de la factura.");

        if (pago.FechaPago == null)
            pago.FechaPago = new FechaHistoricaVO(DateTime.UtcNow);

        var result = await _unitOfWork.Pagos.AddAsync(pago, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }
    public async Task<bool> UpdateAsync(Pago pago, CancellationToken ct = default)
    {
        if (pago.Monto.Value <= 0)
            throw new ArgumentException("El monto del pago debe ser mayor a cero.");

        var factura = await _unitOfWork.Facturas.GetByIdAsync(pago.FacturaId, ct);
        if (factura == null)
            throw new InvalidOperationException("La factura asociada no existe.");

        var pagos = await _unitOfWork.Pagos.GetByFacturaIdAsync(pago.FacturaId, ct);
        var totalPagadoSinEste = pagos.Where(p => p.Id != pago.Id).Sum(p => p.Monto.Value);

        if (totalPagadoSinEste + pago.Monto.Value > factura.Total.Value)
            throw new InvalidOperationException("El pago actualizado excede el total pendiente de la factura.");

        var result = await _unitOfWork.Pagos.UpdateAsync(pago, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var pago = await _unitOfWork.Pagos.GetByIdAsync(id, ct);
        if (pago == null)
            throw new InvalidOperationException("No se puede eliminar un pago que no existe.");

        var result = await _unitOfWork.Pagos.DeleteAsync(id, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<decimal> GetTotalPagadoPorFacturaAsync(IdVO facturaId, CancellationToken ct = default)
    {
        var pagos = await _unitOfWork.Pagos.GetByFacturaIdAsync(facturaId, ct);
        return pagos.Sum(p => p.Monto.Value);
    }

    public async Task<IReadOnlyList<Pago>> GetByFacturaIdAsync(IdVO facturaId, CancellationToken ct = default)
        => await _unitOfWork.Pagos.GetByFacturaIdAsync(facturaId, ct);
}
