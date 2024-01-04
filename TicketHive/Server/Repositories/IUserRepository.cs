using Microsoft.AspNetCore.Identity;
using TicketHive.Server.Enums;
using TicketHive.Server.Models;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Repository;

public interface IUserRepository
{
    Task<UserModel?> GetMainUserByIdAsync(string userId);
    Task<SignInResult> SignInUserAsync(string username, string password);
    Task<IdentityResult> RegisterUserAsync(string username, string password, Country country);
    Task ChangePasswordAsync(string id, string currentPassword, string newPassword);
    Task ChangeCountryAsync(string id, Country country);
    Task<ApplicationUser> GetApplicationUserByName(string userName);
    Task DeleteUserAsync(string id);
    Task<bool> CheckUsernameAvailability(string username);
}
