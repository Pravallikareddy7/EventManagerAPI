using EventManagementApi.Models;
using EventManagementApi.Repositories;
using EventManagementApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EventManagementApi.Tests.Services
{
    public class EventServiceTests
    {
        private readonly EventService _service;
        private readonly Mock<IEventRepository> _eventRepoMock;

        public EventServiceTests()
        {
            _eventRepoMock = new Mock<IEventRepository>();
            _service = new EventService(_eventRepoMock.Object);
        }

        [Fact]
        public async Task GetAllEvents_ReturnsEventList()
        {
            var events = new List<Event>
            {
                new Event { Id = 1, Name = "Event1", Date = DateTime.Now, Location = "Location1", Description = "Description1", Organizer = "Organizer1" },
                new Event { Id = 2, Name = "Event2", Date = DateTime.Now, Location = "Location2", Description = "Description2", Organizer = "Organizer2" }
            };

            _eventRepoMock.Setup(repo => repo.GetEventsAsync()).ReturnsAsync(events);

            var result = await _service.GetAllEvents();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetEventById_ReturnsEvent()
        {
            var eventItem = new Event { Id = 1, Name = "Event1", Date = DateTime.Now, Location = "Location1", Description = "Description1", Organizer = "Organizer1" };

            _eventRepoMock.Setup(repo => repo.GetEventByIdAsync(1)).ReturnsAsync(eventItem);

            var result = await _service.GetEventById(1);

            Assert.NotNull(result);
            Assert.Equal("Event1", result.Name);
        }
    }
}
