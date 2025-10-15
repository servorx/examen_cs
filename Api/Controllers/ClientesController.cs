using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.DTOs.Clientes;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ClientesController : BaseApiController
{
    private readonly IClienteService _service;
    private readonly IMapper _mapper;

    public ClientesController(IClienteService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET /api/clientes/all
    [HttpGet("all")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll(CancellationToken ct)
    {
        var clientes = await _service.GetAllAsync();
        return Ok(clientes);
    }

    // GET /api/clientes/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<ClienteDto>> GetById(int id)
    {
        try
        {
            var cliente = await _service.GetByIdAsync(id);
            return Ok(cliente);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST /api/clientes
    [HttpPost]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult> Create([FromBody] CreateClienteDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // PUT /api/clientes/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateClienteDto dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // DELETE /api/clientes/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"No se encontró el cliente con ID {id}" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
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
