using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("CategoryID")]
        public int CategoryId { get; set; }

        [BsonElement("Title")]
        public string? Title { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }

        public bool isDeleted = false;
    }
}
