using Api.DTOs.EstadosOrden;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class EstadoOrdenProfile : Profile
{
    public EstadoOrdenProfile()
    {
        // Entidad -> DTO
        CreateMap<EstadoOrden, EstadoOrdenDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value));

        // Crear DTO -> Entidad
        CreateMap<CreateEstadoOrdenDto, EstadoOrden>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // generado automáticamente
                dest.Nombre = new NombreVO(src.Nombre);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateEstadoOrdenDto, EstadoOrden>()
            .AfterMap((src, dest) =>
            {
                // si el nombre no es nulo, genera uno nuevo
                if (src.Nombre != null)
                    dest.Nombre = new NombreVO(src.Nombre);
            });
    }
}
