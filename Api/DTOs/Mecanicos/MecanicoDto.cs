namespace Api.DTOs.Mecanicos;

public record MecanicoDto(
    int Id,
    string Nombre,
    string? Telefono,
    string? Especialidad,
    bool IsActive,
    int UserId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
