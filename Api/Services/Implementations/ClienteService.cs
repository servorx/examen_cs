using Api.DTOs.Clientes;
using Api.Services.Interfaces;
using Application.Abstractions;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class ClienteService : IClienteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync()
    {
        var clientes = await _unitOfWork.Clientes.GetAllAsync();
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(new IdVO(id));
        if (cliente == null)
            throw new KeyNotFoundException("Cliente no encontrado");

        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> CreateAsync(CreateClienteDto dto)
    {
        // Validación: correo único
        var exists = await _unitOfWork.Clientes.ExistsByEmailAsync(new CorreoVO(dto.Correo));
        if (exists)
            throw new InvalidOperationException("Ya existe un cliente con este correo");

        var cliente = new Cliente
        {
            Id = new IdVO(0),
            Nombre = new NombreVO(dto.Nombre),
            Correo = new CorreoVO(dto.Correo),
            Telefono = new TelefonoVO(dto.Telefono),
            Direccion = new DireccionVO(dto.Direccion),
            IsActive = new EstadoVO(dto.IsActive),
            UserId = dto.UserId
        };

        await _unitOfWork.Clientes.AddAsync(cliente);
        await _unitOfWork.SaveChanges(); // guardar los cambios

        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> UpdateAsync(int id, UpdateClienteDto dto)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(new IdVO(id));
        if (cliente == null)
            throw new KeyNotFoundException("Cliente no encontrado");

        // Actualizar propiedades
        cliente.Nombre = new NombreVO(dto.Nombre);
        cliente.Correo = new CorreoVO(dto.Correo);
        cliente.Telefono = new TelefonoVO(dto.Telefono);
        cliente.Direccion = new DireccionVO(dto.Direccion);
        cliente.IsActive = new EstadoVO(dto.IsActive);

        await _unitOfWork.Clientes.UpdateAsync(cliente);
        await _unitOfWork.SaveChanges();

        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(new IdVO(id));
        if (cliente == null)
            return false;

        await _unitOfWork.Clientes.DeleteAsync(cliente.Id);
        await _unitOfWork.SaveChanges();
        return true;
    }
}
