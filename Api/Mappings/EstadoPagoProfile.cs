using Api.DTOs.EstadosPago;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class EstadoPagoProfile : Profile
{
    public EstadoPagoProfile()
    {
        // Entidad -> DTO
        CreateMap<EstadoPago, EstadoPagoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value));

        // Crear DTO -> Entidad
        CreateMap<CreateEstadoPagoDto, EstadoPago>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // generado automáticamente
                dest.Nombre = new NombreVO(src.Nombre);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateEstadoPagoDto, EstadoPago>()
            .AfterMap((src, dest) =>
            {
                // si el nombre no es nulo, genera uno nuevo
                if (src.Nombre != null)
                    dest.Nombre = new NombreVO(src.Nombre);
            });
    }
}
