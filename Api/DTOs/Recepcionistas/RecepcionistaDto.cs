namespace Api.DTOs.Recepcionistas;

public sealed record RecepcionistaDto(
    int Id,
    string Nombre,
    string Telefono,
    int AniosExperiencia,
    bool IsActive,
    int UserId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);