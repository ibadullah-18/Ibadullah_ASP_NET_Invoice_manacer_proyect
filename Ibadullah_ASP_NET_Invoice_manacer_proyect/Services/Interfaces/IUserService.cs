using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;

public interface IUserService
{
    Task RegisterAsync(RegisterDto dto);

    Task<string> LoginAsync(string email, string password);

    Task EditProfileAsync(Guid userId, EditProfileDto dto);

    Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto);

    Task DeleteProfileAsync(Guid userId);
}