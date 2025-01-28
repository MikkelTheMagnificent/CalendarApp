using CalendarApp.Data;
using CalendarApp.Models;
using MongoDB.Driver;

namespace CalendarApp.Services
{
    public class CalendarService
    {
        private readonly MongoDbContext _dbContext;

        public CalendarService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<EventDTO> GetEvents()
        {
            var events = _dbContext.CalendarEvents.Find(e => true).ToList(); // Brug Find korrekt
            return events.Select(e => new EventDTO
            {
                Id = e.Id,
                UserId = e.UserId,
                EventName = e.EventName, // EventName bruges her
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            }).ToList();
        }

        public void AddEvent(EventDTO eventDTO)
        {
            var newEvent = new EventDTO
            {
                UserId = eventDTO.UserId,
                EventName = eventDTO.EventName,
                Description = eventDTO.Description,
                StartDate = eventDTO.StartDate,
                EndDate = eventDTO.EndDate
            };

            _dbContext.CalendarEvents.InsertOne(newEvent);
        }
    }
}
