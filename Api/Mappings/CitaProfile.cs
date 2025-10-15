
using Api.DTOs.Citas;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class CitaProfile : Profile
{
    public CitaProfile()
    {
        // Convertir FechaCitaVO a DateTime de forma explicita
        CreateMap<FechaCitaVO, DateTime>().ConvertUsing(src => src.Value);

        // Entidad ->  DTO
        CreateMap<Cita, CitaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Nombre.Value))
            .ForMember(dest => dest.VehiculoPlaca, opt => opt.MapFrom(src => src.Vehiculo.Vin.Value))
            .ForMember(dest => dest.FechaCita, opt => opt.MapFrom(src => src.FechaCita)) // AutoMapper usará el ConvertUsing
            .ForMember(dest => dest.Motivo, opt => opt.MapFrom(src => src.Motivo != null ? src.Motivo.Value : null))
            .ForMember(dest => dest.EstadoNombre, opt => opt.MapFrom(src => src.Estado.Nombre.Value));


        // Crear DTO -> Entidad
        CreateMap<CreateCitaDto, Cita>()
            .ConstructUsing(src => new Cita(
                new IdVO(src.ClienteId),
                new IdVO(src.VehiculoId),
                new FechaCitaVO(src.FechaCita),
                src.Motivo != null ? new DescripcionVO(src.Motivo) : null,
                new IdVO(src.EstadoId)
            ));

        // Actualizar DTO -> Entidad
        var actualizarCitaMap = CreateMap<UpdateCitaDto, Cita>();
        actualizarCitaMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        actualizarCitaMap.AfterMap((src, dest) =>
        {
            if (src.FechaCita.HasValue)
                dest.FechaCita = new FechaCitaVO(src.FechaCita.Value);

            if (!string.IsNullOrWhiteSpace(src.Motivo))
                dest.Motivo = new DescripcionVO(src.Motivo!);

            if (src.EstadoId.HasValue)
                dest.EstadoId = new IdVO(src.EstadoId.Value);
        });
    }
}
