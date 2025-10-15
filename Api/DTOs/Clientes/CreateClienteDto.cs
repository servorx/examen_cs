namespace Api.DTOs.Clientes;

public sealed record CreateClienteDto(
    string Nombre,
    string Correo,
    string Telefono,
    string Direccion,
    bool IsActive,
    int UserId
);
