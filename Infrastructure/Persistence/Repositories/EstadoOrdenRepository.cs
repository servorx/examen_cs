using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class EstadoOrdenRepository : IEstadoOrdenRepository
{
    private readonly AppDbContext _context;

    public EstadoOrdenRepository(AppDbContext context) =>_context = context;

    public async Task<EstadoOrden?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.EstadosOrden
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<EstadoOrden?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default)
    {
        return await _context.EstadosOrden
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Nombre == nombre, ct);
    }

    public async Task<IReadOnlyList<EstadoOrden>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.EstadosOrden
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<EstadoOrden> AddAsync(EstadoOrden estado, CancellationToken ct = default)
    {
        await _context.EstadosOrden.AddAsync(estado, ct);
        await _context.SaveChangesAsync(ct);
        return estado; // 👉 ahora tiene el ID generado por la base de datos
    }


    public async Task<bool> UpdateAsync(EstadoOrden estado, CancellationToken ct = default)
    {
        var existing = await _context.EstadosOrden
            .FirstOrDefaultAsync(e => e.Id.Equals(estado.Id), ct);

        if (existing is null)
            return false;

        _context.Entry(existing).CurrentValues.SetValues(estado);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _context.EstadosOrden
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (existing is null)
            return false;

        _context.EstadosOrden.Remove(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
