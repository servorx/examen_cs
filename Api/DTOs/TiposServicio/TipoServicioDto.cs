namespace Api.DTOs.TiposServicio;

public sealed record TipoServicioDto(
    int Id,
    string Nombre, 
    string Descripcion, 
    decimal PrecioBase
);
