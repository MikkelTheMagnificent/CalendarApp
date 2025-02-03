using System;

namespace CalendarApp.Desktop.Models
{
    public class EventModel
    {
        public EventModel() { } // <-- Tilføj lukning af konstruktøren

        public string Id { get; set; }
        public string UserId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
