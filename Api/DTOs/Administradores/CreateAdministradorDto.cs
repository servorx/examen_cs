namespace Api.DTOs.Administradores;

public sealed record CreateAdministradorDto(
    string Nombre,
    string Telefono,
    string NivelAcceso,
    string AreaResponsabilidad,
    bool IsActive,
    int UserId
);
