using Newtonsoft.Json;
using System.Net.Http.Json;
using TicketHive.Server.Enums;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets the user with the specified Id 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>
    /// Returns a Task that contains the retrieved UserModel if the operation succeeds, Otherwise returns null.
    /// </returns>
    public async Task<UserModel?> GetUserByIdAsync(string userId)
    {
        var response = await _client.GetAsync($"api/users/{userId}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserModel>(json);
        }

        return null;
    }

    /// <summary>
    /// Updates country of the user with the specified Id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="country">.</param>
    /// <returns>
    /// Returns a Task that contains a boolean indicating whether the operation succeeded or not. 
    /// </returns>
    public async Task<bool> UpdateUserCountryAsync(string userId, Country country)
    {
        var signedInUserBefore = await GetUserByIdAsync(userId);
        Country countryBefore = signedInUserBefore!.Country;

        await _client.PutAsJsonAsync($"api/users/{userId}/{country}", country);

        var signedInUserAfter = await GetUserByIdAsync(userId);
        Country countryAfter = signedInUserAfter!.Country;

        if (countryBefore != countryAfter)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Updates the password of the user with the specified Id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="currentPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns>
    /// Returns a Task that contains a HttpResponseMessage containing the API response 
    /// </returns>
    public async Task<HttpResponseMessage> UpdateUserPasswordAsync(string userId, string currentPassword, string newPassword)
    {
        string[] passwordStrings = new string[2] { currentPassword, newPassword };

        return await _client.PutAsJsonAsync($"api/users/{userId}", passwordStrings);
    }

    /// <summary>
    /// Deletes the user with the specified Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>
    /// Returns a Task that represents the asynchronous operation.
    /// </returns>
    public async Task DeleteUserAsync(string userId)
    {
        var response = await _client.DeleteAsync($"api/users/{userId}");
    }
}
