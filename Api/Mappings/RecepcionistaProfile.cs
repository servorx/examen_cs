using Api.DTOs.Recepcionistas;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class RecepcionistaProfile : Profile
{
    public RecepcionistaProfile()
    {
        // Configuración global para todos los ValueObjects simples
        CreateMap<IdVO, int>().ConvertUsing(vo => vo.Value);
        CreateMap<NombreVO, string>().ConvertUsing(vo => vo.Value);
        CreateMap<TelefonoVO, string>().ConvertUsing(vo => vo.Value);
        CreateMap<AnioExperienciaVO, int>().ConvertUsing(vo => vo.Value);
        CreateMap<EstadoVO, bool>().ConvertUsing(vo => vo.Value);

        // Entidad -> DTO
        CreateMap<Recepcionista, RecepcionistaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono.Value))
            .ForMember(dest => dest.AniosExperiencia, opt => opt.MapFrom(src => src.AnioExperiencia.Value))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive.Value))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        // Create DTO -> Entidad
        CreateMap<CreateRecepcionistaDto, Recepcionista>()
            .AfterMap((src, dest) =>
            {
                dest.Nombre = new NombreVO(src.Nombre);
                dest.Telefono = new TelefonoVO(src.Telefono);
                dest.AnioExperiencia = new AnioExperienciaVO(src.AniosExperiencia);
                dest.IsActive = new EstadoVO(true);
            });

        // Update DTO -> Entidad (actualización parcial)
        var updateMap = CreateMap<UpdateRecepcionistaDto, Recepcionista>();
        updateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        updateMap.AfterMap((src, dest) =>
        {
            if (src.Nombre != null) dest.Nombre = new NombreVO(src.Nombre);
            if (src.Telefono != null) dest.Telefono = new TelefonoVO(src.Telefono);
            if (src.AniosExperiencia != null) dest.AnioExperiencia = new AnioExperienciaVO(src.AniosExperiencia);
            if (src.IsActive.HasValue) dest.IsActive = new EstadoVO(src.IsActive.Value);
        });
    }
}
