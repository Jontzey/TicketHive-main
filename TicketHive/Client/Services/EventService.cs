using Newtonsoft.Json;
using System.Net.Http.Json;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Services;

public class EventService : IEventService
{
    private readonly HttpClient _client;

    public EventService(HttpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets the event with the specified Id from MainDb. 
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns>
    /// Returns a Task that contains the retrieved EventModel if the operation succeeds. Otherwise, returns null.
    /// </returns>
    public async Task<EventModel?> GetEventAsync(int eventId)
    {
        var response = await _client.GetAsync($"api/Events/{eventId}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EventModel>(json);
        }
        else
        {
            Console.WriteLine(response.Content);
        }

        return null;
    }

    /// <summary>
    /// Gets a list of all events from the MainDb.
    /// </summary>
    /// <returns>
    /// Returns a Task that contains a List of EventModel objects if it succeeds. Returns null otherwise. 
    /// </returns>
    public async Task<List<EventModel?>> GetEventsAsync()
    {
        var response = await _client.GetAsync("api/Events");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<EventModel>>(json);
        }
        else
        {
            Console.WriteLine(response.Content);
        }

        return null;
    }

    /// <summary>
    /// Adds new event to MainDb.
    /// </summary>
    /// <param name="eventModel"></param>
    /// <returns>
    /// Returns a Task that contains a bool indicating whether the operation succeeded or not.
    /// </returns>
    public async Task<EventModel> AddEventAsync(EventModel eventModel)
    {
        var response = await _client.PostAsJsonAsync("api/Events", eventModel);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EventModel>(json);
        }

        Console.WriteLine(response.Content);
        return null;
    }

    /// <summary>
    /// Deletes event with the specified ID
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns>
    /// Returns a Task that with a boolean indicating if it succeeded or not.
    /// </returns>
    public async Task<bool> DeleteEventAsync(int eventId)
    {
        var response = await _client.DeleteAsync($"api/Events/{eventId}");

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            Console.WriteLine(response.Content);
            return false;
        }
    }

    /// <summary>
    /// Books event with the specified ID and quantity for the specified user Id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eventId"></param>
    /// <param name="quantity"></param>
    /// <returns>
    /// Returns a Task that contains a boolean indicating whether the operation succeeded or not. 
    /// </returns>
    public async Task<bool> BookEventAsync(string userId, int eventId, int quantity)
    {
        EventModel? eventBefore = await GetEventAsync(eventId);
        int numberOfBookingsBefore = eventBefore!.Bookings.Count;

        string[] parameters = new string[2] { eventId.ToString(), quantity.ToString() };

        await _client.PostAsJsonAsync($"api/Events/{userId}/{parameters}", parameters);

        EventModel? eventAfter = await GetEventAsync(eventId);
        int numberOfBookingsAfter = eventAfter!.Bookings.Count;

        if (numberOfBookingsAfter > numberOfBookingsBefore)
        {
            return true;
        }

        return false;
    }
}

