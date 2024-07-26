using System;
using System.Threading.Tasks;
using EventManagerAPI.Dtos;
using EventManagerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EventManagerAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var ev = await _eventService.GetEventById(id);
            if (ev == null) return NotFound();

            return Ok(ev);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEvent(EventCreateDto eventCreateDto)
        {
            var ev = await _eventService.AddEvent(eventCreateDto);
            return CreatedAtAction(nameof(GetEventById), new { id = ev.Id }, ev);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(int id, EventUpdateDto eventUpdateDto)
        {
            try
            {
                await _eventService.UpdateEvent(id, eventUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                await _eventService.DeleteEvent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchEvents([FromQuery] string name, [FromQuery] DateTime? date, [FromQuery] string location)
        {
            var events = await _eventService.SearchEvents(name, date, location);
            return Ok(events);
        }
    }
}

