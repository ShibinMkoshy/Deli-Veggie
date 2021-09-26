using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.Domain.Entity
{
    public class PriceReduction
    {
        [BsonId]
        public ObjectId oid { get; set; }
        [BsonElement("DayOfWeek")]
        public int DayOfWeek { get; set; }
        [BsonElement("Reduction")]
        public double Reduction { get; set; }
    }
}
