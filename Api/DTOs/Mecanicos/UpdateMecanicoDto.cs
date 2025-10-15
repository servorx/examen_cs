namespace Api.DTOs.Mecanicos;

public record UpdateMecanicoDto(
    string Nombre,
    string? Telefono,
    string? Especialidad,
    bool? IsActive
);
