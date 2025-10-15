using Api.DTOs.Repuestos;

namespace Api.DTOs.Proveedores;

public sealed record class ProveedorDetailDto
{
    // esto se hace para que se pueda crear instancias de este DTO sin pasar parámetros
    public int Id { get; init; }
    public string Nombre { get; init; } = default!;
    public string? Telefono { get; init; }
    public string? Correo { get; init; }
    public string? Direccion { get; init; }
    public bool IsActive { get; init; }
    public int UserId { get; init; }
    public string? UserName { get; init; }
    public IEnumerable<RepuestoDto>? Repuestos { get; init; }
}
