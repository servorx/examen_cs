namespace Api.DTOs.Mecanicos;

public record CreateMecanicoDto(
    string Nombre,
    string? Telefono,
    string? Especialidad,
    bool IsActive,
    int UserId
);
