using Api.DTOs.OrdenesServicio;

namespace Api.DTOs.TiposServicio;

public sealed record TipoServicioDetailDto(
    int Id,
    string Nombre,
    string Descripcion,
    decimal PrecioBase,
    IReadOnlyList<OrdenServicioDto> OrdenesServicio
);
