using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class VehiculosSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Vehiculos.AnyAsync()) return;

        var cliente = await db.Clientes.FirstOrDefaultAsync();
        if (cliente == null) return;

        db.Vehiculos.Add(new Vehiculo
        {
            ClienteId = cliente.Id,
            Marca = new NombreVO("Toyota"),
            Modelo = new NombreVO("Corolla"),
            Anio = new AnioVehiculoVO(2020),
            Vin = new VinVO("JTDBR32E820123456"),
            Kilometraje = new KilometrajeVO(45000)
        });

        await db.SaveChangesAsync();
    }
}
