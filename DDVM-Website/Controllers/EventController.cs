using DDVM_Website.DTOs;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDVM_Website.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // GET: api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
                return NotFound(new { message = "Event not found" });

            return Ok(eventItem);
        }

        // POST: api/events
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newEvent = await _eventService.CreateEventAsync(eventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.EventId }, newEvent);
        }

        // PUT: api/events/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedEvent = await _eventService.UpdateEventAsync(id, eventDto);
            if (updatedEvent == null)
                return NotFound(new { message = "Event not found" });

            return Ok(updatedEvent);
        }

        // DELETE: api/events/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var isDeleted = await _eventService.DeleteEventAsync(id);
            if (!isDeleted)
                return NotFound(new { message = "Event not found" });

            return Ok(new { message = "Event deleted successfully" });
        }
    }
}
