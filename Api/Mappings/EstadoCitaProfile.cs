using Api.DTOs.EstadosCita;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class EstadoCitaProfile : Profile
{
    public EstadoCitaProfile()
    {
        // Entidad -> DTO
        CreateMap<EstadoCita, EstadoCitaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value));

        // Crear DTO -> Entidad
        CreateMap<CreateEstadoCitaDto, EstadoCita>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // generado automáticamente
                dest.Nombre = new NombreVO(src.Nombre);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateEstadoCitaDto, EstadoCita>()
            .AfterMap((src, dest) =>
            {
                // si el nombre no es nulo, genera uno nuevo
                if (src.Nombre != null)
                    dest.Nombre = new NombreVO(src.Nombre);
            });
    }
}
