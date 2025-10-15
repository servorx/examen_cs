using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class CitaRepository : ICitaRepository
{
    private readonly AppDbContext _context;

    public CitaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cita?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.Citas
            .Include(c => c.Cliente)
            .Include(c => c.Vehiculo)
            .Include(c => c.Estado)
            .FirstOrDefaultAsync(c => c.Id == id, ct); //  Sin .Value
    }

    public async Task<IReadOnlyList<Cita>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Citas
        .Include(c => c.Cliente)  // ⚠ cargar Cliente
        .Include(c => c.Vehiculo) // ⚠ cargar Vehiculo
        .Include(c => c.Estado)   // ⚠ cargar Estado
        .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Cita>> GetByClienteIdAsync(IdVO clienteId, CancellationToken ct = default)
    {
        return await _context.Citas
            .Include(c => c.Cliente)
            .Include(c => c.Vehiculo)
            .Where(c => c.ClienteId == clienteId) //  Sin .Value
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Cita>> GetByVehiculoIdAsync(IdVO vehiculoId, CancellationToken ct = default)
    {
        return await _context.Citas
            .Include(c => c.Cliente)
            .Include(c => c.Vehiculo)
            .Where(c => c.VehiculoId == vehiculoId) //  Sin .Value
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Cita>> GetByFechaAsync(FechaCitaVO fecha, CancellationToken ct = default)
    {
        return await _context.Citas
            .Include(c => c.Cliente)
            .Include(c => c.Vehiculo)
            .Where(c => c.FechaCita == fecha) //  Igual, EF entiende el VO
            .ToListAsync(ct);
    }

    public async Task<int> AddAsync(Cita cita, CancellationToken ct = default)
    {
        await _context.Citas.AddAsync(cita, ct);
        await _context.SaveChangesAsync(ct);
        return cita.Id.Value; //  Aquí sí puedes usar .Value, ya está en memoria
    }

    public async Task<bool> UpdateAsync(Cita cita, CancellationToken ct = default)
    {
        _context.Citas.Update(cita);
        return await _context.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var cita = await _context.Citas.FirstOrDefaultAsync(c => c.Id == id, ct); //  Sin .Value
        if (cita is null) return false;

        _context.Citas.Remove(cita);
        return await _context.SaveChangesAsync(ct) > 0;
    }
}
