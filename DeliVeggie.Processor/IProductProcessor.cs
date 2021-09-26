using DeliVeggie.Domain.Request;
using DeliVeggie.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.Processor
{
    public interface IProductProcessor
    {
        List<ProductsResponse> GetProducts();
        ProductDetailsResponse GetProductDetails(int id);
    }
}
