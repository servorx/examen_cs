using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class CitasSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Citas.AnyAsync()) return;

        var cliente = await db.Clientes.FirstOrDefaultAsync();
        var vehiculo = await db.Vehiculos.FirstOrDefaultAsync();
        var estadoCita = await db.EstadosCita.FirstOrDefaultAsync();

        if (cliente == null || vehiculo == null || estadoCita == null)
            return;

        db.Citas.AddRange(new[]
        {
            new Cita
            {
                ClienteId = cliente.Id,
                VehiculoId = vehiculo.Id,
                FechaCita = new FechaCitaVO(DateTime.UtcNow.AddDays(3)),
                Motivo = new DescripcionVO("Revisión general y cambio de aceite"),
                EstadoId = estadoCita.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Cita
            {
                ClienteId = cliente.Id,
                VehiculoId = vehiculo.Id,
                FechaCita = new FechaCitaVO(DateTime.UtcNow.AddDays(5)),
                Motivo = new DescripcionVO("Chequeo de frenos y suspensión"),
                EstadoId = estadoCita.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        });
        await db.SaveChangesAsync();
    }
}