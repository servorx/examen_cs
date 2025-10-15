using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TipoServicioRepository : ITipoServicioRepository
{
    private readonly AppDbContext _context;
    // constructor con contexto
    public TipoServicioRepository(AppDbContext context) => _context = context;

    public async Task<TipoServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.TiposServicio
            .Include(t => t.OrdenesServicio)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<TipoServicio?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default)
    {
        return await _context.TiposServicio
            .Include(t => t.OrdenesServicio)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Nombre == nombre, ct);
    }

    public async Task<IReadOnlyList<TipoServicio>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.TiposServicio
            .Include(t => t.OrdenesServicio)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<int> AddAsync(TipoServicio tipo, CancellationToken ct = default)
    {
        _context.TiposServicio.Add(tipo);
        return await _context.SaveChangesAsync(ct);
    }

    public async Task<bool> UpdateAsync(TipoServicio tipo, CancellationToken ct = default)
    {
        var existing = await _context.TiposServicio
            .FirstOrDefaultAsync(t => t.Id == tipo.Id, ct);

        if (existing is null)
            return false;

        existing.Nombre = tipo.Nombre;
        existing.Descripcion = tipo.Descripcion;
        existing.PrecioBase = tipo.PrecioBase;

        _context.TiposServicio.Update(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _context.TiposServicio
            .FirstOrDefaultAsync(t => t.Id == id, ct);

        if (existing is null)
            return false;

        _context.TiposServicio.Remove(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
