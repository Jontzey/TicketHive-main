using Newtonsoft.Json;
using TicketHive.Client.Managers;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Pages;

public partial class CheckoutPage
{
    public List<EventModel> MyTickets { get; set; } = new();
    public List<EventModel>? AllEvents { get; set; } = new();
    public decimal TotalCost { get; set; }
    public UserModel? SignedInUser { get; set; } = new();
    public decimal PricePerTicket { get; set; }
    public decimal FinalCost { get; set; }

    protected async override Task OnInitializedAsync()
    {
        AllEvents = await _eventService.GetEventsAsync();

        if (AllEvents != null)
        {
            await CheckShoppingCartContent();
        }

        var authenticationState = await _authentication.GetAuthenticationStateAsync();

        var userId = authenticationState.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        SignedInUser = await _userService.GetUserByIdAsync(userId);

        GetFinalCost();
    }

    /// <summary>
    /// Checks content of shopping cart in local storage and adds to local list.
    /// </summary>
    /// <returns></returns>
    private async Task CheckShoppingCartContent()
    {
        foreach (EventModel eventModel in AllEvents!)
        {
            string jsonCart = await _storage.GetItemAsStringAsync(eventModel.Id.ToString());

            if (!String.IsNullOrWhiteSpace(jsonCart))
            {
                EventModel eventFromShoppingCart = JsonConvert.DeserializeObject<EventModel>(jsonCart);

                MyTickets.Add(eventFromShoppingCart);
            }
        }
    }

    private decimal GetTicketCost(EventModel eventModel)
    {
        return PricePerTicket = CurrencyManager.GetConvertedTicketPrice(SignedInUser!.Country, eventModel.Price);
    }

    private string GetCurrencyLabel()
    {
        return CurrencyManager.GetCurrencyAbbreviation(SignedInUser!.Country);
    }

    private decimal GetTotalCost(decimal price, int numberOfTickets)
    {
        return (decimal)(price * numberOfTickets);
    }

    private async Task ConfirmPurchase()
    {
        foreach (EventModel eventModel in MyTickets)
        {
            await _eventService.BookEventAsync(SignedInUser.Id, eventModel.Id, eventModel.NumberOfTickets);
        }

        await _storage.ClearAsync();

        _navigation.NavigateTo("/confirmed");
    }

    private void GetFinalCost()
    {
        foreach (EventModel eventModel in MyTickets)
        {
            FinalCost += (GetTicketCost(eventModel) * eventModel.NumberOfTickets);
        }
    }

    private void NavigateToEvents()
    {
        _navigation.NavigateTo("/allevents");
    }

    private void ReturnToShoppingCart()
    {
        _navigation.NavigateTo("/cart");
    }
}