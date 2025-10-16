using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Api.DTOs.Recepcionistas;
using Api.Services.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

// el autorizado sirve para que el usuario al realizar la solicitud deba de validar el token
[Authorize(Roles = "Administrador, Recepcionista")]
public class RecepcionistasController : BaseApiController
{
    private readonly IRecepcionistaService _service;
    private readonly IMapper _mapper;

    public RecepcionistasController(IRecepcionistaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<RecepcionistaDto>>> GetAll(CancellationToken ct)
    {
        try
        {
            var recepcionistass = await _service.GetAllAsync(ct);

            if (recepcionistass == null || !recepcionistass.Any())
                return NotFound(new { message = "No se encontraron recepcionistas registrados." });

            var dto = _mapper.Map<IEnumerable<RecepcionistaDto>>(recepcionistass);
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
            return StatusCode(500, new { message = "Ocurrió un error inesperado al obtener los recepcionistas.", detail = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RecepcionistaDto>> GetById(int id, CancellationToken ct)
    {
        try
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser mayor que 0." });

            var admin = await _service.GetByIdAsync(new IdVO(id), ct);

            if (admin is null)
                return NotFound(new { message = $"No se encontró un recepcionista con ID {id}." });

            return Ok(_mapper.Map<RecepcionistaDto>(admin));
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
    public async Task<IActionResult> Create([FromBody] CreateRecepcionistaDto body, CancellationToken ct)
    {
        try
        {
            var recepc = _mapper.Map<Recepcionista>(body);
            var id = await _service.AddAsync(recepc, ct);

            var dto = _mapper.Map<RecepcionistaDto>(recepc);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRecepcionistaDto body, CancellationToken ct)
    {
        var existing = await _service.GetByIdAsync(new IdVO(id), ct);
        if (existing is null) return NotFound();

        // Map parcial con value objects
        if (body.Nombre != null) existing.Nombre = new NombreVO(body.Nombre);
        if (body.Telefono != null) existing.Telefono = new TelefonoVO(body.Telefono);
        if (body.AniosExperiencia != null) existing.AnioExperiencia = new AnioExperienciaVO(body.AniosExperiencia);
        if (body.IsActive.HasValue) existing.IsActive = new EstadoVO(body.IsActive.Value);

        try
        {
            var updated = await _service.UpdateAsync(existing, ct);
            if (!updated) return StatusCode(500, new { message = "Error actualizando recepcionista" });

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
