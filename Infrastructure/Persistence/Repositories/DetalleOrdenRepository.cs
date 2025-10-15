using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class DetalleOrdenRepository : IDetalleOrdenRepository
{
    private readonly AppDbContext _context;

    public DetalleOrdenRepository(AppDbContext context) =>_context = context;

    public async Task<DetalleOrden?> GetByIdsAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default)
    {
        return await _context.DetallesOrden
            .Include(d => d.OrdenServicio)
            .Include(d => d.Repuesto)
            .AsNoTracking()
            .FirstOrDefaultAsync(d =>
                d.OrdenServicioId == ordenServicioId &&
                d.RepuestoId == repuestoId,
                ct);
    }

    public async Task<IReadOnlyList<DetalleOrden>> GetByOrdenServicioIdAsync(IdVO ordenServicioId, CancellationToken ct = default)
    {
        return await _context.DetallesOrden
            .Include(d => d.OrdenServicio)
            .Include(d => d.Repuesto)
            .AsNoTracking()
            .Where(d => d.OrdenServicioId == ordenServicioId) //  sin .Value
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<DetalleOrden>> GetByRepuestoIdAsync(IdVO repuestoId, CancellationToken ct = default)
    {
        return await _context.DetallesOrden
            .Include(d => d.OrdenServicio)
            .Include(d => d.Repuesto)
            .AsNoTracking()
            .Where(d => d.RepuestoId == repuestoId) //  sin .Value
            .ToListAsync(ct);
    }

    public async Task<int> AddAsync(DetalleOrden detalle, CancellationToken ct = default)
    {
        await _context.DetallesOrden.AddAsync(detalle, ct);
        return await _context.SaveChangesAsync(ct);
    }

    public async Task<bool> UpdateAsync(DetalleOrden detalle, CancellationToken ct = default)
    {
        var existing = await _context.DetallesOrden
            .FirstOrDefaultAsync(d =>
                d.OrdenServicioId == detalle.OrdenServicioId &&   // ✅
                d.RepuestoId == detalle.RepuestoId,
                ct);

        if (existing is null)
            return false;

        _context.Entry(existing).CurrentValues.SetValues(detalle);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default)
    {
        var existing = await _context.DetallesOrden
            .FirstOrDefaultAsync(d =>
                d.OrdenServicioId == ordenServicioId &&   // ✅
                d.RepuestoId == repuestoId,
                ct);

        if (existing is null)
            return false;

        _context.DetallesOrden.Remove(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}

