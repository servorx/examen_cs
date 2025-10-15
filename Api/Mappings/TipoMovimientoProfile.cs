using Api.DTOs.TiposMovimiento;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class TipoMovimientoProfile : Profile
{
    public TipoMovimientoProfile()
    {
        // Entidad → DTO
        CreateMap<TipoMovimiento, TipoMovimientoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value));

        // Crear DTO → Entidad
        CreateMap<CreateTipoMovimientoDto, TipoMovimiento>()
            .ConstructUsing(src => new TipoMovimiento(
                IdVO.CreateNew(),
                new NombreVO(src.Nombre)
            ));

        // Actualizar DTO → Entidad
        CreateMap<UpdateTipoMovimientoDto, TipoMovimiento>()
            .ConstructUsing(src => new TipoMovimiento(
                new IdVO(src.Id),
                new NombreVO(src.Nombre)
            ));
    }
}
