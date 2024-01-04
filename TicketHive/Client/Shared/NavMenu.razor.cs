using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Shared;

public partial class NavMenu
{
    private bool collapseNavMenu = true;
    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;


    private void BeginLogOut()
    {
        _navigation.NavigateToLogout("authentication/logout", "");
    }

    private async Task NavigateToSettings()
    {
        var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();

        var userId = authenticationState.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        UserModel? user = await _service.GetUserByIdAsync(userId);


        if (user != null)
        {
            _navigation.NavigateTo($"/settings/{user.Id}");
        }
        else
        {
            // Display some error message...
        }
    }

    private async Task<bool> IsUserInRole(string role)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.IsInRole(role);
    }

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    private void NavigateToHome()
    {
        _navigation.NavigateTo("");
    }
    private void NavigateToAllEvents()
    {
        _navigation.NavigateTo("/allevents");
    }
    private void NavigateToShoppingCart()
    {
        _navigation.NavigateTo("/cart");
    }

    private void NavigateToAdmin()
    {
        _navigation.NavigateTo("/admin");
    }
}
