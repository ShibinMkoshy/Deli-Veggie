using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeliVeggie.Domain.Entity
{
    public class Product
    {
        [BsonId]
        public ObjectId oid { get; set; }

        [BsonElement("Id")]
        public int Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("EntryDate")]
        public DateTime EntryDate { get; set; }
        [BsonElement("Price")]
        public int Price { get; set; }
    }
}
