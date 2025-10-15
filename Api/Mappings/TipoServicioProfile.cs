using Api.DTOs.TiposServicio;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class TipoServicioProfile : Profile
{
    public TipoServicioProfile()
    {
        // Entidad -> DTO de respuesta
        CreateMap<TipoServicio, TipoServicioDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion.Value))
            .ForMember(dest => dest.PrecioBase, opt => opt.MapFrom(src => src.PrecioBase.Value));

        // Crear DTO -> Entidad
        CreateMap<CreateTipoServicioDto, TipoServicio>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // se generará automáticamente
                dest.Nombre = new NombreVO(src.Nombre);
                dest.Descripcion = new DescripcionVO(src.Descripcion);
                dest.PrecioBase = new DineroVO(src.PrecioBase);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateTipoServicioDto, TipoServicio>()
            .AfterMap((src, dest) =>
            {
                dest.Nombre = new NombreVO(src.Nombre);
                dest.Descripcion = new DescripcionVO(src.Descripcion);
                dest.PrecioBase = new DineroVO(src.PrecioBase);
            });

        // Entidad => Detail DTO
        CreateMap<TipoServicio, TipoServicioDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion.Value))
            .ForMember(dest => dest.PrecioBase, opt => opt.MapFrom(src => src.PrecioBase.Value))
            // Mapeo de colecciones relacionadas usando los DTOs de cada entidad
            .ForMember(dest => dest.OrdenesServicio, opt => opt.MapFrom(src => src.OrdenesServicio));
    }
}
