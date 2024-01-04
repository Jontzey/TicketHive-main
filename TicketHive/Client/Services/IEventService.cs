using TicketHive.Shared.Models;

namespace TicketHive.Client.Services;

public interface IEventService
{
    Task<EventModel?> GetEventAsync(int eventId);
    Task<List<EventModel?>> GetEventsAsync();
    Task<EventModel> AddEventAsync(EventModel eventModel);
    Task<bool> DeleteEventAsync(int eventId);
    Task<bool> BookEventAsync(string userId, int eventId, int quantity);
}
