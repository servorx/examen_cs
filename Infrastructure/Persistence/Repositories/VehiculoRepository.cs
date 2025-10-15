using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class VehiculoRepository: IVehiculoRepository
{
    private readonly AppDbContext _context;
    // constructor con contexto
    public VehiculoRepository(AppDbContext context) => _context = context;
    public async Task<Vehiculo?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.Vehiculos
            .Include(v => v.Cliente)
            .Include(v => v.Citas)
            .Include(v => v.OrdenesServicio)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id, ct);
    }

    public async Task<Vehiculo?> GetByVinAsync(VinVO vin, CancellationToken ct = default)
    {
        return await _context.Vehiculos
            .Include(v => v.Cliente)
            .Include(v => v.Citas)
            .Include(v => v.OrdenesServicio)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Vin.Value == vin.Value, ct);
    }

    public async Task<IReadOnlyList<Vehiculo>> GetByClienteIdAsync(IdVO clienteId, CancellationToken ct = default)
    {
        return await _context.Vehiculos
            .Include(v => v.Cliente)
            .Include(v => v.Citas)
            .Include(v => v.OrdenesServicio)
            .AsNoTracking()
            .Where(v => v.ClienteId == clienteId)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Vehiculo>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Vehiculos
            .Include(v => v.Cliente)
            .Include(v => v.Citas)
            .Include(v => v.OrdenesServicio)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsByVinAsync(VinVO vin, CancellationToken ct = default)
    {
        return await _context.Vehiculos
            .AnyAsync(v => v.Vin.Value == vin.Value, ct);
    }

    public async Task<int> AddAsync(Vehiculo vehiculo, CancellationToken ct = default)
    {
        _context.Vehiculos.Add(vehiculo);
        return await _context.SaveChangesAsync(ct);
    }

    public async Task<bool> UpdateAsync(Vehiculo vehiculo, CancellationToken ct = default)
    {
        var existing = await _context.Vehiculos
            .FirstOrDefaultAsync(v => v.Id == vehiculo.Id, ct);

        if (existing is null)
            return false;

        existing.Marca = vehiculo.Marca;
        existing.Modelo = vehiculo.Modelo;
        existing.Anio = vehiculo.Anio;
        existing.Vin = vehiculo.Vin;
        existing.Kilometraje = vehiculo.Kilometraje;
        existing.ClienteId = vehiculo.ClienteId;

        _context.Vehiculos.Update(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _context.Vehiculos
            .FirstOrDefaultAsync(v => v.Id == id, ct);

        if (existing is null)
            return false;

        _context.Vehiculos.Remove(existing);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
