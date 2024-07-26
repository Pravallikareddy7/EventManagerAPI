namespace EventManagerAPI.Dtos
{
    public class EventCreateDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }
    }

    public class EventUpdateDto : EventCreateDto { }

}
