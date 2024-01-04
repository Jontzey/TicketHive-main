using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Repositories;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        /// <summary>
        /// Gets a list of all events in MainDb
        /// </summary>
        [HttpGet]
        public async Task<List<EventModel>?> GetEventsAsync()
        {
            return await eventRepository.GetEventsAsync();
        }

        /// <summary>
        /// Gets event by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The event with the specified Id</returns>
        [HttpGet("{id}")]
        public async Task<EventModel?> GetByIdAsync(int id)
        {
            return await eventRepository.GetEventAsync(id);
        }

        /// <summary>
        /// Adds new event to MainDb
        /// </summary>
        /// <param name="eventModel"></param>
        [HttpPost]
        public async Task<ActionResult<EventModel>> AddEventAsync([FromBody] EventModel eventModel)
        {
            var eventWasAdded = await eventRepository.AddEventAsync(eventModel);

            if (eventWasAdded)
            {
                return Ok(eventModel);
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes event from MainDb
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An HTTP response indicating if delete was successful or not</returns>  
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteEventAsync(int id)
        {
            bool isSuccessfulDelete = await eventRepository.DeleteEventAsync(id);

            if (isSuccessfulDelete)
            {
                string successMessage = "Event was successfully removed from the database";
                return Ok(successMessage);
            }

            return BadRequest();
        }

        /// <summary>
        /// Books event for user with the specified quantity (tickets). parameters contains the quantity and eventId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parameters"></param>
        [HttpPost("{userId}/{parameters}")]
        public async Task BookEventAsync(string userId, string[] parameters)
        {
            int eventId = int.Parse(parameters[0]);
            int quantity = int.Parse(parameters[1]);

            await eventRepository.BookEventAsync(userId, eventId, quantity);
        }
    }
}
