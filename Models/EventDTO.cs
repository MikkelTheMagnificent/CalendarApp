using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CalendarApp.Models
{
    public class EventDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
