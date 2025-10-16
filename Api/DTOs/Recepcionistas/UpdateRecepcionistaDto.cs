namespace Api.DTOs.Recepcionistas;

public sealed record UpdateRecepcionistaDto(
    string? Nombre,
    string? Telefono,
    int AniosExperiencia,
    bool? IsActive
);
