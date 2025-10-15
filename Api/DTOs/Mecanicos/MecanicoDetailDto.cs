using Api.DTOs.OrdenesServicio;

namespace Api.DTOs.Mecanicos;

public sealed record MecanicoDetailDto(
    int Id,
    string Nombre,
    string? Telefono,
    string? Especialidad,
    bool IsActive,
    int UserId,
    IReadOnlyList<OrdenServicioDto> OrdenesServicio
);
