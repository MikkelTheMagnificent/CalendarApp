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
            var events = _dbContext.CalendarEvents.Find(e => true).ToList();
            return events.Select(e => new EventDTO
            {
                Id = e.Id,
                UserId = e.UserId,
                EventName = e.EventName, 
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            }).ToList();
        }

        public EventDTO GetEvent(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid id.");
            }

            var filter = Builders<EventDTO>.Filter.Eq(e => e.Id, id);
            var eventDTO = _dbContext.CalendarEvents.Find(filter).FirstOrDefault();
            return eventDTO;
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

        public void UpdateEvent(string id, EventDTO eventDTO)
        {
            if (string.IsNullOrEmpty(id) || eventDTO == null)
            {
                throw new ArgumentException("Invalid id or event data.");
            }

            var filter = Builders<EventDTO>.Filter.Eq(e => e.Id, id);
            var update = Builders<EventDTO>.Update
                .Set(e => e.UserId, eventDTO.UserId)
                .Set(e => e.EventName, eventDTO.EventName)
                .Set(e => e.Description, eventDTO.Description)
                .Set(e => e.StartDate, eventDTO.StartDate)
                .Set(e => e.EndDate, eventDTO.EndDate);

            _dbContext.CalendarEvents.UpdateOne(filter, update);
        }

        public void DeleteEvent(string id)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid id.");
            }

            var filter = Builders<EventDTO>.Filter.Eq(e => e.Id, id);
            _dbContext.CalendarEvents.DeleteOne(filter);
        }


    }
}
