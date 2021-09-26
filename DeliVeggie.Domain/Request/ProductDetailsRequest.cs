using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;

namespace DeliVeggie.Domain.Request
{
    [Queue("Qka.Client", ExchangeName = "Qka.Client")]
    public class ProductDetailsRequest
    {
        public int ProductId { get; set; }


    }
}
