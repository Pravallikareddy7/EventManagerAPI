using EventManagerAPI.Dtos;
using EventManagerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagerAPI.Services
{
    public interface IEventService
    {
        Task<Event> AddEvent(EventCreateDto eventCreateDto);
        Task<Event> GetEventById(int id);
        Task<IEnumerable<Event>> GetAllEvents();
        Task UpdateEvent(int id, EventUpdateDto eventUpdateDto);
        Task DeleteEvent(int id);
        Task<IEnumerable<Event>> SearchEvents(string name, DateTime? date, string location);
    }
}

