using Api.DTOs.Vehiculos;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class VehiculoProfile : Profile
{
    public VehiculoProfile()
    {
        // 🔹 Mapeos para Value Objects
        CreateMap<AnioVehiculoVO, int>().ConvertUsing(src => src.Value);
        CreateMap<NombreVO, string>().ConvertUsing(src => src.Value);
        CreateMap<KilometrajeVO, int>().ConvertUsing(src => src.Value);
        CreateMap<VinVO, string>().ConvertUsing(src => src.Value);

        // 🔹 Mapeo principal de entidad -> DetailDTO
        CreateMap<Vehiculo, VehiculoDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId.Value))
            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.Value))
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo.Value))
            .ForMember(dest => dest.Anio, opt => opt.MapFrom(src => src.Anio.Value))
            .ForMember(dest => dest.Vin, opt => opt.MapFrom(src => src.Vin.Value))
            .ForMember(dest => dest.Kilometraje, opt => opt.MapFrom(src => src.Kilometraje.Value));

        // mapeo de entidad a dto simple
        CreateMap<Vehiculo, VehiculoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca))
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo))
            .ForMember(dest => dest.Anio, opt => opt.MapFrom(src => src.Anio))
            .ForMember(dest => dest.Vin, opt => opt.MapFrom(src => src.Vin))
            .ForMember(dest => dest.Kilometraje, opt => opt.MapFrom(src => src.Kilometraje));
        // Crear DTO -> Entidad
        CreateMap<CreateVehiculoDto, Vehiculo>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // se generará automáticamente
                dest.Cliente = null!;  // se debe asignar después del fetch del cliente
                dest.Marca = new NombreVO(src.Marca);
                dest.Modelo = new NombreVO(src.Modelo);
                dest.Anio = new AnioVehiculoVO(src.Anio);
                dest.Vin = new VinVO(src.Vin);
                dest.Kilometraje = new KilometrajeVO(src.Kilometraje);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateVehiculoDto, Vehiculo>()
            .AfterMap((src, dest) =>
            {
                dest.Marca = new NombreVO(src.Marca);
                dest.Modelo = new NombreVO(src.Modelo);
                dest.Anio = new AnioVehiculoVO(src.Anio);
                dest.Vin = new VinVO(src.Vin);
                dest.Kilometraje = new KilometrajeVO(src.Kilometraje);
            })
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        // Entidad -> Detail DTO
        CreateMap<Vehiculo, VehiculoDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId.Value))
            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.Value))
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo.Value))
            .ForMember(dest => dest.Anio, opt => opt.MapFrom(src => src.Anio.Value))
            .ForMember(dest => dest.Vin, opt => opt.MapFrom(src => src.Vin.Value))
            .ForMember(dest => dest.Kilometraje, opt => opt.MapFrom(src => src.Kilometraje.Value))
            .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Nombre.Value))
            .ForMember(dest => dest.ClienteCorreo, opt => opt.MapFrom(src => src.Cliente.Correo.Value))
            // Mapeo de colecciones relacionadas usando los DTOs de cada entidad
            .ForMember(dest => dest.Citas, opt => opt.MapFrom(src => src.Citas))
            .ForMember(dest => dest.OrdenesServicio, opt => opt.MapFrom(src => src.OrdenesServicio));
    }
}
