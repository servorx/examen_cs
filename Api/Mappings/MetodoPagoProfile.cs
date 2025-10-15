using Api.DTOs.MetodosPago;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class MetodoPagoProfile : Profile
{
    public MetodoPagoProfile()
    {
        // Entidad -> DTO de respuesta
        CreateMap<MetodoPago, MetodoPagoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value));

        // Crear DTO -> Entidad
        CreateMap<CreateMetodoPagoDto, MetodoPago>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // se generará automáticamente
                dest.Nombre = new NombreVO(src.Nombre);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateMetodoPagoDto, MetodoPago>()
            .AfterMap((src, dest) =>
            {
                dest.Nombre = new NombreVO(src.Nombre);
            });
    }
}
