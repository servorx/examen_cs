using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class CatalogsSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Estados Cita
        if (!await db.EstadosCita.AnyAsync())
            db.EstadosCita.AddRange(
                new EstadoCita { Nombre = new NombreVO("Pendiente") },
                new EstadoCita { Nombre = new NombreVO("Confirmada") },
                new EstadoCita { Nombre = new NombreVO("Finalizada") },
                new EstadoCita { Nombre = new NombreVO("Cancelada") }
            );

        // Estados Orden
        if (!await db.EstadosOrden.AnyAsync())
            db.EstadosOrden.AddRange(
                new EstadoOrden { Nombre = new NombreVO("Pendiente") },
                new EstadoOrden { Nombre = new NombreVO("En Proceso") },
                new EstadoOrden { Nombre = new NombreVO("Completada") },
                new EstadoOrden { Nombre = new NombreVO("Cancelada") }
            );

        // Estados Pago
        if (!await db.EstadosPago.AnyAsync())
            db.EstadosPago.AddRange(
                new EstadoPago { Nombre = new NombreVO("Pendiente") },
                new EstadoPago { Nombre = new NombreVO("Pagado") },
                new EstadoPago { Nombre = new NombreVO("Rechazado") }
            );

        // Tipos Movimiento
        if (!await db.TiposMovimiento.AnyAsync())
            db.TiposMovimiento.AddRange(
                new TipoMovimiento { Nombre = new NombreVO("Ingreso") },
                new TipoMovimiento { Nombre = new NombreVO("Salida") }
            );

        // Métodos Pago
        if (!await db.MetodosPago.AnyAsync())
            db.MetodosPago.AddRange(
                new MetodoPago { Nombre = new NombreVO("Efectivo") },
                new MetodoPago { Nombre = new NombreVO("Tarjeta") },
                new MetodoPago { Nombre = new NombreVO("Transferencia") }
            );

        // Tipos Servicio
        if (!await db.TiposServicio.AnyAsync())
            db.TiposServicio.AddRange(
                new TipoServicio
                {
                    Nombre = new NombreVO("Cambio de Aceite"),
                    Descripcion = new DescripcionVO("Cambio de aceite y filtro"),
                    PrecioBase = new DineroVO(120000)
                },
                new TipoServicio
                {
                    Nombre = new NombreVO("Alineación"),
                    Descripcion = new DescripcionVO("Alineación de ruedas"),
                    PrecioBase = new DineroVO(80000)
                },
                new TipoServicio
                {
                    Nombre = new NombreVO("Balanceo"),
                    Descripcion = new DescripcionVO("Balanceo de ruedas"),
                    PrecioBase = new DineroVO(60000)
                },
                new TipoServicio
                {
                    Nombre = new NombreVO("Diagnóstico"),
                    Descripcion = new DescripcionVO("Revisión general del vehículo"),
                    PrecioBase = new DineroVO(100000)
                }
            );

        await db.SaveChangesAsync();
    }
}
