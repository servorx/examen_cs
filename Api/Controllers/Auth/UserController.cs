using System;
using Api.DTOs.Auth;
using Api.Services;
using Api.Services.Interfaces;
using Api.Services.Interfaces.Auth;
using AutoMapper;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Auth;
public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);
        if (!string.IsNullOrEmpty(result.RefreshToken))
        {
            SetRefreshTokenInCookie(result.RefreshToken);
        }
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is missing.");
        }
        var response = await _userService.RefreshTokenAsync(refreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        return Ok(response);
    }
    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    // Obtener por id 
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DataUserDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var user = await _userService.GetByIdAsync(id, ct);
        if (user == null)
            return NotFound("Usuario no encontrado.");

        // Mapear UserMember -> DataUserDto
        var result = _mapper.Map<DataUserDto>(user);
        return Ok(result);
    }
    // obtener todos los usuarios creados
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<DataUserDto>>> GetAllAsync(CancellationToken ct)
    {
        var users = await _userService.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<DataUserDto>>(users);
        return Ok(result);
    }
    
}
