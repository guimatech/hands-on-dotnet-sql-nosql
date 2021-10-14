using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace hands_on_console
{
    class Event
    {
        public Event(string title, string description, DateTime takesPlaceOn)
        {
            Title = title;
            Description = description;
            TakesPlaceOn = takesPlaceOn;
            CreateAt = DateTime.Now;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime TakesPlaceOn { get; set; }
    }
}
