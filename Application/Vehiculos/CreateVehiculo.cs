using Domain.ValueObjects;
using MediatR;

namespace Application.Vehiculos;

public sealed record CreateVehiculo(
    IdVO ClienteId,
    NombreVO Marca,
    NombreVO Modelo,
    AnioVehiculoVO Anio,
    VinVO Vin,
    KilometrajeVO Kilometraje
) : IRequest<IdVO>;
