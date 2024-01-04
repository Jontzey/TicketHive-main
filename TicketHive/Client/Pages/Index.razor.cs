using TicketHive.Shared.Models;

namespace TicketHive.Client.Pages;

public partial class Index
{
    public string? UserName { get; set; }
    public string? SignedInUsersId { get; set; }
    public List<BookingModel>? SignedInUsersBookings { get; set; } = new();
    public List<EventModel>? AllEventsInDb { get; set; } = new();
    public List<EventModel> AllEventsButBooked { get; set; } = new();
    public List<int> uniqueNumbers { get; set; } = new();
    public List<EventModel> RandomEvents { get; set; } = new();
    private int ActiveIndex { get; set; }
    public int ActiveEventId { get; set; }

    /// <summary>
    /// Checks if user is authenticated and if so, gets the user.
    /// Then populates a list of all events in the database, a list of 
    /// all available events the user has not booked, and a list 
    /// of 5 random events from the available events.
    /// Finally makes a call to the Currency API.
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await authentication.GetAuthenticationStateAsync();

        var user = authenticationState.User;

        // Check if user is authenticated and if so, get the user
        if (user.Identity.IsAuthenticated)
        {
            UserName = user.Identity.Name;

            SignedInUsersId = authenticationState.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            UserModel? userModel = await userService.GetUserByIdAsync(SignedInUsersId);

            // If user is authenticated, get the user's bookings and populates a list of all events in the database, 
            // and a list of all available events the user has not booked
            if (userModel != null)
            {
                SignedInUsersBookings = userModel.Bookings;

                AllEventsInDb = await eventService.GetEventsAsync();

                foreach (EventModel eventModel in AllEventsInDb)
                {
                    if (!SignedInUsersBookings.Any(b => b.EventId == eventModel.Id))
                    {
                        AllEventsButBooked.Add(eventModel);
                    }
                }


                // Populates a list of 5 unique random numbers from the total number of available events
                Random random = new Random();

                while (uniqueNumbers.Count < 5)
                {
                    int randomIndex = random.Next(AllEventsButBooked.Count);

                    if (!uniqueNumbers.Contains(randomIndex))
                    {
                        uniqueNumbers.Add(randomIndex);
                    }
                }

                // Populates a list of 5 random events from the available events based on the unique random numbers
                foreach (int number in uniqueNumbers)
                {
                    EventModel randomEvent = AllEventsButBooked[number];

                    RandomEvents.Add(randomEvent);
                }
            }
        }

        //CurrencyManager.CurrencyApiCall();
    }

    private void Login()
    {
        navigationManager.NavigateTo("authentication/login");
    }

    private void Register()
    {
        navigationManager.NavigateTo("authentication/register");
    }

    private void NavigateToEvent(int eventId)
    {
        navigationManager.NavigateTo($"/allEvents/{eventId}");
    }

    // Carousel methods for random events 
    private void NavigateCarousel(int direction)
    {
        ActiveIndex += direction;

        if (ActiveIndex < 0)
        {
            ActiveIndex = RandomEvents.Count - 1;
        }
        else if (ActiveIndex >= RandomEvents.Count)
        {
            ActiveIndex = 0;
        }

        ActiveEventId = RandomEvents[ActiveIndex].Id;
    }
}
