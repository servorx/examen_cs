namespace Api.DTOs.Recepcionistas;

public sealed record CreateRecepcionistaDto(
    string Nombre,
    string Telefono,
    int AniosExperiencia,
    bool IsActive,
    int UserId
);
