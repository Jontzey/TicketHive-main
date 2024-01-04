using TicketHive.Shared.Models;

namespace TicketHive.Client.Pages
{
    public partial class AdminPage
    {
        private string? ResponseMessage { get; set; }
        public DateTime StartHoursAndMinutes { get; set; } = new();
        public DateTime EndHoursAndMinutes { get; set; } = new();

        private EventModel newEvent = new()
        {
            StartTime = DateTime.Now,
            EndTime = DateTime.Now
        };

        private async Task AddEvent()
        {
            try
            {
                newEvent.ImageUrl = $"image {new Random().Next(1, 27)}.png";

                newEvent.StartTime = new DateTime(newEvent.StartTime.Year,
                                    newEvent.StartTime.Month,
                                    newEvent.StartTime.Day,
                                    StartHoursAndMinutes.Hour,
                                    StartHoursAndMinutes.Minute,
                                    newEvent.StartTime.Second);

                newEvent.EndTime = new DateTime(newEvent.EndTime.Year,
                                  newEvent.EndTime.Month,
                                  newEvent.EndTime.Day,
                                  EndHoursAndMinutes.Hour,
                                  EndHoursAndMinutes.Minute,
                                  newEvent.EndTime.Second);

                if (await eventService.AddEventAsync(newEvent) != null)
                {
                    ResponseMessage = "The event has been successfully added!";

                    StateHasChanged();
                }
                else
                {
                    ResponseMessage = "The provided event name is not available. Please try again.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}