namespace Api.DTOs.Proveedores;

public sealed record CreateProveedorDto(
    string Nombre,
    string? Telefono,
    string? Correo,
    string? Direccion,
    bool IsActive,
    int UserId
);
