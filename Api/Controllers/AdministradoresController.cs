using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Api.DTOs.Administradores;
using Api.Services.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

// el autorizado sirve para que el usuario al realizar la solicitud deba de validar el token
[Authorize(Roles = "Administrador")]
public class AdministradoresController : BaseApiController
{
    private readonly IAdministradorService _service;
    private readonly IMapper _mapper;

    public AdministradoresController(IAdministradorService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<AdministradorDto>>> GetAll(CancellationToken ct)
    {
        try
        {
            var admins = await _service.GetAllAsync(ct);

            if (admins == null || !admins.Any())
                return NotFound(new { message = "No se encontraron administradores registrados." });

            var dto = _mapper.Map<IEnumerable<AdministradorDto>>(admins);
            return Ok(dto);
        }
        catch (OperationCanceledException)
        {
            // Cuando se cancela la solicitud (por ejemplo, timeout o cancelación manual)
            return StatusCode(499, new { message = "La operación fue cancelada por el cliente." });
        }
        catch (DbUpdateException ex)
        {
            // Errores relacionados con la base de datos
            return StatusCode(500, new { message = "Error al consultar los datos en la base de datos.", detail = ex.InnerException?.Message });
        }
        catch (Exception ex)
        {
            // Errores inesperados
            return StatusCode(500, new { message = "Ocurrió un error inesperado al obtener los administradores.", detail = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AdministradorDto>> GetById(int id, CancellationToken ct)
    {
        try
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser mayor que 0." });

            var admin = await _service.GetByIdAsync(new IdVO(id), ct);

            if (admin is null)
                return NotFound(new { message = $"No se encontró un administrador con ID {id}." });

            return Ok(_mapper.Map<AdministradorDto>(admin));
        }
        catch (ArgumentException ex)
        {
            // Errores de argumentos inválidos, por ejemplo en el Value Object IdVO
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            // Cuando un repositorio o servicio lanza que no existe el registro
            return NotFound(new { message = ex.Message });
        }
        catch (DbUpdateException ex)
        {
            // Errores de base de datos
            return StatusCode(500, new { message = "Error al acceder a la base de datos.", detail = ex.InnerException?.Message });
        }
        catch (Exception ex)
        {
            // Error inesperado
            return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAdministradorDto body, CancellationToken ct)
    {
        try
        {
            var admin = _mapper.Map<Administrador>(body);
            var id = await _service.AddAsync(admin, ct);

            var dto = _mapper.Map<AdministradorDto>(admin);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAdministradorDto body, CancellationToken ct)
    {
        var existing = await _service.GetByIdAsync(new IdVO(id), ct);
        if (existing is null) return NotFound();

        // Map parcial con value objects
        if (body.Nombre != null) existing.Nombre = new NombreVO(body.Nombre);
        if (body.Telefono != null) existing.Telefono = new TelefonoVO(body.Telefono);
        if (body.NivelAcceso != null) existing.NivelAcceso = new NivelAccesoVO(body.NivelAcceso);
        if (body.AreaResponsabilidad != null) existing.AreaResponsabilidad = new DescripcionVO(body.AreaResponsabilidad);
        if (body.IsActive.HasValue) existing.IsActive = new EstadoVO(body.IsActive.Value);

        try
        {
            var updated = await _service.UpdateAsync(existing, ct);
            if (!updated) return StatusCode(500, new { message = "Error actualizando administrador" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(new IdVO(id), ct);
        if (!deleted) return NotFound();

        return NoContent();
    }

    [HttpGet("nivel/{nivel}")]
    public async Task<ActionResult<IEnumerable<AdministradorDto>>> GetByNivelAcceso(string nivel, CancellationToken ct)
    {
        try
        {
            var admins = await _service.GetAllAsync(ct);
            var filtered = admins
                .Where(a => a.NivelAcceso.Value.Equals(nivel, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filtered.Count == 0)
                return NotFound(new { message = $"No se encontraron administradores con nivel de acceso '{nivel}'." });

            return Ok(_mapper.Map<IEnumerable<AdministradorDto>>(filtered));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los administradores por nivel de acceso.", detail = ex.Message });
        }
    }
    // esto es un enpoint de prueba para probar el JWT
    [Authorize]
    [HttpGet("check-auth")]
    public IActionResult CheckAuth()
    {
        return Ok(new
        {
            message = " Token válido y autenticado.",
            user = User.Identity?.Name,
            claims = User.Claims.Select(c => new { c.Type, c.Value })
        });
    }
}
