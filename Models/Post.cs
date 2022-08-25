using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string? Id { get; set; }

        [BsonElement("PostID")]
        public int PostID { get; set; }

        [BsonElement("Title")]
        public string? Title { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("Content")]
        public string? Content  { get; set; }

        [BsonElement("CategoryID")]
        public int CategoryID  { get; set; }

        [BsonElement("isDeleted")]
        public bool isDeleted = false;
    }
}
