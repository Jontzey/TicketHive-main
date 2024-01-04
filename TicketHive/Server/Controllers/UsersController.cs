using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Enums;
using TicketHive.Server.Repository;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Get user with specified Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User with the specified Id, or null if user not found.</returns>
        [HttpGet("{id}")]
        public async Task<UserModel?> GetAsync(string id)
        {
            return await userRepository.GetMainUserByIdAsync(id);
        }

        /// <summary>
        /// Updates the password for the user with the specified Id. passwordStrings contains old and new password.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordStrings"></param>
        [HttpPut("{id}")]
        public async Task UpdateUserPasswordAsync(string id, string[] passwordStrings)
        {
            await userRepository.ChangePasswordAsync(id, passwordStrings[0], passwordStrings[1]);
        }

        /// <summary>
        /// Updates the country for the user with the specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="country"></param>
        [HttpPut("{id}/{country}")]
        public async Task UpdateUserCountryAsync(string id, Country country)
        {
            await userRepository.ChangeCountryAsync(id, country);
        }

        /// <summary>
        /// Deletes the user with the specified Id from MainDb
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task DeleteUserAsync(string id)
        {
            await userRepository.DeleteUserAsync(id);
        }
    }
}
