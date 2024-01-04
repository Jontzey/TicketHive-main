using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using TicketHive.Client;
using TicketHive.Client.Shared;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using TicketHive.Client.Services;
using TicketHive.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using TicketHive.Client.Managers;

namespace TicketHive.Client.Pages
{
    [BindProperties]
    public partial class ShowEventsSingle
    {
        [Parameter]
        public int Id { get; set; }
        private int DesiredNoOfTickets { get; set; }
        private EventModel? EventToDisplay { get; set; } = new();
        private decimal Price { get; set; }
        private string? CurrencyCode { get; set; }
        private bool IsShowingModal { get; set; }
        private bool IsShowingModalAddToCart { get; set; }
        private UserModel? SignedInUser { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            EventToDisplay = await GetEventToDisplay();

            SignedInUser = await GetSignedInUser();

            if(SignedInUser != null)
            {
                Price = CurrencyManager.GetConvertedTicketPrice(SignedInUser.Country, EventToDisplay.Price);
                CurrencyCode = CurrencyManager.GetCurrencyAbbreviation(SignedInUser.Country);
            }
        }

        private async Task<UserModel?> GetSignedInUser()
        {
            var authenticationState = await authentication.GetAuthenticationStateAsync();

            var userId = authenticationState.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if(userId != null)
            {
                return await userService.GetUserByIdAsync(userId!);
            }

            return null;
        }

        private async Task<EventModel> GetEventToDisplay()
        {
            return await eventService.GetEventAsync(Id);
        }
            
        public async Task AddToCart()
        {
            if(EventToDisplay != null)
            {
                EventToDisplay.NumberOfTickets = DesiredNoOfTickets;

                string jsonEvent = JsonConvert.SerializeObject(EventToDisplay);

                await localStorage.SetItemAsStringAsync(EventToDisplay.Id.ToString(), jsonEvent);
                
                IsShowingModalAddToCart= true;
            }
        }

        public void ShowModal()
        {
            IsShowingModal = true;
            StateHasChanged();
        }

        public async Task CloseModal()
        {
            EventToDisplay = await GetEventToDisplay();
            SignedInUser = await GetSignedInUser();

            IsShowingModal = false;
            IsShowingModalAddToCart = false;
            StateHasChanged();
        }

        public async Task DeleteEvent(EventModel eventModel)
        {
            await eventService.DeleteEventAsync(eventModel.Id);

            IsShowingModal = false;

            navigationManager.NavigateTo("/allevents");
        }

        private void NavigateToAllEvents()
        {
            IsShowingModal = false;
            navigationManager.NavigateTo("/allevents");
        }

        private void NavigateToCart()
        {
            
            navigationManager.NavigateTo("/cart");
        }
    }
}