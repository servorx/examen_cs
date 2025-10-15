using Api.DTOs.Proveedores;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProveedoresController : BaseApiController
{
    private readonly IProveedorService _service;
    private readonly IMapper _mapper;

    public ProveedoresController(IProveedorService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    //  GET: api/Proveedor/all
    [HttpGet("all")]
    [Authorize(Roles = "Proveedor, Administrador")]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetAllAsync(CancellationToken ct)
    {
        var proveedores = await _service.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<ProveedorDto>>(proveedores);
        return Ok(result);
    }

    //  GET: api/Proveedor/activos
    [HttpGet("activos")]
    [Authorize(Roles = "Proveedor, Administrador")]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetActivosAsync(CancellationToken ct)
    {
        var proveedores = await _service.GetActivosAsync(ct);
        var result = _mapper.Map<IEnumerable<ProveedorDto>>(proveedores);
        return Ok(result);
    }

    //  GET: api/Proveedor/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Proveedor, Administrador")]
    public async Task<ActionResult<ProveedorDetailDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var proveedor = await _service.GetByIdAsync(new IdVO(id), ct);
        if (proveedor == null)
            return NotFound("Proveedor no encontrado.");

        var result = _mapper.Map<ProveedorDetailDto>(proveedor);
        return Ok(result);
    }

    //  POST: api/Proveedor
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<ProveedorDto>> CreateAsync([FromBody] CreateProveedorDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        var proveedor = new Proveedor
        {
            Id = IdVO.CreateNew(),
            Nombre = new NombreVO(dto.Nombre),
            Telefono = string.IsNullOrEmpty(dto.Telefono) ? null : new TelefonoVO(dto.Telefono),
            Correo = string.IsNullOrEmpty(dto.Correo) ? null : new CorreoVO(dto.Correo),
            Direccion = string.IsNullOrEmpty(dto.Direccion) ? null : new DireccionVO(dto.Direccion),
            IsActive = new EstadoVO(dto.IsActive),
            UserId = dto.UserId
        };

        try
        {
            await _service.AddAsync(proveedor, ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var result = _mapper.Map<ProveedorDto>(proveedor);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = proveedor.Id.Value }, result);
    }

    //  PUT: api/Proveedor/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateProveedorDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        try
        {
            var existing = await _service.GetByIdAsync(new IdVO(id), ct);
            if (existing == null)
                return NotFound("Proveedor no encontrado.");

            existing.Nombre = new NombreVO(dto.Nombre);
            existing.Telefono = string.IsNullOrEmpty(dto.Telefono) ? null : new TelefonoVO(dto.Telefono);
            existing.Correo = string.IsNullOrEmpty(dto.Correo) ? null : new CorreoVO(dto.Correo);
            existing.Direccion = string.IsNullOrEmpty(dto.Direccion) ? null : new DireccionVO(dto.Direccion);
            existing.IsActive = new EstadoVO(dto.IsActive);
            existing.UserId = dto.UserId;

            var updated = await _service.UpdateAsync(existing, ct);
            if (!updated)
                return BadRequest("No se pudo actualizar el proveedor.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //  DELETE: api/Proveedor/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        try
        {
            var deleted = await _service.DeleteAsync(new IdVO(id), ct);
            if (!deleted)
                return NotFound("Proveedor no encontrado.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
