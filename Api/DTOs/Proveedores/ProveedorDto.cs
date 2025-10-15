namespace Api.DTOs.Proveedores;

public sealed record ProveedorDto(
    int Id,
    string Nombre,
    string? Telefono,
    string? Correo,
    string? Direccion,
    bool IsActive,
    int UserId
);
