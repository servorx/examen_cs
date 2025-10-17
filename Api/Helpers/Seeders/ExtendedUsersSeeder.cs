using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class ExtendedUsersSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        var users = await db.UsersMembers.ToListAsync();

        // Clientes
        if (!await db.Clientes.AnyAsync())
        {
            var clienteUser = users.FirstOrDefault(u => u.Username == "cliente1");
            if (clienteUser != null)
            {
                db.Clientes.Add(new Cliente
                {
                    Nombre = new NombreVO("Juan Pérez"),
                    Correo = new CorreoVO(clienteUser.Email!),
                    Telefono = new TelefonoVO("3001234567"),
                    Direccion = new DireccionVO("Cra 12 #45-67, Bogotá"),
                    UserId = clienteUser.Id,
                    IsActive = new EstadoVO(true)
                });
            }
        }

        // Mecanicos
        if (!await db.Mecanicos.AnyAsync())
        {
            var mecUser = users.FirstOrDefault(u => u.Username == "mecanico1");
            if (mecUser != null)
            {
                db.Mecanicos.Add(new Mecanico
                {
                    Nombre = new NombreVO("Carlos Gómez"),
                    Telefono = new TelefonoVO("3017654321"),
                    Especialidad = new EspecialidadVO("Frenos y Suspensión"),
                    UserId = mecUser.Id,
                    IsActive = new EstadoVO(true)
                });
            }
        }

        // Administradores
        if (!await db.Administradores.AnyAsync())
        {
            var adminUser = users.FirstOrDefault(u => u.Username == "admin1");
            if (adminUser != null)
            {
                db.Administradores.Add(new Administrador
                {
                    Nombre = new NombreVO("Laura Torres"),
                    Telefono = new TelefonoVO("3025556666"),
                    NivelAcceso = new NivelAccesoVO("Total"),
                    AreaResponsabilidad = new DescripcionVO("Gestión General"),
                    UserId = adminUser.Id,
                    IsActive = new EstadoVO(true)
                });
            }
        }

        // Proveedores
        if (!await db.Proveedores.AnyAsync())
        {
            var provUser = users.FirstOrDefault(u => u.Username == "proveedor1");
            if (provUser != null)
            {
                db.Proveedores.Add(new Proveedor
                {
                    Nombre = new NombreVO("Repuestos ABC"),
                    Telefono = new TelefonoVO("3041112222"),
                    Correo = new CorreoVO(provUser.Email!),
                    Direccion = new DireccionVO("Zona Industrial 45"),
                    UserId = provUser.Id,
                    IsActive = new EstadoVO(true)
                });
            }
        }

        // Recepcionistas
        if (!await db.Recepcionistas.AnyAsync())
        {
            var recepUser = users.FirstOrDefault(u => u.Username == "recepcionista1");
            if (recepUser != null)
            {
                db.Recepcionistas.Add(new Recepcionista
                {
                    Nombre = new NombreVO("Pedro Pérez"),
                    Telefono = new TelefonoVO("3051234567"),
                    AnioExperiencia = new AnioExperienciaVO(10),
                    UserId = recepUser.Id,
                    IsActive = new EstadoVO(true)
                });
            }
        }

        await db.SaveChangesAsync();
    }
}
