using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagerAPI.Data;
using EventManagerAPI.Dtos;
using EventManagerAPI.Models.Entities;
using EventManagerAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event> AddEvent(EventCreateDto eventCreateDto)
        {
            var ev = new Event
            {
                Name = eventCreateDto.Name,
                Date = eventCreateDto.Date,
                Location = eventCreateDto.Location,
                Description = eventCreateDto.Description,
                Organizer = eventCreateDto.Organizer
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return ev;
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task UpdateEvent(int id, EventUpdateDto eventUpdateDto)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) throw new Exception("Event not found");

            ev.Name = eventUpdateDto.Name;
            ev.Date = eventUpdateDto.Date;
            ev.Location = eventUpdateDto.Location;
            ev.Description = eventUpdateDto.Description;
            ev.Organizer = eventUpdateDto.Organizer;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) throw new Exception("Event not found");

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> SearchEvents(string name, DateTime? date, string location)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            if (date.HasValue)
            {
                query = query.Where(e => e.Date.Date == date.Value.Date);
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location.Contains(location));
            }

            return await query.ToListAsync();
        }
    }
}
