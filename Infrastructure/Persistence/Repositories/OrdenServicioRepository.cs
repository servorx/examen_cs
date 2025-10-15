using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class OrdenServicioRepository : IOrdenServicioRepository
{
    private readonly AppDbContext _context;

    public OrdenServicioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(OrdenServicio orden, CancellationToken ct = default)
    {
        await _context.OrdenesServicio.AddAsync(orden, ct);
        await _context.SaveChangesAsync(ct);
        return orden.Id.Value.GetHashCode();
    }

    public async Task<bool> UpdateAsync(OrdenServicio orden, CancellationToken ct = default)
    {
        _context.OrdenesServicio.Update(orden);
        var updated = await _context.SaveChangesAsync(ct);
        return updated > 0;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var orden = await _context.OrdenesServicio.FirstOrDefaultAsync(o => o.Id == id, ct);
        if (orden == null) return false;

        _context.OrdenesServicio.Remove(orden);
        var deleted = await _context.SaveChangesAsync(ct);
        return deleted > 0;
    }

    public async Task<OrdenServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.OrdenesServicio
            .Include(o => o.Vehiculo)
                // incluir cliente para que no se genere un error de circular referencia
                .ThenInclude(v => v.Cliente) 
            .Include(o => o.Mecanico)
            .Include(o => o.TipoServicio)
            .Include(o => o.Estado)
            .Include(o => o.Detalles)
                .ThenInclude(d => d.Repuesto)
            .Include(o => o.Facturas)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, ct);
    }

    public async Task<IReadOnlyList<OrdenServicio>> GetByVehiculoIdAsync(IdVO vehiculoId, CancellationToken ct = default)
    {
        return await _context.OrdenesServicio
            .Include(o => o.Vehiculo)
            .Include(o => o.Mecanico)
            .Include(o => o.TipoServicio)
            .Include(o => o.Estado)
            .Where(o => o.VehiculoId == vehiculoId)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<OrdenServicio>> GetByMecanicoIdAsync(IdVO mecanicoId, CancellationToken ct = default)
    {
        return await _context.OrdenesServicio
            .Include(o => o.Vehiculo)
            .Include(o => o.Mecanico)
            .Include(o => o.TipoServicio)
            .Include(o => o.Estado)
            .Where(o => o.MecanicoId == mecanicoId)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<OrdenServicio>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.OrdenesServicio
            .Include(o => o.Vehiculo)
            .Include(o => o.Mecanico)
            .Include(o => o.TipoServicio)
            .Include(o => o.Estado)
            .ToListAsync(ct);
    }
}