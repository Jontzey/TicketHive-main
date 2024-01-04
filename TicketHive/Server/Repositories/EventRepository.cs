using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Server.Models;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Repositories;

public class EventRepository : IEventRepository
{
    private readonly MainDbContext _mainDbContext;

    public EventRepository(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    /// <summary>
    /// Adds a user to database 
    /// </summary>
    /// <param name="user"></param>
    public async Task AddUserToEventDb(ApplicationUser user)
    {
        UserModel userModel = new()
        {
            Id = user.Id,
            Username = user.UserName!,
            Country = user.Country
        };

        await _mainDbContext.Users.AddAsync(userModel);

        _mainDbContext.SaveChanges();
    }

    /// <summary>
    /// Deletes an event from the database
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns>Bool true if the event was successfully deleted, false otherwise</returns>
    public async Task<bool> DeleteEventAsync(int eventId)
    {
        var eventToDelete = await GetEventAsync(eventId);

        if (eventToDelete != null)
        {
            _mainDbContext.Events.Remove(eventToDelete);

            await _mainDbContext.SaveChangesAsync();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets event from db with the specified Id
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns>The EventModel with the specified Id, or null if it doesn't exist.</returns>
    public async Task<EventModel?> GetEventAsync(int eventId)
    {
        EventModel? eventModel = await _mainDbContext.Events.Include(e => e.Bookings).FirstOrDefaultAsync(e => e.Id == eventId);

        if (eventModel != null)
        {
            return eventModel;
        }

        return null;
    }

    /// <summary>
    /// Gets list of all events from MainDb
    /// </summary>
    /// <returns>A List of all events, or null if there are no events in MainDb</returns>
    public Task<List<EventModel>?> GetEventsAsync()
    {
        return _mainDbContext.Events?.Include(e => e.Bookings).ToListAsync();
    }

    /// <summary>
    /// Adds an event to the database.
    /// </summary>
    /// <param name="eventModel"></param>
    public async Task<bool> AddEventAsync(EventModel eventModel)
    {
        try
        {
            if (!await _mainDbContext.Events.AnyAsync(e => e.Name == eventModel.Name))
            {
                _mainDbContext.Add(eventModel);
                await _mainDbContext.SaveChangesAsync();

                if (await _mainDbContext.Events.AnyAsync(e => e.Name == eventModel.Name))
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return false;
        }
    }

    /// <summary>
    /// Books an event for a user and updates MainDb 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eventId"></param>
    /// <param name="quantity"></param>
    public async Task BookEventAsync(string userId, int eventId, int quantity)
    {
        EventModel? eventModel = await GetEventAsync(eventId);

        BookingModel bookingModel = new()
        {
            UserId = userId,
            EventId = eventId,
            EventName = eventModel!.Name,
            Quantity = quantity
        };

        UserModel? userModel = await _mainDbContext.Users.Include(u => u.Bookings).FirstOrDefaultAsync(u => u.Id == userId);

        if (eventModel != null && userModel != null && eventModel.NumberOfTickets >= quantity)
        {
            eventModel.Bookings.Add(bookingModel);
            eventModel.NumberOfTickets -= quantity;
            _mainDbContext.Events.Update(eventModel);

            userModel.Bookings.Add(bookingModel);
            _mainDbContext.Users.Update(userModel);

            await _mainDbContext.SaveChangesAsync();
        }
    }
}
