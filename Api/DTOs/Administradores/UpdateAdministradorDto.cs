namespace Api.DTOs.Administradores;

public sealed record UpdateAdministradorDto(
    string? Nombre,
    string? Telefono,
    string? NivelAcceso,
    string? AreaResponsabilidad,
    bool? IsActive
);
