using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        try
        {
            await _userService.RegisterAsync(dto);
            return Ok("İstifadəçi uğurla yaradıldı");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        try
        {
            var token = await _userService.LoginAsync(dto.Email, dto.Password);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> EditProfile(EditProfileDto dto)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            await _userService.EditProfileAsync(userId, dto);
            return Ok("Profil uğurla yeniləndi");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            await _userService.ChangePasswordAsync(userId, dto);
            return Ok("Parol uğurla dəyişdirildi");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteProfile()
    {
        try
        {
            var userId = GetUserIdFromClaims();
            await _userService.DeleteProfileAsync(userId);
            return Ok("Profil uğurla silindi");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private Guid GetUserIdFromClaims()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (userIdClaim == null) throw new Exception("İstifadəçi identifikatoru nişanda tapılmadı");
        return Guid.Parse(userIdClaim);
    }
}