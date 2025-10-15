using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class RepuestosSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Repuestos.AnyAsync()) return;

        var proveedor = await db.Proveedores.FirstOrDefaultAsync();
        if (proveedor == null) return;

        db.Repuestos.AddRange(
            new Repuesto
            {
                Codigo = new CodigoRepuestoVO("REP001"),
                Descripcion = new DescripcionVO("Filtro de aceite"),
                CantidadStock = new CantidadVO(50),
                PrecioUnitario = new DineroVO(20000),
                ProveedorId = proveedor.Id
            },
            new Repuesto
            {
                Codigo = new CodigoRepuestoVO("REP002"),
                Descripcion = new DescripcionVO("Pastillas de freno"),
                CantidadStock = new CantidadVO(30),
                PrecioUnitario = new DineroVO(45000),
                ProveedorId = proveedor.Id
            },
            new Repuesto
            {
                Codigo = new CodigoRepuestoVO("REP003"),
                Descripcion = new DescripcionVO("Batería 12V"),
                CantidadStock = new CantidadVO(10),
                PrecioUnitario = new DineroVO(150000),
                ProveedorId = proveedor.Id
            }
        );

        await db.SaveChangesAsync();
    }
}
