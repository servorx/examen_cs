using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MecanicoRepository: IMecanicoRepository
{
    private readonly AppDbContext _context;
    // constructor con contexto
    public MecanicoRepository(AppDbContext context) => _context = context;

    public async Task<Mecanico?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.Mecanicos
            .Include(m => m.User) // relación con UserMember
            .Include(m => m.OrdenesServicio) // relación uno a muchos
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task<IReadOnlyList<Mecanico>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Mecanicos
            .Include(m => m.User)
            .Include(m => m.OrdenesServicio)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default)
    {
        return await _context.Mecanicos
            .AnyAsync(m => m.Nombre.Value == nombre.Value, ct);
    }
    public async Task<int> AddAsync(Mecanico mecanico, CancellationToken ct = default)
    {
        _context.Mecanicos.Add(mecanico);
        return await _context.SaveChangesAsync(ct);
    }

    public async Task<bool> UpdateAsync(Mecanico mecanico, CancellationToken ct = default)
    {
        var existing = await _context.Mecanicos
            .FirstOrDefaultAsync(m => m.Id == mecanico.Id, ct);

        if (existing is null)
            return false;

        existing.Nombre = mecanico.Nombre;
        existing.Telefono = mecanico.Telefono;
        existing.Especialidad = mecanico.Especialidad;
        existing.IsActive = mecanico.IsActive;
        existing.User = mecanico.User;

        _context.Mecanicos.Update(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _context.Mecanicos
            .FirstOrDefaultAsync(m => m.Id == id, ct);

        if (existing is null)
            return false;

        _context.Mecanicos.Remove(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
