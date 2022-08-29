using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Models
{
    public class UserLogin
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string? UserName { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }
    }
}
