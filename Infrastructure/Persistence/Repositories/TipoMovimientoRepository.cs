using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class TipoMovimientoRepository : ITipoMovimientoRepository
{
    private readonly AppDbContext _context;

    public TipoMovimientoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(TipoMovimiento tipo, CancellationToken ct = default)
    {
        await _context.TiposMovimiento.AddAsync(tipo, ct);
        await _context.SaveChangesAsync(ct);
        return tipo.Id.Value.GetHashCode();
    }

    public async Task<bool> UpdateAsync(TipoMovimiento tipo, CancellationToken ct = default)
    {
        _context.TiposMovimiento.Update(tipo);
        var updated = await _context.SaveChangesAsync(ct);
        return updated > 0;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var tipo = await _context.TiposMovimiento.FirstOrDefaultAsync(t => t.Id == id, ct);
        if (tipo == null) return false;

        _context.TiposMovimiento.Remove(tipo);
        var deleted = await _context.SaveChangesAsync(ct);
        return deleted > 0;
    }

    public async Task<TipoMovimiento?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.TiposMovimiento
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<TipoMovimiento?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default)
    {
        return await _context.TiposMovimiento
            .FirstOrDefaultAsync(t => t.Nombre == nombre, ct);
    }

    public async Task<IReadOnlyList<TipoMovimiento>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.TiposMovimiento.ToListAsync(ct);
    }
}
