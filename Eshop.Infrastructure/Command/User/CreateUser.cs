using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Infrastructure.Command.User
{
    public class CreateUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
