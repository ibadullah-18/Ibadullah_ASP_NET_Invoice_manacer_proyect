using Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var exists = await _context.Users.AnyAsync(x => x.Email == dto.Email);

        if (exists)
            throw new Exception("E-poçt artıq mövcuddur");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            Email = dto.Email,
            Password = dto.Password, 
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null || user.Password != password)
            throw new Exception("E-poçt və ya parol səhvdir");

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            }),
            Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["ExpiresInHours"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task EditProfileAsync(Guid userId, EditProfileDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new Exception("User tapılmadı");

        user.Name = dto.Name;
        user.Address = dto.Address;
        user.PhoneNumber = dto.PhoneNumber;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new Exception("User tapılmadı");

        if (user.Password != dto.OldPassword)
            throw new Exception("Köhnə parol səhvdir");

        user.Password = dto.NewPassword;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProfileAsync(Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new Exception("User tapılmadı");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}