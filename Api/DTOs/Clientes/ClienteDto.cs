namespace Api.DTOs.Clientes;

public sealed record ClienteDto(
    int Id,
    string Nombre,
    string Correo,
    string Telefono,
    string Direccion,
    bool IsActive,
    int UserId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
