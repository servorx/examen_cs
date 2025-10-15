namespace Api.DTOs.Clientes;

public sealed record UpdateClienteDto(
    string Nombre,
    string Correo,
    string Telefono,
    string Direccion,
    bool IsActive
);
