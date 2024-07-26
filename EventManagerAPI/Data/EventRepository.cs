using EventManagerAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event> GetEventById(int id)
        {

            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task AddEvent(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEvent(Event ev)
        {
            _context.Entry(ev).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent(Event ev)
        {
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
