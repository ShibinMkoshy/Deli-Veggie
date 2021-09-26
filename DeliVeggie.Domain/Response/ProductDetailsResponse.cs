using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.Domain.Response
{
    public class ProductDetailsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public double PriceWithReduction { get; set; }
    }
}
