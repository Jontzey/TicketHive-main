using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Server.Enums;
using TicketHive.Server.Models;
using TicketHive.Server.Repository;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly MainDbContext _mainDbcontext;

    public UserRepository(SignInManager<ApplicationUser> signInManager, MainDbContext mainDbcontext)
    {
        _signInManager = signInManager;
        _mainDbcontext = mainDbcontext;
    }

    /// <summary>
    /// Gets user with the given user Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>User with the given user Id, or null if it doesn't exist</returns>
    public async Task<UserModel?> GetMainUserByIdAsync(string userId)
    {
        ApplicationUser? applicationUser = await _signInManager.UserManager.FindByIdAsync(userId);

        if (applicationUser != null)
        {
            UserModel? mainUser = await _mainDbcontext.Users.Include(b => b.Bookings).FirstOrDefaultAsync(u => u.Id == applicationUser.Id);

            return mainUser;
        }

        return null;
    }

    /// <summary>
    /// Registers user with the given username, password, and country.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="country"></param>
    /// <returns>Returns a Task containing the IdentityResult</returns>
    public async Task<IdentityResult> RegisterUserAsync(string username, string password, Country country)
    {
        ApplicationUser newUser = new()
        {
            UserName = username,
            Country = country
        };

        return await _signInManager.UserManager.CreateAsync(newUser, password!);
    }

    /// <summary>
    /// Signs in a user with the given username and password 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>Returns a Task containing the SignInResult</returns>
    public async Task<SignInResult> SignInUserAsync(string username, string password)
    {
        return await _signInManager.PasswordSignInAsync(username, password, false, false);
    }

    /// <summary>
    /// Gets user with the given username from IdentityDb 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>The user with the given username, or null if not found</returns>
    public async Task<ApplicationUser?> GetApplicationUserByName(string userName)
    {
        return await _signInManager.UserManager.FindByNameAsync(userName);
    }

    /// <summary>
    /// Changes the country of the user with the given user Id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="country"></param>
    public async Task ChangeCountryAsync(string userId, Country country)
    {
        ApplicationUser? applicationUser = await _signInManager.UserManager.FindByIdAsync(userId);
        UserModel? mainUser = await _mainDbcontext.Users.Include(b => b.Bookings).FirstOrDefaultAsync(u => u.Id == userId);

        if (mainUser != null && applicationUser != null)
        {
            mainUser.Country = country;
            _mainDbcontext.Update(mainUser);
            await _mainDbcontext.SaveChangesAsync();

            applicationUser.Country = country;
            await _signInManager.UserManager.UpdateAsync(applicationUser);
        }
    }

    /// <summary>
    /// Changes the password of the user with the given Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="currentPassword"></param>
    /// <param name="newPassword"></param>
    public async Task ChangePasswordAsync(string id, string currentPassword, string newPassword)
    {
        ApplicationUser? user = await _signInManager.UserManager.FindByIdAsync(id);

        if (user != null)
        {
            await _signInManager.UserManager.ChangePasswordAsync(user, currentPassword, newPassword);
            await _signInManager.UserManager.UpdateAsync(user);
        }
    }

    /// <summary>
    /// Deletes a user from MainDb and IdentityDb
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Task that represents the deletion of the user</returns>
    public async Task DeleteUserAsync(string id)
    {
        ApplicationUser? applicationUser = await _signInManager.UserManager.FindByIdAsync(id);
        UserModel? mainUser = await _mainDbcontext.Users.Include(b => b.Bookings).FirstOrDefaultAsync(u => u.Id == id);

        if (mainUser != null && applicationUser != null)
        {
            _mainDbcontext.Remove(mainUser);
            await _mainDbcontext.SaveChangesAsync();

            await _signInManager.UserManager.DeleteAsync(applicationUser);
        }
    }

    /// <summary>
    /// Checks if username is available 
    /// </summary>
    /// <param name="username"></param>
    /// <returns>A task that contains a bool that is true if the username is available, otherwise false.</returns>
    public async Task<bool> CheckUsernameAvailability(string username)
    {
        ApplicationUser? user = await _signInManager.UserManager.FindByNameAsync(username);

        if (user == null)
        {
            return true;
        }

        return false;
    }
}
