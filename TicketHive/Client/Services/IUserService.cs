using TicketHive.Server.Enums;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Services;

public interface IUserService
{
	Task<UserModel?> GetUserByIdAsync(string userId);
	Task<bool> UpdateUserCountryAsync(string userId, Country country);
	Task<HttpResponseMessage> UpdateUserPasswordAsync(string userId, string currentPassword, string newPassword);
	Task DeleteUserAsync(string userId);
}
