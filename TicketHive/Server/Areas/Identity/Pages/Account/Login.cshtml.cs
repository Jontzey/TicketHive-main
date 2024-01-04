using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Repository;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TicketHive.Server.Areas.Identity.Pages.Account
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        [Required(ErrorMessage = "Please input your username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please input your password")]
        public string? Password { get; set; }

        public string? InvalidCredentials { get; set; }

        public LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                SignInResult signInResult = await _userRepository.SignInUserAsync(Username!, Password!);

                if (signInResult.Succeeded)
                {
                    InvalidCredentials = null;

                    return Redirect("~/");
                }
                else
                {
                    InvalidCredentials = "Username or password was incorrect, please try again.";
                }
            }

            return Page();
        }
    }
}
