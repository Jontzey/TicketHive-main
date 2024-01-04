using Newtonsoft.Json;
using TicketHive.Client.Managers;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Pages
{
    public partial class ShowCart
    {
        public decimal TotalPrice { get; set; }
        public List<EventModel?> ShoppingCart { get; set; } = new();
        public List<EventModel?> AllEvents { get; set; } = new();
        public UserModel? SignedInUser { get; set; } = new();
        public decimal PricePerTicket { get; set; }

        protected async override Task OnInitializedAsync()
        {
            AllEvents = await _eventService.GetEventsAsync();

            if (AllEvents != null)
            {
                await CheckShoppingCartContent();
            }

            SignedInUser = await GetSignedInUser();
        }

        private async Task CheckShoppingCartContent()
        {
            ShoppingCart.Clear();

            foreach (EventModel? eventModel in AllEvents)
            {
                string jsonCart = await _localStorage.GetItemAsStringAsync(eventModel.Id.ToString());

                if (!String.IsNullOrWhiteSpace(jsonCart))
                {
                    EventModel eventFromShoppingCart = JsonConvert.DeserializeObject<EventModel>(jsonCart);

                    ShoppingCart.Add(eventFromShoppingCart);
                }
            }
        }

        private async Task<UserModel?> GetSignedInUser()
        {
            var authenticationState = await _authentication.GetAuthenticationStateAsync();

            var userId = authenticationState.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (userId != null)
            {
                return await _userService.GetUserByIdAsync(userId!);
            }

            return null;
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


        private bool AreThereAvailableTickets(EventModel eventModel)
        {
            EventModel? eventToCheck = AllEvents.Find(e => e.Id.Equals(eventModel.Id));

            if ((eventToCheck.NumberOfTickets - eventModel.NumberOfTickets) == 0)
            {
                return true;
            }

            return false;
        }

        private async void IncrementTickets(EventModel eventModel)
        {
            eventModel.NumberOfTickets++;

            string jsonEvent = JsonConvert.SerializeObject(eventModel);

            await _localStorage.SetItemAsStringAsync(eventModel.Id.ToString(), jsonEvent);

            await CheckShoppingCartContent();

            StateHasChanged();
        }

        private async Task DecrementTickets(EventModel eventModel)
        {
            eventModel.NumberOfTickets--;

            if (eventModel.NumberOfTickets >= 1)
            {
                string jsonEvent = JsonConvert.SerializeObject(eventModel);

                await _localStorage.SetItemAsStringAsync(eventModel.Id.ToString(), jsonEvent);

                await CheckShoppingCartContent();

                StateHasChanged();
            }
            else
            {
                RemoveEvent(eventModel);
            }
        }

        private async void RemoveEvent(EventModel eventToRemove)
        {
            if (eventToRemove != null)
            {
                await _localStorage.RemoveItemAsync(eventToRemove.Id.ToString());
            }

            await CheckShoppingCartContent();

            StateHasChanged();
        }

        private void PurchaseTickets()
        {
            _navigation.NavigateTo("/checkout");
        }

        private void NavigateToHome()
        {
            _navigation.NavigateTo("");
        }
    }
}