using System;

namespace Api.Helpers;

public class UserAuthorization
{
    public enum Roles
    {
        Administrador,
        Cliente,
        Mecanico,
        Proveedor
    }

    public const Roles rol_default = Roles.Cliente;
}