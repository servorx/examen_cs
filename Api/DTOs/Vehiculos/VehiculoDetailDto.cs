using Api.DTOs.Citas;
using Api.DTOs.OrdenesServicio;

namespace Api.DTOs.Vehiculos;

public sealed record VehiculoDetailDto(
    int Id,
    int ClienteId,
    string Marca,
    string Modelo,
    int Anio,
    string Vin,
    int Kilometraje,
    // Información resumida del cliente
    string ClienteNombre,
    string ClienteCorreo,
    // Colecciones relacionadas
    IReadOnlyList<CitaDto> Citas,
    IReadOnlyList<OrdenServicioDto> OrdenesServicio
);
