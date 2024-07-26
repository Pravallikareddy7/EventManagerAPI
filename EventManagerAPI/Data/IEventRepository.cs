using EventManagerAPI.Models.Entities;

namespace EventManagerAPI.Data
{
    public interface IEventRepository
    {
        Task<Event> GetEventById(int id);
        Task<IEnumerable<Event>> GetAllEvents();
        Task AddEvent(Event ev);
        Task UpdateEvent(Event ev);
        Task DeleteEvent(Event ev);
        Task<IEnumerable<Event>> SearchEvents(string name, DateTime? date, string location);
    }
}
