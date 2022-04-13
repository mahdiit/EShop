using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Eshop.Infrastructure.Command.Product
{
    public class CreateProduct
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
